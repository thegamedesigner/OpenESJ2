using UnityEngine;

using System;
#if STEAMWORKS
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MsgPack;
#endif
public enum GhostEvent {
	none,
	death,
	portal,
	finish,
	size,
}

public class Ghost {

	#if STEAMWORKS
	private struct GhostFrameData {
		public float x;
		public float y;
		public short animationFrame;
		public byte ghostEvent;
	}

	private static int MAX_RECORDING_LENGTH = 180000; //One Hour.
	private static string FILE_PATH = Application.persistentDataPath; // temporaryCachePath
	private static MD5 md5;

	public bool isLoaded = false;
	public bool isRecording = false;
	public bool isDone = false;

	public string levelName;
	public string levelID;
	public string playerSkin;
	public string steamName;

	private int _Length = 0;
	public int Length {
		get { return _Length; }
		set { }
	}
	
	private List<GhostFrameData> currentRecording;
	private GhostFrameData currentFrame;
	private int currentFrameIndex;

	private delegate byte[] AsyncDelegate();
	private AsyncDelegate saveRecordingAsyncDelegate;
	private IAsyncResult saving;

	public bool IsDoneSaving {
		get {
			if(saving == null)  return true;
			else				return saving.IsCompleted;
		}
		set { }
	}

    public void PrintAllFrames(string label) {

        string result = "";
        result += label + ", ";
        result += steamName + " frames: ";
        int index = 0;
        while (index < currentRecording.Count)
        {
            result += "" + currentRecording[index].animationFrame + ", ";
            index++;
 
        }
        Setup.GC_DebugLog(result);

    }

	public Ghost() {
		steamName = "";
		currentRecording = new List<GhostFrameData>();
		saveRecordingAsyncDelegate = new AsyncDelegate(SaveRecording);
		currentFrameIndex = 0;
		_Length = 0;
		isLoaded = false;
		isRecording = false;

		if(md5 == null) {
			md5 = new MD5CryptoServiceProvider();
		}
	}

	public void Step() {
		if(currentFrameIndex < Length - 1) {
			currentFrameIndex++;
			currentFrame = currentRecording[currentFrameIndex];
		}
		if(currentFrameIndex >= Length - 1){
			isDone = true;
		}
	}

	public void Rewind() {
		isDone = false;
		currentFrameIndex = 0;
		if(Length > 0) {
			currentFrame = currentRecording[currentFrameIndex];
		}
	}

	public void Seek(int i) {
		if(i < 0) {
			currentFrameIndex = 0;
		}
		else if(i >= Length) {
			currentFrameIndex = Length-1;
		}
		else {
			currentFrameIndex = i;
		}
		currentFrame = currentRecording[currentFrameIndex];
	}

	public Vector3 GetPosition(float z = 0f) {
		if( Length > 0 ) {
			return new Vector3(currentFrame.x,currentFrame.y,z);
		}
		return new Vector3(0,0,z);
	}

	public int GetAnimationFrame() {
		if( Length > 0 ) {
			return (int)currentFrame.animationFrame;
		}
		return 0;
	}

	public GhostEvent GetGhostEvent() {
		if( Length > 0 ) {
			return (GhostEvent)(int)currentFrame.ghostEvent;
		}
		return GhostEvent.none;
	}

	public void StartRecording(string levelName, string playerSkin="default") {
		this.levelName = levelName;
		this.playerSkin = playerSkin;
		currentRecording.Clear();
		_Length = 0;
		levelID = Hash( levelName );
		isLoaded = true;
		isRecording = true;
		isDone = false;
	}

	public void Record(Vector2 position, int animationFrame, GhostEvent ghostEvent = GhostEvent.none ) {
		Record(position.x,position.y,animationFrame,ghostEvent);
	}

	public void Record(float x, float y, int animationFrame, GhostEvent ghostEvent = GhostEvent.none ) {
		if(currentRecording.Count >= MAX_RECORDING_LENGTH) return;

		GhostFrameData frame = new GhostFrameData();
		frame.x = x;
		frame.y = y;
		frame.animationFrame = (short)animationFrame;
		frame.ghostEvent = (byte)ghostEvent;

		currentRecording.Add(frame);
		_Length++;
	}

	public void ClearRecording() {
		steamName = "";
		levelName = "";
		levelID = "";
		playerSkin = "";
		currentRecording.Clear();
		_Length = 0;
		isLoaded = false;
		isRecording = false;
		isDone = false;
		Rewind();
	}

	public void BeginSavingRecording() {
		saving = saveRecordingAsyncDelegate.BeginInvoke(null,null);
	}

	public byte[] FinishSavingRecording() {
		byte[] result = saveRecordingAsyncDelegate.EndInvoke(saving);
		saving = null;
		return result;
	}

