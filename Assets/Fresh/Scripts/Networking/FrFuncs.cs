using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrFuncs : MonoBehaviour
{
	public class Result
	{
		public string data = null;
		public Type type = Type.None;
		public bool done = false;
	}

	public static List<Result> results = new List<Result>();

	public enum Type
	{
		None,
		Generic,
		NewToken,
		ReportedLevelTime,
		SetUsername,
		GetLeaderboard,
		QC_GetLeaderboard,
		ValidateToken,
		ValidateTokenAndSolve,
		ValidateTokenAndSet,
		End
	}

	public static void Init()
	{
		if(fa.dontConnect3rdParty) {return; }
		results = new List<Result>();
		if (PlayerPrefs.HasKey("frtoken"))
		{
			//validate it
			fa.token = PlayerPrefs.GetString("frtoken", null);
			Debug.Log("Old token: " + fa.token);

			ValidateTokenAndSolve(fa.token);

		}
		else
		{
			AskForToken();
		}


	}

	public static void AddResult(string data, Type type)
	{
		Result r = new Result();
		r.data = data;
		r.type = type;
		results.Add(r);
	}

	public static void SetUsername(string username)
	{
		if(fa.dontConnect3rdParty) {return; }
		FrRemoteData.Fr_SetUsername(username, (data) =>
		   {
			   AddResult(data, Type.Generic);
		   });
	}

	public static void AskForToken()
	{
		if(fa.dontConnect3rdParty) {return; }
		FrRemoteData.Fr_AskForToken((data) =>
		  {
			  AddResult(data, Type.NewToken);
		  });
	}
	
	public static void ReportLevelTime(FreshLevels.Type type, float levelTime)
	{
		if(fa.dontConnect3rdParty) {return; }
		if(fa.cheater) {return; }
		FrRemoteData.Fr_ReportLevelTime("" + type, levelTime, (data) =>
		  {
			  AddResult(data, Type.Generic);
		  });
	}

	public static void Qc_ReportLevelTime(FreshLevels.Type type, float levelTime, string ghostData)
	{
		if(fa.dontConnect3rdParty) {return; }
		if(fa.cheater) {return; }
		if(FreshLevels.NotALeaderboardLevel(type)) {return; }//There are / should be no leaderboards for this level

		Debug.Log("Reported QC level time");
		FrRemoteData.Qc_ReportLevelTime("" + type, levelTime, ghostData, (data) =>
		  {
			  AddResult(data, Type.Generic);
		  });
	}
	
	public static void GetLeaderboard(FreshLevels.Type type)
	{
		if(fa.dontConnect3rdParty) {return; }
		Debug.Log("Trying to get leaderboard");
		FrRemoteData.Fr_GetLeaderboard("" + type, fa.token, (data) =>
		  {
			  AddResult(data, Type.GetLeaderboard);
		  });
	}
	
	public static void Qc_GetEntireLeaderboard(FreshLevels.Type type, int index)
	{
		if(fa.dontConnect3rdParty) {return; }
		Debug.Log("Trying to get qc leaderboard");
		FrRemoteData.Qc_GetEntireLeaderboard("" + type, fa.token, fa.lengthOfLeaderboard, (data) =>
		  {
			  fa.entireLeaderboard[index] = data;
		  });
	}
	
	public static void Qc_GetEntireMMLeaderboard(FreshLevels.Type type)
	{
		if(fa.dontConnect3rdParty) {return; }
		Debug.Log("Trying to get qc leaderboard");
		FrRemoteData.Qc_GetEntireLeaderboard("" + type, fa.token, fa.lengthOfMainMenuLeaderboard, (data) =>
		  {
			  fa.entireMMLeaderboard = data;
		  });
	}
	
	public static void Qc_GetLeaderboardTimeSlot(FreshLevels.Type type, int slot)
	{
		
		if(fa.dontConnect3rdParty) {return; }
		//Debug.Log("Trying to get qc leaderboard time slot " + slot);
		FrRemoteData.Qc_GetLeaderboardSlotTime(slot, "" + type, (data) =>
		  {
				fa.leaderboardTimes[FreshLevels.LevelForIndexLeaderboard(type), slot] = data;
		  });
	}

	public static void Qc_GetLeaderboardNameSlot(FreshLevels.Type type, int slot)
	{
		if(fa.dontConnect3rdParty) {return; }
		//Debug.Log("Trying to get qc leaderboard time slot " + slot);
		FrRemoteData.Qc_GetLeaderboardSlotName(slot, "" + type, (data) =>
		  {
				fa.leaderboardNames[FreshLevels.LevelForIndexLeaderboard(type),slot] = data;
		  });
	}

	public static void Qc_GetMMLeaderboardTimeSlot(FreshLevels.Type type, int slot)
	{
		if(fa.dontConnect3rdParty) {return; }
		//Debug.Log("Trying to get qc leaderboard time slot " + slot);
		FrRemoteData.Qc_GetLeaderboardSlotTime(slot, "" + type, (data) =>
		  {
				fa.mmleaderboardTimes[slot] = data;
		  });
	}

	public static void Qc_GetMMLeaderboardNameSlot(FreshLevels.Type type, int slot)
	{
		if(fa.dontConnect3rdParty) {return; }
		//Debug.Log("Trying to get qc leaderboard time slot " + slot);
		FrRemoteData.Qc_GetLeaderboardSlotName(slot, "" + type, (data) =>
		  {
				fa.mmleaderboardNames[slot] = data;
		  });
	}

	public static void ValidateToken(string token)
	{
		if(fa.dontConnect3rdParty) {return; }
		FrRemoteData.Fr_ValidateToken("" + token, (data) =>
		  {
			  AddResult(data, Type.ValidateToken);
		  });
	}

	public static void ValidateTokenAndSolve(string token)
	{
		if(fa.dontConnect3rdParty) {return; }
		FrRemoteData.Fr_ValidateToken("" + token, (data) =>
		  {
			  AddResult(data, Type.ValidateTokenAndSolve);
		  });
	}

	public static void ValidateTokenAndSet(string token)
	{
		if(fa.dontConnect3rdParty) {return; }
		FrRemoteData.Fr_ValidateToken("" + token, (data) =>
		  {
			  AddResult(data, Type.ValidateTokenAndSet);
		  });
	}

	void Update()
	{
		for (int i = 0; i < results.Count; i++)
		{
			if (!results[i].done)
			{
				switch (results[i].type)
				{
					case Type.Generic:
						Debug.Log(results[i].data);
						results[i].done = true;
						break;
					case Type.ValidateToken:
						if (results[i].data == "success")
						{
							Debug.Log("Token is valid");
							fa.tokenIsValid = true;
						}
						else
						{
							Debug.Log("Token was invalid");
							fa.tokenIsValid = false;
							//token wasn't valid. Get a new one.
							//fa.token = null;
							//AskForToken();
						}
						results[i].done = true;
						break;
					case Type.ValidateTokenAndSolve:
						if (results[i].data == "success")
						{
							Debug.Log("Token is valid");
							fa.tokenIsValid = true;
						}
						else
						{
							Debug.Log("Token was invalid");
							fa.tokenIsValid = false;
							//token wasn't valid. Get a new one.
							fa.token = null;
							AskForToken();
						}
						results[i].done = true;
						break;
					case Type.ValidateTokenAndSet:
						if (fa.checkToken)
						{
							fa.checkToken = false;
							if (results[i].data == "success")
							{
								fa.token = fa.tempToken;
								PlayerPrefs.SetString("frtoken", fa.token);
								PlayerPrefs.Save();
								fa.tempToken = null;
								Debug.Log("Token is valid");
								fa.tokenIsValid = true;

								if (RawFuncs.self != null)
								{
									RawFuncs.self.MenuOn(RawInfo.MenuType.SetTokenMenu);
									RawFuncs.self.UpdateTokenDisplay(true, "");
								}

							}
							else
							{
								string temp = fa.tempToken;
								fa.tempToken = null;
								fa.tokenIsValid = false;
								fa.checkToken = false;
								Debug.Log("Token was invalid");

								
								if (RawFuncs.self != null)
								{
									RawFuncs.self.MenuOn(RawInfo.MenuType.SetTokenMenu);
									RawFuncs.self.UpdateTokenDisplay(false, temp);
								}
							}
						}
						else
						{
							//was cancelled before validation arrived
							fa.tempToken = null;
							fa.tokenIsValid = false;
							Debug.Log("Cancelled");

						}
						results[i].done = true;
						break;
					case Type.GetLeaderboard:
						Debug.Log("Got leaderboard!");
						fa.receivedLeaderboard = true;
						fa.leaderboardData = results[i].data;
						results[i].done = true;
						break;
					case Type.QC_GetLeaderboard:
						Debug.Log("Got QC leaderboard!");
						fa.receivedLeaderboard = true;
						fa.leaderboardData = results[i].data;
						results[i].done = true;
						break;
					case Type.NewToken:
						fa.token = results[i].data;
						PlayerPrefs.SetString("frtoken", fa.token);
						PlayerPrefs.Save();
						Debug.Log("Got new token! " + fa.token);
						results[i].done = true;

						if (!PlayerPrefs.HasKey("username")) { SetUsername("defaultUser"); }

						break;
					case Type.SetUsername:
						if (results[i].data == "success")
						{
							PlayerPrefs.SetString("username", fa.username);
							PlayerPrefs.Save();
							Debug.Log("Successfully set username on server!");
							results[i].done = true;

						}
						break;

				}
			}
		}

		//clean
		for (int a = 0; a < 10; a++)
		{
			bool leave = true;
			for (int i = 0; i < results.Count; i++)
			{
				if (results[i].done)
				{
					results.RemoveAt(i);
					leave = false;
					break;
				}
			}
			if (leave) { break; }
		}
	}

}
