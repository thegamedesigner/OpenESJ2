using UnityEngine;

public class xa : MonoBehaviour
{
	public static string verStr = "Ver 1.8";//ESJ2 on steam: this becomes the string that is displayed in the main menu
	public static bool hasBonusDLC = false;
	public static bool hasAlpDLC = false;

	//Old, defunct, for weekly builds
	public static float freshVersionNumber = 1.03f;//Set to the week number, 
	public static string buildWord = "BugPatch2";
	public static string week = "103";

	//Versions
	public static bool playtestingVersion = false;//records data to the internet
	public static bool cheat_invinciblePlayer = false;//true;
	public static bool cheat_perfectFlight = false;//true;
	public static int cheat_perfectFlight_speed = 0;//true;
	public static bool onlyXboxControls = false;
	public static bool useBetaDestroy = false;//check to see if it should trigger destroy
	public static bool betaVersion = false;//checks if is already destroyed

	public static bool attractModeVersion = false;
	public static bool includeAlex01InAttractModeVersion = false;
	public static bool displayVersion = false;

	//Multi-Spawn Text
	public static int mst_timesALevelHasBeenLoaded = 0;//Just for mutli-spawn text, not for scoring/gameplay
	public static string mst_levelName = "";

	public static bool lockWorld1 = false;//Locks everything except the first few levels
	public static bool lockWorld2 = false;//Locks the world (for beta testers who have a free version)
	public static bool lockWorld3 = false;
	public static bool forceUnlockOfFirstLevelOfWorld2 = false;//world 1's first level is always unlocked anyway
	public static bool forceUnlockOfFirstLevelOfWorld3 = false;
	//Free beta
	/*
	public static bool lockWorld1 = true;//Locks everything except the first few levels
	public static bool lockWorld2 = false;//Locks the world (for beta testers who have a free version)
	public static bool lockWorld3 = true;
	public static bool forceUnlockOfFirstLevelOfWorld2 = true;//world 1's first level is always unlocked anyway
	public static bool forceUnlockOfFirstLevelOfWorld3 = false;
	*/

	public static int savedProgress = 0;
	public static Main.GameMode gameMode = Main.GameMode.NORMAL;
	public static float soundVolume = 0.4f;// 0.7f;//0.8f;//.4f;
	public static float musicVolume = 0.5f;//1f;//0.6f;//0.5f;
	public static float soundVolumeSetTo = 0.4f;//should be set to the exact same number as the above
	public static float musicVolumeSetTo = 0.5f;
	public static LocalNodeScript localNodeScript = null;
	public static Fresh_LocalNode fresh_localNode = null;

	public static bool[] hasPlayedThisFrame = { false, false, false, false, false };
	
	public static Vector3 playerPos = Vector3.zero;

	public static Main ma = null;
	public static Defines de = null;
	public static HUD hd = null;
	public static Setup se = null;
	public static Restart re = null;
	public static GC_SoundScript sn = null;

	public enum NPCs { None, Sleeper, Kuma, Lerp }
	public static CameraScript caS;

	public static GameObject timeLord = null;
	public static GameObject player = null;
	//public static PlayerScript playerScript = null;
	public static NovaPlayerScript playerScript = null;
	public static GameObject playerHitBox = null;
	public static GameObject parkedShip = null;
	public static GameObject cameraRocket = null;
	public static GameObject boss = null;
	public static GameObject bard = null;
	public static bool vsyncON = false;
	public static bool fullscreen = false;
	public static GameObject timeObject = null;
	public static GameObject scoreObject = null;
	public static GameObject countdownObject = null;
	public static GameObject deathCounter = null;
	public static GameObject awardPopupSpawner = null;
	public static AwardPopupSpawner awardPopupSpawnerScript = null;
	public static bool playingLoopingSecondTrack = false;
	public static TextMesh debugTxtMesh = null;
	public static GameObject onScreenCollider = null;
	public static GameObject strictOnScreenCollider = null;
	public static GameObject onScreenColliderController = null;
	public static GameObject quitPopup = null;
	public static GameObject worldPopup = null;
	public static GameObject overlayCamera = null;

	public static bool beenToLevel0 = false;
	public static bool fadingOut = false;//read-only, set by fadeOutScript
	public static bool fadingIn = false;//read-only, set by fadeInScript
	public static bool fadeInFast = false;//set by fadenScriptI
	public static float fadeOutProgress = 0;
	public static GameObject faderOut = null;
	public static GameObject faderIn = null;
	public static FaderOutScript faderOutScript = null;
	public static bool fadingToSameLevel = false;
	public static bool fadingToOtherLevelWithMenuMusic = false;
	public static bool fadingAtAll = false;//turned on at the first fade in, turned off when loaded. 
	public static InfLoveFader infLoveFader = null;

