using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


public class RawFuncs : MonoBehaviour
{
	////
	//The main local & static hub for the raw menu
	////

	public static RawFuncs self;

	public static bool InRawMenu = false;
	public static bool RawMenuHandover = false;
	public static bool sendUsageStats = true;
	public static float cursorZ = 2;
	public static bool inOptionsFromInGame = false;

	public static float QCUpdateTimeSet = 0;
	public static float QCUpdateTimeDelay = 1;
	public static float QCAskTimeSet = 0;
	public static float QCAskDelay = 2;
	public static FreshLevels.Type QCAskType = FreshLevels.Type.None;
	public static int mainMenuLeaderboardIndex = -1;
	public static int LeaderboardItemIndex = -1;

	public TextMesh DeathsText;
	public TextMesh SpeedRunTimer;
	public TextMesh FramesPerSecond;
	public TextMesh AmmoText;
	public TextMesh DebugText;
	public GameObject DebugController;

	public GameObject cursor;
	public GameObject[] rawMenus;
	public bool updateStats = false;

	public bool settingCustomControls = false;
	public bool settingCustomControls_ready = false;//This is true once no key is pressed
	public bool settingCustomControls_done = false;
	public Catagory setControls_Catagory = Catagory.None;
	public Controls.Type setCustomControls_type = Controls.Type.None;
	public int setCustomControls_stage = 0;
	public TextMesh setCustomControls_text;

	public enum Catagory
	{
		None,
		Platformer,
		FPS,
		Menu,
		End
	}

	public bool inTextBox = false;
	public int textBoxIndex = -1;
	public string textBoxStr = "";
	public float textBoxFlasherTimeSet = 0;
	public float textBoxFlasherDelay = 0.3f;
	public string flasher = "_";
	public bool flasherFlip = false;
	public TextBoxType textBoxType = TextBoxType.None;

	public enum TextBoxType
	{
		None,
		Username,
		Token,
		End
	}

	public FreshLevels.Type leaderboardLevel = FreshLevels.Type.None;
	public WaitingForLeaderboard waitingForLeaderboard = WaitingForLeaderboard.None;
	public static int leaderboardScroll = 0;//0-14
	public static int leaderboardIndex = 0;

	public enum WaitingForLeaderboard
	{
		None,
		MainMenuCurrentBoard,
		LeaderboardMenu,
		End
	}
	public enum StatMode
	{
		None,
		AvgEachLevel,
		End
	}

	Vector2 oldMousePos = Vector2.zero;
	List<RawInfo.Item> items;

	public static void UpdateTextColor(FreshLevels.Type type)//only for deaths & timer text
	{
		if (self == null) { return; }
		if (FreshLevels.UseBlackTextLevel(type))
		{
			self.DeathsText.color = Color.black;
			self.SpeedRunTimer.color = Color.black;
		}
		else
		{
			self.DeathsText.color = Color.white;
			self.SpeedRunTimer.color = Color.white;
		}


	}

	void Awake()
	{
		self = this;
	}


	void Start()
	{
		InitRawMenus();

	}

	void Update()
	{
		if (QCAskType != FreshLevels.Type.None)
		{
			if (Time.time > (QCAskTimeSet + QCAskDelay))
			{
				QCAskTimeSet = Time.time;
				AskForLeaderboard(QCAskType);
				QCAskType = FreshLevels.Type.None;
			}
		}
		if (Time.time > (QCUpdateTimeSet + QCUpdateTimeDelay))
		{
			QCUpdateTimeSet = Time.time;
			if (RawInfo.currentMenu == RawInfo.MenuType.MainMenu)
			{
				UpdateQCMainMenu();
			}
			if (RawInfo.currentMenu == RawInfo.MenuType.LeaderboardsMenu)
			{
				UpdateQCLeaderboard(LeaderboardItemIndex, leaderboardIndex);
			}
		}

		//UpdateLeaderboards();
		UpdateRawMenus();

		if (settingCustomControls)
		{
			UpdateSetCustomControls();
		}
		UpdateFramesPerSecond();
		UpdateDeathCounterAndSpeedRunDisplay();
	}

