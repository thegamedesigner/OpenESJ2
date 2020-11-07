using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class FrRemoteData : MonoBehaviour
{
	public bool useLocal = true;
	private static string origin = "http://www.twoandthirtysoftware.com/ESJ2FR/";
	private static string localOrigin = "http://localhost:8000/";


	public static void Fr_SetUsername(string username, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "fr_SetUsername.php?token=" + fa.token + "&username=" + username + "&a=" + "ABuzzingInTheBrain";


		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void Fr_AskForToken(Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "fr_AskForToken.php?a=" + "ABuzzingInTheBrain";


		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}
	
	public static void Fr_ReportLevelTime(string level, float time, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "fr_ReportLevelTime.php?token=" + fa.token + "&level=" + level + "&time=" + time + "&a=" + "ABuzzingInTheBrain";


		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}
	
	public static void Qc_ReportLevelTime(string level, float lvltime, string ghostData, Action<string> callback)
	{
		Debug.Log("Token: " + fa.token);
		WebClient client = new WebClient();
		string url = origin + "qc_ReportLevelTime.php?token=" + fa.token + "&level=" + level + "&time=" + lvltime + "&ghost=" + ghostData + "&a=" + "ABuzzingInTheBrain";


		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}
	
	public static void Fr_GetLeaderboard(string level, string token, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "fr_GetLeaderboard.php?level=" + level + "&token=" + token + "&a=" + "ABuzzingInTheBrain";

		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}
	
	public static void Qc_GetEntireLeaderboard(string level, string token, int amount, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "qc_GetEntireLeaderboard.php?level=" + level + "&token=" + token + "&amount=" + amount + "&a=" + "ABuzzingInTheBrain";

		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}
	
	public static void Qc_GetLeaderboardSlotTime(int slot, string level, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "qc_GetLeaderboardSlotTime.php?slot=" + slot + "&level=" + level + "&a=" + "ABuzzingInTheBrain";

		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void Qc_GetLeaderboardSlotName(int slot, string level, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "qc_GetLeaderboardSlotName.php?slot=" + slot + "&level=" + level + "&a=" + "ABuzzingInTheBrain";

		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

	public static void Fr_ValidateToken(string token, Action<string> callback)
	{
		WebClient client = new WebClient();
		string url = origin + "fr_ValidateToken.php?token=" + token + "&a=" + "ABuzzingInTheBrain";

		client.UploadStringCompleted += (s, e) =>
		{ if (callback != null) { callback(e.Result); } client.Dispose(); };
		client.Headers.Add("Content-Type", "text/plain");
		client.UploadStringAsync(new Uri(url), "POST", "nothing");
	}

}