	public static float genericBossHealth = 0;//Used by GenericBossScript.cs
	public static float combatHeight = 1;//Used to level off various hitchecks against each other
	public static bool playerIsDead = false;
	public static float bossHealth = 0;
	public static float bossHealthMax = 0;
	public static float halfBlock = 0.5f;
	public static float blockSize = 1f;
	public static float playerBoxHeight = 0;
	public static float playerBoxWidth = 0;
	public static int playerDir = 0;
	public static int playerControlsHeldDir = 0;//Set the same time the player presses left/right buttons, doesn't line up with the animation, does line up with the controls
	public static bool playerMoving = false;
	public static bool playerMoveLeft = false;
	public static bool playerMoveRight = false;
	public static bool playerOnGround = false;
	public static bool playerOnAniGround = false;//This triggers slightly before the player really is on the ground
	public static bool playerJumped = false;
	public static bool playerJumpedFresh = false;
	public static Vector2 playerVel = new Vector2(0, 0);
	public static float playerPoundTimer = 0;
	public static bool playerClungToWallTHISFrame = false;
	public static bool playerStuck = false;
	public static float playerStuckDir = 0;
	public static bool playerCrouching = false;
	public static bool playerAirSwording = false;
	public static bool playerDead = false;//used for the 'going up to heaven bit'. Does not mean the player is actually destroyed yet.
	public static bool frozenCamera = false;
	public static bool freezePlayer = false;
	public static bool freezePlayerForCutscene = false;
	public static int forcePlayerDirection = 0;
	public static string levelBeforeGoingToZero = "";
	public static float backEdgeOfScreen = -999;
	public static float frontEdgeOfScreen = -999;
	public static float topEdgeOfScreen = -999;
	public static float bottomEdgeOfScreen = -999;
	public static bool playerHasMoved = false;
	public static Vector3 cameraStartPos = Vector3.zero;
	public static int countdownSecondsLeft = 300;
	public static int countdownSecondsTotal = 300;
	public static int checkpointSecondsLeft = 300;

	public static bool nearDog = false;
	public static float resetTimer = 0;
	public static bool hasBeenOffGroundSincePress = false;
	public static bool hasBeenOnGroundSincePress = false;
	public static bool playerHasGroundPound = false;
	public static bool playerHasDoubleJump = false;
	public static bool playerHasJetpack = false;
	public static bool playerHasSuperJump = false;
	public static float camGoalX = 0;
	public static float popeStage = 0;
	public static GameObject pope = null;
	public static int popeHits = 0;
	public static float popeHealth = 122;
	public static int rainbowAmmo = 0;
	public static int rainbowAmmoLoaded = 0;
	public static int runes = 0;
	public static int runesExisting = 0;
	public static int runeFinalText = 0;
	public static int runesCollected = 0;
	public static int checkpointScore = 0;//what the player's score was at the last checkpoint
	public static int displayScore = 0;//what is displayed. May not match actual score (busy animating)
	public static int realScore = 0;//always exactly how much the player has
	public static int totalRespawns = 0;
	public static int totalGameRespawns = 0;
	public static float totalLevelTime = 0;
	public static float totalCachedLevelTime = 0;
	public static string playtesterName = "blank";
	public static string oldLevelName = "";// loadedLevelFromDifferentLevel = false;
	public static bool haltForSecondGlorg = false;
	public static int fakeRandom = 17;
	public static CutsceneCharacterScript wizardCutsceneScript = null;
	public static int levelSelectCurrentNumber = 0;
	public static float startTimeThisLevel = 0; // in seconds
	public static int lastSpeedRunTime = 0;
	public static float[] speedRunTimes = new float[LevelInfo.getNumberOfLevels()]; // also in seconds
	public static int[] playerDeaths = new int[LevelInfo.getNumberOfLevels()]; // per level
	public static int deathCountThisLevel = 0;
	public static int carryingStars = 0; // picked up but not taken to checkpoint
	public static int checkpointedStarsThisLevel = 0; // saved at checkpoint but not finished level
	public static int[] playerStars = new int[LevelInfo.getNumberOfLevels()]; // per level
	public static bool showTimer = true;
	public static bool showScore = true;
	public static bool showCountdown = true;
	public static bool allowPlayerInput = true;
	public static bool pgMode = false;
	public static bool showDeathCounter = false;
	public static bool speedRunDirty = false;
	// Track objects on-screen (so we don't loop everything everywhere all the goddamn time)
	public static bool onScreenObjectsDirty = true;
	//public static List<GameObject> onScreenSolid    = new List<GameObject>();
	public static GameObject[] onScreenSolid = null;
	public static GameObject[] levelWindZones = null;
	public static GameObject[] levelRevWindZones = null;
	public static GameObject[] levelDontDieZones = null;
	public static GameObject[] levelStickyZones = null;
	public static GameObject[] levelDontDeleteZones = null;
	//public static List<GameObject> onScreen;

