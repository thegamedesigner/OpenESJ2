using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;

public class Main : MonoBehaviour
{
	readonly string[] profanity = { "FUCK", "SHIT", "NIPPLES" };
	public enum GameMode { NORMAL, INFINITE_LOVE };
	public bool steamInit = false;

	void OnLevelWasLoaded()
	{
		
		FreshLevels.Type type = FreshLevels.GetTypeNameForStr(SceneManager.GetActiveScene().name);
		RawFuncs.UpdateTextColor(type);

		if (FreshLevels.IsGameplayLevel(SceneManager.GetActiveScene().name))
		{
			fa.lastLevelPlayed = FreshLevels.GetTypeNameForStr(SceneManager.GetActiveScene().name);

			Ghosts.StartPlaybackOfGhosts();
		}
		else
		{
		}

		if (xa.mst_levelName != SceneManager.GetActiveScene().name)
		{
			xa.mst_levelName = SceneManager.GetActiveScene().name;
			xa.mst_timesALevelHasBeenLoaded = 0;
			//za.speedrunTimeSet = fa.timeInSeconds;

		}
		/*
		if ("StartMenu" == SceneManager.GetActiveScene().name)
		{
			//Raw menu system
			RawFuncs.self.MenuOn(RawInfo.MenuType.MainMenu, "main.cs");

			//Old "fresh" menu system
			//if (ProfileScript.self == null)
			//{
			//	Fresh_InGameMenus.self.ProfileMenu.SetActive(true);
			//	ProfileScript.self = Fresh_InGameMenus.self.ProfileMenu.GetComponent<ProfileScript>();
			//}
			//ProfileScript.self.AutoLogIn();

		}
		*/
		xa.mst_timesALevelHasBeenLoaded++;
		GenericMonsterScript.monsters = null;
		// Setup.GC_DebugLog("Arrived in level: " + Application.loadedLevel);
		xa.cleanXa();
		loadSubfiles();
		createBasics();
		findBasics();
		xa.null_quat = transform.rotation;


		if (fa.mouseGrab)
		{
			if (SceneManager.GetActiveScene().name == "MegaMetaWorld")
			{
				Setup.SetCursor(Setup.C.NotVisible, Setup.C.Locked);
			}
		}
		else
		{
			if (FreshLevels.IsFPSLevel(SceneManager.GetActiveScene().name))
			{
				Setup.SetCursor(Setup.C.NotVisible, Setup.C.Locked);
			}
			else
			{

				Setup.SetCursor(Setup.C.Visible, Setup.C.Unlocked);
			}
		}
		//xa.fadeOutToLevel                   = "";
		xa.faderOutScript = null;
		xa.spawnLibraryScript = null;
		xa.glx = Camera.main.GetComponent<Camera>().transform.position;
		//xa.cameraStartPos                     = xa.glx;
		//xa.glx.x                              = -999;
		//Camera.main.camera.transform.position = xa.glx;
		xa.debugTxtMesh = xa.de.debugTxt.GetComponent<TextMesh>();
		xa.realScore = xa.checkpointScore;
		xa.displayScore = xa.realScore;
		xa.countdownSecondsLeft = xa.checkpointSecondsLeft;
		StarScript.overwriteCollectedStarsWithCheckpointedStars();
		za.destroyBossMissiles = false;
		za.dontKillPlayerForBeingOffscreen = false;
		za.paralyzePlayerForCutscenes = false;
		za.artificalMusicVolumeCap = 1;
		za.dontRespawn = false;
		ShrineScript.registeredShrines = new System.Collections.Generic.List<ShrineScript>();

		// do this on first frame of update after shit's been spawned.
		//xa.onScreenSolid        = GameObject.FindGameObjectsWithTag("solidThing");
		//must be called every level load
		xa.onScreenObjectsDirty = true;
		xa.levelWindZones = GameObject.FindGameObjectsWithTag("windZone");//Defunct
		xa.levelRevWindZones = GameObject.FindGameObjectsWithTag("reverseWindZone");//defunct
		xa.levelDontDieZones = GameObject.FindGameObjectsWithTag("dontDieZone");//still in use
		xa.levelStickyZones = GameObject.FindGameObjectsWithTag("stickyZone");//defunct
		xa.levelDontDeleteZones = GameObject.FindGameObjectsWithTag("dontDeleteZone");//still in use

		za.relativeYForAwards = 0;


		if (Camera.main)
		{
			Camera.main.GetComponent<Camera>().farClipPlane = 500;
		}

		//hack to fix the Removing-letterboxing-on-respawn bug

		if (xa.de)
		{
			//Setup.GC_DebugLog("Called aspect script");
			xa.de.aspectScript.HandleAspectUtility();
		}


		calledOncePerLevel();
	}

