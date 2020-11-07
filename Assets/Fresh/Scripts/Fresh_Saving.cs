using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;
using System.Text;
using UnityEngine.SceneManagement;

public class Fresh_Saving : MonoBehaviour
{

	public static void SaveLocalAchivos()
	{
		int[] bs = new int[AchivoFuncs.myAchivos.Length];
		for (int i = 0; i < bs.Length; i++)
		{
			if (AchivoFuncs.myAchivos[i]) { bs[i] = 1; }
			else { bs[i] = 0; }

			PlayerPrefs.SetInt("localAchivos_" + i, bs[i]);
		}
		PlayerPrefs.Save();
	}

	public static void SaveLocalGhostAttempt(List<Ghosts.GhostFrame> frames)//This saves the local player's current attempt on this level.
	{
		string str = Ghosts.FramesToString(frames);
		Debug.Log("saving local ghost attempt: " + str);
		PlayerPrefs.SetString("localGhostAttempt_" + SceneManager.GetActiveScene().name, str);
		PlayerPrefs.Save();
	}

	public static void SaveUsernameAndPw(string username, string pwhash)
	{
		PlayerPrefs.SetString("savedUsername", username);
		PlayerPrefs.SetString("savedPwHash", pwhash);

		PlayerPrefs.Save();
	}

	public static void SaveLvlNum()
	{
		//defunct
	//	PlayerPrefs.SetInt("lvlNum", NodeController.lvlNum);
	//	PlayerPrefs.Save();
	}

	public static void SavePGMode()
	{
		SetBool("pgmode", xa.pgMode);
		PlayerPrefs.Save();
	}

	public static void SaveSendUsageStats(bool state)
	{
		if (state)
		{
			PlayerPrefs.SetInt("sendUsageStats", 1);
		}
		else
		{
			PlayerPrefs.SetInt("sendUsageStats", 0);
		}

		PlayerPrefs.Save();
	}

	public static void SaveControlsMode(bool state)
	{
		SetBool("controlsMode", state);
		PlayerPrefs.Save();
	}

	public static void SaveRememberMe(bool rememberMe)
	{
		if (rememberMe)
		{
			PlayerPrefs.SetInt("rememberMe", 1);
		}
		else
		{
			PlayerPrefs.SetInt("rememberMe", 0);
		}

		PlayerPrefs.Save();
	}

	public static void SaveCustomControls()
	{
		for (int i = 0; i < Controls.controls.Count; i++)
		{
			//Debug.Log("Saving: Controls_" + Controls.controls[i].type + ": " + Controls.controls[i].keyInt);

			PlayerPrefs.SetInt("MCon_Saved_" + Controls.controls[i].player + "_" + Controls.controls[i].type, 1);
			PlayerPrefs.SetInt("MCon_KeyInt_" + Controls.controls[i].player + "_" + Controls.controls[i].type, Controls.controls[i].keyInt);
			PlayerPrefs.SetInt("MCon_JoyNum_" + Controls.controls[i].player + "_" + Controls.controls[i].type, Controls.controls[i].joyNum);
			PlayerPrefs.SetInt("MCon_AxisNum_" + Controls.controls[i].player + "_" + Controls.controls[i].type, Controls.controls[i].axisNum);
			SetBool("MCon_AxisDir_" + Controls.controls[i].player + "_" + Controls.controls[i].type, Controls.controls[i].posAxis);
		}
		Debug.Log("Saving!");
		PlayerPrefs.Save();
	}

	public static void SaveLevelTime(FreshLevels.Type type, float levelTime)
	{
		if (fa.cheater) { return; }
		//RemoteData.CallReportLevelTime(type,levelTime);

		float previousPB = -1;
		if (PlayerPrefs.HasKey("LevelTime_" + type))
		{
			previousPB = PlayerPrefs.GetFloat("LevelTime_" + type, -1);
		}

		if (previousPB < 0 || previousPB > levelTime)
		{
			PlayerPrefs.SetFloat("LevelTime_" + type, levelTime);
			PlayerPrefs.Save();
		}
	}

	public static void SaveFancyLevel()
	{
		//Debug.Log("Saved fancy level: " + fa.fancyLevel);
		PlayerPrefs.SetInt("fancy", (int)fa.fancyLevel);
		PlayerPrefs.Save();
	}

	public static void SaveLevelDeaths(FreshLevels.Type type, int deaths)
	{
		if (fa.cheater) { return; }
		int previous = -1;
		previous = PlayerPrefs.GetInt("LevelDeaths_" + type, -1);

		if (previous <= -1 || previous > deaths)
		{
			PlayerPrefs.SetInt("LevelDeaths_" + type, deaths);
			PlayerPrefs.Save();
		}
	}

	public static void SetBool(string name, bool value)
	{
		int fakeBool = 0;
		if (value) fakeBool = 1;
		PlayerPrefs.SetInt(name, fakeBool);
	}
}