	//Infinite Love Mode
	public static GameObject[] enabledInfLoveGameObjects;
	public static TriggerZoneScript infLoveTriggerZone = null;
	public static int portalStep = 0;
	public static int lastPortalStep = 0;
	public static int maxPortalStep = 0;

	//zones
	public static int inZoneIce = 0;
	public static int inZoneStickyFloor = 0;
	public static bool hasCheckpointed = false;//has touched a checkpoint before
	public static bool firstCheckpointTriggered = false;
	public static Vector3 lastSpawnPoint = Vector3.zero;//last checkpoint pos

	public static int tap1 = 0;
	public static int tap2 = 0;
	public static int tap3 = 0;
	public static int tap4 = 0;
	public static int tap5 = 0;
	public static TapSlaveScript[] tapSlaves = new TapSlaveScript[400];
	public static bool[] tapSlavesCheck = new bool[400];
	public static bool button1Pressed = false;
	public static bool button2Pressed = false;
	public static bool button3Tapped = false;//true for the frame when it's just started being tapped / OnKeyDown
	public static bool jumpButtonHeld = false;

	// Add new entries at the bottom above Max
	// MUST match up with renderLayerDepth below.
	// Do not rearrange under pain of breaking all unity-made levels.
	public enum layers {
		None = 0,
		Invisible = None,
		exploOvertop1,
		GribblyFront0,
		GribblyFront,
		GribblyFront2,
		PlayerAndBlocks,  // Player and blocks
		Monsters,         // ...Monsters
		InWorld,          // Stuff on the ground but behind the player (checkpoints, etc)
		GribblyBehind0,
		GribblyBehind,
		Explo1,           // Explos in front of all other explos, but behind everything else but backgrounds
		Explo2,           // Explos behind explo1Layer, but in front of explo3Layer
		Explo3,
		Explo4,
		Background,       // real background stuff (sky beams, that kind of thing)
		Background2,
		Background3,
		GribblyFront3,
		GribblyFront4,
		GribblyBehind2,
		GribblyBehind3,
		Explo5,
		Explo6,           // Explos behind all other explos
		Background4,
		Background5,
		Background6,
		RaycastLayer,
		JustABitBehindPlayer,
		JustABitFurtherBehindPlayer,
		HomingMissile,
		PlayerDeath,
		Max // Add new layers above me. (I am behind the camera)
	}

	// Match order of the enum above. Add new entries only just above the last one
	private static readonly float[] renderLayerDepth = new float[] {
		float.MinValue, // None / Invisible
		30.0f,  // exploOvertop1,
		60.0f,  // GribblyFront0,
		70.0f,  // GribblyFront,
		80.0f,  // GribblyFront2,
		110.0f, // PlayerAndBlocks,
		120.0f, // Monsters,
		130.0f, // InWorld,
		140.0f, // GribblyBehind0,
		150.0f, // GribblyBehind,
		180.0f, // Explo1,
		190.0f, // Explo2,
		200.0f, // Explo3,
		210.0f, // Explo4,
		240.0f, // Background,
		250.0f, // Background2,
		260.0f, // Background3,
		90.0f,  // GribblyFront3,
		100.0f, // GribblyFront4,
		160.0f, // GribblyBehind2,
		170.0f, // GribblyBehind3,
		220.0f, // Explo5,
		230.0f, // Explo6,
		270.0f, // Background4,
		280.0f, // Background5,
		290.0f, // Background6,
		500.0f, // RaycastLayer,
		114.0f, // JustABitBehindPlayer,
		118.0f, // JustABitFurtherBehindPlayer
		25.0f,  // HomingMissile
		40.0f,  // PlayerDeath
		float.MaxValue // MAX -- add new layers above me.
	};

	public static float GetLayer(layers layer)
	{
		return renderLayerDepth[(int)layer];
	}

