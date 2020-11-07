using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class AchivoFuncs : MonoBehaviour
{
	public enum Achivos
	{
		None = 0,
		Achivo_AllasKlar = 1,
		Achivo_DaddysLove = 2,
		Achivo_DontStompa = 3,
		Achivo_MagicMonk = 4,
		Achivo_Routes66 = 5,
		Achivo_Cheater = 6,
		Achivo_Reverso = 7,
		Achivo_Champion = 8,
		Achivo_NoThanksImGood = 9,
		Achivo_MitLiebeGemacht = 10,
		Achivo_GoinFastImTowerBound = 11,

		End
	}

	public static bool[] myAchivos = new bool[12];

	public static void GetAchivo(Achivos achivo)
	{
		if (myAchivos[(int)achivo] == false)
		{
			PopupAchivo(achivo);

			if (achivo == Achivos.Achivo_AllasKlar)
			{
				if (SteamManager.Initialized)
				{
					SteamUserStats.SetAchievement("allesklar");
				}
			}

		}
		myAchivos[(int)achivo] = true;
		Fresh_Saving.SaveLocalAchivos();


		SteamStatsAndAchievements.TellSteamAboutMyAchievos();
	}


	public static bool CheckAchivo(Achivos achivo)
	{
		return myAchivos[(int)achivo];
	}

	public static void InitAchivos()
	{
		//All Achivos start as false
		myAchivos = new bool[100];

		//Load them locally (additive)

		//Load them from Michael's server (additive)

		//Load them from Steam (additive)
	}

	public static void PopupAchivo(Achivos achivo)
	{
		Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Achivo);
		GameObject go = null;
		if (achivo == Achivos.Achivo_AllasKlar) { go = Instantiate(xa.de.AchivoPopup_AllasKlar, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_DaddysLove) { go = Instantiate(xa.de.AchivoPopup_DaddyLove, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_DontStompa) { go = Instantiate(xa.de.AchivoPopup_DontStomp, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_MagicMonk) { go = Instantiate(xa.de.AchivoPopup_MagicMonk, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_Routes66) { go = Instantiate(xa.de.AchivoPopup_Routes66, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_Cheater) { go = Instantiate(xa.de.AchivoPopup_Cheater, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_Reverso) { go = Instantiate(xa.de.AchivoPopup_Reverso, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_Champion) { go = Instantiate(xa.de.AchivoPopup_Champion, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_NoThanksImGood) { go = Instantiate(xa.de.AchivoPopup_NoThanks, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_MitLiebeGemacht) { go = Instantiate(xa.de.AchivoPopup_MitLiebe, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }
		if (achivo == Achivos.Achivo_GoinFastImTowerBound) { go = Instantiate(xa.de.AchivoPopup_GoingFast, xa.de.AchivoPopupSpawnPoint.transform.position, xa.de.AchivoPopupSpawnPoint.transform.rotation); }

		if (go != null)
		{
			DontDestroyOnLoad(go);
			iTween.MoveTo(go, iTween.Hash("x", 9, "time", 1f, "easetype", iTween.EaseType.easeOutSine));
			iTween.MoveTo(go, iTween.Hash("delay", 4, "y", 15, "time", 1f, "easetype", iTween.EaseType.easeInSine));
		}

	}

}