	public byte[] SaveRecording() {
		//Four separate arrays results in a smaller file size and quicker encoding time.
		int size = currentRecording.Count;
		float[] x_data = new float[size];
		float[] y_data = new float[size];
		short[] animationFrame_data = new short[size];
		byte[] ghostEvent_data = new byte[size];

		GhostFrameData frame;
		for(int i = 0; i < size; i++) {
			frame = currentRecording[i];
			x_data[ i ] = frame.x;
			y_data[ i ] = frame.y;
			animationFrame_data[ i ] = frame.animationFrame;
			ghostEvent_data[ i ] = frame.ghostEvent;
		}

		string filePath = FILE_PATH + "/"+levelID+".ghost";

        string result = "";

        result = "SAVING: " + filePath + ", frames: ";
		for(int i = 0; i < size; i++) {
            result += animationFrame_data[i] + ", ";
		}

		Dictionary<string,object> recording = new Dictionary<string,object>();
		recording.Add("levelName", levelName);
		recording.Add("playerSkin", playerSkin);
		recording.Add("x_data", x_data);
		recording.Add("y_data", y_data);
		recording.Add("animationFrame_data", animationFrame_data);
		recording.Add("ghostEvent_data", ghostEvent_data);

		byte[] packedRecordingBytes = EncodeMessage(recording); //Compress(  );
		File.WriteAllBytes(filePath, packedRecordingBytes);

		isRecording = false;
		//Debug.Log("Saved Ghost:"+filePath);

		return packedRecordingBytes;
		//Write to SteamCloud
		//FileShare to get UGCHandle
		//Attach UGCHandle to Leaderboard score.
	}

	public void SetSteamName(string steamName) {
		this.steamName = steamName;
	}

	public void LoadRecording(string levelName) {
		levelID = Hash(levelName);
		string filePath = FILE_PATH + "/"+levelID+".ghost";
		LoadRecordingFromFile(filePath);
	}

	public void LoadRecordingFromFile(string filePath) {
		//Get UGCHandles from the friends leaderboards call
		//Call download UGC with those handles
		//Call ReadData on the result of that
		//Pass that data to this function instead of steamID / levelID
		
		bool success = false;

		if( File.Exists(filePath) ) {
			byte[] decompressedRecording = File.ReadAllBytes(filePath);
			try {
				//byte[] decompressedRecording = Decompress(compressedRecording);
				Dictionary<string,object> recording = DecodeMessage( decompressedRecording ) as Dictionary<string,object>;

				StartRecording(
					(string) recording["levelName"],
					(string) recording["playerSkin"]
				);

				List<object> x_data = (List<object>) recording["x_data"];
				List<object> y_data = (List<object>) recording["y_data"];
				List<object> animationFrame_data = (List<object>) recording["animationFrame_data"];
				List<object> ghostEvent_data = (List<object>) recording["ghostEvent_data"];

				for(int i = 0; i < x_data.Count; i++) {
					Record(
						new Vector2( (float)x_data[i],(float)y_data[i] ),
						(int)		  animationFrame_data[i],
						(GhostEvent)(int) ghostEvent_data[i]
					);
				}

				success = true;
			}
			catch(Exception e) {}
		}

		if( success ) {
			//Debug.Log("Loaded Ghost:");
			//Debug.Log(this.levelName);
			//Debug.Log(playerSkin);
			//Debug.Log(currentRecording.Count);
			//Debug.Log("~~~~~~~~~~~~~");
			isLoaded = true;
			isRecording = false;
			Rewind();
		}
		else {
			//Debug.Log( "Failed to load recording:" + levelName );
		}
	}

	public byte[] EncodeMessage(IDictionary<string,object> message) {
        BoxingPacker packer = new BoxingPacker();
        return packer.Pack( message );
    }

    public IDictionary<string,object> DecodeMessage(byte[] message) {
        BoxingPacker packer = new BoxingPacker ();
        try {
            return (IDictionary<string,object>)packer.Unpack( message );
        }
        catch ( Exception e ) {
            return new Dictionary<string,object>();
        }
    }

	public byte[] Compress(byte[] message) {
		using( MemoryStream ms = new MemoryStream() ) {
			GZipStream gzipStream = new GZipStream(ms, CompressionMode.Compress, true);
			gzipStream.Write(message, 0, message.Length);
			gzipStream.Close();
			return ms.ToArray();
		}
	}

	public byte[] Decompress(byte[] message) {
		using(GZipStream gzipStream = new GZipStream(new MemoryStream(message), CompressionMode.Decompress)) {
			int size = 4096;
			byte[] buffer = new byte[size];
			using(MemoryStream memory = new MemoryStream()) {
				int bytesRead = 0;
				do {
					bytesRead = gzipStream.Read(buffer, 0, size); 
					if(bytesRead > 0) {
						memory.Write(buffer, 0, bytesRead);
					}
				} while(bytesRead > 0);

				return memory.ToArray();
			}
		}
	}

	private string Hash(string data,string key="") {
		byte[] hash = md5.ComputeHash( System.Text.Encoding.UTF8.GetBytes(data+key) );
		return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
	}

#endif
}