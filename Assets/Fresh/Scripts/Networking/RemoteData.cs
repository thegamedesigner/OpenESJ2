using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class RemoteData
{
	public class Account
	{
		public string username;
		public string pwHash;
		public string token;
		public ProfileScript.AvatarType avatarType = ProfileScript.AvatarType.Default;

		public string[] data = new string[100];
		public string[] dataType = new string[100];

	}

	public static Account myAccount = null;
	public static Account pending = null;

	private static string origin2 = "http://www.twoandthirtysoftware.com/ESJ2DBE/";
	private static string origin = "http://localhost:8000/";
	private static string secretKey = "!HAS3Ka^#_YY189";
	private static MD5 md5;

	
	public static string Base64Encode(string input)
	{
		return Convert.ToBase64String(Encoding.ASCII.GetBytes(input));
	}



	public static string GetSecretKey()
	{
		return secretKey;
	}


	private static string GenerateSignature(string data)
	{
		if (md5 == null) { md5 = new MD5CryptoServiceProvider(); }
		byte[] hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data + secretKey));
		return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
	}

	public static string GeneratePwHash(string pw)
	{
		string extraData = "We are but dust and shadows";
		if (md5 == null) { md5 = new MD5CryptoServiceProvider(); }
		byte[] hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pw + extraData + secretKey));
		return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
	}

	////////////////
	//Non-signed in functions (ie they have to be done before logging in, or they're cheap)
	////////////////
	
	public static void Ping(Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "ping.php";

		client.DownloadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				if (!e.Cancelled && e.Error == null)
				{
					callback(e.Result);
				}
				else
				{
					if (e.Cancelled) { callback("Cancelled"); }
					else if (e.Error != null) { callback("Error: " + e.Error + "Result: " + e.Result); }
					else { callback("Unknown failure"); }
				}
			}
			client.Dispose();
		};
		client.DownloadStringAsync(new Uri(url), "GET");
	}

	public static void TestScript(Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "testScript.php";

		client.DownloadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				if (!e.Cancelled && e.Error == null)
				{
					callback(e.Result);
				}
				else
				{
					if (e.Cancelled) { callback("Cancelled"); }
					else if (e.Error != null) { callback("Error: " + e.Error + "Result: " + e.Result); }
					else { callback("Unknown failure"); }
				}
			}
			client.Dispose();
		};
		client.DownloadStringAsync(new Uri(url), "GET");
	}

	public static void CreateAccount(Account account, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "createAccount.php?username=" + account.username + "&pw=" + account.pwHash + "&a=" + GenerateSignature(account.username);

		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void LogIn(Account account, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "logIn.php?username=" + account.username + "&pw=" + account.pwHash + "&a=" + GenerateSignature(account.username);

		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void ReportLevelTime(Account account, string leaderboard, string score, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "reportLevelTime.php?username=" + account.username + "&pw=" + account.pwHash + "&leaderboard=" + leaderboard + "&score=" + score + "&a=" + GenerateSignature(account.username);
		//Debug.Log("ReportTime: RemoteData, called PHP");
		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void ReportUsageStat(string name, string lvlName, string timeStr, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "reportUsageStat.php?username=" + name + "&lvlName=" + lvlName + "&time=" + timeStr;
		//Debug.Log("ReportUsageStat: RemoteData, called PHP");
		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void GetUsageStats(string lvlName, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "getUsageStats.php?lvlName=" + lvlName;
		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void RequestLeaderboard(Account account, string leaderboard, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "requestLeaderboard.php?token=" + account.token + "&leaderboard=" + leaderboard + "&ver=" + xa.freshVersionNumber;
		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void UploadGhostAttempt(Account account, string chunk, bool isStart, bool isEnd, Action<string> callback)
	{
		string start;
		string end;
		if (isStart) { start = "true"; } else { start = "false"; }
		if (isEnd) { end = "true"; } else { end = "false"; }
		string level = SceneManager.GetActiveScene().name;
		WebClient client = new WebClient();
		string url = origin + "uploadGhostAttempt.php?token=" + account.token + "&username=" + account.username + "&pw=" + account.pwHash + "&level=" + level + "&start=" + start + "&end=" + end + "&chunk=" + chunk;

		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void DownloadGhost(Account account, string name, Action<string> callback)
	{
		string level = SceneManager.GetActiveScene().name;
		WebClient client = new WebClient();
		string url = origin + "downloadGhost.php?token=" + account.token + "&level=" + level + "&username=" + name;

		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void DownloadBestGhost(Account account, Action<string> callback)
	{
		string level = SceneManager.GetActiveScene().name;
		WebClient client = new WebClient();
		string url = origin + "downloadBestGhost.php?token=" + account.token + "&level=" + level;

		client.UploadStringCompleted += (s, e) =>
		{
			if (callback != null)
			{
				callback(e.Result);
			}
			client.Dispose();
		};
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}
}
