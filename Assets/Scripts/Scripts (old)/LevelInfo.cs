using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    //savable stats
    public static bool[] unlocked = new bool[30];
    public static bool[] beaten = new bool[30];
    public static bool[] beatenThisPlaySession = new bool[30];//Never saved, just static
    public static int[] score = new int[30];
    public static int[] timeLeft = new int[30];
    public static int[] rainbowStars = new int[30];//This is only updated if you beat your best score (time, stars, secrets, total)
    //public static int[] nonScoreRainbowStars = new int[30];//This is updated everytime you find a secret, and saved on it's own - For Achievements
    //public static int[] totalRainbowStars = new int[30];
    public static int[] deaths = new int[30];
    public static int[] totalScore = new int[30];
    public static float[] bestTime = new float[30];

    public static bool disableGhosts = false;
    public static int totalRainbowStars = 0;
    public static Vector2[] firstTimeRainbowStars = new Vector2[50];



    public static void UpdateDisableGhostsOrNot()
    {
        if(disableGhosts)
        {
            GhostManager.DisableGhosts();
        }
        else
        {
            GhostManager.EnableGhosts();
        }
    }



    //All non-menu-levels level strings
    public static string[] sceneNames = {
                                         
    //Quest for the Laser-Nipples
	"", 
	"YazarGC_IntroStory", 
	"YazarGC_10_HappyCity", 
    "YazarGC_20_RobotCutscene1",
    "YazarGC_40_RobotCutscene2",
    "YazarGC_50_RuinedCity",
    "YazarGC_55_LearningLevel",
    "YazarGC_60_Tower",
    "YazarGC_65_TowerTop",
    "YazarGC_70_RampUp",
    "YazarGC_75_Factory",
    "YazarGC_80_BlackStarsBlueSky",
    "YazarGC_110_Playground",
    "YazarGC_125_SlowDance2",
    "YazarGC_127_Alley",
    "YazarGC_130_StripClub",
    "YazarGC_140_TrainFight",
    "YazarGC_150_Biplane",
	"YazarGC_OutroStory", 
    
    //Bonus content
    "YazarGC_Secret1_Ice",
    "YazarGC_Secret2_MissileMountain",
    "YazarGC_Girders",

    //Derp's Cave
	"YazarGC_Derps_10_Tutorial",
	"YazarGC_Derps_20_Eyeball",
	"YazarGC_Derps_30_UpsideDown",
	"YazarGC_Derps_40_Ice",
	"YazarGC_Derps_50_Boss",
    };

    //*All* level strings
    public static string[] developerLevels = {
                                         
    //Quest for the Laser-Nipples
	"", 
	"SamuraiSword1", 
	"YazarGC_IntroStory", 
	"YazarGC_10_HappyCity", 
    "YazarGC_20_RobotCutscene1",
    "YazarGC_40_RobotCutscene2",
    "YazarGC_50_RuinedCity",
    "YazarGC_55_LearningLevel",
    "YazarGC_60_Tower",
    "YazarGC_65_TowerTop",
    "YazarGC_70_RampUp",
    "YazarGC_75_Factory",
    "YazarGC_80_BlackStarsBlueSky",
    "YazarGC_110_Playground",
    "YazarGC_125_SlowDance2",
    "YazarGC_127_Alley",
    "YazarGC_130_StripClub",
    "YazarGC_140_TrainFight",
    "YazarGC_150_Biplane",
	"YazarGC_OutroStory", 
    
    //Bonus content
    "YazarGC_Secret1_Ice",
    "YazarGC_Secret2_MissileMountain",
    "YazarGC_Girders",

    //Derp's Cave
	"YazarGC_Derps_10_Tutorial",
	"YazarGC_Derps_20_Eyeball",
	"YazarGC_Derps_30_UpsideDown",
	"YazarGC_Derps_40_Ice",
	"YazarGC_Derps_50_Boss",

    //menus
    "YazarGC_World",
    "YazarGC_MenuTutorial",
    };
    public static string[] levelNames = {
                  
    //Quest for the Laser-Nipples     
	"",                        
	"_Intro Story",
	"Groove City",
	"_Robot Cutscene 1",
	"_Robot Cutscene 2",
	"Ruined City",
	"Bouncing Around",
	"Skyscraper",
	"Rooftop",
	"Getting Harder",
	"Junk Yard",
	"Dark Star",
	"Playground",
	"Square Dance",
	"Alleyway",
	"Strip Club",
	"Train",
	"Biplane",
	"_Outro Story",
                   
    //Bonus/Secret content
    "Ice",
    "Missile Mountain",
    "Girders",

    //Derp's Cave
	"The Cave",
	"Eyeball",
	"Looking Down",
	"Slimy",
	"Showdown",                           
  };

    public static string[] levelNamesPG = {
                  
    //Quest for the Laser-Nipples     
	"",                        
	"_Intro Story",
	"Groove City",
	"_Robot Cutscene 1",
	"_Robot Cutscene 2",
	"Ruined City",
	"Bouncing Around",
	"Skyscraper",
	"Rooftop",
	"Getting Harder",
	"Junk Yard",
	"Dark Star",
	"Playground",
	"Square Dance",
	"Alleyway",
	"Silly Club",
	"Train",
	"Biplane",
	"_Outro Story",
                   
    //Bonus/Secret content
    "Ice",
    "Missile Mountain",
    "Girders",

    //Derp's Cave
	"The Cave",
	"Eyeball",
	"Looking Down",
	"Slimy",
	"Showdown",                           
  };

    public static string[] leaderboardNames = {
		
		//Quest for the Laser-Nipples     
		"",                        
		"",
		"01. Happy City",
		"",
		"",
		"02. Ruined City",
		"03. Bouncing Around",
		"04. Skyscraper",
		"05. Rooftop",
		"06. Getting Harder",
		"07. Junk Yard",
		"08. Dark Star",
		"09. Playground",
		"10. Square Dance",
		"",
		"11. Strip Club",
		"12. Train",
		"13. Biplane",
		"",
		
		//Bonus/Secret content
		"Secret Level 02. Ice",
		"Secret Level 01. Missile Mountain",
		"",
		
		//Derp's Cave
		"",
		"",
		"",
		"",
		"",                           
	};

    public static string[] displayNumbers = {
                                         
    //Quest for the Laser-Nipples
	"", 
	"", 
	"01", //Happy city
    "",
    "",
    "02", //Ruined city
    "03", //Learning level
    "04", //Tower
    "05", //Tower top
    "06",//YazarGC_70_RampUp
    "07",//YazarGC_75_Factory
    "08",//YazarGC_80_BlackStarsBlueSky
    "09",//YazarGC_110_Playground
    "10",//YazarGC_125_SlowDance2
    "",//YazarGC_127_Alley
    "11",//YazarGC_130_StripClub
    "12",//YazarGC_140_TrainFight
    "13",//YazarGC_150_Biplane
	"", //YazarGC_OutroStory
    
    //Bonus content
    "01",//YazarGC_Secret1_Ice
    "02",//YazarGC_Secret2_MissileMountain",
    "",//YazarGC_Girders

    //Derp's Cave
	"",//YazarGC_Derps_10_Tutorial",
	"",//YazarGC_Derps_20_Eyeball",
	"",//YazarGC_Derps_30_UpsideDown",
	"",//YazarGC_Derps_40_Ice",
	"",//YazarGC_Derps_50_Boss",
    };

    public static float[] goldScore = 
    { 
        0, //Ignore - Exit num
        0, //Ignore - Intro Story
        1200f,//happy City 
        0, //Ignore - Robotcutscene 1
        0, //Ignore - Robotcutscene 2
        300f, //Ruined city
        300f, //Bouncing Around
        300f, //Skyscraper
        300f, //Rooftop
        300f, //Getting Harder
        300f, //Junk Yard
        300f, //Dark Star
        300f, //Playground
        300f, //Square Dance
        0, //Alleyway
        300f, //Strip Club
        300f, //Train
        300f, //Biplane
      //other levels (bonus, microadventure)
        300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 
                                       300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 
                                       300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f, 300f };



    static readonly string firstHardcore = "Hardcore1";
    static readonly string firstBonus = "ThatIcySmashyOne";

    static readonly int[] dlcLevels = {
		53, 54 };

    static readonly float[] targetWorldTimes = { 600.0f, 1020.0f, 1320.0f, 1560.0f };

    public static bool isSecretLevel(string str)
    {
        return (str == "YazarGC_Secret1_Ice" ||
            str == "YazarGC_Secret2_MissileMountain");
    }

    public static bool isCoreLevel(string str)
    {
        return (str == "YazarGC_10_HappyCity" ||
            str == "YazarGC_50_RuinedCity" ||
            str == "YazarGC_55_LearningLevel" ||
            str == "YazarGC_60_Tower" ||
            str == "YazarGC_65_TowerTop" ||
            str == "YazarGC_70_RampUp" ||
            str == "YazarGC_75_Factory" ||
            str == "YazarGC_80_BlackStarsBlueSky" ||
            str == "YazarGC_110_Playground" ||
            str == "YazarGC_125_SlowDance2" ||
            str == "YazarGC_130_StripClub" ||
            str == "YazarGC_140_TrainFight" ||
            str == "YazarGC_150_Biplane");
    }

    public static void unlockAllLevels()
    {
        int index = 0;
        while (index < LevelInfo.unlocked.Length)
        {
            LevelInfo.unlocked[index] = true;
            index++;
        }
    }

    /**
     * Returns the world for a given scene !Zero Indexed!
     * (array index of sceneNames)
     */
    /**
     * Returns the level number in the world. ie World1 Level n
     */

    public static int getSceneNumFromName(string level)
    {
        level = level.ToLower();
        for (int i = 0; i < sceneNames.Length; ++i)
        {
            if (sceneNames[i].ToLower() == level)
                return i;
        }
        return -1;
    }

    public static int getNumberOfLevels()
    {
        return sceneNames.Length;
    }

    public static int getFirstSceneOfWorld(int world)
    {
        int scene = 0;
        switch (world)
        {
            case 1: scene = getSceneNumFromName("PAXATutorialOfAwesome"); break;
            case 2: scene = getSceneNumFromName("sweetGreen"); break;
            case 3: scene = getSceneNumFromName("CassiesRedLevel1of2"); break;
            case 4: scene = getSceneNumFromName("Hardcore1"); break;
            case 5: scene = getSceneNumFromName("ThatIcySmashyOne"); break;
        }
        return scene;
    }

    public static bool isFirstSceneOfWorld(string str)
    {
        return (str == "AChunkOfStory" || str == "PAXATutorialOfAwesome" || str == "sweetGreen" || str == "CassiesRedLevel1of2" || str == "Hardcore1" || str == "ThatIcySmashyOne");
    }

    public static bool HasUnlockedSecrets()
    {
        return (LevelInfo.unlocked[19] || LevelInfo.unlocked[20]);
    }
    public static string getSceneName(int levelNum)
    {
        if (levelNum < 0 || levelNum >= sceneNames.Length)
        {
            return "";
        }
        return sceneNames[levelNum];
    }

    public static string getLevelName(int levelNum)
    {
        if (levelNum < 0 || levelNum >= levelNames.Length)
        {
            return "";
        }
            return levelNames[levelNum];
    }

    public static string getLeaderboardName(int levelNum)
    {
        if (levelNum < 0 || levelNum >= leaderboardNames.Length)
        {
            return "";
        }
        return leaderboardNames[levelNum];
    }


    public static float getGoldScore(int levelNum)
    {
        if (levelNum < 0 || levelNum >= goldScore.Length)
        {
            return 0.0f;
        }
        return goldScore[levelNum];
    }

    public static float getTargetWorldTime(int worldNum)
    {
        if (worldNum > 0 && worldNum <= targetWorldTimes.Length)
        {
            return targetWorldTimes[worldNum - 1];
        }
        return 0.0f;
    }


    public static bool CheckIsDeveloperLevel(string str)
    {
        foreach (string s in developerLevels)
        {
            if(s == str){return true;}
        }
        return false;
    }


}