	void calledOncePerLevel() // (per level load)
	{
		//Once per level total, not once per death/respawn (ie: actual App.loadLevels)
		string level = SceneManager.GetActiveScene().name;
		if (level != xa.oldLevelName)
		{
			//ONCE PER LEVEL (NOT PER RESPAWN)
			za.numOfRainbowStarsFound = 0;
			za.totalNumRainbowStarsInLevel = 0;
			xa.totalRespawns = 0;
			xa.totalLevelTime = 0;
			xa.realScore = 0;
			xa.displayScore = 0;
			xa.checkpointScore = 0;
			xa.checkpointSecondsLeft = xa.countdownSecondsTotal;
			za.deaths = 0;
			StarScript.cleanStarsRegister();
			xa.oldLevelName = level;



		}

	}

	void Awake()
	{
		
		if (PlayerPrefs.HasKey("fullscreen"))
		{
			xa.fullscreen = false;
			int re = PlayerPrefs.GetInt("fullscreen", 0);
			if (re == 1) { xa.fullscreen = true; }

			if (xa.fullscreen)
			{
				Screen.fullScreen = true;
			}
			else
			{
				Screen.fullScreen = false;
			}
		}
		else
		{
			xa.fullscreen = false;
			Screen.fullScreen = false;
		}

		if (PlayerPrefs.HasKey("pgmode")) { xa.pgMode = Fresh_Loading.GetBool("pgmode", false); }//load this super early, because other scripts want it on Start()
		DetailBlockInfo.InitDetailBlockInfo();
		// SetPlayerXControls.axisDownList = new List<int>();
		// SetPlayerXControls.axisDown = new bool[210];
		// SetPlayerXControls.SetDefaultControls();
	}

	void Start()
	{
		FrFuncs.Init();
		UnlockSystemScript.BackwardsCompat_Misc();
		//UsageStatsFunc.InitUsageStats();
		Application.targetFrameRate = 60;


		if (RawFuncs.sendUsageStats)
		{
			UsageStatsFunc.GetAllLvls();
		}

		//QualitySettings.vSyncCount = 1;

		/*
		case VidSettingsType.VSYNC :
			if(xa.vSync == 0)
				display.text = "Off";
			else
				display.text = (60 / xa.vSync).ToString() + "FPS";
			break;
		*/

		// loadSubfiles();
		checkForOtherTimeLords();
		//ConfigureResolution();

		Fresh_Loading.LoadMiscSettings();
		Fresh_Loading.LoadCustomControls();
		Fresh_Loading.LoadVolumes();
		//GhostManager.Init(LevelInfo.disableGhosts);

		//clean tapSlave arrays
		xa.cleanXa();

		Controls.DetectCurrentPlatform();
		Fresh_Loading.checkForNonSteamDLC();
		AskForMMLeaderboard();
	}

	public static void AskForMMLeaderboard()
	{
		//ask for weekly level 
		FreshLevels.Type weeklyLevel = FreshLevels.GetWeeklyLevel();
		//weeklyLevel = FreshLevels.Type.Tut_PortalRules;
		for (int i = 1; i <= fa.lengthOfMainMenuLeaderboard; i++)//load the main menu leaderboard
		{
			FrFuncs.Qc_GetMMLeaderboardNameSlot(weeklyLevel, i);
			FrFuncs.Qc_GetMMLeaderboardTimeSlot(weeklyLevel, i);
		}
	}

