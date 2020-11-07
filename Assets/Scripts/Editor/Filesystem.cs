using UnityEditor;
using UnityEngine;

public class Filesystem : MonoBehaviour
{
	[MenuItem("Filesystem/Open Data Path")]
	public static void OpenDataPath()
	{
		System.Diagnostics.Process.Start(Application.dataPath);
	}

	[MenuItem("Filesystem/Open Persistent Data Path")]
	public static void OpenPersistentDataPath()
	{
		System.Diagnostics.Process.Start(Application.persistentDataPath);
	}

	[MenuItem("Filesystem/Open Streaming Assets Path")]
	public static void OpenStreamingAssetsPath()
	{
		System.Diagnostics.Process.Start(Application.streamingAssetsPath);
	}

	[MenuItem("Filesystem/Open Temp Cache Path")]
	public static void OpenTempCachePath()
	{
		System.Diagnostics.Process.Start(Application.temporaryCachePath);
	}
}
