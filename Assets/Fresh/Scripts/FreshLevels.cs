using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FreshLevels : MonoBehaviour
{
	public static List<Level> levels;
	public enum Type
	{
		None,
		CaveOfWonders1,
		CaveOfWondersHell,
		BablinAlong,
		Pink1,
		Pink2,
		Blue1,
		Blue2,
		Challenge1,
		JamLvl1,
		Tut_Village,
		Tut_Pink1,
		Challenge2,
		Tut_Chill,
		Challenge3,
		Tut_PortalRules,
		Tut_JumpAndAirsword,
		Tut_DoubleJump,
		GodOfDeath,
		JamLvl3,
		IntroStory,
		UpwardsHell,
		FPS1,
		MusicLvl1,
		MusicLvl2,
		KPulvMetroid1,
		FridayLevel,
		DoggoLevel1,
		Challenge4,
		Santa1,
		Santa2,
		Santa3,
		MissileBoostLevel,
		FPS_hallway,
		JamLvl2,
		FrEd_Play,
		SwordArtOF,
		NC_4,
		NC_1,
		NC_2,
		NC_3,
		NC_5,
		NC_6,
		NC_7,
		TestingLevel,
		ZooLevel,
		TO_1,
		TO_2,
		TinyTut_StickyWalls,
		TR_1,
		ThreeDeeLevel,
		Tut_Stomp,
		OutroStory,
		MegaSatan1,
		TR_2,
		TR_3,
		TR_4,
		TR_5,
		SlimeDaddy_TentaclesGalore,
		SlimeDaddy_BatterUp,
		MerpsVillage,
		Transformation,
		MegaSatan2,
		FPS_LavaPits,
		FPS_Hallway2,
		FPS_Boss1,
		FPS_SlimeCity,
		FPS_IceCaves,
		ThreeDee_Spinning,
		PreSlimeDaddyStory,
		ThreeDee_Sewer,
		Warnings,
		HeartsOfFlesh,
		DerpyDragon,
		IsThisADaggerISeeBeforeMe,
		Alp0,
		Alp1,
		Alp2,
		Alp3,
		Alp4,
		Alp5,
		Alp6,
		Alp7,
		Alp8,
		Alp9,
		Alp10,
		Alp11,
		Alp12,
		AlpBoss,
		AlpSecret,
		AlpDLC_IntroStory,
		AlpDLC_OutroStory,
		PostSatanStory,
		End
	}

	public class Level
	{
		public Type type;
		public string strName;
		public bool unlocked = false;
		public int lowestDeaths = -1;
		public float lowestTime = -1;
	}

	public static void InitFreshLevelInfo()//Called on entering any level that displays levels, in FreshNode
	{
		levels = new List<Level>();

		//load each piece of possible info
		for (int i = 1; i < (int)Type.End; i++)
		{
			Level level = new Level();
			level.type = (Type)i;
			level.strName = GetStrNameForType((Type)i);
			level.unlocked = Fresh_Loading.LoadLevelUnlocked((Type)i);
			level.lowestDeaths = Fresh_Loading.LoadLevelDeaths((Type)i);
			level.lowestTime = Fresh_Loading.LoadLevelTime((Type)i);
			levels.Add(level);
		}
	}

	public static string GetStrNameForType(Type type)
	{
		switch (type)
		{
			case Type.CaveOfWonders1: return "SamuraiSword1";
			case Type.CaveOfWondersHell: return "SamuraiSword2";
			case Type.BablinAlong: return "Bablinalong";
			case Type.Pink1: return "Pink1";
			case Type.Pink2: return "Pink2";
			case Type.Blue1: return "Blue1";
			case Type.Blue2: return "Blue2";
			case Type.Challenge1: return "Ninja1";
			case Type.JamLvl1: return "JamLvl1";
			case Type.Tut_Village: return "Tut_Village1";
			case Type.Tut_Pink1: return "Tut_Pink1";
			case Type.Tut_Chill: return "Tut_Chill";
			case Type.Challenge2: return "Ninja2";
			case Type.Challenge3: return "Ninja3";
			case Type.Tut_JumpAndAirsword: return "Tut_SecondAttempt";
			case Type.Tut_PortalRules: return "TinyTut_PortalRulesSecondAttempt";
			case Type.Tut_DoubleJump: return "Tut_DoubleJump";
			case Type.GodOfDeath: return "GodOfDeath";
			case Type.JamLvl3: return "JamLvl3";
			case Type.IntroStory: return "IntroStory";
			case Type.UpwardsHell: return "UpwardsHell";
			case Type.FPS1: return "FPS_level1";
			case Type.MusicLvl1: return "MusicLvl1";
			case Type.MusicLvl2: return "MusicLvl2";
			case Type.KPulvMetroid1: return "KPulvMetroid1";
			case Type.FridayLevel: return "FridayLevel";
			case Type.DoggoLevel1: return "DoggoLevel1";
			case Type.Challenge4: return "SewerButt";
			case Type.Santa1: return "Santa1";
			case Type.Santa2: return "Santa2";
			case Type.Santa3: return "Santa3";
			case Type.MissileBoostLevel: return "MissileBoostLevel";
			case Type.FPS_hallway: return "FPS_hallway";
			case Type.JamLvl2: return "JamLvl2";
			case Type.FrEd_Play: return "FrEd_Play";
			case Type.SwordArtOF: return "SwordArtOF";
			case Type.NC_1: return "NC_1";
			case Type.NC_2: return "NC_2";
			case Type.NC_3: return "NC_3";
			case Type.NC_4: return "NC_4";
			case Type.NC_5: return "NC_5";
			case Type.NC_6: return "NC_6";
			case Type.NC_7: return "NC_7";
			case Type.TestingLevel: return "TestingLevel";
			case Type.ZooLevel: return "ZooLevel";
			case Type.TO_1: return "TO_1";
			case Type.TO_2: return "TO_2";
			case Type.TinyTut_StickyWalls: return "TinyTut_StickyWalls";
			case Type.TR_1: return "TR_1";
			case Type.ThreeDeeLevel: return "ThreeDeeLevel";
			case Type.Tut_Stomp: return "Tut_Stomp";
			case Type.OutroStory: return "OutroStory";
			case Type.MegaSatan1: return "MegaSatan1";
			case Type.TR_4: return "TR_4";
			case Type.TR_3: return "TR_3";
			case Type.TR_2: return "TR_2";
			case Type.TR_5: return "TR_5";
			case Type.SlimeDaddy_TentaclesGalore: return "SlimeDaddy_TentaclesGalore";
			case Type.SlimeDaddy_BatterUp: return "SlimeDaddy_BatterUp";
			case Type.MerpsVillage: return "MerpsVillage";
			case Type.Transformation: return "Transformation";
			case Type.MegaSatan2: return "MegaSatan2";
			case Type.FPS_LavaPits: return "FPS_LavaPits";
			case Type.FPS_Hallway2: return "FPS_Hallway2";
			case Type.FPS_Boss1: return "FPS_Boss1";
			case Type.FPS_SlimeCity: return "FPS_SlimeCity";
			case Type.FPS_IceCaves: return "FPS_IceCaves";
			case Type.ThreeDee_Spinning: return "ThreeDee_Spinning";
			case Type.ThreeDee_Sewer: return "ThreeDee_Sewer1";
			case Type.Warnings: return "Warnings";
			case Type.HeartsOfFlesh: return "HeartsOfFlesh";
			case Type.DerpyDragon: return "DerpyDragon";
			case Type.IsThisADaggerISeeBeforeMe: return "IsThisADaggerISeeBeforeMe";
			case Type.Alp0: return "0 - A Welcome Party";
			case Type.Alp1: return "1 - Steroid Balls";
			case Type.Alp2: return "2 - Shadow Walker";
			case Type.Alp3: return "3 - Come Closer";
			case Type.Alp4: return "4 - Chillstep";
			case Type.Alp5: return "5 - A Jumping Massacre";
			case Type.Alp6: return "6 - An Intermission";
			case Type.Alp7: return "7 - To The Skies";
			case Type.Alp8: return "8 - Open The Gates!";
			case Type.Alp9: return "9 - Twisty Cam";
			case Type.Alp10: return "10 - The Sky Whale";
			case Type.Alp11: return "11 - The Tower Gardens";
			case Type.Alp12: return "12 - The Tower Entrance";
			case Type.AlpBoss: return "Boss - The Shadow Of Hunger";
			case Type.AlpSecret: return "Secret - Alex's Love Letter";
			case Type.AlpDLC_IntroStory: return "AlpDLC_IntroStory";
			case Type.AlpDLC_OutroStory: return "AlpDLC_OutroStory";
			case Type.PostSatanStory: return "PostSatanStory";
		
		}
		return "";
	}

	public static Type GetTypeNameForStr(string str)
	{
		switch (str)
		{
			case "SamuraiSword1": return Type.CaveOfWonders1;
			case "SamuraiSword2": return Type.CaveOfWondersHell;
			case "Bablinalong": return Type.BablinAlong;
			case "Pink1": return Type.Pink1;
			case "Pink2": return Type.Pink2;
			case "Blue1": return Type.Blue1;
			case "Blue2": return Type.Blue2;
			case "Ninja1": return Type.Challenge1;
			case "JamLvl1": return Type.JamLvl1;
			case "Tut_Village1": return Type.Tut_Village;
			case "Tut_Pink1": return Type.Tut_Pink1;
			case "Tut_Chill": return Type.Tut_Chill;
			case "Ninja2": return Type.Challenge2;
			case "Ninja3": return Type.Challenge3;
			case "Tut_SecondAttempt": return Type.Tut_JumpAndAirsword;
			case "TinyTut_PortalRulesSecondAttempt": return Type.Tut_PortalRules;
			case "Tut_DoubleJump": return Type.Tut_DoubleJump;
			case "GodOfDeath": return Type.GodOfDeath;
			case "JamLvl3": return Type.JamLvl3;
			case "IntroStory": return Type.IntroStory;
			case "UpwardsHell": return Type.UpwardsHell;
			case "FPS_level1": return Type.FPS1;
			case "MusicLvl1": return Type.MusicLvl1;
			case "MusicLvl2": return Type.MusicLvl2;
			case "FridayLevel": return Type.FridayLevel;
			case "DoggoLevel1": return Type.DoggoLevel1;
			case "SewerButt": return Type.Challenge4;
			case "Santa1": return Type.Santa1;
			case "Santa2": return Type.Santa2;
			case "Santa3": return Type.Santa3;
			case "MissileBoostLevel": return Type.MissileBoostLevel;
			case "FPS_hallway": return Type.FPS_hallway;
			case "JamLvl2": return Type.JamLvl2;
			case "FrEd_Play": return Type.FrEd_Play;
			case "SwordArtOF": return Type.SwordArtOF;
			case "NC_1": return Type.NC_1;
			case "NC_2": return Type.NC_2;
			case "NC_3": return Type.NC_3;
			case "NC_4": return Type.NC_4;
			case "NC_5": return Type.NC_5;
			case "NC_6": return Type.NC_6;
			case "NC_7": return Type.NC_7;
			case "TestingLevel": return Type.TestingLevel;
			case "ZooLevel": return Type.ZooLevel;
			case "TO_1": return Type.TO_1;
			case "TO_2": return Type.TO_2;
			case "TinyTut_StickyWalls": return Type.TinyTut_StickyWalls;
			case "TR_1": return Type.TR_1;
			case "ThreeDeeLevel": return Type.ThreeDeeLevel;
			case "Tut_Stomp": return Type.Tut_Stomp;
			case "OutroStory": return Type.OutroStory;
			case "MegaSatan1": return Type.MegaSatan1;
			case "TR_4": return Type.TR_4;
			case "TR_3": return Type.TR_3;
			case "TR_2": return Type.TR_2;
			case "TR_5": return Type.TR_5;
			case "SlimeDaddy_TentaclesGalore": return Type.SlimeDaddy_TentaclesGalore;
			case "SlimeDaddy_BatterUp": return Type.SlimeDaddy_BatterUp;
			case "MerpsVillage": return Type.MerpsVillage;
			case "Transformation": return Type.Transformation;
			case "MegaSatan2": return Type.MegaSatan2;
			case "FPS_LavaPits": return Type.FPS_LavaPits;
			case "FPS_Hallway2": return Type.FPS_Hallway2;
			case "FPS_Boss1": return Type.FPS_Boss1;
			case "FPS_SlimeCity": return Type.FPS_SlimeCity;
			case "FPS_IceCaves": return Type.FPS_IceCaves;
			case "ThreeDee_Spinning": return Type.ThreeDee_Spinning;
			case "ThreeDee_Sewer1": return Type.ThreeDee_Sewer;
			case "Warnings": return Type.Warnings;
			case "HeartsOfFlesh": return Type.HeartsOfFlesh;
			case "DerpyDragon": return Type.DerpyDragon;
			case "IsThisADaggerISeeBeforeMe": return Type.IsThisADaggerISeeBeforeMe;
			case "0 - A Welcome Party": return Type.Alp0;
			case "1 - Steroid Balls": return Type.Alp1;
			case "2 - Shadow Walker": return Type.Alp2;
			case "3 - Come Closer": return Type.Alp3;
			case "4 - Chillstep": return Type.Alp4;
			case "5 - A Jumping Massacre": return Type.Alp5;
			case "6 - An Intermission": return Type.Alp6;
			case "7 - To The Skies": return Type.Alp7;
			case "8 - Open The Gates!": return Type.Alp8;
			case "9 - Twisty Cam": return Type.Alp9;
			case "10 - The Sky Whale": return Type.Alp10;
			case "11 - The Tower Gardens": return Type.Alp11;
			case "12 - The Tower Entrance": return Type.Alp12;
			case "Boss - The Shadow Of Hunger": return Type.AlpBoss;
			case "Boss - The Shadow of Hunger": return Type.AlpBoss;
			case "Secret - Alex's Love Letter": return Type.AlpSecret;
			case "AlpDLC_IntroStory": return Type.AlpDLC_IntroStory;
			case "AlpDLC_OutroStory": return Type.AlpDLC_OutroStory;
			case "PostSatanStory": return Type.PostSatanStory;

		}
		return Type.None;
	}

	public static float GetGoldTimeForLevel(Type type)
	{
		switch (type)
		{
			case Type.AlpDLC_IntroStory: return 45;
			case Type.Alp1: return 85;
			case Type.Alp2: return 50;
			case Type.Alp3: return 95;
			case Type.Alp4: return 70;
			case Type.Alp5: return 90;
			case Type.Alp6: return 17;
			case Type.Alp7: return 85;
			case Type.Alp8: return 50;
			case Type.Alp9: return 95;
			case Type.Alp10: return 95;
			case Type.Alp11: return 220;
			case Type.Alp12: return 32;
			case Type.AlpBoss: return 75;
			case Type.AlpSecret: return 35;

			case Type.FPS_LavaPits: return 55;
			case Type.FPS_Hallway2: return 55;
			case Type.FPS_Boss1: return 130;
			case Type.FPS_SlimeCity: return 110;
			case Type.FPS_IceCaves: return 60;

			//Set
			case Type.IntroStory: return 15;
			case Type.Tut_JumpAndAirsword: return 15;
			case Type.Tut_PortalRules: return 8;
			case Type.Tut_Chill: return 30;
			case Type.TinyTut_StickyWalls: return 7;
			case Type.TR_1: return 40;
			case Type.Tut_Stomp: return 12;
			case Type.Pink1: return 90;
			case Type.Blue1: return 35;
			case Type.Blue2: return 35;
			case Type.CaveOfWonders1: return 38;
			case Type.CaveOfWondersHell: return 55;
			case Type.Challenge1: return 30;
			case Type.Challenge2: return 30;
			case Type.Challenge3: return 27;
			case Type.Challenge4: return 15;
			case Type.FridayLevel: return 51;
			case Type.DerpyDragon: return 35;
			case Type.HeartsOfFlesh: return 55;
			case Type.IsThisADaggerISeeBeforeMe: return 29;
			case Type.JamLvl1: return 65;
			case Type.GodOfDeath: return 12;
			case Type.JamLvl3: return 45;
			case Type.JamLvl2: return 28;
			case Type.MegaSatan1: return 46;
			case Type.MegaSatan2: return 35;
			case Type.MerpsVillage: return 70;
			case Type.MissileBoostLevel: return 15;
			case Type.MusicLvl1: return 50;
			case Type.MusicLvl2: return 35;
			case Type.NC_1: return 80;
			case Type.NC_2: return 52;
			case Type.NC_3: return 62;
			case Type.NC_4: return 49;
			case Type.NC_5: return 77;
			case Type.NC_6: return 57;
			case Type.NC_7: return 56;
			case Type.Pink2: return 58;
			case Type.Santa1: return 46;
			case Type.Santa2: return 34;
			case Type.Santa3: return 42;
			case Type.SlimeDaddy_BatterUp: return 130;
			case Type.SlimeDaddy_TentaclesGalore: return 70;
			case Type.SwordArtOF: return 43;
			case Type.Tut_DoubleJump: return 45;
			case Type.UpwardsHell: return 99;
			case Type.DoggoLevel1: return 45;
			case Type.ThreeDee_Sewer: return 24;
			case Type.ThreeDee_Spinning: return 8;
			case Type.ThreeDeeLevel: return 16;
			case Type.TO_1: return 69;
			case Type.TO_2: return 64;
			case Type.TR_2: return 39;
			case Type.TR_3: return 21;
			case Type.TR_4: return 84;
			case Type.TR_5: return 55;
			case Type.Transformation: return 20;
			case Type.ZooLevel: return 45;
		}
		return 120;
	}

	public static float GetImpossibleTimeForLevel(Type type)
	{
		switch (type)
		{

			case Type.FPS_LavaPits: return 2;// 55;
			case Type.FPS_Hallway2: return 2;// 55;
			case Type.FPS_Boss1: return 2;// 130;
			case Type.FPS_SlimeCity: return 2;// 110;
			case Type.FPS_IceCaves: return 2;// 60;

			//Set
			case Type.IntroStory: return 2;// 15;
			case Type.Tut_JumpAndAirsword: return 2;// 15;
			case Type.Tut_PortalRules: return 2;// 8;
			case Type.Tut_Chill: return 2;// 30;
			case Type.TinyTut_StickyWalls: return 2;// 7;
			case Type.TR_1: return 2;// 40;
			case Type.Tut_Stomp: return 2;// 12;
			case Type.Pink1: return 2;// 90;
			case Type.Blue1: return 2;// 35;
			case Type.Blue2: return 2;// 35;
			case Type.CaveOfWonders1: return 2;// 38;
			case Type.CaveOfWondersHell: return 2;// 55;
			case Type.Challenge1: return 2;// 30;
			case Type.Challenge2: return 2;// 30;
			case Type.Challenge3: return 2;// 27;
			case Type.Challenge4: return 2;// 15;
			case Type.FridayLevel: return 2;// 51;
			case Type.DerpyDragon: return 2;// 35;
			case Type.HeartsOfFlesh: return 2;// 55;
			case Type.IsThisADaggerISeeBeforeMe: return 2;// 29;
			case Type.JamLvl1: return 2;// 65;
			case Type.GodOfDeath: return 2;// 12;
			case Type.JamLvl3: return 2;// 45;
			case Type.JamLvl2: return 2;// 28;
			case Type.MegaSatan1: return 2;// 46;
			case Type.MegaSatan2: return 2;// 35;
			case Type.MerpsVillage: return 2;// 70;
			case Type.MissileBoostLevel: return 2;// 15;
			case Type.MusicLvl1: return 2;// 50;
			case Type.MusicLvl2: return 2;// 35;
			case Type.NC_1: return 2;// 80;
			case Type.NC_2: return 2;// 52;
			case Type.NC_3: return 2;// 62;
			case Type.NC_4: return 2;// 49;
			case Type.NC_5: return 2;// 77;
			case Type.NC_6: return 2;// 57;
			case Type.NC_7: return 2;// 56;
			case Type.Pink2: return 2;// 58;
			case Type.Santa1: return 2;// 46;
			case Type.Santa2: return 2;// 34;
			case Type.Santa3: return 2;// 42;
			case Type.SlimeDaddy_BatterUp: return 2;// 130;
			case Type.SlimeDaddy_TentaclesGalore: return 2;// 70;
			case Type.SwordArtOF: return 2;// 43;
			case Type.Tut_DoubleJump: return 2;// 45;
			case Type.UpwardsHell: return 2;// 99;
			case Type.DoggoLevel1: return 2;// 45;
			case Type.ThreeDee_Sewer: return 2;// 24;
			case Type.ThreeDee_Spinning: return 2;// 8;
			case Type.ThreeDeeLevel: return 2;// 16;
			case Type.TO_1: return 2;// 69;
			case Type.TO_2: return 2;// 64;
			case Type.TR_2: return 2;// 39;
			case Type.TR_3: return 2;// 21;
			case Type.TR_4: return 2;// 84;
			case Type.TR_5: return 2;// 55;
			case Type.Transformation: return 2;// 20;
			case Type.ZooLevel: return 2;// 45;
		}
		return 1;
	}


	public static bool UseBlackTextLevel(Type type)
	{
		switch (type)
		{
			case Type.CaveOfWonders1: return true;
			case Type.ThreeDeeLevel: return true;
			case Type.FPS_IceCaves: return true;
			case Type.MerpsVillage: return true;
		}

		return false;
	}

	public static bool DoNotSyncLevel(Type type)
	{
		switch (type)
		{
			case Type.Tut_Chill: return true;
			case Type.Tut_DoubleJump: return true;
			case Type.FPS_Hallway2: return true;
			case Type.ThreeDeeLevel: return true;
			case Type.UpwardsHell: return true;
		}

		return false;
	}

	public static bool NotALeaderboardLevel(Type type)//normally because it's autoscrolling or a cutscene
	{
		switch (type)
		{
			case Type.SlimeDaddy_BatterUp: return true;
			case Type.Transformation: return true;
			case Type.NC_5: return true;
		}

		return false;
	}


	public static bool IsFPSLevel(string str)
	{
		switch (str)
		{
			case "FPS_SlimeCity": return true;
			case "FPS_IceCaves": return true;
			case "FPS_LavaPits": return true;
			case "FPS_Hallway2": return true;
			case "FPS_Hallway": return true;
			case "FPS_Boss1": return true;
			case "FPS_TestLevel": return true;
			case "FPS_TestLevel2": return true;

		}

		return false;
	}
	public static bool IsGameplayLevel(string str)
	{
		switch (str)
		{
			case "level0": return false;
			case "MegaMetaWorld": return false;
			case "ESJ2Title": return false;
			case "IntroStory": return false;
			case "StartMenu": return false;
			case "FrEd_Layout": return false;
			case "FrEd_Play": return false;
			case "PostSatanStory": return false;
			case "PreSlimeDaddyStory": return false;
			case "Warnings": return false;


		}

		return true;

	}



	public static int GetIndexForType(Type type)
	{
		for (int i = 0; i < levels.Count; i++)
		{
			if (levels[i].type == type) { return i; }
		}
		return -1;
	}

	public static Type GetTypeOfCurrentLevel()
	{
		return GetTypeNameForStr(SceneManager.GetActiveScene().name);
	}

	public static Type GetWeeklyLevel()
	{
		int day = System.DateTime.Today.DayOfYear;
		int week = Mathf.RoundToInt(day / 7);
		if (week == 0) { return Type.NC_3; }
		if (week == 1) { return Type.CaveOfWonders1; }
		if (week == 2) { return Type.TO_1; }
		if (week == 3) { return Type.MusicLvl2; }
		if (week == 4) { return Type.SlimeDaddy_TentaclesGalore; }
		if (week == 5) { return Type.Santa3; }
		if (week == 6) { return Type.MerpsVillage; }
		if (week == 7) { return Type.ZooLevel; }
		if (week == 8) { return Type.MusicLvl1; }
		if (week == 9) { return Type.SlimeDaddy_TentaclesGalore; }
		if (week == 10) { return Type.GodOfDeath; }
		if (week == 11) { return Type.NC_6; }
		if (week == 12) { return Type.Pink1; }
		if (week == 13) { return Type.Blue2; }
		if (week == 14) { return Type.TO_1; }
		if (week == 15) { return Type.TR_1; }
		if (week == 16) { return Type.MusicLvl2; }
		if (week == 17) { return Type.NC_3; }
		if (week == 18) { return Type.SlimeDaddy_TentaclesGalore; }
		if (week == 19) { return Type.UpwardsHell; }
		if (week == 20) { return Type.Santa1; }
		if (week == 21) { return Type.TR_3; }
		if (week == 22) { return Type.Blue2; }
		if (week == 23) { return Type.JamLvl3; }
		if (week == 24) { return Type.TR_2; }
		if (week == 25) { return Type.NC_3; }
		if (week == 26) { return Type.JamLvl2; }
		if (week == 27) { return Type.NC_6; }
		if (week == 28) { return Type.TO_2; }
		if (week == 29) { return Type.MusicLvl1; }
		if (week == 30) { return Type.DoggoLevel1; }
		if (week == 31) { return Type.Blue2; }
		if (week == 32) { return Type.Pink1; }
		if (week == 33) { return Type.Challenge2; }
		if (week == 34) { return Type.MerpsVillage; }
		if (week == 35) { return Type.Challenge2; }
		if (week == 36) { return Type.NC_7; }
		if (week == 37) { return Type.Blue1; }
		if (week == 38) { return Type.Challenge1; }
		if (week == 39) { return Type.FridayLevel; }
		if (week == 40) { return Type.JamLvl1; }
		if (week == 41) { return Type.Pink2; }
		if (week == 42) { return Type.NC_3; }
		if (week == 43) { return Type.Blue2; }
		if (week == 44) { return Type.NC_2; }
		if (week == 45) { return Type.GodOfDeath; }
		if (week == 46) { return Type.Santa3; }
		if (week == 47) { return Type.Tut_Chill; }
		if (week == 48) { return Type.Challenge4; }
		if (week == 49) { return Type.FridayLevel; }
		if (week == 50) { return Type.Tut_Pink1; }
		if (week == 51) { return Type.Santa2; }
		if (week == 52) { return Type.MusicLvl2; }
		if (week == 53) { return Type.ZooLevel; }

		return FreshLevels.Type.JamLvl2;
	}

	public static int totalIndexs = 59;//total leaderboards, 0-(total minus 1)
	public static Type IndexForLevelLeaderboard(int index)//for leaderboards, doesn't include levels with no leaderboard
	{
		switch (index)
		{
			case 0: return Type.Tut_JumpAndAirsword;
			case 1: return Type.Tut_PortalRules;
			case 2: return Type.Tut_Chill;
			case 3: return Type.TinyTut_StickyWalls;
			case 4: return Type.TR_1;
			case 5: return Type.Tut_Stomp;
			case 6: return Type.Pink1;
			case 7: return Type.ThreeDeeLevel;
			case 8: return Type.ThreeDee_Spinning;
			case 9: return Type.ThreeDee_Sewer;
			case 10: return Type.CaveOfWonders1;
			case 11: return Type.Blue1;
			case 12: return Type.MegaSatan1;
			case 13: return Type.MusicLvl1;
			case 14: return Type.TO_1;
			case 15: return Type.FPS_Hallway2;
			case 16: return Type.FPS_LavaPits;
			case 17: return Type.FPS_IceCaves;
			case 18: return Type.FPS_SlimeCity;
			case 19: return Type.FPS_Boss1;
			case 20: return Type.Tut_DoubleJump;
			case 21: return Type.GodOfDeath;
			case 22: return Type.MerpsVillage;
			case 23: return Type.NC_6;
			case 24: return Type.FridayLevel;
			case 25: return Type.Challenge3;
			case 26: return Type.MissileBoostLevel;
			case 27: return Type.MusicLvl2;
			case 28: return Type.NC_1;
			case 29: return Type.SlimeDaddy_TentaclesGalore;
			case 30: return Type.Challenge1;
			case 31: return Type.NC_4;
			case 32: return Type.NC_3;
			case 33: return Type.TO_2;
			case 34: return Type.CaveOfWondersHell;
			case 35: return Type.NC_5;
			case 36: return Type.DoggoLevel1;
			case 37: return Type.ZooLevel;
			case 38: return Type.JamLvl2;
			case 39: return Type.Santa3;
			case 40: return Type.Santa2;
			case 41: return Type.Challenge2;
			case 42: return Type.JamLvl3;
			case 43: return Type.Santa1;
			case 44: return Type.TR_2;
			case 45: return Type.Pink2;
			case 46: return Type.Blue2;
			case 47: return Type.NC_7;
			case 48: return Type.Challenge4;
			case 49: return Type.NC_2;
			case 50: return Type.JamLvl1;
			case 51: return Type.UpwardsHell;
			case 52: return Type.TR_3;
			case 53: return Type.SwordArtOF;
			case 54: return Type.TR_4;
			case 55: return Type.MegaSatan2;
			case 56: return Type.IsThisADaggerISeeBeforeMe;
			case 57: return Type.HeartsOfFlesh;
			case 58: return Type.DerpyDragon;
		}

		return FreshLevels.Type.JamLvl2;
	}

	public static int LevelForIndexLeaderboard(Type level)//for every leaderboard
	{
		switch (level)
		{

			case Type.Tut_JumpAndAirsword: return 0;
			case Type.Tut_PortalRules: return 1;
			case Type.Tut_Chill: return 2;
			case Type.TinyTut_StickyWalls: return 3;
			case Type.TR_1: return 4;
			case Type.Tut_Stomp: return 5;
			case Type.Pink1: return 6;
			case Type.ThreeDeeLevel: return 7;
			case Type.ThreeDee_Spinning: return 8;
			case Type.ThreeDee_Sewer: return 9;
			case Type.CaveOfWonders1: return 10;
			case Type.Blue1: return 11;
			case Type.MegaSatan1: return 12;
			case Type.MusicLvl1: return 13;
			case Type.TO_1: return 14;
			case Type.FPS_Hallway2: return 15;
			case Type.FPS_LavaPits: return 16;
			case Type.FPS_IceCaves: return 17;
			case Type.FPS_SlimeCity: return 18;
			case Type.FPS_Boss1: return 19;
			case Type.Tut_DoubleJump: return 20;
			case Type.GodOfDeath: return 21;
			case Type.MerpsVillage: return 22;
			case Type.NC_6: return 23;
			case Type.FridayLevel: return 24;
			case Type.Challenge3: return 25;
			case Type.MissileBoostLevel: return 26;
			case Type.MusicLvl2: return 27;
			case Type.NC_1: return 28;
			case Type.SlimeDaddy_TentaclesGalore: return 29;
			case Type.Challenge1: return 30;
			case Type.NC_4: return 31;
			case Type.NC_3: return 32;
			case Type.TO_2: return 33;
			case Type.CaveOfWondersHell: return 34;
			case Type.NC_5: return 35;
			case Type.DoggoLevel1: return 36;
			case Type.ZooLevel: return 37;
			case Type.JamLvl2: return 38;
			case Type.Santa3: return 39;
			case Type.Santa2: return 40;
			case Type.Challenge2: return 41;
			case Type.JamLvl3: return 42;
			case Type.Santa1: return 43;
			case Type.TR_2: return 44;
			case Type.Pink2: return 45;
			case Type.Blue2: return 46;
			case Type.NC_7: return 47;
			case Type.Challenge4: return 48;
			case Type.NC_2: return 49;
			case Type.JamLvl1: return 50;
			case Type.UpwardsHell: return 51;
			case Type.TR_3: return 52;
			case Type.SwordArtOF: return 53;
			case Type.TR_4: return 54;
			case Type.MegaSatan2: return 55;
			case Type.IsThisADaggerISeeBeforeMe: return 56;
			case Type.HeartsOfFlesh: return 57;
			case Type.DerpyDragon: return 58;


		}

		return 0;
	}

	public static string GetStrLabelForType(Type type)
	{
		switch (type)
		{
			case Type.Tut_JumpAndAirsword: return "Dark Bargain";
			case Type.CaveOfWonders1: return "Cave of Wonders";
			case Type.CaveOfWondersHell: return "Hell Cave";
			case Type.Pink1: return "Very Pink";
			case Type.Pink2: return "Back to Bravo";
			case Type.Blue1: return "Upward Bound";
			case Type.Blue2: return "Sticky Remains";
			case Type.Challenge1: return "Golden Butt: Circus Tent";
			case Type.JamLvl1: return "Sticky Situation";
			case Type.Tut_Chill: return "Chill Cave";
			case Type.Challenge2: return "Golden Butt: Seb's Trick";
			case Type.Challenge3: return "Golden Butt: Speed of Light";
			case Type.Tut_PortalRules: return "Between Portals";
			case Type.GodOfDeath: return "Pest Control";
			case Type.JamLvl3: return "Closing Time";
			case Type.UpwardsHell: return "Upwards Hell";
			case Type.MusicLvl1: return "Flowing Dark";
			case Type.MusicLvl2: return "Minivania";
			case Type.FridayLevel: return "Groove Nights";
			case Type.DoggoLevel1: return "Bolting down!";
			case Type.Challenge4: return "Golden Butt: Sewers";
			case Type.Santa3: return "Santa's Slay";
			case Type.Santa2: return "Santa's Factory";
			case Type.Santa1: return "Final Snowdown";
			case Type.MissileBoostLevel: return "Slime Factory";
			case Type.JamLvl2: return "To the North Pole!";
			case Type.SwordArtOF: return "Art Of The Sword";
			case Type.NC_1: return "The Deep Sewers";
			case Type.NC_2: return "Slimy Customers";
			case Type.NC_3: return "Slime Surfing";
			case Type.NC_4: return "Slime Guardians";
			case Type.NC_5: return "I've Got A Crush On You";
			case Type.NC_6: return "A Stomp In The Dark";
			case Type.NC_7: return "Purple Depths!";
			case Type.ZooLevel: return "Tinyvania";
			case Type.TO_1: return "Hang Loose";
			case Type.TO_2: return "Sticky Ends";
			case Type.TinyTut_StickyWalls: return "Sticky Lessons";
			case Type.TR_1: return "Happy Pink";
			case Type.ThreeDeeLevel: return "Glitching Out!";
			case Type.ThreeDee_Spinning: return "Trippy Bits!";
			case Type.ThreeDee_Sewer: return "Acid Sewers!";
			case Type.Tut_DoubleJump: return "Groovin' Again";
			case Type.Tut_Stomp: return "Smashing Job";
			case Type.MegaSatan1: return "Mega-Satan";
			case Type.TR_4: return "Deja Vu";
			case Type.TR_3: return "Golden Butt: Brutal Butt";
			case Type.TR_2: return "Flaming Hell";
			case Type.TR_5: return "Box of Snakes";
			case Type.SlimeDaddy_TentaclesGalore: return "Tentacles Galore!";
			case Type.SlimeDaddy_BatterUp: return "Batter Up!";
			case Type.MerpsVillage: return "Oh Dearie Me";
			case Type.MegaSatan2: return "MegaSatan's Playground";
			case Type.HeartsOfFlesh: return "DLC: Hearts Of Flesh";
			case Type.DerpyDragon: return "DLC: Derpy Dragon";
			case Type.IsThisADaggerISeeBeforeMe: return "DLC: Sky Dagger";



			case Type.FPS_Hallway2: return "Doomed";
			case Type.FPS_LavaPits: return "Lava Pits";
			case Type.FPS_IceCaves: return "Ice Caves";
			case Type.FPS_SlimeCity: return "Slime City";
			case Type.FPS_Boss1: return "Abode of Flame";


			case Type.Alp0: return "A Welcome Party";
			case Type.Alp1: return "Steroid Balls";
			case Type.Alp2: return "Shadow Walker";
			case Type.Alp3: return "Come Closer";
			case Type.Alp4: return "Chillstep";
			case Type.Alp5: return "A Jumping Massacre";
			case Type.Alp6: return "An Intermission";
			case Type.Alp7: return "To The Skies";
			case Type.Alp8: return "Open The Gates!";
			case Type.Alp9: return "Twisty Cam";
			case Type.Alp10: return "The Sky Whale";
			case Type.Alp11: return "The Tower Gardens";
			case Type.Alp12: return "The Tower Entrance";
			case Type.AlpBoss: return "The Shadow Of Hunger";
			case Type.AlpSecret: return "Alex's Love Letter";
			case Type.PostSatanStory: return "Post-Satan Story";

		}
		return "NULL";
	}


	public static string GetStrictLabelForType(Type type)//no puncuation, colons, commas, etc, Of To The Are all uppercase
	{
		switch (type)
		{
			case Type.Tut_JumpAndAirsword: return "Dark Bargain";
			case Type.CaveOfWonders1: return "Cave Of Wonders";
			case Type.CaveOfWondersHell: return "Hell Cave";
			case Type.Pink1: return "Very Pink";
			case Type.Pink2: return "Back To Bravo";
			case Type.Blue1: return "Upward Bound";
			case Type.Blue2: return "Sticky Remains";
			case Type.Challenge1: return "Circus Tent";
			case Type.JamLvl1: return "Sticky Situation";
			case Type.Tut_Chill: return "Chill Cave";
			case Type.Challenge2: return "Sebs Trick";
			case Type.Challenge3: return "Speed Of Light";
			case Type.Tut_PortalRules: return "Between Portals";
			case Type.GodOfDeath: return "Pest Control";
			case Type.JamLvl3: return "Closing Time";
			case Type.UpwardsHell: return "Upwards Hell";
			case Type.MusicLvl1: return "Flowing Dark";
			case Type.MusicLvl2: return "Minivania";
			case Type.FridayLevel: return "Groove Nights";
			case Type.DoggoLevel1: return "Bolting down";
			case Type.Challenge4: return "Sewers";
			case Type.Santa3: return "Santas Slay";
			case Type.Santa2: return "Santas Factory";
			case Type.Santa1: return "Final Snowdown";
			case Type.MissileBoostLevel: return "Slime Factory";
			case Type.JamLvl2: return "To The North Pole";
			case Type.SwordArtOF: return "Art Of The Sword";
			case Type.NC_1: return "The Deep Sewers";
			case Type.NC_2: return "Slimy Customers";
			case Type.NC_3: return "Slime Surfing";
			case Type.NC_4: return "Slime Guardians";
			case Type.NC_5: return "Ive Got A Crush On You";
			case Type.NC_6: return "A Stomp In The Dark";
			case Type.NC_7: return "Purple Depths";
			case Type.ZooLevel: return "Tinyvania";
			case Type.TO_1: return "Hang Loose";
			case Type.TO_2: return "Sticky Ends";
			case Type.TinyTut_StickyWalls: return "Sticky Lessons";
			case Type.TR_1: return "Happy Pink";
			case Type.ThreeDeeLevel: return "Glitching Out";
			case Type.ThreeDee_Spinning: return "Trippy Bits";
			case Type.ThreeDee_Sewer: return "Acid Sewers";
			case Type.Tut_DoubleJump: return "Groovin Again";
			case Type.Tut_Stomp: return "Smashing Job";
			case Type.MegaSatan1: return "MegaSatan";
			case Type.TR_4: return "Deja Vu";
			case Type.TR_3: return "Brutal Butt";
			case Type.TR_2: return "Flaming Hell";
			case Type.TR_5: return "Box Of Snakes";
			case Type.SlimeDaddy_TentaclesGalore: return "Tentacles Galore";
			case Type.SlimeDaddy_BatterUp: return "Batter Up";
			case Type.MerpsVillage: return "Oh Dearie Me";
			case Type.MegaSatan2: return "MegaSatans Playground";
			case Type.HeartsOfFlesh: return "Hearts Of Flesh";
			case Type.DerpyDragon: return "Derpy Dragon";
			case Type.IsThisADaggerISeeBeforeMe: return "Sky Dagger";
				
			case Type.FPS_Hallway2: return "Doomed";
			case Type.FPS_LavaPits: return "Lava Pits";
			case Type.FPS_IceCaves: return "Ice Caves";
			case Type.FPS_SlimeCity: return "Slime City";
			case Type.FPS_Boss1: return "Abode Of Flame";
				
			case Type.Alp0: return "A Welcome Party";
			case Type.Alp1: return "Steroid Balls";
			case Type.Alp2: return "Shadow Walker";
			case Type.Alp3: return "Come Closer";
			case Type.Alp4: return "Chillstep";
			case Type.Alp5: return "A Jumping Massacre";
			case Type.Alp6: return "An Intermission";
			case Type.Alp7: return "To The Skies";
			case Type.Alp8: return "Open The Gates";
			case Type.Alp9: return "Twisty Cam";
			case Type.Alp10: return "The Sky Whale";
			case Type.Alp11: return "The Tower Gardens";
			case Type.Alp12: return "The Tower Entrance";
			case Type.AlpBoss: return "The Shadow Of Hunger";
			case Type.AlpSecret: return "Alexs Love Letter";
			case Type.PostSatanStory: return "Post Satan Story";

		}
		return "NULL";
	}

	
	public static int GetLvlNumForType(Type type)//backwards compaitable thing. Used in UnlockSystemScript.cs
	{
		switch (type)
		{
			case Type.Tut_JumpAndAirsword: return 10;
			case Type.Tut_PortalRules: return 20;
			case Type.Tut_Chill: return 30;
			case Type.TinyTut_StickyWalls: return 40;
			case Type.TR_1: return 50;
			case Type.Tut_Stomp: return 60;
			case Type.Pink1: return 70;
			case Type.ThreeDeeLevel: return 80;
			case Type.CaveOfWonders1: return 90;
			case Type.Blue1: return 100;
			case Type.MegaSatan1: return 110;

			case Type.MusicLvl1: return 120;
			case Type.TO_1: return 130;
			case Type.FPS_Hallway2: return 140;
			case Type.Tut_DoubleJump: return 150;
			case Type.GodOfDeath: return 160;
			case Type.MerpsVillage: return 170;
			case Type.NC_6: return 180;
			case Type.FridayLevel: return 190;
			case Type.MissileBoostLevel: return 200;
			case Type.MusicLvl2: return 210;
			case Type.NC_1: return 220;
			case Type.SlimeDaddy_TentaclesGalore: return 230;
			case Type.SlimeDaddy_BatterUp: return 240;
			case Type.NC_4: return 250;
			case Type.NC_3: return 260;
				
			case Type.TO_2: return 270;
			case Type.CaveOfWondersHell: return 280;
			case Type.NC_5: return 290;
			case Type.DoggoLevel1: return 300;
			case Type.ZooLevel: return 310;
			case Type.JamLvl2: return 320;
			case Type.Santa3: return 330;
			case Type.Santa2: return 340;
			case Type.JamLvl3: return 350;
			case Type.Santa1: return 360;
			case Type.Transformation: return 370;
				
			case Type.TR_2: return 380;
			case Type.Pink2: return 390;
			case Type.Blue2: return 400;
			case Type.TR_5: return 410;
			case Type.NC_7: return 420;
			case Type.NC_2: return 430;
			case Type.JamLvl1: return 440;
			case Type.UpwardsHell: return 450;
			case Type.SwordArtOF: return 460;
			case Type.TR_4: return 470;
			case Type.MegaSatan2: return 480;
				
		}
		return 999;
	}
}
