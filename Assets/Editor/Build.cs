using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ShowPopupExample : EditorWindow
{
	void OnGUI()
	{
		EditorGUILayout.LabelField("Did you change the week, build word, version number and leaderboard??", EditorStyles.wordWrappedLabel);
		GUILayout.Space(70);
		if (GUILayout.Button("Yup!"))
		{
			Build.ActuallyBuildGame();
			this.Close();
		}
		if (GUILayout.Button("Nope!"))
		{
			this.Close();
		}
	}
}

public class Build
{


	public string[] dlls = {
		"libsteam_api.dylib",
		"libsteam_api.so",
		"steam_api.dll",
		"steam_api64.dll",
		"steam_appid.txt",
		"SteamworksNative.dll"};


	static string[] GetScenePaths()
	{
		string[] scenes = new string[EditorBuildSettings.scenes.Length];

		for (int i = 0; i < scenes.Length; i++)
		{
			scenes[i] = EditorBuildSettings.scenes[i].path;
		}

		return scenes;
	}
	
	[MenuItem("Tools/Build All")]
	public static void BuildGame()
	{
		ShowPopupExample window = ScriptableObject.CreateInstance<ShowPopupExample>();
		window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
		window.ShowPopup();

	}

	[MenuItem("Tools/BuildAndUploadTest")]
	public static void BuildTest()
	{
		string path = "build/";
		string windows_path = path + "/" + "ESJ2_Windows" + "/";
		BuildPipeline.BuildPlayer(GetScenePaths(), windows_path + "ESJ2.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

		Process proc = new Process();
		proc.StartInfo.FileName = "sdk\\tools\\ContentBuilder\\run_build_test.bat";
		proc.Start();
	}

	public static void ActuallyBuildGame()
	{
		string path = "build/";
		//xa.Debug = Logging.Level.None;
		string windows_path = path + "/" + "ESJ2_Windows" + "/";
		string linux_path = path + "/" + "ESJ2_Linux" + "/";
		string osx_path = path + "/" + "ESJ2_OSX" + "/";
		BuildPipeline.BuildPlayer(GetScenePaths(), windows_path + "ESJ2.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
		BuildPipeline.BuildPlayer(GetScenePaths(), linux_path + "ESJ2.x86", BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);
		BuildPipeline.BuildPlayer(GetScenePaths(), osx_path + "ESJ2.app", BuildTarget.StandaloneOSX, BuildOptions.None);
	}
	
	[MenuItem("Tools/BuildForSteam")]
	public static void BuildForSteam()
	{
		ActuallyBuildGame();
	}

	[MenuItem("Tools/BuildForItchio")]
	public static void BuildForItchio()
	{
		string path = "build/";
		//xa.Debug = Logging.Level.None;
		string windows_path = path + "/" + "ESJ2_Windows" + "/";
		string linux_path = path + "/" + "ESJ2_Linux" + "/";
		string osx_path = path + "/" + "ESJ2_OSX" + "/";
		BuildPipeline.BuildPlayer(GetScenePaths(), windows_path + "ESJ2.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
		BuildPipeline.BuildPlayer(GetScenePaths(), linux_path + "ESJ2.x86", BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);
		BuildPipeline.BuildPlayer(GetScenePaths(), osx_path + "ESJ2.app", BuildTarget.StandaloneOSX, BuildOptions.None);
	
	}

	[MenuItem("Tools/Publish")]
	public static void Publish()
	{
		Process proc = new Process();
		proc.StartInfo.FileName = "sdk\\tools\\ContentBuilder\\run_build.bat";
		proc.Start();
	}

	[MenuItem("Tools/BuildAndPublish")]
	public static void BuildAndPublish()
	{
		ActuallyBuildGame();
		Publish();
	}
}
