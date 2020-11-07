//
// Copyright 2011 Kazuki Oikawa
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

// This DictionaryPacker is a modified version of the ObjectPacker, by Angelo Yazar.

using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace MsgPack
{
	public class DictionaryPacker
	{
		byte[] _buf = new byte[64];
		//Encoding _encoding = Encoding.UTF8;
		static Dictionary<Type, PackDelegate> PackerMapping;
		static Dictionary<Type, UnpackDelegate> UnpackerMapping;

		delegate void PackDelegate (DictionaryPacker packer, MsgPackWriter writer, object o);
		delegate object UnpackDelegate (DictionaryPacker packer, MsgPackReader reader);

		static DictionaryPacker ()
		{
			PackerMapping = new Dictionary<Type, PackDelegate> ();
			UnpackerMapping = new Dictionary<Type, UnpackDelegate> ();

			PackerMapping.Add (typeof (string), StringPacker);
			UnpackerMapping.Add (typeof (string), StringUnpacker);
		}

		public byte[] Pack (object o)
		{
			using (MemoryStream ms = new MemoryStream ()) {
				Pack (ms, o);
				return ms.ToArray ();
			}
		}

		public void Pack (Stream strm, object o)
		{
			if (o != null && o.GetType ().IsPrimitive)
				throw new NotSupportedException ();
			MsgPackWriter writer = new MsgPackWriter (strm);
			Pack (writer, o);
		}

		void Pack (MsgPackWriter writer, object o)
		{
			if (o == null) {
				writer.WriteNil ();
				return;
			}

			Type t = o.GetType ();
			if (t.IsPrimitive) {
				if (t.Equals (typeof (int))) writer.Write ((int)o);
				else if (t.Equals (typeof (uint))) writer.Write ((uint)o);
				else if (t.Equals (typeof (float))) writer.Write ((float)o);
				else if (t.Equals (typeof (double))) writer.Write ((double)o);
				else if (t.Equals (typeof (long))) writer.Write ((long)o);
				else if (t.Equals (typeof (ulong))) writer.Write ((ulong)o);
				else if (t.Equals (typeof (bool))) writer.Write ((bool)o);
				else if (t.Equals (typeof (byte))) writer.Write ((byte)o);
				else if (t.Equals (typeof (sbyte))) writer.Write ((sbyte)o);
				else if (t.Equals (typeof (short))) writer.Write ((short)o);
				else if (t.Equals (typeof (ushort))) writer.Write ((ushort)o);
				else if (t.Equals (typeof (char))) writer.Write ((ushort)(char)o);
				else throw new NotSupportedException ();
				return;
			}

			PackDelegate packer;
			if (PackerMapping.TryGetValue (t, out packer)) {
				packer (this, writer, o);
				return;
			}

			if (t.IsArray) {
				Array ary = (Array)o;
				writer.WriteArrayHeader (ary.Length);
				for (int i = 0; i < ary.Length; i++) {
					Pack(writer, ary.GetValue(i));
				}
				return;
			}

			if( o is Dictionary<string,object> ) {
				Dictionary<string,object> table = o as Dictionary<string,object>;
				writer.WriteMapHeader (table.Count);
				foreach( string key in table.Keys ) {
					writer.Write (key, _buf, true);
					object v = table[key];
					Pack (writer, v);
				}
				return;
			}
		}

		public Dictionary<string,object> Unpack (byte[] buf)
		{
			return Unpack (buf, 0, buf.Length);
		}

		public Dictionary<string,object> Unpack(byte[] buf, int offset, int size)
		{
			using (MemoryStream ms = new MemoryStream (buf, offset, size)) {
				return Unpack (ms);
			}
		}

		public Dictionary<string,object> Unpack (Stream strm)
		{
			MsgPackReader reader = new MsgPackReader (strm);
			return (Dictionary<string,object>)Unpack (reader);
		}

		public object Unpack (Type type, byte[] buf)
		{
			return Unpack (type, buf, 0, buf.Length);
		}

		public object Unpack (Type type, byte[] buf, int offset, int size)
		{
			using (MemoryStream ms = new MemoryStream (buf, offset, size)) {
				return Unpack (type, ms);
			}
		}

		public object Unpack (Type type, Stream strm)
		{
			if (type.IsPrimitive)
				throw new NotSupportedException ();
			MsgPackReader reader = new MsgPackReader (strm);
			return Unpack (reader);
		}

		object Unpack (MsgPackReader reader)
		{

			if( !reader.Read()	)	
				throw new FormatException();

			if( !reader.IsMap() && !reader.IsArray() && !reader.IsRaw() ) {
				     if( reader.IsSigned64()   )	return reader.ValueSigned64;
				else if( reader.IsUnsigned64() )	return reader.ValueUnsigned64;
				else if( reader.IsSigned()     ) 	return reader.ValueSigned;
				else if( reader.IsUnsigned()   )	return reader.ValueUnsigned;
				else if( reader.Type == TypePrefixes.Float ) return reader.ValueFloat;
				else if( reader.Type == TypePrefixes.Double) return reader.ValueDouble;
				else if( reader.IsBoolean()    )	return (reader.Type == TypePrefixes.True);
			}

			if(reader.IsRaw()) { //string
				this.CheckBufferSize ((int)reader.Length);
				reader.ReadValueRaw (this._buf, 0, (int)reader.Length);
				return Encoding.UTF8.GetString (this._buf, 0, (int)reader.Length);
			}

			if (reader.IsArray()) {
				if ((!reader.IsArray () && reader.Type != TypePrefixes.Nil))
					throw new FormatException ();
				if (reader.Type == TypePrefixes.Nil)
					return null;
				object changeThis = new object();
				Array ary = Array.CreateInstance (changeThis.GetType() , (int)reader.Length);
				for (int i = 0; i < ary.Length; i ++)
					ary.SetValue (Unpack (reader), i);
				return ary;
			}

			if (reader.Type == TypePrefixes.Nil)
				return null;
			if (!reader.IsMap())
				throw new FormatException ();

			Dictionary<string,object> o = new Dictionary<string,object>();
			int members = (int)reader.Length;
			for (int i = 0; i < members; i ++) {
				if (!reader.Read () || !reader.IsRaw ())
					throw new FormatException ();
				
				CheckBufferSize ((int)reader.Length);
				reader.ReadValueRaw (_buf, 0, (int)reader.Length);
				string name = Encoding.UTF8.GetString (_buf, 0, (int)reader.Length);
				if( o.ContainsKey(name) ) {
					//Debug.Log( name );
					Unpack(reader);
				}
				else {
					o.Add(name, Unpack(reader));
				}
			}

			IDeserializationCallback callback = o as IDeserializationCallback;
			if (callback != null)
				callback.OnDeserialization (this);
			return o;
		}

		void CheckBufferSize (int size)
		{
			if (_buf.Length < size)
				Array.Resize<byte> (ref _buf, size);
		}

		static void StringPacker (DictionaryPacker packer, MsgPackWriter writer, object o)
		{
			writer.Write (Encoding.UTF8.GetBytes ((string)o));
		}

		static object StringUnpacker (DictionaryPacker packer, MsgPackReader reader)
		{
			if (!reader.Read ())
				throw new FormatException ();
			if (reader.Type == TypePrefixes.Nil)
				return null;
			if (!reader.IsRaw ())
				throw new FormatException ();
			packer.CheckBufferSize ((int)reader.Length);
			reader.ReadValueRaw (packer._buf, 0, (int)reader.Length);
			return Encoding.UTF8.GetString (packer._buf, 0, (int)reader.Length);
		}
	}
}
