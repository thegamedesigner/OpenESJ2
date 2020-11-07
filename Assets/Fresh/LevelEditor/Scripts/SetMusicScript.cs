using UnityEngine;

public class SetMusicScript : MonoBehaviour
{
	public FrEdInfo frEdInfo;

	void Update()
	{
		if (frEdInfo != null && frEdInfo.set) {
			if (FrEdLibrary.instance != null) {
				AudioClip a = FrEdLibrary.instance.GetTrack((FrEdLibrary.TrackType)frEdInfo.int1);
				if (a != null) {
					Debug.Log("Tried to play music: " + frEdInfo.int1);
					za.skaldScript.FrEd_PlayClip(a);
				}
			}
			this.enabled = false;
		}
	}
}
