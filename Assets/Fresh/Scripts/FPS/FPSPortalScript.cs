using UnityEngine;

public class FPSPortalScript : MonoBehaviour
{
	public bool isSecretExit = false;
	public FreshLevels.Type gotoLvl = FreshLevels.Type.Transformation;
	bool triggered = false;
	float portalDist = 3.0f;

	GameObject playerObj = null;

	void Update()
	{
		if (playerObj == null)
		{
			playerObj = GameObject.FindGameObjectWithTag("Player");

		}
		else
		{
			//Debug.Log("HERE1 " + playerObj.transform.position + ", " + transform.position + ", " + portalDist + ", Triggered/Fading: " + triggered + ", " + xa.fadingOut);
			if (Vector3.Distance(playerObj.transform.position, transform.position) < portalDist)
			{
				if (!triggered)
				{
					triggered = true;
					xa.allowPlayerInput = false;
					xa.hasCheckpointed = false;
					Time.timeScale = 1;
					FreshLevels.Type levelType = FreshLevels.GetTypeOfCurrentLevel();

					
					if(!isSecretExit)
					{
						Fresh_Saving.SaveLevelDeaths(levelType, za.deaths);
						Fresh_Saving.SaveLevelTime(levelType, fa.speedrunTime);
					
						FrFuncs.Qc_ReportLevelTime(levelType, fa.speedrunTime, "noGhostData"); 
						SteamLeaderboards.SendPBToSteam(levelType, fa.speedrunTime);
					}



					

					//Update unlocked levels (lvlNum)
					NodeController.WonLevel(levelType);

					xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, FreshLevels.GetStrNameForType(gotoLvl));
				}
			}
		}

	}
}