	void Update()
	{
		SteamLeaderboards.SteamAPITickUpdate();//handles all "should-use" checks internally, just call it every frame, all the time
											   //fa.dontConnect3rdParty = true;

		UnlockSystemScript.BackwardsCompat_LvlNumCheck();

		//if(fa.leaderboardTimes[1] != null) {RawFuncs.Print(fa.leaderboardTimes[1]); }
		if (fa.username == "thegamedev" || Application.isEditor)
		{
			fa.devMode = true;
			//fa.cheater = true; 
		}

		if (fa.devMode)
		{
			if (Input.GetKeyDown(KeyCode.V))
			{
				if (xa.playerScript != null) { xa.playerScript.hpScript.health = 0; }
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				if (RawFuncs.self != null)
				{
					RawFuncs.self.DebugController.SetActive(!RawFuncs.self.DebugController.activeSelf);
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha4)) { FrFuncs.Qc_ReportLevelTime(FreshLevels.Type.Tut_PortalRules, 40, "noGhostData"); }
			if (Input.GetKeyDown(KeyCode.Alpha5)) { FrFuncs.Qc_ReportLevelTime(FreshLevels.Type.Tut_PortalRules, 50, "noGhostData"); }
			if (Input.GetKeyDown(KeyCode.Alpha6)) { FrFuncs.Qc_ReportLevelTime(FreshLevels.Type.Tut_PortalRules, 60, "noGhostData"); }
			if (Input.GetKeyDown(KeyCode.Alpha7)) { FrFuncs.Qc_ReportLevelTime(FreshLevels.Type.Tut_PortalRules, 70, "noGhostData"); }
			if (Input.GetKeyDown(KeyCode.Alpha8)) { FrFuncs.Qc_ReportLevelTime(FreshLevels.Type.Tut_PortalRules, 80, "noGhostData"); }
			if (Input.GetKeyDown(KeyCode.Alpha9)) { FrFuncs.Qc_ReportLevelTime(FreshLevels.Type.Tut_PortalRules, 90, "noGhostData"); }
		}
		
		if (!fa.dontConnectSteam && !steamInit && SteamManager.Initialized)
		{
			RawFuncs.Print("Did init steam in Main.cs");


			steamInit = true;
			SteamStatsAndAchievements.SteamInit();
			fa.tellsteamtimeset = Time.time;
			fa.tellsteam = true;
		}
		else
		{
			if (!steamInit)
			{
				RawFuncs.Print("Didn't init steam in Main.cs, because: DontConnect: " + fa.dontConnectSteam + ", steamInit: " + steamInit + ", SteamManager: " + SteamManager.Initialized);

			}
		}
		
		if (fa.tellsteam)
		{
			if (Time.time > (fa.tellsteamtimeset + fa.tellsteamdelay))
			{
				SteamStatsAndAchievements.TellSteamAboutMyAchievos();
			}
		}


		CheckControlsReset();

		//if (Input.GetKeyDown(KeyCode.T)) { DBFuncs.self.TestScriptFunc(); }
		//if (Input.GetKeyDown(KeyCode.T)) { Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Coin); SteamStatsAndAchievements.GetSteamAchievement("Test1"); }
		//if (Input.GetKeyDown(KeyCode.Y)) { Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.ShotgunFiring); SteamStatsAndAchievements.GetSteamAchievement("Test2"); }
		//if (Input.GetKeyDown(KeyCode.U)) { Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.PG_Checkpoint); SteamStatsAndAchievements.GetSteamAchievement("Test3"); }
		//if (Input.GetKeyDown(KeyCode.N)) { Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Checkpoint); SteamStatsAndAchievements.WhatsMySteamName(); }


		AutoControls.UpdateAutoControls();
		UsageStatsFunc.UpdateUnparsed();
		//UsageStatsFunc.CheckStatus();
		//handle usage stats
		if (RawFuncs.sendUsageStats)
		{
			if (SceneManager.GetActiveScene().name != fa.usageStat_lvl)
			{
				if (fa.usageStat_timeSet != -1)
				{
					//coming off another level
					//Debug.Log("--> STOP: " + fa.usageStat_lvl + " " + Time.time);
					float totalTime = Time.time - fa.usageStat_timeSet;
					UsageStatsFunc.SendUsageStats(fa.usageStat_lvl, totalTime);


				}

				//now start again, for this new level
				fa.usageStat_lvl = SceneManager.GetActiveScene().name;
				fa.usageStat_timeSet = Time.time;
				//Debug.Log("--> START: " + fa.usageStat_lvl + " " + Time.time);
			}
		}








		DetectController.DetectCurrentController();

		//if (FreshLevels.IsGameplayLevel(SceneManager.GetActiveScene().name))
		//{
		//	Ghosts.UpdatePlaybackOfGhosts();
		//}

		HurtZoneScript.CleanDeadHurtZones();
		Controls.DetectControllerAxises();

		fa.UpdateTime();
		Controls.HandleDetectKeys();
		SetRendererBasedOnControlsType.checkControlsType();
		if (xa.onScreenObjectsDirty)
		{
			xa.onScreenSolid = GameObject.FindGameObjectsWithTag("solidThing");
			xa.onScreenObjectsDirty = false;
		}
		//IceZone reset
		xa.inZoneIce = 0;
		xa.inZoneStickyFloor = 0;

		for (int i = 0; i < 5; ++i)
		{
			xa.hasPlayedThisFrame[i] = false; // audio sfx
		}

		if (xa.resetFiringPatterns > 0)
		{
			xa.resetFiringPatterns--;
		}

		//if (!Setup.isMenuLevel(Application.loadedLevelName) && xa.allowPlayerInput)
		if (xa.allowPlayerInput)
		{
			bool failOut = false;
			if (xa.fresh_localNode != null)
			{
				if (xa.fresh_localNode.dontLetRespawnAndRestartHappen) { failOut = true; }
			}
			if (!failOut)
			{
				if (Controls.GetInputDown(Controls.Type.Restart, 0) && !(xa.fadingIn || xa.fadingOut) && !xa.localNodeScript.disableRespawnKey)
				{
					// Clean restart level
					za.deaths = 0;
					fa.ResetSpeedrun();

					fa.lastCheckpointX = -9999;
					fa.lastCheckpointY = -9999;
					fa.lastCheckpointLvlIndex = -9999;
					ThrowingBallScript.ResetSavedDaddysLove();
					xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_START, SceneManager.GetActiveScene().name);
				}
				if (Controls.GetInputDown(Controls.Type.Respawn, 0) && !(xa.fadingIn || xa.fadingOut) && !xa.localNodeScript.disableRespawnKey)
				{
					// respawn level
					xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_CHECKPOINT, SceneManager.GetActiveScene().name);
				}
			}
		}




		//fresh cheats
		if (Input.GetKey(KeyCode.Alpha3))
		{
			if (Input.GetKeyDown(KeyCode.T))
			{
				fa.cheater = true;
				NodeController.Cheat_UnlockALevel();
			}
			if (Input.GetKeyDown(KeyCode.Y))
			{
				fa.cheater = true;
				NodeController.Cheat_LockALevel();
			}
			if (Input.GetKeyDown(KeyCode.U))
			{
				fa.cheater = true;
				NodeController.Cheat_LockAllLevels();
			}
		}
		//if (Application.isEditor)
		//{
		if (fa.devMode)
		{
			if (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.E))
			{
				if (Input.GetKeyDown(KeyCode.I))
				{
					fa.cheater = true;
					xa.cheat_invinciblePlayer = !xa.cheat_invinciblePlayer;
				}
				if (Input.GetKeyDown(KeyCode.F))
				{
					fa.cheater = true;
					xa.cheat_perfectFlight_speed = 1;
					xa.cheat_perfectFlight = !xa.cheat_perfectFlight;
					xa.cheat_invinciblePlayer = !xa.cheat_invinciblePlayer;
					NodeController.lvlNum = 999;
					Fresh_Saving.SaveLvlNum();
				}
			}
		}
	}


	public static bool esc_waiting = false;
	public static float esc_timeset = -1;

	void CheckControlsReset()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			esc_waiting = true;
			esc_timeset = Time.time;
		}
		if (!Input.GetKey(KeyCode.Escape))
		{
			esc_waiting = false;
			esc_timeset = -1;
		}

		if (esc_waiting)
		{
			if (Time.time > (esc_timeset + 5) && esc_timeset != -1)
			{
				//reset controls
				esc_timeset = -1;
				esc_waiting = false;

				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.PG_Checkpoint);
				if (RawFuncs.self != null)
				{
					for (int i = 0; i < 25; i++)
					{
						Controls.customControls[i] = false;
						PlayerPrefs.SetInt("customControl" + i, 0);
					}
					PlayerPrefs.Save();
					Controls.SetDefaultControls();

					RawFuncs.self.UpdateAllListedDisplaysIfPossible();
					//Fresh_Saving.SaveControlsMode(Controls.useCustom);
					Fresh_Saving.SaveCustomControls();
					xa.de.ControlsResetPopupController.transform.LocalSetY(-7);
					iTween.MoveTo(xa.de.ControlsResetPopupController, iTween.Hash("islocal", true, "y", 0, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine));
					iTween.MoveTo(xa.de.ControlsResetPopupController, iTween.Hash("islocal", true, "delay", 4, "y", -7, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));

				}
			}
		}
	}



	void createBasics()//creates all this stuff on level load, for every level (use to create local hud elements, etc)
	{
		xa.tempobj = new GameObject("tempObj");
		xa.emptyObj = new GameObject("emptyObj2");
		xa.createdObjects = (GameObject)(Instantiate(xa.de.createdObjectsPrefab, Vector3.zero, xa.null_quat));
	}

	void loadSubfiles()
	{
		xa.ma = (Main)(this.gameObject.GetComponent("Main"));
		xa.de = (Defines)(this.gameObject.GetComponent("Defines"));
		xa.se = (Setup)(this.gameObject.GetComponent("Setup"));
		xa.re = (Restart)(this.gameObject.GetComponent("Restart"));
		xa.sn = (GC_SoundScript)(this.gameObject.GetComponentInChildren<GC_SoundScript>());
	}

	void checkForOtherTimeLords()
	{
		if (!xa.timeLord)
		{
			DontDestroyOnLoad(this.gameObject);
			xa.timeLord = this.gameObject;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void findBasics()
	{
		//find player
		// Setup.GC_DebugLog("Looking for player");
		GameObject go;
		go = GameObject.FindWithTag("playerTag");
		if (go)
		{
			xa.player = go;
			//   Setup.GC_DebugLog("Found player");
		}
		else
		{
			//	Setup.GC_DebugLog("Didnt find player");
			xa.player = null;
		}
	}

	public bool checkStringForProfanity(string str)
	{
		if (string.IsNullOrEmpty(str))
			return false;

		foreach (string s in profanity)
		{
			if (str.ToUpper().Contains(s.ToUpper()))
				return true;
		}
		return false;
	}


}
