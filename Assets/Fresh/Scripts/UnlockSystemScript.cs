using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSystemScript : MonoBehaviour
{
	public static void UnlockThisLevel(string level)
	{
		if (level == "StartMenu") { return; }
		if (level == "ESJ2Title") { return; }
		if (level == "MegaMetaWorld") { return; }

		FreshLevels.Type t = FreshLevels.GetTypeNameForStr(level);
		if (t == FreshLevels.Type.None) { Debug.Log("Error: string " + level + " returned type .None in GetTypeNameForStr. Level not unlocked!"); return; }
		UnlockThisLevel(t);
	}

	public static void UnlockThisLevel(FreshLevels.Type type)
	{
		UnlockThisLevel(type, false);
	}
	public static void UnlockThisLevel(FreshLevels.Type type, bool noSave)
	{
		if (type == FreshLevels.Type.None)
		{
			//Debug.Log("Error: Level type was None. Level not unlocked!");
			return;
		}

		//special cases (when you go to these text intros, also unlock the next level, in case the player quits during the story)
		if (type == FreshLevels.Type.PreSlimeDaddyStory) { type = FreshLevels.Type.SlimeDaddy_BatterUp; }
		if (type == FreshLevels.Type.PostSatanStory) { type = FreshLevels.Type.MusicLvl1; }

		string strictLabel = FreshLevels.GetStrictLabelForType(type);
		if (strictLabel == null)
		{
			//Debug.Log("Error: Level name was NULL. Level not unlocked!");
			return;
		}
		PlayerPrefs.SetInt("nu_" + strictLabel, 1);
		if (!noSave) { PlayerPrefs.Save(); }

		//Debug.Log("Unlocked level. " + type + ", " + strictLabel);
	}

	public static int BackwardsCompat_Index = 0;
	public static bool BackwardsCompat_MiscFlag = false;

	public static void BackwardsCompat_Misc()
	{
		if (BackwardsCompat_MiscFlag) { return; }

		int r = PlayerPrefs.GetInt("doneBackCompat2", 0);
		if (r == 1) { BackwardsCompat_MiscFlag = true; return; }

		//check some of the side levels, see if you've played them

		FreshLevels.Type t = FreshLevels.Type.None;
		for (int i = 0; i <= 11; i++)
		{
			switch (i)
			{
				case 0: t = FreshLevels.Type.Challenge1; break;
				case 1: t = FreshLevels.Type.Challenge2; break;
				case 2: t = FreshLevels.Type.Challenge3; break;
				case 3: t = FreshLevels.Type.Challenge4; break;
				case 4: t = FreshLevels.Type.TR_3; break;
				case 5: t = FreshLevels.Type.FPS_Hallway2; break;
				case 6: t = FreshLevels.Type.FPS_LavaPits; break;
				case 7: t = FreshLevels.Type.FPS_IceCaves; break;
				case 8: t = FreshLevels.Type.FPS_SlimeCity; break;
				case 9: t = FreshLevels.Type.FPS_Boss1; break;
				case 10: t = FreshLevels.Type.ThreeDee_Sewer; break;
				case 11: t = FreshLevels.Type.ThreeDee_Spinning; break;
			}

			if (PlayerPrefs.GetInt("LevelDeaths_" + t, -1) > -1) { UnlockThisLevel(t, true); }
		}


		PlayerPrefs.SetInt("doneBackCompat2", 1);
		PlayerPrefs.Save();
	}

	public static void BackwardsCompat_LvlNumCheck()
	{
		if (BackwardsCompat_Index == 999) { return; }

		int r = PlayerPrefs.GetInt("doneBackCompat1", 0);
		if (r == 1) { BackwardsCompat_Index = 999; return; }
		int lvlnum = PlayerPrefs.GetInt("lvlNum", 0);
		int index = BackwardsCompat_Index;
		FreshLevels.Type t = (FreshLevels.Type)index;
		if (t == FreshLevels.Type.End)
		{
			BackwardsCompat_Index = 999;

			PlayerPrefs.SetInt("doneBackCompat1", 1);
			return;
		}

		if (lvlnum >= FreshLevels.GetLvlNumForType(t))
		{
			UnlockThisLevel(t, true);
		}
		if (BackwardsCompat_Index == 50) { PlayerPrefs.Save(); }//Only save every so often. Don't need to save every frame.
		if (BackwardsCompat_Index == 100) { PlayerPrefs.Save(); }
		if (BackwardsCompat_Index == 150) { PlayerPrefs.Save(); }
		if (BackwardsCompat_Index == 200) { PlayerPrefs.Save(); }
		if (BackwardsCompat_Index == 250) { PlayerPrefs.Save(); }
		if (BackwardsCompat_Index == 300) { PlayerPrefs.Save(); }
		if (BackwardsCompat_Index == 500) { PlayerPrefs.Save(); }
		BackwardsCompat_Index++;
	}
}
