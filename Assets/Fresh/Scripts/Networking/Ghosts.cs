using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class Ghosts : MonoBehaviour
{
	public static bool recording = false;
	public static bool playingBack = false;
	public static List<GhostFrame> frames;
	public static List<List<GhostFrame>> loadedGhosts;
	public static List<Puppet> createdPuppets;

	public static float recordingTimeSet = 0;
	public static float uploadDelay = 3;

	public static bool loadedGhostData = false;
	public static string downloadedGhost = "";
	public static string downloadedGhostUsername = "";
	public static bool createdGhosts = false;



	public class Puppet
	{
		public GameObject go;
		public GhostAniController aniScript;
	}

	public enum GhostEvent
	{//Ported these 4 over from Angelo's old system
		None,
		Death,
		Portal,
		Finish,//Not sure what this is for? 
		Size,//This is probably not useful, it was for marking the end of the 4 chunks he saved the data in?
	}

	public class GhostFrame
	{
		public Vector2 pos;
		public float timestamp;
		public LegController2Script.aniTypes ani;
		public GhostEvent ghostEvent = GhostEvent.None;
		public float dir = 2;
	}

	public static void CleanStartRecording()
	{
		if (FreshLevels.IsGameplayLevel(SceneManager.GetActiveScene().name))
		{
			recording = true;
			frames = new List<GhostFrame>();

			if (DBFuncs.self != null)
			{
				DBFuncs.self.UploadGhostAttempt("", true, false);
			}
		}
		else
		{
			recording = false;
			frames = new List<GhostFrame>();
		}
	}

	public static void StartRecording()
	{
		if(!fa.useGhosts) {return; }
		//Debug.Log("Starting recording");
		if (recording) return;

		CleanStartRecording();

	}

	public static void UpdateRecording()
	{
		if(!fa.useGhosts) {return; }
		if (recording && xa.player != null)
		{
			//Debug.Log("Updating recording");
			GhostFrame frame = new GhostFrame();
			frame.pos.x = xa.player.transform.position.x;
			frame.pos.y = xa.player.transform.position.y;
			frame.timestamp = fa.speedrunTime;
			frame.ani = fa.playerAni;
			frame.dir = 2 * xa.playerDir;
			frames.Add(frame);

			if (frames.Count >= 200)
			{
				//let's do an upload
				if (DBFuncs.self != null)
				{
					string s = FramesToString(frames);
					frames = new List<GhostFrame>();
					DBFuncs.self.UploadGhostAttempt(s, false, false);
				}
			}
		}
	}

	public static string FramesToString(List<Ghosts.GhostFrame> frames)
	{
		//This is just the latest attempt, so we don't need to worry about which is faster, etc
		StringBuilder sb = new StringBuilder();
		sb.Length = 0;

		for (int i = 0; i < frames.Count; i++)
		{
			sb.Append(frames[i].timestamp + ",");
			sb.Append(frames[i].pos.x + ",");
			sb.Append(frames[i].pos.y + ",");
			sb.Append((int)frames[i].ani + ",");
			sb.Append((int)frames[i].ghostEvent + ",");
			sb.Append(frames[i].dir + ":");
		}
		return sb.ToString();
	}

	public static List<Ghosts.GhostFrame> StringToFrames(string str)
	{
		List<Ghosts.GhostFrame> frames = new List<Ghosts.GhostFrame>();

		string[] chunks = str.Split(new char[] { ':' }, System.StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < chunks.Length; i++)
		{
			Ghosts.GhostFrame frame = new Ghosts.GhostFrame();
			string[] bits = chunks[i].Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
			frame.timestamp = float.Parse(bits[0]);
			//Debug.Log("Time: " + frame.timestamp);
			frame.pos = new Vector2(0, 0);
			frame.pos.x = float.Parse(bits[1]);
			frame.pos.y = float.Parse(bits[2]);
			frame.ani = (LegController2Script.aniTypes)int.Parse(bits[3]);
			frame.ghostEvent = (Ghosts.GhostEvent)int.Parse(bits[4]);
			frame.dir = float.Parse(bits[5]);
			frames.Add(frame);
		}
		return frames;
	}

	public static void StopAndSaveRecording()//Called when you've finished a run
	{
		if(!fa.useGhosts) {return; }
		recording = false;

		//Debug.Log("Stopping recording");


		//let's do a final ghost upload
		if (DBFuncs.self != null)
		{
			//Debug.Log("HERE, COCKSUCKER");
			string s = FramesToString(frames);
			frames = new List<GhostFrame>();
			DBFuncs.self.UploadGhostAttempt(s, false, true);
		}

		//Alright, locally save this as an Attempt
		//Fresh_Saving.SaveLocalGhostAttempt(frames);

	}


	public static void ResetRecording()//called when Q is pressed, or the run is otherwise reset totally.
	{
		if(!fa.useGhosts) {return; }
		Debug.Log("Resetting ghost recording");

		CleanStartRecording();
	}

	public static void StartPlaybackOfGhosts()
	{
		if(!fa.useGhosts) {return; }
		if (!FreshLevels.IsGameplayLevel(SceneManager.GetActiveScene().name))
		{
			playingBack = false;
			return;
		}

		playingBack = true;
		loadedGhosts = new List<List<GhostFrame>>();
		createdPuppets = new List<Puppet>();
		loadedGhostData = false;
		createdGhosts = false;

		if (DBFuncs.self != null)
		{
			//DBFuncs.self.DownloadGhost("thegamedesigner");
			DBFuncs.self.DownloadBestGhost();
		}


		//bool useYourLastAttempt = true;//Placeholder for the types of ghosts that should be loaded

		//if (useYourLastAttempt)
		//{
		//	List<GhostFrame> ghost = Fresh_Loading.LoadLocalGhostAttempt();
		//	if (ghost != null && ghost.Count > 0) { loadedGhosts.Add(ghost); }
		//}

	}

	public static void CreateGhosts()
	{
		if(!fa.useGhosts) {return; }
		//Now create ghostPuppets
		int num = loadedGhosts.Count;
		for (int i = 0; i < num; i++)
		{
			Puppet p = new Puppet();
			p.go = Instantiate(xa.de.ghostPuppet);
			p.aniScript = p.go.GetComponent<GhostAniController>();
			p.aniScript.name.text = downloadedGhostUsername;
			createdPuppets.Add(p);
		}

		if (num <= 0) { playingBack = false; }//No ghosts were loaded, don't do playback

		Debug.Log("LoadedGhosts: " + loadedGhosts.Count + ", CreatedPuppets: " + createdPuppets.Count + ", Num: " + num);

	}

	public static void UpdatePlaybackOfGhosts()
	{
		if(!fa.useGhosts) {return; }
		if (!playingBack) { return; }

		if (loadedGhostData)
		{
			bool failed = false;

			if (downloadedGhost == null) { failed = true; }
			if (!failed && downloadedGhost.Length < 4) { failed = true; }
			if (!failed)
			{
				string chkFail = downloadedGhost.Substring(0, 4);
				if (chkFail == "Fail") { failed = true; }
			}

			if (!failed)
			{

				string[] splitString = downloadedGhost.Split(new string[] { ":::" }, System.StringSplitOptions.RemoveEmptyEntries);
				downloadedGhostUsername = splitString[0];
				string data = splitString[1];
				Debug.Log("Name: " + downloadedGhostUsername);
				loadedGhosts.Add(StringToFrames(data));//currently this just does one ghost

			}

			//now create the ghosts
			CreateGhosts();
			loadedGhostData = false;
			createdGhosts = true;
		}

		if (createdGhosts)
		{
			float puppetZ = 50;//The Z Layer all ghosts are on.
							   //Debug.Log("Spdruntime: " + za.speedrunTime);
			for (int i = 0; i < loadedGhosts.Count; i++)
			{
				float nearestDist = 9999;
				int nearest = -1;
				for (int a = 0; a < loadedGhosts[i].Count; a++)
				{
					float dist = Setup.Distance(fa.speedrunTime, loadedGhosts[i][a].timestamp);
					if (dist < nearestDist)
					{
						nearestDist = dist;
						nearest = a;
					}
				}
				if (nearest >= loadedGhosts[i].Count - 1) { nearest = -1; }

				if (nearest != -1)
				{
					//now use this latest frame
					createdPuppets[i].go.transform.position = new Vector3(loadedGhosts[i][nearest].pos.x, loadedGhosts[i][nearest].pos.y, puppetZ);

					createdPuppets[i].aniScript.aniType = loadedGhosts[i][nearest].ani;
					createdPuppets[i].aniScript.puppet.transform.SetScaleX(loadedGhosts[i][nearest].dir);
				}
				else
				{
					createdPuppets[i].go.transform.position = new Vector3(-9999, -9999, puppetZ);
				}
			}
		}

	}
}