	// public enum saveTypes { None, Coin, Goomba, TinyGoomba, ExploThatShouldDie }
	public enum saveActionTypes {
		None,
		Respawn,
		Destroy
	}

	public static SpawnLibraryScript spawnLibraryScript = null;
	public static float[] saveState = new float[1000];
	public static float[] frozenSaveState = new float[1000];
	public static int[] saveType = new int[1000];
	public static string[] saveName = new string[1000];
	public static saveActionTypes[] saveActionType = new saveActionTypes[1000];
	public static Vector3[] savePos = new Vector3[1000];
	public static GameObject[] saveObj = new GameObject[1000];
	public static GameObject[] savePrefab = new GameObject[1000];
	public static int resetFiringPatterns = 0;
	public static string editorCurrentLevelName = "BlankLevel";
	public static string loadThisLevelOnReload = "";
	public static int editorMaxObjectsPerLevel = 1000;

	public static float debug1;
	public static float debug2;
	public static float debug3;
	public static float debug4;
	public static float debug5;

	public static GameObject createdObjects;
	//public enum cubeType { NoType, Grass, FenceStraight }
	//public static Cube[,] grid = new Cube[9, 9];

	public static Quaternion null_quat;
	public static Vector3 glx;
	public static Vector3 glx2;
	public static Vector3 glx3;
	public static GameObject tempobj;
	public static GameObject tempobj2;
	public static GameObject emptyObj;
	public static Color tempColor;
	public static float tempFloat;
	public static float tempFloat2;

	public static Vector3[] path = new Vector3[1000];

	public static float beat_Freq = 0.0f;
	public static float music_Time = 0.0f;
	public static float[] music_Spectrum = new float[128];
	public static MusicLineManager musicLineManager = null;

	//Button strings
	public static string jumpButton = "Z";
	public static string poundButton = "X";

	//Stats
	public static bool postToAltLeaderboards = false;
	public static bool deleteAllConfirm = false;
	public static int muteMusic = 1; // 1 = play at intended volume, 0 = mute
	public static int muteSound = 1;
	public static float localMute = 1;//Mutes everything. Is never saved.

	// Fresh Video Settings
	public static int FrameLimit = 60;

	//fresh video settings
	public static Resolution[] freshSupportedResolutions;
	public static int resIndex = 0;

	// Old Video Settings
	//public static Resolution[] supportedResolutions;
	public static int vSync = 1;
	//public static bool fullscreen                    = true;
	//public static int currentResolutionIndex         = -1;
	//public static int setResolutionIndex             = -1;
	//public static int oldResolutionIndex             = -1;
	//public static int oldVSync                       = 1;
	//public static bool oldFullscreen                 = true;
	//public static bool videoSettingsConfirmExit      = false;
	//public static bool videoSettingsRevertAfterApply = false;

	// Was the save file created this game?
	public static bool saveFileCreatedThisSession = false;


	public static void cleanXa()
	{
		cleanTapSlaveArrays();
		xa.pope                 = null;
		xa.popeStage            = 0;
		xa.popeHits             = 0;
		xa.frontEdgeOfScreen    = -999;
		xa.backEdgeOfScreen     = -999;
		xa.topEdgeOfScreen      = -999;
		xa.bottomEdgeOfScreen   = -999;
		xa.playerHasGroundPound = false;
		xa.playerHasDoubleJump  = false;
		xa.playerHasJetpack     = false;
		xa.playerHasSuperJump   = false;
		xa.gameMode             = Main.GameMode.NORMAL;
		HurtZoneScript.HZs      = null;

		string level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (level == "InfLove" || level == "InfLove2")
		{
			xa.gameMode = Main.GameMode.INFINITE_LOVE;
		}
	}

	public static void cleanXaOncePerNewLevel()
	{
		xa.playingLoopingSecondTrack = false;
	}

	public static void cleanTapSlaveArrays()
	{
		int index = 0;
		while (index < xa.tapSlaves.Length)
		{
			xa.tapSlaves[index] = null;
			xa.tapSlavesCheck[index] = false;
			index++;
		}
	}

	public static void calledOncePerRunningTheOfGame()
	{
		int index = 0;
		while (index < xa.saveState.Length)
		{
			xa.saveState[index] = 0;
			xa.frozenSaveState[index] = 0;
			xa.savePos[index] = Vector3.zero;
			xa.saveType[index] = 0;
			xa.saveName[index] = "";
			xa.saveObj[index] = null;
			index++;
		}
	}
}
