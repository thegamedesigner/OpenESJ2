using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static T ParseEnum<T>(string value, bool ignoreCase = false)
	{
		return (T)System.Enum.Parse(typeof(T), value, ignoreCase);
	}

	public static T ToEnum<T>(this string value, bool ignoreCase = false)
	{
		return ParseEnum<T>(value, ignoreCase);
	}

	public static bool HasFlag(this Enum thisInstance, Enum flag)
	{
		long checkBits = Convert.ToInt64(flag);
		return (Convert.ToInt64(thisInstance) & checkBits) == checkBits;
	}

	public static Rect RectFromPoints(Vector3 start, Vector3 end)
	{
		float minX   = Mathf.Min(start.x, end.x);
		float minY   = Mathf.Min(start.y, end.y);
		float width  = Mathf.Max(start.x, end.x) - minX;
		float height = Mathf.Max(start.y, end.y) - minY;
		if (start == end) {
			width = 1.0f;
			height = 1.0f;
		}
		return new Rect(minX, minY, width, height);
	}

	public static ArraySegment<T> Range<T>(this T[] lhs, int offset, int count = 0)
	{
		if (count <= 0) {
			count = lhs.Length - offset;
		}

		return new ArraySegment<T>(lhs, offset, count);
	}

	public static T[] ShallowCopy<T>(this ArraySegment<T> lhs)
	{
		int count = lhs.Array.Length - lhs.Offset;
		return lhs.Array.ShallowCopyRange<T>(lhs.Offset, count);
	}

	public static T[] ShallowCopyRange<T>(this T[] lhs, int offset, int count = 0)
	{
		if (count <= 0) {
			count = lhs.Length - offset;
		}
		
		T[] shallow = new T[count];
		Array.Copy(lhs, offset, shallow, 0, count);
		return shallow;
	}

	/***************************************************************************
	 * .butt serialization stuff
	 **************************************************************************/
	public static string SerializeButtEntities(List<ButtEntity> ents)
	{
		System.Text.StringBuilder b = new System.Text.StringBuilder(ents.Count * 30);
		for (int i = 0; i < ents.Count; ++i) {
			b.AppendLine(ents[i].Serialize());
		}
		return b.ToString();
	}

	public static string SerializeButtEntityText(string label, object arg)
	{
		return SerializeButtEntityText(new string[] { label }, arg);
	}

	public static string SerializeButtEntityText(string[] labels, params object[] args)
	{
		int length       = Mathf.Min(labels.Length, args.Length);
		string[] outVals = new string[length];

		for (int i = 0; i < length; ++i) {
			if (args[i] == null) {
				continue;
			}
			System.Type vtype = args[i].GetType();
			string serializedValue = null;

			if (vtype == typeof(int) ||
			    vtype == typeof(float) ||
			    vtype == typeof(bool) ||
			    vtype.IsEnum)
			{
				serializedValue = args[i].ToString();
			}
			else if (vtype == typeof(Vector2))
			{
				Vector2 value = (Vector2)args[i];
				serializedValue = string.Join("/", new string[] {
					value[0].ToString(),
					value[1].ToString()
				});
			}
			else if (vtype == typeof(Vector3))
			{
				Vector3 value = (Vector3)args[i];
				serializedValue = string.Join("/", new string[] {
					value[0].ToString(),
					value[1].ToString(),
					value[2].ToString()
				});
			}
			else if (vtype == typeof(Vector4))
			{
				Vector4 value = (Vector4)args[i];
				serializedValue = string.Join("/", new string[] {
					value[0].ToString(),
					value[1].ToString(),
					value[2].ToString(),
					value[3].ToString()
				});
			}
			else if (vtype == typeof(Color))
			{
				Color value = (Color)args[i];
				serializedValue = string.Join("/", new string[] {
					Mathf.Clamp(Mathf.Floor(value.r * 256.0f), 0.0f, 255.0f).ToString(),
					Mathf.Clamp(Mathf.Floor(value.g * 256.0f), 0.0f, 255.0f).ToString(),
					Mathf.Clamp(Mathf.Floor(value.b * 256.0f), 0.0f, 255.0f).ToString()
				});
			}

			if (!string.IsNullOrEmpty(serializedValue)) {
				outVals[i] = labels[i] + ":" + serializedValue;
			}
		}
		return string.Join(",", outVals);
	}
}
