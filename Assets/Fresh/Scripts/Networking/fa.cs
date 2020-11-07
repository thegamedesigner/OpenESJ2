using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class fa : MonoBehaviour
{

	public static bool devMode = false;//set by a secret username (because the blasted steam dev branch wont refresh so fuck it)
	public static bool cheater = false;
	public static bool forceUnlockOfAlpDLC = false;//For playtesting it
	public static Vector3 cameraPos = Vector3.zero;
	public static GameObject mainCameraObject = null;//Not nessicarially the Camera, just the parent object you should move around
	public static FreshLevels.Type lastLevelPlayed = FreshLevels.Type.None;
	public static float timeInSeconds = 0;
	public static float timePaused = 0;
	public static float currentTimePaused = 0;
	public static float timeSet_forPaused = -1;
	public static float time = 0.0f;//equiv of TimeSinceGameStart, NOT TimeSinceLevelLoad
	public static float timeSinceGameStarted = 0.0f;
	public static float timeSinceLevelWasFirstLoaded = 0;//Resets if the level is changed
	public static float timeSinceLevelWasFirstLoaded_offset = 0;//Resets if the level is changed
	public static float slowmoFactor = 1;
	public static float timeUpdater = 0;
	public static float pausedFloat = 0;
	public static float timeScale = 1;
	public static float deltaTime = 0;
	public static float screenshakeMultiplier = 1;
	public static float hardwiredVolume = 1;
	public static int timeSinceLevelWasFirstLoaded_num = -99;
	public static bool paused = false;
	public static bool mouseGrab = true;
	public static bool isMenuLevel = false;
	public static bool escapeDoesntTriggerInGameMainMenu = false;
	public static float tellsteamdelay = 10;
	public static float tellsteamtimeset = 0;
	public static bool tellsteam = false;
	public static bool forceAltMenuControls = false;
	public static bool teleportedOnJumpingMassacre = false;


	public static bool useBlackFaders = false;
	public static bool muteCheckpointSounds = false;//check is in sounds, not checkpoints, as I use them in various places
	public static bool dontConnect3rdParty = false;//should block all of my server stuff
	public static bool dontConnectSteam = false;//should block all of steam's stuff

	public enum BackgroundFancy { NotFancy = 0, Fancy = 1, End }
	public static BackgroundFancy fancyLevel = BackgroundFancy.Fancy;
	public static bool fancyWasLoaded = false;
	public static int fancy_loaded = 2;//default to 2, this is basiclly just to set the slider.value

	public static bool useGhosts = false;//turned off by default

	public static float speedrunTime = 0;
	public static float speedrunTimeSet = 0;

	public static LegController2Script.aniTypes playerAni = LegController2Script.aniTypes.Stand;//the player's current animation
	
	public static string usageStat_lvl = "";
	public static float usageStat_timeSet = -1;
	
	public static List<int> coinX = new List<int>();
	public static List<int> coinY = new List<int>();
	public static List<int> coinLvl = new List<int>();
	public static int coinsCollected = 0;
	public static int coinsCollectedGoal = 666;
	public static int lastCheckpointX = -9999;
	public static int lastCheckpointY = -9999;
	public static int lastCheckpointLvlIndex = -9999;

	
	
	public static int goldenButtsCollected = 0;
	public static int goldenButtsTotal = 5;
	public static int puppiesCollected = 0;
	public static int puppiesTotal = 5;
	public static int showDeathCounter = 0;//0 = false, 1 = true
	public static int showSpeedrunTimer = 0;//0 = false, 1 = true
	
	public static string username = "defaultUser";
	public static string token = null;
	public static bool tokenIsValid = true;
	public static bool receivedLeaderboard = false;
	public static string leaderboardTitle = null;
	public static string leaderboardData = null;
	public static string mmleaderboardData = null;
	public static string tempToken = null;
	public static bool checkToken = false;
	public static string[] mmleaderboardNames = new string[11];
	public static string[] mmleaderboardTimes = new string[11];
	public static string[,] leaderboardNames = new string[61, 31];
	public static string[,] leaderboardTimes = new string[61, 31];
	public static int lengthOfLeaderboard = 20;
	public static int lengthOfMainMenuLeaderboard = 10;
	public static string[] entireLeaderboard = new string[61];
	public static string entireMMLeaderboard;

	

	public static void ResetSpeedrun()
	{
		Ghosts.ResetRecording();//Resets (wipes) but stays in active recording mode
		speedrunTimeSet = timeInSeconds;//sets the timeset (the amount subtracted) from speedrun, forward.
	}

	public static void UpdateTime()
	{
		bool localPaused = false;
		if (paused)
		{
			localPaused = true;
			pausedFloat = 0.0f;
		}
		else
		{
			pausedFloat = 1.0f;
		}

		if (SceneManager.GetActiveScene().buildIndex != timeSinceLevelWasFirstLoaded_num)
		{
			timeSinceLevelWasFirstLoaded_num = SceneManager.GetActiveScene().buildIndex;
			timeSinceLevelWasFirstLoaded_offset = fa.timeSinceGameStarted;
		}

		timeSinceLevelWasFirstLoaded = fa.timeSinceGameStarted - timeSinceLevelWasFirstLoaded_offset;

		if (localPaused)
		{
			if (timeSet_forPaused == -1)
			{
				timeSet_forPaused = Time.time;
			}
			else if (timeSet_forPaused != -1)
			{
				currentTimePaused = Time.time - timeSet_forPaused;
			}
		}

		if (!localPaused)
		{
			if (timeSet_forPaused != -1)
			{
				timeSet_forPaused = -1;
				timePaused += currentTimePaused;
				currentTimePaused = 0;
			}
			fa.timeInSeconds = Time.time - timePaused;
			fa.timeSinceGameStarted = Time.realtimeSinceStartup - timePaused;
		}


		fa.timeScale = Time.timeScale * fa.slowmoFactor;
		fa.deltaTime = Time.deltaTime * fa.timeScale;

		if (localPaused)
		{
			fa.deltaTime = 0;
		}
		if (Time.realtimeSinceStartup > fa.timeUpdater)
		{
			if (!localPaused)
			{
				fa.time += (Time.realtimeSinceStartup - fa.timeUpdater) * fa.timeScale;
			}
			fa.timeUpdater = Time.realtimeSinceStartup;
		}

		//if (xa.allowPlayerInput && Input.GetKeyDown(KeyCode.P))
		//{
		//	fa.paused = !fa.paused;
		//	Debug.Log("PAUSED: " + fa.paused);
		//}



		//fa speedruntime calcs
		//fa.timeInSeconds == time since start of game, minus time spent paused.
		fa.speedrunTime = fa.timeInSeconds - fa.speedrunTimeSet;
	}

}
