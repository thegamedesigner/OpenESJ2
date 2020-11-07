using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBFuncs : MonoBehaviour
{
	public static DBFuncs self;
	public Text displayText;
	string genericPrint = "";
	string genericDebug = "";
	public static bool printDebug = false;

	public static bool callLogInFromSignUp = false;
	public static string callLogInFromSignUp_result;
	public static bool callLogInFromLogIn = false;
	public static string callLogInFromLogIn_result;

	public static bool resultFromRequestLeaderboard = false;
	public static string resultFromRequestLeaderboard_data;

	public void TestPing()
	{
		Debug.Log("Firing Ping!");
		RemoteData.Ping((data) =>
		  {



			  printDebug = true;
			  // genericPrint += "" + data;
			  genericDebug += "" + data;
			  // UnityEngine.Debug.Log("STUFF");
		  });
	}





	







	///////////////////

	public void TestScriptFunc()
	{
		Debug.Log("Called testScript.php!");
		RemoteData.TestScript((data) =>
		  {
			  printDebug = true;
			  genericDebug += "" + data;
		  });
	}


	public static void CreateAccount(string username, string password, string email)
	{
		if(fa.dontConnect3rdParty) {return; }
		RemoteData.Account account = new RemoteData.Account();
		account.username = username;
		account.pwHash = RemoteData.GeneratePwHash(password);
		account.data[0] = email;
		account.dataType[0] = "email";
		RemoteData.pending = account;
		RemoteData.CreateAccount(account, (data) =>
		  {
			  callLogInFromSignUp = true;
			  callLogInFromSignUp_result = data;
			  // genericPrint += "" + data;
		  });
	}

	public static void LogIn(string username, string password)
	{
		if(fa.dontConnect3rdParty) {return; }
		string pwhash = RemoteData.GeneratePwHash(password);
		LogIn(username, pwhash, true);
	}

	public static void LogIn(string username, string pwhash, bool isHash)
	{
		if(fa.dontConnect3rdParty) {return; }
		RemoteData.Account account = new RemoteData.Account();
		account.username = username;
		account.pwHash = pwhash;
		RemoteData.pending = account;
		RemoteData.LogIn(account, (data) =>
		  {
			  callLogInFromLogIn = true;
			  callLogInFromLogIn_result = data;
			  // genericPrint += "" + data;
		  });
	}

	public static void LoggedIn(string token)
	{
		if(fa.dontConnect3rdParty) {return; }
		RemoteData.myAccount = RemoteData.pending;
		RemoteData.myAccount.token = token;
		ProfileScript.self.LoggedIn();
	}

	public static void FailedLogIn()
	{
		if(fa.dontConnect3rdParty) {return; }
		RemoteData.myAccount = null;
		RemoteData.pending = null;
		ProfileScript.self.CancelOutOfSignUp();
	}

	public void Defunct_CallReportLevelTime(FreshLevels.Type type, float levelTime)
	{
		if(fa.dontConnect3rdParty) {return; }
		RemoteData.Account a = new RemoteData.Account();
		a.username = "TestUser9736";
		a.token = "valid";
		RemoteData.ReportLevelTime(a, type.ToString(), "" + levelTime, (data) =>
		  {
			  genericPrint += "" + data;
		  });
	}


	public void Defunct_ReportLevelTime(FreshLevels.Type type, float levelTime)
	{
		if(fa.dontConnect3rdParty) {return; }
		if (RemoteData.myAccount != null)
		{
			RemoteData.ReportLevelTime(RemoteData.myAccount, type.ToString(), "" + levelTime, (data) =>
			  {
				  genericDebug += "" + data;
			  });
		}
	}

	public void RequestLeaderboard(FreshLevels.Type type)
	{
		if(fa.dontConnect3rdParty) {return; }
		if (RemoteData.myAccount != null)
		{
			RemoteData.RequestLeaderboard(RemoteData.myAccount, type.ToString(), (data) =>
			  {
				  resultFromRequestLeaderboard = true;
				  resultFromRequestLeaderboard_data = data;
			  });
		}
	}

	public void UploadGhostAttempt(string chunk, bool start, bool end)
	{
		if(fa.dontConnect3rdParty) {return; }
		if (RemoteData.myAccount != null)
		{
			RemoteData.UploadGhostAttempt(RemoteData.myAccount, chunk, start, end, (data) =>
			  {
				  //resultFromRequestLeaderboard = true;
				  //resultFromRequestLeaderboard_data = data;
			  });
		}
	}

	public void DownloadGhost(string name)
	{
		if(fa.dontConnect3rdParty) {return; }
		if (RemoteData.myAccount != null)
		{
			RemoteData.DownloadGhost(RemoteData.myAccount, name, (data) =>
			  {
				  //resultFromRequestLeaderboard = true;
				  //resultFromRequestLeaderboard_data = data;
				  Ghosts.downloadedGhost = data;
				  Ghosts.loadedGhostData = true;
			  });
		}
	}

	public void DownloadBestGhost()
	{
		if(fa.dontConnect3rdParty) {return; }
		if (RemoteData.myAccount != null)
		{
			RemoteData.DownloadBestGhost(RemoteData.myAccount, (data) =>
			  {
				  //resultFromRequestLeaderboard = true;
				  //resultFromRequestLeaderboard_data = data;

				  Ghosts.downloadedGhost = data;
				  Ghosts.loadedGhostData = true;
			  });
		}
	}








	void Awake()
	{
		self = this;
	}

	void Update()
	{
		if (displayText != null)
		{
			if (genericPrint != "")
			{
				displayText.text += genericPrint + "\n";
				genericPrint = "";
			}
		}

		if (printDebug)
		{
			printDebug = false;
			Debug.Log(genericDebug);
			genericDebug = "";
		}
		if (callLogInFromSignUp)
		{
			//Debug.Log("Logged in: " + callLogInFromSignUp_result);
			callLogInFromSignUp = false;


			bool failed = false;

			if (callLogInFromSignUp_result == null) { failed = true; }
			if (!failed && callLogInFromSignUp_result.Length < 4) { failed = true; }
			if (!failed)
			{
				string chkFail = callLogInFromSignUp_result.Substring(0, 4);
				if (chkFail == "Fail") { failed = true; }
			}

			if (failed)
			{

			}
			else
			{
				LoggedIn(callLogInFromSignUp_result);
			}



		}
		if (callLogInFromLogIn)
		{
			callLogInFromLogIn = false;

			bool failed = false;

			if (callLogInFromLogIn_result == null) { failed = true; }
			if (!failed && callLogInFromLogIn_result.Length < 4) { failed = true; }
			if (!failed)
			{
				string chkFail = callLogInFromLogIn_result.Substring(0, 4);
				if (chkFail == "Fail") { failed = true; }
			}

			if (failed)
			{
				//Debug.Log(callLogInFromLogIn_result);
				FailedLogIn();
			}
			else
			{
				//logged in. What we got was a token
				//Debug.Log("Successful LogIn! Token: " + callLogInFromLogIn_result);
				LoggedIn(callLogInFromLogIn_result);
			}
		}
		if (resultFromRequestLeaderboard)
		{
			resultFromRequestLeaderboard = false;
			/*
			List<string> mix = new List<string>();
			List<string> names = new List<string>();
			List<float> score = new List<float>();

			string s = "Sticky Ends\nHighscores:\n";
			
			//Fresh_InGameMenus.self.HighscoreText.text = resultFromRequestLeaderboard_data;
	
			string[] splitString = resultFromRequestLeaderboard_data.Split(new string[] { ",", ":" }, System.StringSplitOptions.RemoveEmptyEntries);

			bool flip = false;
			for (int i = 0; i < splitString.Length; i++)
			{
				if (!flip)
				{
					names.Add(splitString[i]);
				}
				else
				{
					float r = 0;
					float.TryParse(splitString[i], out r);
					score.Add(r);
				}
				flip = !flip;
			}
			

			//get your lowest score
			
			float lowest2 = 9999;
			int lowestIndex2 = -1;
			string myScore = "";
			for (int i = 0; i < names.Count; i++)
			{
				if (RemoteData.myAccount != null && names[i] == RemoteData.myAccount.username)
				{
					if (score[i] < lowest2)
					{
						lowest2 = score[i];
						lowestIndex2 = i;
					}
				}
			}

			if (lowestIndex2 != -1)
			{
				myScore = "\n\nYour score:\n" + names[lowestIndex2] + ": " + score[lowestIndex2];
			}


			//sort out leaderboard
			List<string> sorted_names = new List<string>();
			List<float> sorted_score = new List<float>();

			//sort out only the top 3
			for (int i = 0; i < 3; i++)
			{
				float lowest = 999999;
				int lowestIndex = -1;
				for (int a = 0; a < score.Count; a++)
				{
					if (score[a] < lowest)
					{
						//if this name doesn't exist already on the sorted list
						bool alreadyExists = false;
						for (int b = 0; b < sorted_names.Count; b++)
						{
							if (sorted_names[b] == names[a]) { alreadyExists = true; break; }
						}

						if (!alreadyExists)
						{
							lowest = score[a];
							lowestIndex = a;
						}
					}
				}
				if (lowestIndex != -1)
				{
					sorted_names.Add(names[lowestIndex]);
					sorted_score.Add(score[lowestIndex]);
					names.RemoveAt(lowestIndex);
					score.RemoveAt(lowestIndex);
				}

			}


			if (sorted_score.Count != 0)
			{
				for (int i = 0; i < sorted_names.Count; i++)
				{
					s += sorted_names[i] + ": " + sorted_score[i] + "\n";
				}
			}
			Fresh_InGameMenus.self.HighscoreText.text = s + myScore;
		
		
			*/
		}
	}
}
