using UnityEngine;

public class GoBackOneMenu : MonoBehaviour
{

	bool success = false;

	void Update()
	{
		xa.hasCheckpointed = false;
		string loadedLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (loadedLevel == "YazarGC_Mobile_MainMenu") { success = true; }
		if (loadedLevel == "YazarGC_Mobile_Achievements") { success = true; Setup.callFadeOutFunc("YazarGC_Mobile_MainMenu", true, loadedLevel); }
		if (loadedLevel == "YazarGC_Mobile_Levels_QuestForTheLaserNipples") { success = true; Setup.callFadeOutFunc("YazarGC_Mobile_MainMenu", true, loadedLevel); }
		if (loadedLevel == "YazarGC_Mobile_Levels_SecretLevels") { success = true; Setup.callFadeOutFunc("YazarGC_Mobile_Options", true, loadedLevel); }
		if (loadedLevel == "YazarGC_Mobile_Options") { success = true; Setup.callFadeOutFunc("YazarGC_Mobile_MainMenu", true, loadedLevel); }
		if (loadedLevel == "YazarGC_Mobile_SteamKey") { success = true; Setup.callFadeOutFunc("YazarGC_Mobile_Options", true, loadedLevel); }
		if (success == false) { success = true; Setup.callFadeOutFunc("YazarGC_Mobile_MainMenu", true, loadedLevel); }//just go to main menu
		this.enabled = false;
	}
}
