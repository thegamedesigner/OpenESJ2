using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;
using UnityEngine.SceneManagement;
using System.IO;

public class Fresh_Loading : MonoBehaviour
{

	public static bool checkForNonSteamDLC()
	{
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "DLC" + ".txt");
		string levelRawString = null;
		if (File.Exists(filePath))
		{
			levelRawString = System.IO.File.ReadAllText(filePath);

			if (levelRawString == "hasDLC")
			{
				xa.hasBonusDLC = true;//SteamApps.BIsDlcInstalled(appId);
				Debug.Log("NON-STEAM: Checking Bonus DLC: " + xa.hasBonusDLC);

				return true;
			}
		}

		filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AlpDLC" + ".txt");
		levelRawString = null;
		if (File.Exists(filePath))
		{
			levelRawString = System.IO.File.ReadAllText(filePath);

			if (levelRawString == "hasDLC")
			{
				xa.hasAlpDLC = true;//SteamApps.BIsDlcInstalled(appId);
				Debug.Log("NON-STEAM: Checking Alp DLC: " + xa.hasAlpDLC);

				return true;
			}
		}

		return false;
	}



	public static List<Ghosts.GhostFrame> LoadLocalGhostAttempt()
	{
		List<Ghosts.GhostFrame> frames = new List<Ghosts.GhostFrame>();

		string str = PlayerPrefs.GetString("localGhostAttempt_" + SceneManager.GetActiveScene().name, null);
		//Debug.Log("GhostStr: " + str);
		if (str != null || str != "")
		{
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
		else
		{
			return null;
		}
	}


	public static void LoadLocalAchivos()
	{
		int[] bs = new int[AchivoFuncs.myAchivos.Length];
		for (int i = 0; i < bs.Length; i++)
		{
			if (PlayerPrefs.HasKey("localAchivos_" + i))
			{
				bs[i] = PlayerPrefs.GetInt("localAchivos_" + i, 0);
			}
			if (bs[i] == 1) { AchivoFuncs.myAchivos[i] = true; }
		}
	}

	public static void LoadMiscSettings()
	{
		//load use custom controls
		for (int i = 0; i < 25; i++)
		{
			Controls.customControls[i] = false;
			if (PlayerPrefs.HasKey("customControl" + i))
			{
				int r = PlayerPrefs.GetInt("customControl" + i);
				if (r == 1)
				{
					Controls.customControls[i] = true;
				}
			}
		}


		LoadLocalAchivos();

		//Alt Menu Controls
		if (PlayerPrefs.HasKey("altmenucontrols"))
		{
			fa.forceAltMenuControls = false;
			int r = PlayerPrefs.GetInt("altmenucontrols", 0);
			if (r == 1) { fa.forceAltMenuControls = true; }
		}
		//Mute checkponts
		if (PlayerPrefs.HasKey("mutecheckpoints"))
		{
			fa.muteCheckpointSounds = GetBool("mutecheckpoints", false);
		}
		//dark loading screens
		if (PlayerPrefs.HasKey("useblackfaders"))
		{
			fa.useBlackFaders = GetBool("useblackfaders", false);
		}
		//Dont connect to 3rd party server
		if (PlayerPrefs.HasKey("dontconnecttodevserver"))
		{
			fa.dontConnect3rdParty = GetBool("dontconnecttodevserver", true);
		}
		//Dont connect to steam
		if (PlayerPrefs.HasKey("dontconnecttosteam"))
		{
			fa.dontConnectSteam = GetBool("dontconnecttosteam", true);
		}
		//Mouse Grab
		if (PlayerPrefs.HasKey("mousegrab"))
		{
			fa.mouseGrab = GetBool("mousegrab", true);
		}
		//username
		if (PlayerPrefs.HasKey("username"))
		{
			fa.username = PlayerPrefs.GetString("username", "defaultUser");
			Debug.Log("loaded username: " + fa.username);
		}
		//death counter 
		if (PlayerPrefs.HasKey("showDeathCounter"))
		{
			fa.showDeathCounter = PlayerPrefs.GetInt("showDeathCounter", 0);
		}
		//show speed run timer
		if (PlayerPrefs.HasKey("showSpeedrunTimer"))
		{
			fa.showSpeedrunTimer = PlayerPrefs.GetInt("showSpeedrunTimer", 0);
		}
		//puppies
		if (PlayerPrefs.HasKey("puppiesCollected"))
		{
			fa.puppiesCollected = PlayerPrefs.GetInt("puppiesCollected", 0);
		}
		//golden butts
		if (PlayerPrefs.HasKey("goldenButtsCollected"))
		{
			fa.goldenButtsCollected = PlayerPrefs.GetInt("goldenButtsCollected", 0);
		}

		if (PlayerPrefs.HasKey("collectedCoins"))
		{
			fa.coinsCollected = PlayerPrefs.GetInt("collectedCoins", 0);
		}

		//screen res
		int width = 1920;
		int height = 1080;
		if (PlayerPrefs.HasKey("resolutionWidth")) { width = PlayerPrefs.GetInt("resolutionWidth", 1920); }
		if (PlayerPrefs.HasKey("resolutionHeight")) { height = PlayerPrefs.GetInt("resolutionHeight", 1080); }
		for (int i = 0; i < Screen.resolutions.Length; i++)
		{
			if (Screen.resolutions[i].width == width && Screen.resolutions[i].height == height)
			{
				xa.resIndex = i;
				break;
			}
		}

		if (PlayerPrefs.HasKey("vsync"))
		{
			QualitySettings.vSyncCount = PlayerPrefs.GetInt("vsync", 0);
		}
		else
		{
			QualitySettings.vSyncCount = 0;
		}
		if (PlayerPrefs.HasKey("fullscreen"))
		{
			int r = PlayerPrefs.GetInt("fullscreen", 0);

			if (r == 0)
			{
				xa.fullscreen = false;
				Screen.fullScreen = false;
			}
			else
			{
				xa.fullscreen = true;
				Screen.fullScreen = true;
			}
		}
		else
		{
			QualitySettings.vSyncCount = 0;
		}
		if (PlayerPrefs.HasKey("pgmode"))
		{
			xa.pgMode = GetBool("pgmode", false);
		}
		if (PlayerPrefs.HasKey("screenshakemultiplier"))
		{
			fa.screenshakeMultiplier = PlayerPrefs.GetFloat("screenshakemultiplier", 1);
		}
		if (PlayerPrefs.HasKey("fancy"))
		{
			fa.fancy_loaded = PlayerPrefs.GetInt("fancy", 1);
			fa.fancyLevel = (fa.BackgroundFancy)fa.fancy_loaded;
			fa.fancyWasLoaded = true;
			//Debug.Log("loaded fancy: " + fa.fancy_loaded);
		}
		else
		{
			//set to default
			fa.fancy_loaded = 1;
			fa.fancyLevel = (fa.BackgroundFancy)fa.fancy_loaded;
			fa.fancyWasLoaded = true;
			//Debug.Log("Failed to load fancy, setting to default (1)");
		}
		if (PlayerPrefs.HasKey("savedUsername"))
		{
			ProfileScript.loadedUsername = PlayerPrefs.GetString("savedUsername", "");
		}
		if (PlayerPrefs.HasKey("savedPwHash"))
		{
			ProfileScript.loadedPwhash = PlayerPrefs.GetString("savedPwHash", "");
		}
		if (PlayerPrefs.HasKey("sendUsageStats"))
		{
			int r = PlayerPrefs.GetInt("sendUsageStats", 0);
			if (r == 1)
			{
				RawFuncs.sendUsageStats = true;

			}
			else
			{
				RawFuncs.sendUsageStats = false;
			}
		}
		else
		{
			//If nothing has been saved, assume it's true
			PlayerPrefs.SetInt("sendUsageStats", 1);
			PlayerPrefs.Save();
			RawFuncs.sendUsageStats = true;
		}
		if (PlayerPrefs.HasKey("rememberMe"))
		{
			int result = PlayerPrefs.GetInt("rememberMe", 0);
			if (result == 0)
			{
				ProfileScript.loadedRememberMe = false;
			}
			else
			{
				ProfileScript.loadedRememberMe = true;
			}
		}
		if (PlayerPrefs.HasKey("lvlNum"))
		{
			int result = PlayerPrefs.GetInt("lvlNum", 0);
			if (result < 1)
			{
				if (NodeController.lvlNum < result)
				{
					NodeController.lvlNum = 10;//(10 is the starting number)
				}
			}
			else
			{
				if (NodeController.lvlNum < result)
				{
					NodeController.lvlNum = result;
				}
			}
		}
	}

	public static Controls.Control LoadControl(Controls.Type type, int plNum)
	{
		//Try to load a control
		int keyInt = PlayerPrefs.GetInt("MCon_KeyInt_" + plNum + "_" + type, -1);
		int joyNum = PlayerPrefs.GetInt("MCon_JoyNum_" + plNum + "_" + type, -1);
		int axisNum = PlayerPrefs.GetInt("MCon_AxisNum_" + plNum + "_" + type, -1);
		bool posAxis = GetBool("MCon_AxisDir_" + plNum + "_" + type, true);
		int playerNum = plNum;
		Controls.Control c;
		//Now, decide if that worked, or not?
		if (keyInt != -1)
		{
			//Loaded a saved key!
			c = Controls.SetKey(type, keyInt, joyNum, axisNum, posAxis, playerNum);

		}
		else if (joyNum != -1 && axisNum != -1)
		{
			//Loaded a saved axis!
			c = Controls.SetKey(type, keyInt, joyNum, axisNum, posAxis, playerNum);
		}
		else
		{
			//Failed to load a saved control!
			//Debug.Log("Failed to load " + type);
			//c = Controls.SetDefaultControl(type);
			c = null;
		}
		if (c != null)
		{
			c.label = Controls.GetLabelForControl(c);
		}
		return c;

	}
	public static void LoadCustomControls()
	{
		Controls.controls = new List<Controls.Control>();
		//Debug.Log("Loading controls");
		for (int a = 0; a < 4; a++)
		{
			for (int i = 1; i < (int)Controls.Type.End; i++)
			{
				LoadControl((Controls.Type)i, a);
			}
		}
		//string s = "Controls:";
		//for (int i = 0; i < Controls.controls.Count; i++)
		//{
		//	s += Controls.controls[i].type + ", Pl" + Controls.controls[i].player + "\n";// + ", " + Controls.controls[i].keyInt + ", " + Controls.controls[i].joyNum + ", " + Controls.controls[i].axisNum + ", " + Controls.controls[i].posAxis + ", ";
		//}
		//Debug.Log(s);
	}

	public static void LoadVolumes()
	{
		xa.musicVolume = PlayerPrefs.GetFloat("MusicXVolume", 0.5f);
		xa.soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
	}

	public static bool LoadLevelUnlocked(FreshLevels.Type fType)
	{
		FreshLevels.Type type = fType;
		if (type == FreshLevels.Type.IntroStory) { return (true); }//Always unlocked

		int result = 0;
		/*
		if (PlayerPrefs.HasKey("LevelUnlocked_" + type))
		{
			result = PlayerPrefs.GetInt("LevelUnlocked_" + type, 0);
		}*/

		if (fType != FreshLevels.Type.None)
		{
			string s = FreshLevels.GetStrictLabelForType(fType);
			if (PlayerPrefs.HasKey("nu_" + s))
			{
				result = PlayerPrefs.GetInt("nu_" + type, 0);
			}
		}


		return (result == 1);
	}

	public static int LoadLevelDeaths(FreshLevels.Type fType)
	{
		FreshLevels.Type type = fType;
		if (type == FreshLevels.Type.IntroStory) { type = FreshLevels.Type.Tut_JumpAndAirsword; }//IntroStory isn't a real level
		if (type == FreshLevels.Type.AlpDLC_IntroStory) { type = FreshLevels.Type.Alp0; }//IntroStory isn't a real level

		int result = -1;
		if (PlayerPrefs.HasKey("LevelDeaths_" + type))
		{
			result = PlayerPrefs.GetInt("LevelDeaths_" + type, -1);
		}
		return result;
	}

	public static float LoadLevelTime(FreshLevels.Type fType)
	{
		FreshLevels.Type type = fType;
		if (type == FreshLevels.Type.IntroStory) { type = FreshLevels.Type.Tut_JumpAndAirsword; }
		if (type == FreshLevels.Type.AlpDLC_IntroStory) { type = FreshLevels.Type.Alp0; }
		float result = -1;
		if (PlayerPrefs.HasKey("LevelTime_" + type))
		{
			result = PlayerPrefs.GetFloat("LevelTime_" + type, -1);
		}
		return result;
	}

	public static bool GetBool(string name, bool defaultResult)
	{
		int fakeResult = 0;
		if (defaultResult) fakeResult = 1;

		int fakeBool = PlayerPrefs.GetInt(name, fakeResult);
		if (fakeBool == 0) { return false; } else { return true; }
	}

}
