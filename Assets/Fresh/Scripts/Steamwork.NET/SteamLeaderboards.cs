using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamLeaderboards : MonoBehaviour
{
	public static bool initd = false;
	public static int totalNumOfLeaderboards = 59;//current total of supported leaderboards
	public static string[] leaderboardLabels;
	public static FreshLevels.Type[] leaderboardTypes;
	public static float SteamTickDelay = 1;
	public static float SteamTickTimeSet;

	private const ELeaderboardUploadScoreMethod s_leaderboardMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;
	private static CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>();
	private static CallResult<LeaderboardScoreUploaded_t> m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>();

	public static List<Item> queue;
	public static float queueTimeSet = 0;
	public static float queueDelay = 20;//20 seconds
	public static float queueGiveup = 900;//15 minutes




	public class Item
	{
		public bool open = false;
		public bool hasRef = false;
		public bool waitingForRef = false;
		public bool waitingForUpload = false;
		public bool finished = false;
		public float timespent = 0;
		public int score = -1;
		public string leaderboard = "";
		public SteamLeaderboard_t steamRef;
		public FreshLevels.Type type = FreshLevels.Type.None;
	}

	public static void UpdateQueue()
	{
		if (fa.dontConnectSteam) { return; }
		if (!SteamManager.Initialized) { return; }

		//loop through, find the open one, update that.
		for (int i = 0; i < queue.Count; i++)
		{
			if (queue[i].open && !queue[i].finished)
			{
				queue[i].timespent += queueDelay;
				if (queue[i].timespent > queueGiveup)
				{
					//giving up
					RawFuncs.Print("Giving up on " + queue[i].leaderboard);
					queue[i].open = false;

				}
				else
				{
					UpdateItem(queue[i]);
				}
				return;
			}
		}

		//if it didn't find one, open one
		for (int i = 0; i < queue.Count; i++)
		{
			if (!queue[i].finished && queue[i].timespent < queueGiveup)//if I haven't already spent enough time attempting this one
			{
				RawFuncs.Print("Opening new item for " + queue[i].leaderboard);
				queue[i].open = true;
				return;
			}
		}
	}

	public static void UpdateItem(Item item)
	{
		if (fa.dontConnectSteam) { return; }
		if (!SteamManager.Initialized) { return; }
		//whats the next step for this item?


		//no reference? Ask steam for one, and return out
		if (!item.hasRef && !item.waitingForRef)
		{
			item.waitingForRef = true;
			RawFuncs.Print("1. Requesting: " + item.leaderboard);

			SteamAPICall_t hSteamAPICall = SteamUserStats.FindLeaderboard(item.leaderboard);
			m_findResult.Set(hSteamAPICall, OnFindLeaderboardResult);

			return;
		}

		//does it have a reference?
		if (item.hasRef && !item.waitingForUpload)
		{
			item.waitingForUpload = true;
			RawFuncs.Print("3. uploading score(" + item.score + ") to steam leaderboard(" + item.leaderboard + ")");
			SteamAPICall_t hSteamAPICall = SteamUserStats.UploadLeaderboardScore(item.steamRef, s_leaderboardMethod, item.score, null, 0);
			m_uploadResult.Set(hSteamAPICall, OnLeaderboardUploadResult);
		}
	}

	public static void AddToQueue(FreshLevels.Type type, float speedruntime)
	{
		//Fail out, if:
		if (fa.dontConnectSteam) { return; }
		if (!SteamManager.Initialized) { return; }
		if (speedruntime < 2) { return; }//less than 2 seconds is always impossible
		if (speedruntime < FreshLevels.GetImpossibleTimeForLevel(type)) { return; }//time was impossible
		if(FreshLevels.DoNotSyncLevel(type)) {return; }//don't sync this level if it's got a secret exit or other reason to not keep old scores

		Item a = new Item();
		a.leaderboard = FreshLevels.GetStrictLabelForType(type);
		a.score = Mathf.FloorToInt(speedruntime * 1000);
		a.type = type;
		queue.Add(a);
	}


	static private void OnFindLeaderboardResult(LeaderboardFindResult_t pCallback, bool failure)
	{
		RawFuncs.Print("2. " + Time.time + " STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard);
		for (int i = 0; i < queue.Count; i++)
		{
			if (queue[i].open)
			{
				if (queue[i].waitingForRef && !queue[i].hasRef)
				{
					queue[i].hasRef = true;
					queue[i].steamRef = pCallback.m_hSteamLeaderboard;
				}
				else
				{
					//the open one isn't waiting/already has a ref, which means it's probably the wrong one?
					//shoulnd't ever get here, but probably the packet got lost, then showed up later?

					//just ignore this result

					//(possibly kill this one, and add to it's timespent, as it might be better to move on?)
				}

				return;
			}
		}
		return;



		//RawFuncs.Print("" + Time.time + " STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard);
		//leaderboardSteamRefs[requestIndex] = pCallback.m_hSteamLeaderboard;
		//leaderboardFound[requestIndex] = true;
		//requestIndex++;
		//waiting = false;

	}

	static private void OnLeaderboardUploadResult(LeaderboardScoreUploaded_t pCallback, bool failure)
	{
		RawFuncs.Print("4. STEAM LEADERBOARDS: failure - " + failure + " Completed - " + pCallback.m_bSuccess + " NewScore: " + pCallback.m_nGlobalRankNew + " Score " + pCallback.m_nScore + " HasChanged - " + pCallback.m_bScoreChanged);

		for (int i = 0; i < queue.Count; i++)
		{
			if (queue[i].open)
			{
				if (queue[i].waitingForUpload)
				{
					if (!failure)
					{
						queue[i].finished = true;
						queue[i].open = false;
						RawFuncs.Print("5. Success!");
						PlayerPrefs.SetInt("Syncd_" + queue[i].type, 1);
						PlayerPrefs.Save();
					}
					else
					{
						queue[i].finished = false;
						queue[i].waitingForUpload = false;//try again
						RawFuncs.Print("5b. Trying again...");

					}
				}
				return;
			}
		}

	}

	public static void SendPBToSteam(FreshLevels.Type type, float speedruntime)
	{




		//Fail out, if:
		if (fa.dontConnectSteam) { return; }
		if (!SteamManager.Initialized) { return; }
		if (speedruntime < 2) { return; }//less than 2 seconds is always impossible
		if (speedruntime < FreshLevels.GetImpossibleTimeForLevel(type)) { return; }//time was impossible
		if(FreshLevels.NotALeaderboardLevel(type)) {return; }//There are / should be no leaderboards for this level

		AddToQueue(type, speedruntime);
	}

	public static void Init()
	{
		RawFuncs.Print("Steam init! TestVer: 003");

		SteamStatsAndAchievements.WhatsMySteamName();
		leaderboardLabels = new string[totalNumOfLeaderboards];
		leaderboardTypes = new FreshLevels.Type[totalNumOfLeaderboards];
		queue = new List<Item>();

		for (int i = 0; i < totalNumOfLeaderboards; i++)
		{
			leaderboardTypes[i] = FreshLevels.IndexForLevelLeaderboard(i);
			bool syncThis = true;
			if (PlayerPrefs.HasKey("Syncd_" + leaderboardTypes[i]))
			{
				int result = 0;
				result = PlayerPrefs.GetInt("Syncd_" + leaderboardTypes[i], 0);
				if (result == 1)
				{
					syncThis = false;
				}
			}

			if (syncThis)
			{
				float pb = PlayerPrefs.GetFloat("LevelTime_" + leaderboardTypes[i], -1);
				AddToQueue(FreshLevels.IndexForLevelLeaderboard(i), pb);

				RawFuncs.Print("" + leaderboardTypes[i] + ": " + pb + ", added to queue");
			}





		}

	}

	public static void SteamAPITickUpdate()//called once a second
	{
		if (fa.dontConnectSteam) { return; }
		if (!SteamManager.Initialized) { return; }

		if (!initd) { initd = true; Init(); return; }

		if (Time.time > (SteamTickDelay + SteamTickTimeSet))
		{
			SteamTickTimeSet = Time.time;
			SteamAPI.RunCallbacks();

		}

		if (Time.time > (queueTimeSet + queueDelay))
		{
			queueTimeSet = Time.time;
			UpdateQueue();
		}


	}

}
