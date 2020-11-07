using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PortalScript : MonoBehaviour
{
	public bool isSecretExit = false;
	[SerializeField]
	private string levelStr = null;
	public FreshLevels.Type targetLevelType = FreshLevels.Type.None;
	[SerializeField, Tooltip("Overrides levelStr")]
	private string destinationButt = null; // for the FrEd level editor, for loading .butt files. if this is set, overrides levelStr
	[SerializeField]
	private GameObject portalAmbientSound = null;
	[SerializeField]
	private float portalDist = 3.0f;
	private bool triggered = false;


	public void Initialize(string levelName, bool isButtLevel, bool noSound)//for tim's level editor
	{
		this.levelStr = null;
		this.destinationButt = null;

		if (isButtLevel)
		{
			this.destinationButt = levelName;
		}
		else
		{
			this.levelStr = levelName;
		}

		if (noSound)
		{
			this.portalAmbientSound = null;
		}
	}

	private void Start()
	{
		if (this.portalAmbientSound)
		{
			Vector3 pos = this.transform.position;
			pos.z = Camera.main.gameObject.transform.position.z;
			GameObject.Instantiate<GameObject>(this.portalAmbientSound, pos, Quaternion.identity, this.transform);
		}
	}

	private void Update()
	{
		if (xa.player && !xa.playerDead)
		{
			Vector3 pos = xa.player.transform.position;
			pos.z = this.transform.position.z;
			if (Vector3.Distance(pos, this.transform.position) < this.portalDist)
			{
				if (!this.triggered)
				{
					if (!xa.fadingOut)
					{
						this.triggered = true;
						xa.allowPlayerInput = false;
						//Won Level
						xa.hasCheckpointed = false;

						Time.timeScale = 1.0f;
						FreshLevels.Type levelType = FreshLevels.GetTypeOfCurrentLevel();

						if (!isSecretExit)
						{
							Fresh_Saving.SaveLevelDeaths(levelType, za.deaths);
							Fresh_Saving.SaveLevelTime(levelType, fa.speedrunTime);
							Ghosts.StopAndSaveRecording();
							FrFuncs.Qc_ReportLevelTime(levelType, fa.speedrunTime, "noGhostData");
							SteamLeaderboards.SendPBToSteam(levelType, fa.speedrunTime);
						}

						//Update unlocked levels (lvlNum)
						NodeController.WonLevel(levelType);

						PlayerSpawnerScript.PermaSaveCoins();



						//check achivos
						if (!isSecretExit)
						{
							if (levelType == FreshLevels.Type.MusicLvl2)
							{
								if (fa.speedrunTime < 120) { AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_Routes66); }
							}
							if (levelType == FreshLevels.Type.Blue2)
							{
								if (!NovaPlayerScript.has3rdJumped) { AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_MagicMonk); }
							}
							if (levelType == FreshLevels.Type.Pink1)
							{
								if (!NovaPlayerScript.hasStomped) { AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_DontStompa); }
							}
							if (levelType == FreshLevels.Type.SlimeDaddy_BatterUp)
							{
								if (ThrowingBallScript.currentDaddysLove == 5) { AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_DaddysLove); }
							}
							if (levelType == FreshLevels.Type.AlpSecret)
							{
								AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_MitLiebeGemacht);
							}
							if (levelType == FreshLevels.Type.AlpBoss)
							{
								AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_GoinFastImTowerBound);
							}
							if (levelType == FreshLevels.Type.Alp5)
							{
								if (!fa.teleportedOnJumpingMassacre)
								{
									AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_NoThanksImGood);
								}
							}
						}



						if (!string.IsNullOrEmpty(this.destinationButt))
						{
							FrEdNodeScript.buttFileToOpen = this.destinationButt;
							Setup.callFadeOutFunc("FrEd_Play", true, SceneManager.GetActiveScene().name);
						}
						else
						{
							if (targetLevelType != FreshLevels.Type.None)
							{
								Setup.callFadeOutFunc(FreshLevels.GetStrNameForType(targetLevelType), true, SceneManager.GetActiveScene().name);
							}
							else
							{
								Setup.callFadeOutFunc(this.levelStr, true, SceneManager.GetActiveScene().name);
							}
						}
					}
				}
			}
		}
	}
}
