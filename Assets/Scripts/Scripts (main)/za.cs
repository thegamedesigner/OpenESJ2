using UnityEngine;

public class za : MonoBehaviour
{
	//static stuff for merp

	public static MerpsLocalNode merpsLocalNode;
    public enum merpsWorlds { None, World1, World2 }
    //public static PlayerScript merpsPlayerScript = null;//Old, prenova playerScript update
    public static NovaPlayerScript merpsPlayerScript = null;
	public static GameObject merpsPlayerRenderer = null;
	public static int playerInsideMerpsDoubleJumpZone = 0;

	public static int[] chatObjectStaticStages = new int[10];
	public static int chatObjectStaticStagesLevel = -1;

	public static int merpsStyleNumber = 0;//0=desert, 1=jungle, 2=sewers(not in yet), 3=Ruined city(not in yet), 4=alien mothership(not in yet)

	public static float merpsBackgroundLayer1XOffset = 0;
	public static float merpsBackgroundLayer2XOffset = 0;
	public static float merpsBackgroundLayer3XOffset = 0;
	public static float merpsBackgroundLayer4XOffset = 0;

	public static Color merpsEditorDisplayColor = Color.white;
	public static Color merpsBackgroundLayer1Color = Color.white;
	public static Color merpsBackgroundLayer2Color = Color.white;
	public static Color merpsBackgroundLayer3Color = Color.white;
	public static Color merpsBackgroundLayer4Color = Color.white;
	public static Color merpsBackgroundSkyColor = Color.white;

	public static bool dontPaintbrush = false;

	public static Vector4 cameraLimits = Vector4.zero;//left (<0),right(>0),down(<0y),up(>0y)

	public static bool hasGun = true;
	public static bool forceAimGun = false;
	public static int gunDir = 3;

	public static bool playerFlipped = false;

    public static Vector3[] collectableStars = new Vector3[300];//Must be same legnth as below
    public static Vector3[] checkpointedStars = new Vector3[300];//Must be same length as above

    public static Vector3 inworldScorePos = Vector3.zero;
    public static Vector3 inworldCountdownPos = Vector3.zero;
    public static Vector3 inMainCameraWorldScorePos = Vector3.zero;

    public static bool destroyBossMissiles = false;

    public static bool thisLevelShouldntHaveScoreOrTimers = false;
    public static bool dontUseQuitPopupOnThisLevel = false;
    public static bool allowUseWorldPopupOnThisLevel = false;//Normally false
    public static int menuSelectionBoxValue = 0;
    public static int numberOfElectricalBolts = 0;
    public static bool dontKillPlayerForBeingOffscreen = false;
    public static bool usePlayerMetaPos = false;
    public static Vector3 playerPosInMetaWorld = new Vector3(9999, 9999, 9999);
    public static bool paralyzePlayerForCutscenes = false;
    public static float artificalMusicVolumeCap = 1;
    public static int totalNumRainbowStarsInLevel = 0;
    public static int numOfRainbowStarsFound = 0;
    public static Vector3[] secretAreas = new Vector3[40];//max of 40 secret areas
    public static bool[] secretAreaSlots = new bool[40];
    public static int blackStarsTouched = 0;
    public static bool killSoundEffects = false;
    public static bool forceChangeLevelOnStart = false;
    public static string forceChangeToThisLevel = "";

    public static bool forceMuteMusicForThisLevel = false;

   // public static string speedrunTimeStr = "00:00:00";
   // public static float speedrunTime = 0;
   // public static float speedrunTimeSet = 0;

    public static int deaths = 0;

    public static GameObject skald = null;
    public static SkaldScript skaldScript = null;

    public static float relativeYForAwards = 0;//This is added to by each award object, and subtracted from just before they die. Bumps other awards down.

    public static bool canSetSteamAchievements = false;

    public static int screenX = 0;
    public static int screenY = 0;
    public static bool fullscreen = false;

    public static bool dontRespawn = true;
    public static bool relookForStars = false;
    public static GameObject touchControlsObject;
    public static bool forceKeyboard = false;
    public static bool hideMobileControlsOnThisLevel = false;
    public static bool mobilePopupMenuIsUp = false;
    public static string globalStaticMobileDebugString = "";
    public static bool useSuperForgivingJump = false;//set to true on devices with input lag
    public static bool useSnapCameraToCheckpoint = false;
    public static Vector3 snapCameraToThisPos = Vector3.zero;
}
