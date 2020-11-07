using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsageStatsFunc : MonoBehaviour
{
	public static List<TotalPoint> lvlStats;

	public static string[] unparsedStr;
	public static bool[] unparsedBool;
	public static int[] unparsedInt;

	public class UnparsedData
	{
		public bool done = false;
		public string s;
		public FreshLevels.Type lvlType;
	}

	public class TotalPoint
	{
		public FreshLevels.Type levelType = FreshLevels.Type.None;
		public float average = 0;
		public float players = 0;
		public List<float> times = new List<float>();
		public List<string> names = new List<string>();
		public string rawData;

		public void Calc()
		{
			//calc (re-calc) the average time
			average = 0;
			for (int i = 0; i < times.Count; i++)
			{
				average += times[i];
			}
			average /= times.Count;

			players = names.Count;
		}
	}

	public static void InitUsageStats()
	{

		//Do I have a saved username already?
		string tempName = "tempName";
		if (PlayerPrefs.HasKey("savedUsername"))
		{
			tempName = PlayerPrefs.GetString("savedUsername");//Security doesn't matter, because this is just usage stats, which only matter to the dev
		}
		else
		{
			//then just generate a random name to save
			tempName = "user";
			tempName += "" + Random.Range(0, 10);
			tempName += "" + Random.Range(0, 10);
			tempName += "" + Random.Range(0, 10);
			tempName += "" + Random.Range(0, 10);

		}
		PlayerPrefs.SetString("usageStatName", tempName);
		PlayerPrefs.Save();

		if (RawFuncs.sendUsageStats)
		{

		}
	}

	public static void SendUsageStats(string lvlName, float time)
	{
		string name = PlayerPrefs.GetString("usageStatName", "nameNotFound");
		RemoteData.ReportUsageStat(name, lvlName, time.ToString(), (data) =>
		  {
		  });

	}

	public static void GetAllLvls()
	{
		unparsedBool = new bool[100];
		unparsedInt = new int[100];
		unparsedStr = new string[100];
		lvlStats = new List<TotalPoint>();

		GetALvl("Tut_SecondAttempt");
		GetALvl("Pink1");
		GetALvl("Pink2");
		GetALvl("SamuariSword1");
		GetALvl("SamuariSword2");
		GetALvl("Blue1");
		GetALvl("Blue2");
		GetALvl("Ninja1");
		GetALvl("Ninja2");
		GetALvl("Ninja3");
		GetALvl("TinyTut_PortalRulesSecondAttempt");
		GetALvl("WelcomeToTheJungle");
		GetALvl("TinyTut_StickyWalls");
		GetALvl("Tut_DoubleJump");
		GetALvl("GodOfDeath");
		GetALvl("JamLvl3");
		GetALvl("IntroStory");
		GetALvl("FPS_level1");
		GetALvl("UpwardsHell");
		GetALvl("MusicLvl1");
		GetALvl("MusicLvl2");
		GetALvl("FridayLevel");
		GetALvl("DoggoLevel1");
		GetALvl("SewerButt");
		GetALvl("MissileBoostLevel");
		GetALvl("Santa1");
		GetALvl("Santa2");
		GetALvl("Santa3");
		GetALvl("FPS_hallway");
		GetALvl("JamLvl2");
		GetALvl("SwordArtOF");
		GetALvl("NC_1");
		GetALvl("NC_2");
		GetALvl("NC_3");
		GetALvl("NC_4");
		GetALvl("NC_5");
		GetALvl("NC_6");
		GetALvl("NC_7");
		GetALvl("ZooLevel");
		GetALvl("TO_1");
		GetALvl("TO_2");
		GetALvl("TR_1");
		GetALvl("ThreeDeeLevel");
		GetALvl("Tut_Stomp");
		GetALvl("MegaSatan1");
		GetALvl("TR_2");
		GetALvl("TR_3");
		GetALvl("TR_4");
		GetALvl("TR_5");
		GetALvl("SlimeDaddy_BatterUp");
		GetALvl("SlimeDaddy_TentaclesGalore");
		GetALvl("MerpsVillage");
	}

	public static void UpdateUnparsed()
	{
		for (int i = 0; i < unparsedBool.Length; i++)
		{
			if (unparsedBool[i])
			{
				unparsedBool[i] = false;
				ParseData((FreshLevels.Type)unparsedInt[i], unparsedStr[i]);
			}
		}
	}

	public static void CheckStatus()
	{
		if (lvlStats != null)
		{
			Debug.Log("Amount: " + lvlStats.Count);
		}
		else
		{
			Debug.Log("is Null");
		}
	}


	public static void GetALvl(string lvlName)
	{
		//Get the enum from the string
		FreshLevels.Type lvlType = FreshLevels.Type.None;
		lvlType = FreshLevels.GetTypeNameForStr(lvlName);
		if (lvlType != FreshLevels.Type.None)
		{
			RemoteData.GetUsageStats(lvlName, (data) =>
				{
					if (data != "error")
					{
						 //Debug.Log("Writing unparsed for " + lvlName);
						for (int i = 0; i < unparsedBool.Length; i++)
						{
							if (!unparsedBool[i])
							{
								unparsedBool[i] = true;
								unparsedStr[i] = data;
								unparsedInt[i] = (int)lvlType;
								break;
							}
						}
					}
				});

		}
	}

	public static void ParseData(FreshLevels.Type lvlType, string data)
	{
		if(data == null) {return; }
		if (data.Length < 3) { return; } // must be rubbish data, white space, to be so short

		//Debug.Log("HERE1");

		//find or create the totalData and recalc it
		int lvlIndex = -1;
		for (int i = 0; i < lvlStats.Count; i++)
		{
			if (lvlStats[i].levelType == lvlType)
			{
				lvlIndex = i;
			}
		}
		if (lvlIndex == -1)
		{
			//create it
			TotalPoint tp = new TotalPoint();
			tp.levelType = lvlType;
			lvlIndex = lvlStats.Count;
			lvlStats.Add(tp);
		}
		//Debug.Log("RAW DATA: " + lvlType + ", " + data);

		//Ok, now parse the data
		string[] chunks = data.Split(',');
		//Debug.Log("HERE2 " + chunks.Length);

		List<string> names = new List<string>();
		List<float> times = new List<float>();

		for (int d = 0; d < chunks.Length; d++)
		{
			if (chunks[d].Length < 3) { continue; }
			//Debug.Log("HERE3 " + chunks[d]);
			string[] bits = chunks[d].Split(':');
			names.Add(bits[0]);
			float r = -1;
			float.TryParse(bits[1], out r);
			times.Add(r);

		}
		//Debug.Log("HERE 4");



		for (int i = 0; i < names.Count; i++)
		{
			//Debug.Log(names[i]);
			lvlStats[lvlIndex].names.Add(names[i]);
			
			bool unique = true;
			for (int a = 0; a < lvlStats[lvlIndex].names.Count; a++)
			{
				if (lvlStats[lvlIndex].names[a] == names[i])
				{
					unique = false;
				}
			}
			if (unique)
			{
				Debug.Log(names[i]);
				lvlStats[lvlIndex].names.Add(names[i]);
			}
		}

		for (int i = 0; i < times.Count; i++)
		{
		//	Debug.Log(times[i]);
			lvlStats[lvlIndex].times.Add(times[i]);
		}
		/*
		for (int d = 0; d < chunks.Length; d++)
		{
			string[] bits = chunks[d].Split(':');
			
			Debug.Log("HERE5 " + chunks[d]);
			Debug.Log("HERE6 " + bits[0] + "--" + bits[1] + ", Chunk length: " + chunks.Length);



			lvlStats[lvlIndex].times.Add(float.Parse(bits[1]));


			//check if name already exists, if so, don't add it
			bool unique = true;
			for (int a = 0; a < lvlStats[lvlIndex].names.Count; a++)
			{
				if (lvlStats[lvlIndex].names[a] == bits[0])
				{
					unique = false;
				}
			}
			if (unique)
			{
				lvlStats[lvlIndex].names.Add(bits[0]);
			}
		}*/
		lvlStats[lvlIndex].rawData = data;
		lvlStats[lvlIndex].Calc();

	}


}