	float deltaTime = 0.0f;
	void UpdateFramesPerSecond()
	{
		if (FreshLevels.IsGameplayLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
		{
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			//FramesPerSecond.text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		}
		else
		{
			FramesPerSecond.text = "";
		}
	}

	public static string GetSpeedRunTimeStr(float time)
	{

		float f = time;
		int i = Mathf.FloorToInt(f);

		float d = Mathf.Floor((f - i) * 10000) * 0.0001f;
		string ds = System.String.Format("{0:F4}", d);
		ds = ds.Substring(2);
		string secs = "00";
		if (i >= 10) { secs = "" + i; }
		else if (i >= 1) { secs = "0" + i; }

		return "" + secs + ":" + ds;

	}

	public void UpdateLeaderboards()
	{
		if (fa.receivedLeaderboard)
		{
			fa.receivedLeaderboard = false;
			if (waitingForLeaderboard != WaitingForLeaderboard.None)
			{
				switch (waitingForLeaderboard)
				{
					case WaitingForLeaderboard.MainMenuCurrentBoard:
						{
							if (RawInfo.currentMenu == RawInfo.MenuType.MainMenu)//if it's still on the correct menu (otherwise forget it)
							{
								for (int i = 0; i < items.Count; i++)
								{
									if (items[i].uId == RawInfo.ButtonUID.MainMenu_CurrentLeaderboard)
									{
										ParseMainMenuLeaderboard(i);
										break;
									}
								}


							}
						}
						break;
					case WaitingForLeaderboard.LeaderboardMenu:
						{
							if (RawInfo.currentMenu == RawInfo.MenuType.LeaderboardsMenu)//if it's still on the correct menu (otherwise forget it)
							{
								for (int i = 0; i < items.Count; i++)
								{
									if (items[i].uId == RawInfo.ButtonUID.Leaderboards_Leaderboard)
									{
										ParseLeaderboardMenuLeaderboard(i);
										break;
									}
								}


							}
						}
						break;

				}
				waitingForLeaderboard = WaitingForLeaderboard.None;
			}
		}

	}

	public void UpdateQCMainMenu()
	{
		if (mainMenuLeaderboardIndex == -1) { return; }
		fa.mmleaderboardData = "";

		for (int i = 1; i <= fa.lengthOfMainMenuLeaderboard; i++)
		{
			fa.mmleaderboardData += "" + i + ": ";
			if (fa.mmleaderboardNames[i] != null) { fa.mmleaderboardData += fa.mmleaderboardNames[i]; } else { fa.mmleaderboardData += "   "; }
			fa.mmleaderboardData += " - ";
			if (fa.mmleaderboardTimes[i] != null) { fa.mmleaderboardData += fa.mmleaderboardTimes[i]; } else { fa.mmleaderboardData += "   "; }
			fa.mmleaderboardData += "\n";
		}

		items[mainMenuLeaderboardIndex].otherLabels[1].text = "Weekly level: " + FreshLevels.GetStrLabelForType(FreshLevels.GetWeeklyLevel());
		items[mainMenuLeaderboardIndex].otherLabels[0].text = fa.mmleaderboardData;

	}

	public void UpdateQCLeaderboard(int itemIndex, int leaderindex)
	{
		if (itemIndex == -1) { return; }

		fa.leaderboardData = "";
		for (int i = 1; i <= fa.lengthOfLeaderboard; i++)
		{
			fa.leaderboardData += "" + i + ": ";
			if (fa.leaderboardNames[leaderindex, i] != null) { fa.leaderboardData += fa.leaderboardNames[leaderindex, i]; } else { fa.leaderboardData += "   "; }
			fa.leaderboardData += " - ";
			if (fa.leaderboardTimes[leaderindex, i] != null) { fa.leaderboardData += fa.leaderboardTimes[leaderindex, i]; } else { fa.leaderboardData += "   "; }
			fa.leaderboardData += "\n";
		}

		items[itemIndex].otherLabels[1].text = fa.leaderboardTitle;
		items[itemIndex].otherLabels[0].text = fa.leaderboardData;
		//items[itemIndex].otherLabels[0].text = fa.entireLeaderboard[leaderboardIndex];
	}

	public void AskForLeaderboard(FreshLevels.Type level)
	{
		if (level == FreshLevels.Type.None) { return; }
		Debug.Log("Asking for " + level);
		for (int i = 1; i <= fa.lengthOfLeaderboard; i++)//load the main menu leaderboard
		{
			FrFuncs.Qc_GetLeaderboardNameSlot(level, i);
			FrFuncs.Qc_GetLeaderboardTimeSlot(level, i);
		}
		//fa.entireLeaderboard[leaderboardIndex] = "...";
		//FrFuncs.Qc_GetEntireLeaderboard(level, leaderboardIndex);

	}

	public void ParseLeaderboardMenuLeaderboard(int itemIndex)
	{
		/*if (fa.leaderboardData == "empty")
		{
			items[itemIndex].otherLabels[1].text = "" + (leaderboardIndex + 1) + " " + FreshLevels.GetStrLabelForType(leaderboardLevel) + "";
			items[itemIndex].otherLabels[0].text = "No times set (yet).";

			return;
		}

		items[itemIndex].otherLabels[1].text = "" + (leaderboardIndex + 1) + " " + FreshLevels.GetStrLabelForType(leaderboardLevel) + "";

		List<string> names = new List<string>();
		List<float> score = new List<float>();
		string[] splitString = fa.leaderboardData.Split(new string[] { ",", ":" }, System.StringSplitOptions.RemoveEmptyEntries);

		bool flip = false;
		for (int a = 1; a < splitString.Length; a++)
		{
			if (!flip)
			{
				names.Add(splitString[a]);
			}
			else
			{
				float r = 0;
				float.TryParse(splitString[a], out r);
				score.Add(r);
			}
			flip = !flip;
		}

		//Ok, now sort in order
		int index = 0;
		string[] namesInOrder = new string[names.Count];
		float[] timesInOrder = new float[score.Count];

		for (int b = 0; b < score.Count; b++)
		{
			bool found = false;
			int id = -1;
			float lowestSoFar = 9999;
			for (int a = 0; a < score.Count; a++)
			{
				if (score[a] < lowestSoFar)
				{
					id = a;
					lowestSoFar = score[a];
					found = true;
				}
			}

			if (found)
			{
				namesInOrder[index] = names[id];
				timesInOrder[index] = score[id];
				index++;
				score[id] = 99999;//remove from future loops
			}
		}

		//now print to leaderboard
		items[itemIndex].otherLabels[0].text = "" + leaderboardScroll + " - " + (leaderboardScroll + 15) + "\n\n";
		int i = leaderboardScroll;
		while (i < (leaderboardScroll + 15))
		{
			if (i < namesInOrder.Length)
			{
				items[itemIndex].otherLabels[0].text += "Entry: " + namesInOrder[i] + " " + timesInOrder[i] + "\n";
			}
			i += 1;
		}*/
		//items[itemIndex].otherLabels[0].text += "End";
	}


	public void ParseMainMenuLeaderboard(int itemIndex)
	{
		//Debug.Log(fa.leaderboardData);
		if (fa.leaderboardData == "empty")
		{
			items[itemIndex].otherLabels[0].text = "Weekly leaderboard\n" + FreshLevels.GetStrLabelForType(leaderboardLevel) + "\n\nNo times set.";

			return;
		}

		//parse the data

		items[itemIndex].otherLabels[0].text = "Weekly leaderboard";
		items[itemIndex].otherLabels[0].text += "\n" + FreshLevels.GetStrLabelForType(leaderboardLevel) + "\n\n";

		List<string> names = new List<string>();
		List<float> score = new List<float>();
		Debug.Log(fa.leaderboardData);//OFF BY ONE I THINK IS HERE, there's a 0 at the start
		if (fa.leaderboardData == null) { return; }
		if (fa.leaderboardData == "Null") { return; }
		string[] splitString = fa.leaderboardData.Split(new string[] { ",", ":" }, System.StringSplitOptions.RemoveEmptyEntries);

		string temp = "\n" + fa.username + " " + splitString[0] + "\n\n";

		bool flip = false;
		for (int a = 1; a < splitString.Length; a++)
		{
			if (!flip)
			{
				names.Add(splitString[a]);
			}
			else
			{
				float r = 0;
				float.TryParse(splitString[a], out r);
				score.Add(r);
			}
			flip = !flip;
		}

		//Ok, now add the lowest 5
		for (int b = 0; b < 5; b++)
		{
			bool found = false;
			int id = -1;
			float lowestSoFar = 9999;
			for (int a = 0; a < score.Count; a++)
			{
				if (score[a] < lowestSoFar)
				{
					id = a;
					lowestSoFar = score[a];
					found = true;
				}
			}

			if (found)
			{
				score[id] = 99999;//remove from future loops
				items[itemIndex].otherLabels[0].text += "" + names[id] + " " + lowestSoFar + "\n";

			}
		}

		items[itemIndex].otherLabels[0].text += temp;
	}

	public void UpdateDeathCounterAndSpeedRunDisplay()
	{
		if (FreshLevels.IsFPSLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
		{
			AmmoText.text = "" + FPSPlayer.ammo;
		}
		else
		{
			AmmoText.text = "";
		}
		if (FreshLevels.IsGameplayLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name))
		{
			if (fa.showDeathCounter == 1)
			{
				DeathsText.text = "" + za.deaths;
			}
			else
			{
				DeathsText.text = "";
			}
			if (fa.showSpeedrunTimer == 1)
			{
				SpeedRunTimer.text = GetSpeedRunTimeStr(fa.speedrunTime);
			}
			else
			{
				SpeedRunTimer.text = "";
			}

		}
		else
		{
			DeathsText.text = "";
			SpeedRunTimer.text = "";
		}
	}

	void UpdateSetCustomControls()
	{
		if (setCustomControls_text == null)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i].uId == RawInfo.ButtonUID.SetCustomControls_Text)
				{
					setCustomControls_text = items[i].controllerGO.GetComponent<TextMesh>();
				}
			}
		}


		Controls.Type type = Controls.Type.None;

		//Bypass for ESC key & open menu
		if (Input.GetKey(KeyCode.Escape) && setCustomControls_stage == 5 && setControls_Catagory == Catagory.Menu)
		{
			//If the player is trying to set ESC as the menu button, skip to the next step
			Controls.settingKey = false;
			setCustomControls_stage++;
			setCustomControls_text.text = "Finished!\nPress Escape to cancel\nor any other key to accept & quit";
			type = Controls.Type.None;
			settingCustomControls_done = true;
			return;
		}

		if (!Input.GetKey(KeyCode.Escape) && !Controls.GetAnyKeyDown() && !settingCustomControls_ready)
		{
			settingCustomControls_ready = true;

			TextMesh tm = setCustomControls_text;

			if (setControls_Catagory == Catagory.Platformer)
			{
				switch (setCustomControls_stage)
				{
					case 0: tm.text = "Press Jump..."; type = Controls.Type.Jump; break;
					case 1: tm.text = "Press Use Ability..."; type = Controls.Type.Ability1; break;
					case 2: tm.text = "Press Move Left..."; type = Controls.Type.MoveLeft; break;
					case 3: tm.text = "Press Move Right..."; type = Controls.Type.MoveRight; break;
					case 4: tm.text = "Press Drop Off Sticky Wall..."; type = Controls.Type.MoveDown; break;
					case 5: tm.text = "Press Respawn..."; type = Controls.Type.Respawn; break;
					case 6: tm.text = "Press Restart Level..."; type = Controls.Type.Restart; break;

					case 7: tm.text = "Finished!\nPress Escape to cancel\nor any other key to accept & quit"; type = Controls.Type.None; settingCustomControls_done = true; break;
				}
			}
			else if (setControls_Catagory == Catagory.Menu)
			{
				switch (setCustomControls_stage)
				{
					case 0: tm.text = "Press Menu Up..."; type = Controls.Type.MenuUp; break;
					case 1: tm.text = "Press Menu Down..."; type = Controls.Type.MenuDown; break;
					case 2: tm.text = "Press Menu Left..."; type = Controls.Type.MenuLeft; break;
					case 3: tm.text = "Press Menu Right..."; type = Controls.Type.MenuRight; break;
					case 4: tm.text = "Press Menu Select..."; type = Controls.Type.MenuSelect; break;
					case 5: tm.text = "Press Menu Escape...\n(Escape is always additionally bound to this))"; type = Controls.Type.OpenMenu; break;
					case 6: tm.text = "Finished!\nPress Escape to cancel\nor any other key to accept & quit"; type = Controls.Type.None; settingCustomControls_done = true; break;
				}
			}
			else if (setControls_Catagory == Catagory.FPS)
			{
				switch (setCustomControls_stage)
				{
					case 0: tm.text = "Press Fire Gun..."; type = Controls.Type.FPSFire; break;
					case 1: tm.text = "Press Move Forward..."; type = Controls.Type.FPSForward; break;
					case 2: tm.text = "Press Move Backward..."; type = Controls.Type.FPSBackward; break;
					case 3: tm.text = "Press Strafe Left..."; type = Controls.Type.FPSLeft; break;
					case 4: tm.text = "Press Strafe Right..."; type = Controls.Type.FPSRight; break;
					case 5: tm.text = "Press Switch Weapon..."; type = Controls.Type.FPSCycleWeapon; break;
					case 6: tm.text = "Press Look Left...\n(Mouselook always works)"; type = Controls.Type.FPSLookLeft; break;
					case 7: tm.text = "Press Look Right...\n(Mouselook always works)"; type = Controls.Type.FPSLookRight; break;
					case 8: tm.text = "Press Look Up...\n(Mouselook always works)"; type = Controls.Type.FPSLookUp; break;
					case 9: tm.text = "Press Look Down...\n(Mouselook always works)"; type = Controls.Type.FPSLookDown; break;

					case 10: tm.text = "Finished!\nPress Escape to cancel\nor any other key to accept & quit"; type = Controls.Type.None; settingCustomControls_done = true; break;
				}
			}
			if (type != Controls.Type.None) { Controls.StartSettingKey(type); }
		}


		if (Input.GetKeyDown(KeyCode.Escape))
		{
			settingCustomControls = false;
			MenuOn(RawInfo.MenuType.ControlsCancelledOut);
			return;
		}


		//accept the controls
		if (!Input.GetKey(KeyCode.Escape) && Controls.GetAnyKeyDown() && settingCustomControls_ready)
		{
			if (settingCustomControls_done)
			{
				Setup.SetCursor(Setup.C.Visible);
				Controls.SetControlsFromTempControls(0);
				Fresh_Saving.SaveCustomControls();
				settingCustomControls = false;
				settingCustomControls_ready = false;
				settingCustomControls_done = false;
				AcceptControls(setControls_Catagory);
				setControls_Catagory = Catagory.None;
				MenuOn(RawInfo.MenuType.AfterCustomControls);
			}
			else
			{
				settingCustomControls_ready = false;
				setCustomControls_stage++;
			}
		}



	}

	void AcceptControls(Catagory cat)
	{
		switch (cat)
		{
			case Catagory.Platformer:
				Controls.customControls[(int)Controls.Type.Jump] = true;
				Controls.customControls[(int)Controls.Type.Ability1] = true;
				Controls.customControls[(int)Controls.Type.MoveRight] = true;
				Controls.customControls[(int)Controls.Type.MoveLeft] = true;
				Controls.customControls[(int)Controls.Type.MoveDown] = true;
				Controls.customControls[(int)Controls.Type.Respawn] = true;
				Controls.customControls[(int)Controls.Type.Restart] = true;

				break;
			case Catagory.Menu:
				Controls.customControls[(int)Controls.Type.MenuSelect] = true;
				Controls.customControls[(int)Controls.Type.OpenMenu] = true;
				Controls.customControls[(int)Controls.Type.MenuLeft] = true;
				Controls.customControls[(int)Controls.Type.MenuRight] = true;
				Controls.customControls[(int)Controls.Type.MenuUp] = true;
				Controls.customControls[(int)Controls.Type.MenuDown] = true;
				break;
			case Catagory.FPS:
				Controls.customControls[(int)Controls.Type.FPSFire] = true;
				Controls.customControls[(int)Controls.Type.FPSCycleWeapon] = true;
				Controls.customControls[(int)Controls.Type.FPSForward] = true;
				Controls.customControls[(int)Controls.Type.FPSBackward] = true;
				Controls.customControls[(int)Controls.Type.FPSLeft] = true;
				Controls.customControls[(int)Controls.Type.FPSRight] = true;
				Controls.customControls[(int)Controls.Type.FPSLookRight] = true;
				Controls.customControls[(int)Controls.Type.FPSLookLeft] = true;
				Controls.customControls[(int)Controls.Type.FPSLookUp] = true;
				Controls.customControls[(int)Controls.Type.FPSLookDown] = true;
				break;
		}

		//save custom controls
		for (int i = 0; i < 25; i++)
		{
			int r = 0;
			if (Controls.customControls[i]) { r = 1; }
			PlayerPrefs.SetInt("customControl" + i, r);
		}
		PlayerPrefs.Save();
	}



	public void InitRawMenus()
	{
		//Can't use a TAG for this, because it doesn't find GOs that are inactive.
		for (int i = 0; i < rawMenus.Length; i++)
		{
			rawMenus[i].SetActive(true);
		}

		//Find everything with the RawItem tag and put in a list
		items = new List<RawInfo.Item>();
		GameObject[] gos = GameObject.FindGameObjectsWithTag("RAWItem");
		Debug.Log("Found " + gos.Length + " items!");
		for (int i = 0; i < gos.Length; i++)
		{
			items.Add(gos[i].GetComponent<RawInfo>().item);
		}

		cursor.transform.position = new Vector3(999, 999, cursorZ);
		for (int i = 0; i < items.Count; i++)
		{
			items[i].controllerGO.SetActive(false);
		}
	}

	public void UpdateAchivoDisplay()
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.Achivo_AllasKlar) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_AllasKlar)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_DaddysLove) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_DaddysLove)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_DontStompa) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_DontStompa)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_MagicMonk) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_MagicMonk)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_Routes66) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_Routes66)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_Cheater) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_Cheater)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_Reverso) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_Reverso)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_Champion) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_Champion)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }

			if (items[i].uId == RawInfo.ButtonUID.Achivo_NoThanksImGood) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_NoThanksImGood)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_MitLiebeGemacht) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_MitLiebeGemacht)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
			if (items[i].uId == RawInfo.ButtonUID.Achivo_GoinFastImTowerBound) { if (AchivoFuncs.CheckAchivo(AchivoFuncs.Achivos.Achivo_GoinFastImTowerBound)) { items[i].lockObj.SetActive(false); } else { items[i].lockObj.SetActive(true); } }
		}
	}

	public void UpdateGoldenButtDisplay()
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.Icon_GoldenButts)
			{
				items[i].otherLabels[0].text = "" + fa.goldenButtsCollected + " / 5";
				if (fa.goldenButtsCollected >= fa.goldenButtsTotal)
				{
					items[i].otherDisplays[0].SetActive(true);
				}
				else
				{
					items[i].otherDisplays[0].SetActive(false);
				}
			}
		}
	}

	public void UpdatePuppersDisplay()
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.Icon_Puppies)
			{
				items[i].otherLabels[0].text = "" + fa.puppiesCollected + " / 5";
				if (fa.puppiesCollected >= fa.puppiesTotal)
				{
					items[i].otherDisplays[0].SetActive(true);
				}
				else
				{
					items[i].otherDisplays[0].SetActive(false);
				}
			}
		}
	}

	public void UpdateCoinDisplay()
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.MainMenu_CoinDisplay)
			{
				items[i].otherLabels[0].text = "$" + fa.coinsCollected + " / " + fa.coinsCollectedGoal;

				if (fa.coinsCollected >= fa.coinsCollectedGoal)
				{
					items[i].otherDisplays[0].SetActive(true);
				}
				else
				{
					items[i].otherDisplays[0].SetActive(false);
				}
			}
		}
	}


	public int GetIndexForUID(RawInfo.ButtonUID uid)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == uid)
			{
				return i;
			}
		}
		return -1;
	}

	public void UpdateTokenDisplay(bool success, string temp)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.SetTokenMenu_Display)
			{
				if (success)
				{
					items[i].otherLabels[0].text = "Valid token:\n" + fa.token;
				}
				else
				{
					items[i].otherLabels[0].text = temp + " was not valid\n\nSticking with: " + fa.token;
				}
			}
		}
	}

	public void UpdateRawMenus()
	{

		//if (Controls.GetInputUp(Controls.Type.MenuSelect) && !InRawMenu) { RawFuncs.RawMenuHandover = false; }

		//Handle button functions
		for (int a = 0; a < items.Count; a++)
		{
			if (Controls.GetInputDown(Controls.Type.MenuSelect, 0))
			{
				//Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart);
				if (items[a].hasCursor && items[a].controllerGO.activeSelf)
				{
					//Debug.Log("ButtonFunc!!! " + Time.time);
					switch (items[a].buttonFunc)
					{
						case RawInfo.ButtonFunc.AreYouSureMenu_Yes:
							fa.token = null;
							fa.username = "defaultUser";
							PlayerPrefs.SetString("username", fa.username);
							PlayerPrefs.Save();
							FrFuncs.AskForToken();
							MenuOn(RawInfo.MenuType.AccountMenu);
							return;
						case RawInfo.ButtonFunc.AreYouSureMenu_No:
							MenuOn(RawInfo.MenuType.AccountMenu);
							return;
						case RawInfo.ButtonFunc.AccountMenu_WipeAccount:
							MenuOn(RawInfo.MenuType.AreYouSureMenu);
							return;
						case RawInfo.ButtonFunc.AccountMenu_SetToken:
							MenuOn(RawInfo.MenuType.SetTokenMenu);
							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.SetTokenMenu_Display)
								{
									items[i].otherLabels[0].text = fa.token;
								}
							}
							return;
						case RawInfo.ButtonFunc.WaitingForValidationMenu_Back:
							fa.checkToken = false;
							MenuOn(RawInfo.MenuType.SetTokenMenu);
							return;
						case RawInfo.ButtonFunc.SetTokenMenu_Back:
							MenuOn(RawInfo.MenuType.AccountMenu);
							return;
						case RawInfo.ButtonFunc.MainMenu_OST:
							Application.OpenURL("https://store.steampowered.com/app/1113940/Electronic_Super_Joy_2/");//  /soundtrack.html
							return;
						case RawInfo.ButtonFunc.SetTokenMenu_Set:
							MenuOn(RawInfo.MenuType.TextInput_TokenMenu);

							inTextBox = true;
							textBoxIndex = -1;
							textBoxType = TextBoxType.Token;
							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.TextInput_DisplayToken)
								{
									textBoxIndex = i;
									items[i].otherLabels[0].text = fa.token;
									textBoxStr = items[i].otherLabels[0].text;
								}
							}

							return;

						case RawInfo.ButtonFunc.AccountMenu_DisplayToken:
							MenuOn(RawInfo.MenuType.DisplayTokenMenu);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.DisplayTokenMenu_Display)
								{
									items[i].otherLabels[0].text = fa.token;
								}
							}
							return;
						case RawInfo.ButtonFunc.DisplayTokenMenu_Back:
							MenuOn(RawInfo.MenuType.AccountMenu);
							return;
						case RawInfo.ButtonFunc.NetworkMenu_GoToAccountMenu:
							MenuOn(RawInfo.MenuType.AccountMenu);
							return;
						case RawInfo.ButtonFunc.AccountMenu_Back:
							MenuOn(RawInfo.MenuType.NetworkMenu);
							return;
						case RawInfo.ButtonFunc.AccountMenu_GoToUsernameMenu:
							MenuOn(RawInfo.MenuType.UsernameMenu);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.UsernameMenu_DisplayUsername)
								{
									items[i].otherLabels[0].text = fa.username;
								}
							}

							return;
						case RawInfo.ButtonFunc.UsernameMenu_Back:
							MenuOn(RawInfo.MenuType.AccountMenu);
							return;
						case RawInfo.ButtonFunc.UsernameMenu_AttemptToSetUsername:
							MenuOn(RawInfo.MenuType.TextInput_UsernameMenu);
							inTextBox = true;
							textBoxIndex = -1;
							textBoxType = TextBoxType.Username;
							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.TextInput_DisplayUsername)
								{
									textBoxIndex = i;
									items[i].otherLabels[0].text = fa.username;
									textBoxStr = items[i].otherLabels[0].text;
								}
							}
							return;

						case RawInfo.ButtonFunc.LeaveNetworkingMenu:
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.ToggleUsageStats:
							Debug.Log("SENDING USAGE STATS");
							sendUsageStats = !sendUsageStats;
							SetDisplay(a, sendUsageStats);
							Fresh_Saving.SaveSendUsageStats(sendUsageStats);
							return;
						case RawInfo.ButtonFunc.NetworkMenu_ToggleDevServer:
							fa.dontConnect3rdParty = !fa.dontConnect3rdParty;
							SetDisplay(a, !fa.dontConnect3rdParty);
							Fresh_Saving.SetBool("dontconnecttodevserver", fa.dontConnect3rdParty);
							PlayerPrefs.Save();
							return;
						case RawInfo.ButtonFunc.NetworkMenu_ToggleSteam:
							fa.dontConnectSteam = !fa.dontConnectSteam;
							SetDisplay(a, !fa.dontConnectSteam);
							Fresh_Saving.SetBool("dontconnecttosteam", fa.dontConnectSteam);
							PlayerPrefs.Save();
							return;
						case RawInfo.ButtonFunc.LeaveViewStatsMenu:
							updateStats = false;
							MenuOn(RawInfo.MenuType.NetworkMenu);
							return;
						case RawInfo.ButtonFunc.OptionsHub_GoToCredits:
							MenuOn(RawInfo.MenuType.RollingCredits);
							return;
						case RawInfo.ButtonFunc.RollingCredits_Leave:
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.GotoOptionsHub:
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.LeaveOptionsHub:
							if (inOptionsFromInGame)
							{
								inOptionsFromInGame = false;
								MenuOn(RawInfo.MenuType.InGameHub);
							}
							else
							{
								inOptionsFromInGame = false;
								MenuOn(RawInfo.MenuType.MainMenu);
							}
							return;
						case RawInfo.ButtonFunc.OptionsHub_GoToLeaderboards:
							MenuOn(RawInfo.MenuType.LeaderboardsMenu);
							return;
						case RawInfo.ButtonFunc.LeaderboardsBack:
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.LeaderboardsPrev:
							//show prev leaderboard
							leaderboardIndex--;
							if (leaderboardIndex < 0) { leaderboardIndex = FreshLevels.totalIndexs; }

							waitingForLeaderboard = WaitingForLeaderboard.LeaderboardMenu;
							fa.receivedLeaderboard = false;
							leaderboardLevel = FreshLevels.IndexForLevelLeaderboard(leaderboardIndex);
							//FrFuncs.GetLeaderboard(leaderboardLevel);
							QCUpdateTimeSet = Time.time;
							QCAskTimeSet = Time.time;
							QCAskType = leaderboardLevel;
							fa.leaderboardTitle = "" + leaderboardIndex + ". " + FreshLevels.GetStrLabelForType(leaderboardLevel);
							//AskForLeaderboard(leaderboardLevel);

							UpdateQCLeaderboard(LeaderboardItemIndex, leaderboardIndex);
							return;
						case RawInfo.ButtonFunc.LeaderboardsNext:
							//show next leaderboard
							leaderboardIndex++;
							if (leaderboardIndex > FreshLevels.totalIndexs) { leaderboardIndex = 0; }

							waitingForLeaderboard = WaitingForLeaderboard.LeaderboardMenu;
							fa.receivedLeaderboard = false;
							leaderboardLevel = FreshLevels.IndexForLevelLeaderboard(leaderboardIndex);
							QCUpdateTimeSet = Time.time;
							QCAskTimeSet = Time.time;
							QCAskType = leaderboardLevel;
							fa.leaderboardTitle = "" + leaderboardIndex + ". " + FreshLevels.GetStrLabelForType(leaderboardLevel);

							//FrFuncs.GetLeaderboard(leaderboardLevel);
							//AskForLeaderboard(leaderboardLevel);

							UpdateQCLeaderboard(LeaderboardItemIndex, leaderboardIndex);
							return;
						case RawInfo.ButtonFunc.MainMenu_Quit:
							AllMenusOff();
							Application.Quit();
							//xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "ESJ2Title");
							return;
						case RawInfo.ButtonFunc.MainMenu_Play:
							PlayerPrefs.Save();

							Setup.SetCursor(Setup.C.NotVisible);

							fa.paused = false;
							AllMenusOff();
							xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "MegaMetaWorld");
							return;
						case RawInfo.ButtonFunc.OptionsHub_GoToNetworkingMenu:
							MenuOn(RawInfo.MenuType.NetworkMenu);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.NetworkMenu_ToggleSteam)
								{
									fa.dontConnectSteam = Fresh_Loading.GetBool("dontconnecttosteam", false);
									SetDisplay(i, !fa.dontConnectSteam);
								}
								if (items[i].uId == RawInfo.ButtonUID.NetworkMenu_ToggleDevServer)
								{
									fa.dontConnect3rdParty = Fresh_Loading.GetBool("dontconnecttodevserver", false);
									SetDisplay(i, !fa.dontConnect3rdParty);
								}
							}

							return;
						case RawInfo.ButtonFunc.InGameHub_Resume:
							fa.paused = false;
							AllMenusOff();
							return;
						case RawInfo.ButtonFunc.InGameHub_Restart:
							PlayerPrefs.Save();
							Setup.SetCursor(Setup.C.NotVisible);
							fa.paused = false;
							AllMenusOff();
							ThrowingBallScript.ResetSavedDaddysLove();
							xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_START, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
							return;
						case RawInfo.ButtonFunc.InGameHub_Options:
							inOptionsFromInGame = true;
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.InGameHub_PGMode:
							xa.pgMode = !xa.pgMode;
							Fresh_Saving.SavePGMode();
							SetDisplay(a, xa.pgMode);
							return;
						case RawInfo.ButtonFunc.InGameHub_Quit:
							PlayerPrefs.Save();
							Setup.SetCursor(Setup.C.NotVisible);
							fa.paused = false;
							AllMenusOff();
							ThrowingBallScript.ResetSavedDaddysLove();
							xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "MegaMetaWorld");
							return;
						case RawInfo.ButtonFunc.Options_GoToControls:
							MenuOn(RawInfo.MenuType.ControlsMenu);
							return;
						case RawInfo.ButtonFunc.Options_GoToAdvOptions:
							MenuOn(RawInfo.MenuType.AdvancedOptionsHub);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.AdvOpt_ForceAltMenuOpenButton)
								{
									fa.forceAltMenuControls = false;
									int r = PlayerPrefs.GetInt("altmenucontrols", 0);
									if (r == 1) { fa.forceAltMenuControls = true; }

									SetDisplay(i, fa.forceAltMenuControls);
								}
							}

							return;
						case RawInfo.ButtonFunc.AdvOptions_Back:
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.AdvOptions_Data:
							MenuOn(RawInfo.MenuType.AdvOpt_Data);
							return;
						case RawInfo.ButtonFunc.AdvOpt_Data_Back:
							MenuOn(RawInfo.MenuType.AdvancedOptionsHub);
							return;
						case RawInfo.ButtonFunc.AdvOpt_Data_WipeAllSavedData:
							MenuOn(RawInfo.MenuType.AdvOpt_Data_WipeAllAreYouSure);
							return;
						case RawInfo.ButtonFunc.AdvOpt_Data_Yes:
							PlayerPrefs.DeleteAll();
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RockImpact);
							MenuOn(RawInfo.MenuType.AdvOpt_Data);
							Fresh_Loading.LoadMiscSettings();


							return;
						case RawInfo.ButtonFunc.AdvOpt_Data_No:
							MenuOn(RawInfo.MenuType.AdvOpt_Data);
							return;

						case RawInfo.ButtonFunc.AdvOpt_ForceAltMenuOpenButton:

							fa.forceAltMenuControls = !fa.forceAltMenuControls;

							if (fa.forceAltMenuControls)
							{
								PlayerPrefs.SetInt("altmenucontrols", 1);
								SetDisplay(a, true);
							}
							else
							{
								PlayerPrefs.SetInt("altmenucontrols", 0);
								SetDisplay(a, false);
							}
							PlayerPrefs.Save();
							return;

						case RawInfo.ButtonFunc.OptionsHub_GoToGameplayMenu:
							MenuOn(RawInfo.MenuType.GameplayMenu);


							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_ScreenshakeDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(fa.screenshakeMultiplier * 100);

								}
							}

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_MouseGrab)
								{
									fa.mouseGrab = false;
									int re = PlayerPrefs.GetInt("mousegrab", 1);
									if (re == 1) { fa.mouseGrab = true; }
									if (fa.mouseGrab)
									{
										SetDisplay(i, true);
									}
									else
									{
										SetDisplay(i, false);
									}
								}
								if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_BlackLoadingScreens)
								{
									fa.useBlackFaders = Fresh_Loading.GetBool("useblackfaders", false);
									SetDisplay(i, fa.useBlackFaders);
								}
								if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_MuteCheckpoints)
								{
									fa.muteCheckpointSounds = Fresh_Loading.GetBool("mutecheckpoints", false);
									SetDisplay(i, fa.muteCheckpointSounds);
								}
							}

							return;
						case RawInfo.ButtonFunc.OptionsHub_GoToVisualMenu:
							MenuOn(RawInfo.MenuType.VisualMenu);
							//Debug.Log("vsync: " + QualitySettings.vSyncCount);
							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.VisualMenu_VSync)
								{
									xa.vsyncON = false;
									int re = PlayerPrefs.GetInt("vsync", 0);
									if (re == 1) { xa.vsyncON = true; }
									if (xa.vsyncON)
									{
										SetDisplay(i, true);
									}
									else
									{
										SetDisplay(i, false);
									}
								}
								if (items[i].uId == RawInfo.ButtonUID.VisualMenu_Windowed)
								{
									xa.fullscreen = false;
									int re = PlayerPrefs.GetInt("fullscreen", 0);
									if (re == 1) { xa.fullscreen = true; }
									if (xa.fullscreen)
									{
										SetDisplay(i, false);//because windowed == OFF
									}
									else
									{
										SetDisplay(i, true);//because windowed == ON
									}
								}
							}

							return;
						case RawInfo.ButtonFunc.OptionsHub_GoToAudioMenu:
							MenuOn(RawInfo.MenuType.AudioMenu);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.AudioMenu_SoundDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(xa.soundVolume * 100);
								}
								if (items[i].uId == RawInfo.ButtonUID.AudioMenu_MusicDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(xa.musicVolume * 100);
								}
							}
							return;
						case RawInfo.ButtonFunc.ResMenu_NextRes:
							xa.resIndex++;
							if (xa.resIndex >= Screen.resolutions.Length) { xa.resIndex = 0; }

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.ResMenu_DisplayRes)
								{
									items[i].otherLabels[0].text = "" + Screen.resolutions[xa.resIndex].width + " x " + Screen.resolutions[xa.resIndex].height;
								}
							}
							return;
						case RawInfo.ButtonFunc.ResMenu_PrevRes:
							xa.resIndex--;
							if (xa.resIndex < 0) { xa.resIndex = Screen.resolutions.Length - 1; }

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.ResMenu_DisplayRes)
								{
									items[i].otherLabels[0].text = "" + Screen.resolutions[xa.resIndex].width + " x " + Screen.resolutions[xa.resIndex].height;
								}
							}
							return;
						case RawInfo.ButtonFunc.ResMenu_AcceptRes:
							PlayerPrefs.SetInt("resolutionWidth", Screen.resolutions[xa.resIndex].width);
							PlayerPrefs.SetInt("resolutionHeight", Screen.resolutions[xa.resIndex].height);
							PlayerPrefs.Save();
							Screen.SetResolution(Screen.resolutions[xa.resIndex].width, Screen.resolutions[xa.resIndex].height, Screen.fullScreen);


							return;
						case RawInfo.ButtonFunc.VisualMenu_GoToRes:
							MenuOn(RawInfo.MenuType.ResMenu);
							return;
						case RawInfo.ButtonFunc.ResMenu_Back:
							MenuOn(RawInfo.MenuType.VisualMenu);
							return;
						case RawInfo.ButtonFunc.VisualMenu_ToggleVSync:

							xa.vsyncON = !xa.vsyncON;

							if (xa.vsyncON)
							{

								QualitySettings.vSyncCount = 1;
								//QualitySettings.IncreaseLevel(true);
								PlayerPrefs.SetInt("vsync", 1);
								SetDisplay(a, true);
							}
							else
							{
								QualitySettings.vSyncCount = 0;
								//QualitySettings.DecreaseLevel(true);
								PlayerPrefs.SetInt("vsync", 0);
								SetDisplay(a, false);
							}
							return;
						case RawInfo.ButtonFunc.VisualMenu_Windowed:
							xa.fullscreen = !xa.fullscreen;
							if (xa.fullscreen)
							{
								Screen.fullScreen = true;
								SetDisplay(a, false);//windowed == ON
								PlayerPrefs.SetInt("fullscreen", 1);

							}
							else
							{
								Screen.fullScreen = false;
								PlayerPrefs.SetInt("fullscreen", 0);
								SetDisplay(a, true);
							}
							return;
						case RawInfo.ButtonFunc.VisualMenu_ToggleLowLag:

							if (fa.fancyLevel == fa.BackgroundFancy.Fancy)
							{
								fa.fancyLevel = fa.BackgroundFancy.NotFancy;
								SetDisplay(a, false);
							}
							else
							{
								fa.fancyLevel = fa.BackgroundFancy.Fancy;
								SetDisplay(a, true);
							}
							Fresh_Saving.SaveFancyLevel();

							return;

						case RawInfo.ButtonFunc.Controls_SetCustom:
							PlayerPrefs.SetInt("hasSetCustomControls", 1);
							PlayerPrefs.Save();
							MenuOn(RawInfo.MenuType.CustomControlsHub);
							return;
						case RawInfo.ButtonFunc.Controls_SwitchMode:
							/*if (PlayerPrefs.HasKey("hasSetCustomControls"))
							{
								Controls.useCustom = !Controls.useCustom;
								SetDisplay(a, Controls.useCustom);
								Fresh_Saving.SaveControlsMode(Controls.useCustom);
							}
							else
							{
								Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart);
							}*/
							return;
						case RawInfo.ButtonFunc.Controls_Back:
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.CustomControls_Back:
							MenuOn(RawInfo.MenuType.ControlsMenu);
							return;
						case RawInfo.ButtonFunc.CustomControls_PlatformerControls:
							setControls_Catagory = Catagory.Platformer;

							Setup.SetCursor(Setup.C.NotVisible);
							settingCustomControls = true;
							Controls.playerInt = 0;
							settingCustomControls_ready = false;
							setCustomControls_stage = 0;
							setCustomControls_type = Controls.Type.Jump;
							Controls.SetBackupControlsFromControls();
							MenuOn(RawInfo.MenuType.SetCustomControls);
							return;
						case RawInfo.ButtonFunc.CustomControls_FPSControls:
							setControls_Catagory = Catagory.FPS;

							Setup.SetCursor(Setup.C.NotVisible);
							settingCustomControls = true;
							Controls.playerInt = 0;
							settingCustomControls_ready = false;
							setCustomControls_stage = 0;
							setCustomControls_type = Controls.Type.FPSFire;
							Controls.SetBackupControlsFromControls();
							MenuOn(RawInfo.MenuType.SetCustomControls);
							return;
						case RawInfo.ButtonFunc.CustomControls_MenuControls:
							setControls_Catagory = Catagory.Menu;

							Setup.SetCursor(Setup.C.NotVisible);
							settingCustomControls = true;
							Controls.playerInt = 0;
							settingCustomControls_ready = false;
							setCustomControls_stage = 0;
							setCustomControls_type = Controls.Type.MenuUp;
							Controls.SetBackupControlsFromControls();
							MenuOn(RawInfo.MenuType.SetCustomControls);
							return;
						case RawInfo.ButtonFunc.CustomControls_Reset:
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Coin);
							for (int i = 0; i < 25; i++)
							{
								Controls.customControls[i] = false;
								PlayerPrefs.SetInt("customControl" + i, 0);
							}
							PlayerPrefs.Save();
							return;
						case RawInfo.ButtonFunc.AfterCustomControls_Cancel:
							Controls.SetControlsFromBackupControls(0);
							MenuOn(RawInfo.MenuType.ControlsMenu);
							return;
						case RawInfo.ButtonFunc.AfterCustomControls_Accept:
							Controls.SetControlsFromTempControls(0);
							Fresh_Saving.SaveCustomControls();
							MenuOn(RawInfo.MenuType.ControlsMenu);
							return;
						case RawInfo.ButtonFunc.CancelledControls_Ok:
							Controls.SetControlsFromBackupControls(0);
							MenuOn(RawInfo.MenuType.ControlsMenu);
							return;
						case RawInfo.ButtonFunc.Achivo_AllasKlar:
							SetAchivoDetailText("Find the secret area in \"Dark Bargain\"");
							return;
						case RawInfo.ButtonFunc.Achivo_DaddysLove:
							SetAchivoDetailText("Win \"Batter Up!\" with Maximum Daddy Love");
							return;
						case RawInfo.ButtonFunc.Achivo_DontStompa:
							SetAchivoDetailText("Beat \"Very Pink\" without using Stomping");
							return;
						case RawInfo.ButtonFunc.Achivo_MagicMonk:
							SetAchivoDetailText("Beat \"Sticky Remains\" without using the 3rd Jump");
							return;
						case RawInfo.ButtonFunc.Achivo_Routes66:
							SetAchivoDetailText("Beat \"Minivania\" in under 2 minutes");
							return;
						case RawInfo.ButtonFunc.Achivo_Cheater:
							SetAchivoDetailText("On \"Sticky Endings\", get the Air-Sword ability\nwhile already having the Double-Jump ability");
							return;
						case RawInfo.ButtonFunc.Achivo_Reverso:
							SetAchivoDetailText("On \"Sticky Endings\", get the Double-Jump ability\nwhile already having the Air-Sword ability");
							return;
						case RawInfo.ButtonFunc.Achivo_Champion:
							SetAchivoDetailText("Win the game");
							return;
						case RawInfo.ButtonFunc.Achivo_NoThanksImGood:
							SetAchivoDetailText("Win \"A Jumping Massacre\" without using teleporters");
							return;
						case RawInfo.ButtonFunc.Achivo_MitLiebeGemacht:
							SetAchivoDetailText("Win \"Alex's Love Letter\"");
							return;
						case RawInfo.ButtonFunc.Achivo_GoinFastImTowerBound:
							SetAchivoDetailText("Defeat the Shadow Monster");
							return;
						case RawInfo.ButtonFunc.GameplayMenu_Back:
							Fresh_Saving.SavePGMode();//calls playerPref.save
													  //PlayerPrefs.Save();// not needed, savePGMode calls save
							MenuOn(RawInfo.MenuType.OptionsHub);

							return;
						case RawInfo.ButtonFunc.AudioMenu_Back:
							PlayerPrefs.Save();
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.VisualMenu_Back:
							PlayerPrefs.Save();
							MenuOn(RawInfo.MenuType.OptionsHub);
							return;
						case RawInfo.ButtonFunc.GameplayMenu_ScreenshakeDown:

							fa.screenshakeMultiplier -= 0.05f;
							if (fa.screenshakeMultiplier < 0) { fa.screenshakeMultiplier = 0; }
							PlayerPrefs.SetFloat("screenshakemultiplier", fa.screenshakeMultiplier);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_ScreenshakeDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(fa.screenshakeMultiplier * 100) + "%";
								}
							}

							return;
						case RawInfo.ButtonFunc.GameplayMenu_ScreenshakeUp:

							fa.screenshakeMultiplier += 0.05f;
							if (fa.screenshakeMultiplier > 1) { fa.screenshakeMultiplier = 1; }
							PlayerPrefs.SetFloat("screenshakemultiplier", fa.screenshakeMultiplier);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_ScreenshakeDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(fa.screenshakeMultiplier * 100) + "%";
								}
							}


							return;
						case RawInfo.ButtonFunc.GameplayMenu_TogglePGMode:

							xa.pgMode = !xa.pgMode;
							SetDisplay(a, xa.pgMode);

							return;
						case RawInfo.ButtonFunc.GameplayMenu_ToggleMouseGrab:

							fa.mouseGrab = !fa.mouseGrab;
							int ar = 0;
							if (fa.mouseGrab) { ar = 1; }
							PlayerPrefs.SetInt("mousegrab", ar);
							SetDisplay(a, fa.mouseGrab);

							if (fa.mouseGrab)
							{
								Setup.SetCursor(Setup.C.NotVisible, Setup.C.Locked);
							}
							else
							{
								Setup.SetCursor(Setup.C.Visible, Setup.C.Unlocked);
							}

							return;
						case RawInfo.ButtonFunc.GameplayMenu_MuteCheckpoints:

							fa.muteCheckpointSounds = !fa.muteCheckpointSounds;
							Fresh_Saving.SetBool("mutecheckpoints", fa.muteCheckpointSounds);
							SetDisplay(a, fa.muteCheckpointSounds);
							PlayerPrefs.Save();
							return;
						case RawInfo.ButtonFunc.GameplayMenu_BlackLoadingScreens:
							fa.useBlackFaders = !fa.useBlackFaders;
							Fresh_Saving.SetBool("useblackfaders", fa.useBlackFaders);
							SetDisplay(a, fa.useBlackFaders);
							PlayerPrefs.Save();
							return;
						case RawInfo.ButtonFunc.GameplayMenu_ToggleDeathCounter:
							fa.showDeathCounter = 1 - fa.showDeathCounter;
							PlayerPrefs.SetInt("showDeathCounter", fa.showDeathCounter);
							SetDisplay(a, (fa.showDeathCounter == 1));
							return;
						case RawInfo.ButtonFunc.GameplayMenu_ToggleTimer:
							fa.showSpeedrunTimer = 1 - fa.showSpeedrunTimer;
							PlayerPrefs.SetInt("showSpeedrunTimer", fa.showSpeedrunTimer);
							SetDisplay(a, (fa.showSpeedrunTimer == 1));
							return;
						case RawInfo.ButtonFunc.AudioMenu_SoundDown:

							xa.soundVolume -= 0.05f;
							if (xa.soundVolume < 0) { xa.soundVolume = 0; }
							PlayerPrefs.SetFloat("SoundVolume", xa.soundVolume);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.AudioMenu_SoundDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(xa.soundVolume * 100);
								}
							}
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Coin);
							return;
						case RawInfo.ButtonFunc.AudioMenu_SoundUp:

							xa.soundVolume += 0.05f;
							if (xa.soundVolume > 1) { xa.soundVolume = 1; }
							PlayerPrefs.SetFloat("SoundVolume", xa.soundVolume);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.AudioMenu_SoundDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(xa.soundVolume * 100);
								}
							}
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Coin);
							return;
						case RawInfo.ButtonFunc.AudioMenu_MusicDown:

							xa.musicVolume -= 0.05f;
							if (xa.musicVolume < 0) { xa.musicVolume = 0; }
							PlayerPrefs.SetFloat("MusicXVolume", xa.musicVolume);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.AudioMenu_MusicDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(xa.musicVolume * 100);
								}
							}
							return;
						case RawInfo.ButtonFunc.AudioMenu_MusicUp:

							xa.musicVolume += 0.05f;
							if (xa.musicVolume > 1) { xa.musicVolume = 1; }
							PlayerPrefs.SetFloat("MusicXVolume", xa.musicVolume);

							for (int i = 0; i < items.Count; i++)
							{
								if (items[i].uId == RawInfo.ButtonUID.AudioMenu_MusicDisplay)
								{
									items[i].otherLabels[0].text = "" + Mathf.RoundToInt(xa.musicVolume * 100);
								}
							}
							return;
						case RawInfo.ButtonFunc.Stats_AllLevels:

							//Go through each bar
							for (int i = 0; i < items[a].otherDisplays.Length; i++)
							{
								items[a].otherDisplays[i].transform.SetY(-3.5f);
								items[a].otherDisplays[i].transform.SetScaleY(0);
								items[a].otherLabels[i].text = "";
							}
							for (int i = 0; i < items[a].otherDisplays.Length; i++)
							{
								Info infoScript = items[a].otherDisplays[i].GetComponent<Info>();
								if (infoScript != null && infoScript.level != FreshLevels.Type.None)
								{
									//search through lvlData for this name


									for (int b = 0; b < UsageStatsFunc.lvlStats.Count; b++)
									{
										if (UsageStatsFunc.lvlStats[b].levelType == infoScript.level)
										{
											/*
											350 would be 0.35 (in a 0-1 / 0-1000 scale)

											350 / 1000. 
										*/



											items[a].otherLabels[i].text = "" + UsageStatsFunc.lvlStats[b].levelType;
											float avg = UsageStatsFunc.lvlStats[b].average;
											if (avg != 0)
											{
												avg = (avg / 100) * 30;
											}
											items[a].otherDisplays[i].transform.SetScaleY(avg);

											items[a].otherDisplays[i].transform.SetY((avg * 0.5f));
											items[a].otherDisplays[i].transform.AddY(-3.5f);

											if (i < items[a].otherLabels.Length)
											{
												float r = items[a].otherDisplays[i].transform.position.y;
												r += (items[a].otherDisplays[i].transform.localScale.y * 0.5f) + 1;
												items[a].otherLabels[i].gameObject.transform.SetY(r);
											}

											//Debug.Log("DATA: lvl: " + "" + UsageStatsFunc.lvlStats[b].levelType + ", Avg:" + avg + ", Players: " + UsageStatsFunc.lvlStats[b].players + ", Times (count): " + UsageStatsFunc.lvlStats[b].times.Count);
										}
									}
								}
							}

							return;
						case RawInfo.ButtonFunc.Stats_NumOfPlayers:

							//Go through each bar
							for (int i = 0; i < items[a].otherDisplays.Length; i++)
							{
								items[a].otherDisplays[i].transform.SetY(-3.5f);
								items[a].otherDisplays[i].transform.SetScaleY(0);
								items[a].otherLabels[i].text = "";
							}
							for (int i = 0; i < items[a].otherDisplays.Length; i++)
							{
								Info infoScript = items[a].otherDisplays[i].GetComponent<Info>();
								if (infoScript != null && infoScript.level != FreshLevels.Type.None)
								{
									//search through lvlData for this name

									for (int b = 0; b < UsageStatsFunc.lvlStats.Count; b++)
									{
										if (UsageStatsFunc.lvlStats[b].levelType == infoScript.level)
										{
											/*
											350 would be 0.35 (in a 0-1 / 0-1000 scale)

											350 / 1000. 
											*/

											items[a].otherLabels[i].text = "(" + UsageStatsFunc.lvlStats[b].players + ")\n\n" + UsageStatsFunc.lvlStats[b].levelType;
											float avg = UsageStatsFunc.lvlStats[b].players;
											if (avg != 0)
											{
												avg = (avg / 100) * 30;
											}
											items[a].otherDisplays[i].transform.SetScaleY(avg);

											items[a].otherDisplays[i].transform.SetY((avg * 0.5f));
											items[a].otherDisplays[i].transform.AddY(-3.5f);

											if (i < items[a].otherLabels.Length)
											{
												float r = items[a].otherDisplays[i].transform.position.y;
												r += (items[a].otherDisplays[i].transform.localScale.y * 0.5f) + 1;
												items[a].otherLabels[i].gameObject.transform.SetY(r);
											}

											//Debug.Log("DATA: lvl: " + "" + UsageStatsFunc.lvlStats[b].levelType + ", Avg:" + avg + ", Players: " + UsageStatsFunc.lvlStats[b].players + ", Times (count): " + UsageStatsFunc.lvlStats[b].times.Count);
										}
									}



								}
							}

							return;
						case RawInfo.ButtonFunc.Stats_thegamedesigner:

							items[a].otherLabels[0].text = "trying...";
							StringBuilder sb = new StringBuilder();

							//Column 1
							sb.Append("0	" + GetMyLevelData(FreshLevels.Type.IntroStory));
							sb.Append("\n" + "1	" + GetMyLevelData(FreshLevels.Type.Tut_JumpAndAirsword));
							sb.Append("\n" + "2	" + GetMyLevelData(FreshLevels.Type.Tut_PortalRules));
							sb.Append("\n" + "3	" + GetMyLevelData(FreshLevels.Type.FPS_hallway));
							sb.Append("\n" + "4	" + GetMyLevelData(FreshLevels.Type.Tut_Chill));
							sb.Append("\n" + "5	" + GetMyLevelData(FreshLevels.Type.TinyTut_StickyWalls));
							sb.Append("\n" + "6	" + GetMyLevelData(FreshLevels.Type.TR_1));
							sb.Append("\n" + "7	" + GetMyLevelData(FreshLevels.Type.Tut_DoubleJump));
							sb.Append("\n" + "8	" + GetMyLevelData(FreshLevels.Type.CaveOfWonders1));
							sb.Append("\n" + "9	" + GetMyLevelData(FreshLevels.Type.MusicLvl1));
							sb.Append("\n" + "10	" + GetMyLevelData(FreshLevels.Type.Tut_Stomp));
							sb.Append("\n" + "11	" + GetMyLevelData(FreshLevels.Type.Pink1));
							sb.Append("\n" + "12	" + GetMyLevelData(FreshLevels.Type.ThreeDeeLevel));
							sb.Append("\n" + "13	" + GetMyLevelData(FreshLevels.Type.MissileBoostLevel));
							sb.Append("\n" + "14	" + GetMyLevelData(FreshLevels.Type.CaveOfWondersHell));
							sb.Append("\n" + "15	" + GetMyLevelData(FreshLevels.Type.Santa3));
							sb.Append("\n" + "16	" + GetMyLevelData(FreshLevels.Type.TO_1));
							sb.Append("\n" + "17	" + GetMyLevelData(FreshLevels.Type.Santa1));

							items[a].otherLabels[0].text = sb.ToString();

							//Column 2
							sb.Length = 0;
							sb.Append("1	" + GetMyLevelData(FreshLevels.Type.NC_3));
							sb.Append("\n" + "2	" + GetMyLevelData(FreshLevels.Type.MusicLvl2));
							sb.Append("\n" + "3	" + GetMyLevelData(FreshLevels.Type.NC_1));
							sb.Append("\n" + "4	" + GetMyLevelData(FreshLevels.Type.GodOfDeath));
							sb.Append("\n" + "5	" + GetMyLevelData(FreshLevels.Type.NC_2));
							sb.Append("\n" + "6	" + GetMyLevelData(FreshLevels.Type.NC_4));
							sb.Append("\n" + "7	" + GetMyLevelData(FreshLevels.Type.DoggoLevel1));
							sb.Append("\n" + "8	" + GetMyLevelData(FreshLevels.Type.NC_5));
							sb.Append("\n" + "9	" + GetMyLevelData(FreshLevels.Type.NC_6));
							sb.Append("\n" + "10	" + GetMyLevelData(FreshLevels.Type.NC_7));
							sb.Append("\n" + "11	" + GetMyLevelData(FreshLevels.Type.MerpsVillage));
							sb.Append("\n" + "12	" + GetMyLevelData(FreshLevels.Type.SlimeDaddy_TentaclesGalore));
							sb.Append("\n" + "13	" + GetMyLevelData(FreshLevels.Type.SlimeDaddy_BatterUp));

							items[a].otherLabels[1].text = sb.ToString();

							//Column 3
							sb.Length = 0;
							sb.Append("1	" + GetMyLevelData(FreshLevels.Type.TO_2));
							sb.Append("\n" + "2	" + GetMyLevelData(FreshLevels.Type.Pink2));
							sb.Append("\n" + "3	" + GetMyLevelData(FreshLevels.Type.FridayLevel));
							sb.Append("\n" + "4	" + GetMyLevelData(FreshLevels.Type.ZooLevel));
							sb.Append("\n" + "5	" + GetMyLevelData(FreshLevels.Type.Blue1));
							sb.Append("\n" + "6	" + GetMyLevelData(FreshLevels.Type.SwordArtOF));
							sb.Append("\n" + "7	" + GetMyLevelData(FreshLevels.Type.Blue2));
							sb.Append("\n" + "8	" + GetMyLevelData(FreshLevels.Type.UpwardsHell));
							sb.Append("\n" + "9	" + GetMyLevelData(FreshLevels.Type.MegaSatan1));
							sb.Append("\n" + "10	" + GetMyLevelData(FreshLevels.Type.TR_2));
							sb.Append("\n" + "11	" + GetMyLevelData(FreshLevels.Type.TR_3));
							sb.Append("\n" + "12	" + GetMyLevelData(FreshLevels.Type.TR_4));
							sb.Append("\n" + "13	" + GetMyLevelData(FreshLevels.Type.TR_5));
							sb.Append("\n" + "14	" + GetMyLevelData(FreshLevels.Type.JamLvl1));
							sb.Append("\n" + "15	" + GetMyLevelData(FreshLevels.Type.JamLvl2));
							sb.Append("\n" + "16	" + GetMyLevelData(FreshLevels.Type.JamLvl3));

							items[a].otherLabels[2].text = sb.ToString();


							return;
					}


				}
			}
		}

		UpdateTextBox();
		HandleCursor();

	}

	public void UpdateTextBox()
	{
		if (inTextBox)
		{
			if (textBoxIndex != -1)
			{
				GetTypingInput();
				items[textBoxIndex].otherLabels[0].text = textBoxStr + flasher;
			}

			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
			{
				items[textBoxIndex].otherLabels[0].text = textBoxStr;


				if (textBoxType == TextBoxType.Username)
				{
					//Set username (should actually wait for server to check this)
					fa.username = textBoxStr;
					FrFuncs.SetUsername(fa.username);
					PlayerPrefs.SetString("username", fa.username);
					PlayerPrefs.Save();

					inTextBox = false;
					textBoxIndex = -1;
					textBoxStr = "";


					for (int i = 0; i < items.Count; i++)
					{
						if (items[i].uId == RawInfo.ButtonUID.UsernameMenu_DisplayUsername)
						{
							items[i].otherLabels[0].text = fa.username;
						}
					}
					MenuOn(RawInfo.MenuType.UsernameMenu);
				}

				if (textBoxType == TextBoxType.Token)
				{
					//Set username
					fa.tempToken = textBoxStr;
					fa.checkToken = true;
					FrFuncs.ValidateTokenAndSet(fa.tempToken);

					inTextBox = false;
					textBoxIndex = -1;
					textBoxStr = "";

					for (int i = 0; i < items.Count; i++)
					{
						if (items[i].uId == RawInfo.ButtonUID.WaitingForValidationMenu_Display)
						{
							items[i].otherLabels[0].text = fa.tempToken + "\n\nValidating with server...";
						}
					}
					MenuOn(RawInfo.MenuType.WaitingForValidationMenu);
				}

			}

		}
	}

	public void GetTypingInput()
	{
		if (Time.time > (textBoxFlasherTimeSet + textBoxFlasherDelay))
		{
			textBoxFlasherTimeSet = Time.time;
			if (flasherFlip)
			{
				flasher = " ";
			}
			else
			{
				flasher = "_";
			}
			flasherFlip = !flasherFlip;


		}
		string s = null;
		s = Fetch();
		if (s != null)
		{
			if (textBoxStr.Length < 16)
			{
				textBoxStr += s;
			}
		}

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			if ((textBoxStr.Length - 1) >= 0)
			{
				textBoxStr = textBoxStr.Remove(textBoxStr.Length - 1);
			}
			else
			{
				textBoxStr = "";
			}
		}
	}
	public bool Caps()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { return true; }
		return false;
	}
	public string Fetch()
	{
		if (Input.GetKeyDown(KeyCode.A)) { if (Caps()) { return "A"; } else { return "a"; } }
		if (Input.GetKeyDown(KeyCode.B)) { if (Caps()) { return "B"; } else { return "b"; } }
		if (Input.GetKeyDown(KeyCode.C)) { if (Caps()) { return "C"; } else { return "c"; } }
		if (Input.GetKeyDown(KeyCode.D)) { if (Caps()) { return "D"; } else { return "d"; } }
		if (Input.GetKeyDown(KeyCode.E)) { if (Caps()) { return "E"; } else { return "e"; } }
		if (Input.GetKeyDown(KeyCode.F)) { if (Caps()) { return "F"; } else { return "f"; } }
		if (Input.GetKeyDown(KeyCode.G)) { if (Caps()) { return "G"; } else { return "g"; } }
		if (Input.GetKeyDown(KeyCode.H)) { if (Caps()) { return "H"; } else { return "h"; } }
		if (Input.GetKeyDown(KeyCode.I)) { if (Caps()) { return "I"; } else { return "i"; } }
		if (Input.GetKeyDown(KeyCode.J)) { if (Caps()) { return "J"; } else { return "j"; } }
		if (Input.GetKeyDown(KeyCode.K)) { if (Caps()) { return "K"; } else { return "k"; } }
		if (Input.GetKeyDown(KeyCode.L)) { if (Caps()) { return "L"; } else { return "l"; } }
		if (Input.GetKeyDown(KeyCode.M)) { if (Caps()) { return "M"; } else { return "m"; } }
		if (Input.GetKeyDown(KeyCode.N)) { if (Caps()) { return "N"; } else { return "n"; } }
		if (Input.GetKeyDown(KeyCode.O)) { if (Caps()) { return "O"; } else { return "o"; } }
		if (Input.GetKeyDown(KeyCode.P)) { if (Caps()) { return "P"; } else { return "p"; } }
		if (Input.GetKeyDown(KeyCode.Q)) { if (Caps()) { return "Q"; } else { return "q"; } }
		if (Input.GetKeyDown(KeyCode.R)) { if (Caps()) { return "R"; } else { return "r"; } }
		if (Input.GetKeyDown(KeyCode.S)) { if (Caps()) { return "S"; } else { return "s"; } }
		if (Input.GetKeyDown(KeyCode.T)) { if (Caps()) { return "T"; } else { return "t"; } }
		if (Input.GetKeyDown(KeyCode.U)) { if (Caps()) { return "U"; } else { return "u"; } }
		if (Input.GetKeyDown(KeyCode.V)) { if (Caps()) { return "V"; } else { return "v"; } }
		if (Input.GetKeyDown(KeyCode.W)) { if (Caps()) { return "W"; } else { return "w"; } }
		if (Input.GetKeyDown(KeyCode.X)) { if (Caps()) { return "X"; } else { return "x"; } }
		if (Input.GetKeyDown(KeyCode.Y)) { if (Caps()) { return "Y"; } else { return "y"; } }
		if (Input.GetKeyDown(KeyCode.Z)) { if (Caps()) { return "Z"; } else { return "z"; } }

		if (Input.GetKeyDown(KeyCode.Alpha1)) { return "1"; }
		if (Input.GetKeyDown(KeyCode.Alpha2)) { return "2"; }
		if (Input.GetKeyDown(KeyCode.Alpha3)) { return "3"; }
		if (Input.GetKeyDown(KeyCode.Alpha4)) { return "4"; }
		if (Input.GetKeyDown(KeyCode.Alpha5)) { return "5"; }
		if (Input.GetKeyDown(KeyCode.Alpha6)) { return "6"; }
		if (Input.GetKeyDown(KeyCode.Alpha7)) { return "7"; }
		if (Input.GetKeyDown(KeyCode.Alpha8)) { return "8"; }
		if (Input.GetKeyDown(KeyCode.Alpha9)) { return "9"; }
		if (Input.GetKeyDown(KeyCode.Alpha0)) { return "0"; }



		return null;
	}

	public static string GetMyLevelData(FreshLevels.Type lvlType)
	{
		for (int b = 0; b < UsageStatsFunc.lvlStats.Count; b++)
		{
			if (UsageStatsFunc.lvlStats[b].levelType == lvlType)
			{
				List<float> times = new List<float>();
				float averageTime = 0;
				float highestTime = 0;

				//now find all of my times
				for (int c = 0; c < UsageStatsFunc.lvlStats[b].names.Count; c++)
				{
					if (UsageStatsFunc.lvlStats[b].names[c] == "thegamedesigner")
					{
						times.Add(UsageStatsFunc.lvlStats[b].times[c]);
						if (UsageStatsFunc.lvlStats[b].times[c] > highestTime) { highestTime = UsageStatsFunc.lvlStats[b].times[c]; }
					}
				}
				if (times.Count > 0)
				{
					for (int c = 0; c < times.Count; c++)
					{
						averageTime += times[c];
					}
					averageTime /= times.Count;
				}
				return "" + lvlType + "		" + highestTime;
			}

		}
		return "" + lvlType + "	" + "0";
	}

	public static float[] GetScales()
	{
		float[] result = new float[100];
		for (int i = 0; i < result.Length; i++)
		{
			result[i] = Random.Range(1f, 10f);
		}
		return result;
	}

	public void UpdateAllListedDisplaysIfPossible()
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.NetworkingMenu_SendUsageStats) { SetDisplay(i, sendUsageStats); }
			//if (items[i].uId == RawInfo.ButtonUID.Controls_SwitchMode) { SetDisplay(i, Controls.useCustom); }
			if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_ToggleTimer) { SetDisplay(i, (fa.showSpeedrunTimer == 1)); }
			if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_ToggleDeathCounter) { SetDisplay(i, (fa.showDeathCounter == 1)); }
			if (items[i].uId == RawInfo.ButtonUID.GameplayMenu_PGMode) { SetDisplay(i, xa.pgMode); }
		}
	}

	public void SetAchivoDetailText(string str)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.Achivo_Details)
			{
				items[i].controllerGO.GetComponent<TextMesh>().text = str;
			}
		}
	}

	void HandleCursor()
	{
		if (inTextBox)
		{
			cursor.transform.position = new Vector3(999, 999, 999);
			return;
		}
		Handle4DirCursor();
		/*
		//Handle cursor for mouse
		Vector2 mPos = Input.mousePosition;
		if (mPos.x != oldMousePos.x || mPos.y != oldMousePos.y)
		{
			//mouse has moved. Use mouse controls
			HandleMouseCursor();
		}
		else
		{
			//mouse has not moved. Use keyboard/controller controls
		}
		oldMousePos = mPos;
		*/

		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].selectionThing != null)
			{
				if (items[i].hasCursor)
				{
					items[i].selectionThing.SetActive(true);
				}
				else
				{
					items[i].selectionThing.SetActive(false);
				}
			}
		}
	}
	/*
	void HandleMouseCursor()
	{
		Vector2 mPos = Input.mousePosition;
		Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		Vector3 worldPos = ray.GetPoint(2);

		//Check which item pos is closest to world pos

		worldPos.z = m.cursor.transform.position.z;
		//Which item is closest to the worldPos?
		int closest = -1;
		float dist = 9999;
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < m.items.Length; i++)
		{
			worldPos.z = m.items[i].cursorPosObj.transform.position.z;
			float result = Vector3.Distance(m.items[i].cursorPosObj.transform.position, worldPos);
			if (result < dist)
			{
				closest = i;
				dist = result;
				pos = m.items[i].cursorPosObj.transform.position;
			}
		}
		Debug.Log(dist);
		Debug.DrawLine(worldPos, pos, Color.white);
		pos.z = m.cursor.transform.position.z;
		m.cursor.transform.position = pos;

		//m.cursor.transform.position = worldPos;
	}*/

	void Handle4DirCursor()
	{
		//move the cursor for this menu
		float z = cursor.transform.position.z;
		Vector3 pos = cursor.transform.position;
		int target = -1;
		int me = -1;

		for (int a = 0; a < items.Count; a++)
		{
			//find cursor currently
			if (items[a].controllerGO.activeSelf && items[a].hasCursor)
			{
				//found the cursor currently
				target = a;
				me = a;
				if (Controls.GetInputDown(Controls.Type.MenuUp, 0) && items[a].up != RawInfo.ButtonUID.None) { target = GetIndexForUID(items[a].up); }
				if (Controls.GetInputDown(Controls.Type.MenuDown, 0) && items[a].down != RawInfo.ButtonUID.None) { target = GetIndexForUID(items[a].down); }
				if (Controls.GetInputDown(Controls.Type.MenuLeft, 0) && items[a].left != RawInfo.ButtonUID.None) { target = GetIndexForUID(items[a].left); }
				if (Controls.GetInputDown(Controls.Type.MenuRight, 0) && items[a].right != RawInfo.ButtonUID.None) { target = GetIndexForUID(items[a].right); }
			}
		}

		if (target != -1)
		{
			pos = items[target].cursorPosObj.transform.position;
			items[me].hasCursor = false;
			items[target].hasCursor = true;
			pos.z = cursorZ;
			cursor.transform.position = pos;
			cursor.transform.SetScaleX(items[target].cursorPosObj.transform.localScale.x + 0.3f);
			cursor.transform.SetScaleY(items[target].cursorPosObj.transform.localScale.y + 0.3f);

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_AllasKlar)
			{ SetAchivoDetailText("Find the secret area in \"Dark Bargain\""); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_DaddysLove)
			{ SetAchivoDetailText("Win \"Batter Up!\" with Maximum Daddy Love"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_DontStompa)
			{ SetAchivoDetailText("Beat \"Very Pink\" without using Stomping"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_MagicMonk)
			{ SetAchivoDetailText("Beat \"Sticky Remains\" without using the 3rd Jump"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_Routes66)
			{ SetAchivoDetailText("Beat \"Minivania\" in under 2 minutes"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_Cheater)
			{ SetAchivoDetailText("On \"Sticky Endings\", get the Air-Sword ability\nwhile already having the Double-Jump ability"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_Reverso)
			{ SetAchivoDetailText("On \"Sticky Endings\", get the Double-Jump ability\nwhile already having the Air-Sword ability"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_Champion)
			{ SetAchivoDetailText("Win the game"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_NoThanksImGood)
			{ SetAchivoDetailText("Win \"A Jumping Massacre\" without using teleporters"); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_MitLiebeGemacht)
			{ SetAchivoDetailText("Win \"Alex's Love Letter\""); }

			if (items[target].buttonFunc == RawInfo.ButtonFunc.Achivo_GoinFastImTowerBound)
			{ SetAchivoDetailText("Defeat the Shadow Monster"); }
		}
	}


	public void SetDLCText()
	{

		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].uId == RawInfo.ButtonUID.MainMenu_VerNum)
			{
				if (xa.hasBonusDLC) { items[i].otherLabels[1].text = "Gold Editon DLC"; }
				if (xa.hasAlpDLC) { items[i].otherLabels[1].text = "Groove Wizard's Tower DLC"; }
				if (xa.hasBonusDLC && xa.hasAlpDLC) { items[i].otherLabels[1].text = "All DLC"; }
				if (!xa.hasBonusDLC && !xa.hasAlpDLC) { items[i].otherLabels[1].text = "NO DLC"; }
			}
		}

	}

	public void MenuOn(RawInfo.MenuType menuType)
	{
		Debug.Log("MenuOn: " + menuType);
		AllMenusOff();

		bool foundAtLeastOne = false;
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].menuType == menuType)
			{
				items[i].controllerGO.SetActive(true);

				//If turning on the InGameHub's PG mode, update it's display
				if (items[i].uId == RawInfo.ButtonUID.InGameHub_PGMode) { SetDisplay(i, xa.pgMode); }
				//if (items[i].uId == RawInfo.ButtonUID.Controls_SwitchMode) { SetDisplay(i, Controls.useCustom); }

				foundAtLeastOne = true;
			}
		}
		Debug.Log("FoundAnything?: " + foundAtLeastOne);
		if (!foundAtLeastOne)
		{
			for (int i = 0; i < items.Count; i++)
			{
				Debug.Log("" + items[i].menuType);
			}
		}
		if (foundAtLeastOne)
		{
			RawInfo.currentMenu = menuType;
			UpdateAllListedDisplaysIfPossible();
			cursor.SetActive(true);
			InRawMenu = true;


			if (menuType == RawInfo.MenuType.MainMenu)
			{
				UpdateGoldenButtDisplay();
				UpdatePuppersDisplay();
				UpdateAchivoDisplay();
				UpdateCoinDisplay();

				for (int i = 0; i < items.Count; i++)
				{
					if (items[i].uId == RawInfo.ButtonUID.MainMenu_VerNum)
					{
						items[i].otherLabels[0].text = "" + xa.verStr;

						if (xa.hasBonusDLC) { items[i].otherLabels[1].text = "Gold Editon DLC"; }
						if (xa.hasAlpDLC) { items[i].otherLabels[1].text = "Groove Wizard's Tower DLC"; }
						if (xa.hasBonusDLC && xa.hasAlpDLC) { items[i].otherLabels[1].text = "Groove + Gold DLC"; }
						if (!xa.hasBonusDLC && !xa.hasAlpDLC) { items[i].otherLabels[1].text = "NO DLC"; }

					}

					if (items[i].uId == RawInfo.ButtonUID.MainMenu_CurrentLeaderboard)
					{
						mainMenuLeaderboardIndex = i;
					}

				}

				//load current leaderboard

				UpdateQCMainMenu();
				//For previous leader board system
				//FrFuncs.GetLeaderboard(leaderboardLevel);
				//FrFuncs.QC_GetLeaderboardTimeSlot(leaderboardLevel);

			}



			if (menuType == RawInfo.MenuType.LeaderboardsMenu)
			{
				for (int i = 0; i < items.Count; i++)
				{
					if (items[i].uId == RawInfo.ButtonUID.Leaderboards_Leaderboard)
					{
						LeaderboardItemIndex = i;
					}
				}
				leaderboardLevel = FreshLevels.Type.Tut_JumpAndAirsword;
				leaderboardIndex = 0;
				fa.leaderboardTitle = "" + leaderboardIndex + ". " + FreshLevels.GetStrLabelForType(leaderboardLevel);
				QCAskType = leaderboardLevel;
				//AskForLeaderboard(leaderboardLevel);
			}

			if (menuType == RawInfo.MenuType.InGameHub)
			{
				for (int i = 0; i < items.Count; i++)
				{
					if (items[i].menuType == RawInfo.MenuType.InGameMenuBackground)
					{
						items[i].controllerGO.SetActive(true);
					}
				}
			}
			else
			{
				for (int i = 0; i < items.Count; i++)
				{
					if (items[i].menuType == RawInfo.MenuType.MenuBackground)
					{
						items[i].controllerGO.SetActive(true);
					}
				}
			}

		}
	}

	public void AllMenusOff()
	{
		cursor.SetActive(false);
		for (int i = 0; i < items.Count; i++)
		{
			items[i].controllerGO.SetActive(false);
			if (items[i].selectionThing != null) { items[i].selectionThing.SetActive(false); }
		}

	}

	public void SetDisplay(int i, bool state)
	{
		if (items[i].displayOFF == null || items[i].displayON == null) { return; }

		if (state)
		{
			items[i].displayON.SetActive(true);
			items[i].displayOFF.SetActive(false);
		}
		else
		{
			items[i].displayON.SetActive(false);
			items[i].displayOFF.SetActive(true);
		}
	}

	public static void Print(string str)
	{
		if (RawFuncs.self)
		{
			if (RawFuncs.self.DebugText.text.Length < 5000)
			{
				RawFuncs.self.DebugText.text += "\n" + str;
			}
		}
	}

	public static void WipePrint()
	{
		if (RawFuncs.self)
		{
			RawFuncs.self.DebugText.text = "";
		}
	}

}
