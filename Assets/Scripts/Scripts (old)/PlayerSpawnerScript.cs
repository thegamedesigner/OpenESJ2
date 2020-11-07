using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerSpawnerScript : MonoBehaviour
{
	[SerializeField, UnityEngine.Serialization.FormerlySerializedAs("obj")]
	private GameObject playerEntityPrefab = null;

	[SerializeField]
	private bool enablePlayerTrail = true;
	[SerializeField]
	private float trailLength = 2.0f;
	[SerializeField]
	private Color trailColor = new Color32(255, 255, 255, 77);

	void Start()
	{
		xa.freezePlayerForCutscene = false;
		xa.forcePlayerDirection = 0;

		//clean inf love stuff
		xa.infLoveTriggerZone = null;
		xa.portalStep = 0;
		SaveAbilitiesNodeScript.WipePotentials(true);
		if (!xa.hasCheckpointed)
		{
			//YOU ARE ON THIS LEVEL. Save that you've unlocked it.
			string level = SceneManager.GetActiveScene().name;
			UnlockSystemScript.UnlockThisLevel(level);


			//Instantiate(obj, transform.position, xa.null_quat);
			Vector3 pos = transform.position;
			pos.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
			xa.player = GameObject.Instantiate<GameObject>(this.playerEntityPrefab, pos, xa.null_quat);
			//xa.player = GameObject.Instantiate<GameObject>(xa.de.defaultPlayerPrefab, pos, xa.null_quat);
			xa.firstCheckpointTriggered = true;//fake this
			xa.totalRespawns++;

			//auto-fake pressing the total reset key
			za.deaths = 0;
			fa.ResetSpeedrun();
			fa.lastCheckpointX = -9999;
			fa.lastCheckpointY = -9999;
			fa.lastCheckpointLvlIndex = -9999;
		}
		else
		{
			xa.player = GameObject.Instantiate<GameObject>(this.playerEntityPrefab, xa.lastSpawnPoint, xa.null_quat);
			//xa.player = GameObject.Instantiate<GameObject>(xa.de.defaultPlayerPrefab, xa.lastSpawnPoint, xa.null_quat);
			xa.totalRespawns++;

			if (za.useSnapCameraToCheckpoint)
			{
				fa.mainCameraObject.transform.position = za.snapCameraToThisPos;
				za.useSnapCameraToCheckpoint = false;
			}
		}

		//handle coins
		if (xa.hasCheckpointed)
		{
			if (Mathf.RoundToInt(xa.lastSpawnPoint.x) == fa.lastCheckpointX &&
				Mathf.RoundToInt(xa.lastSpawnPoint.y) == fa.lastCheckpointY &&
				UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == fa.lastCheckpointLvlIndex)
			{
				//respawned at the same checkpoint as lasttime, so probably dont save the coins


			}
			else
			{
				PermaSaveCoins();
			}
			fa.lastCheckpointX = Mathf.RoundToInt(xa.lastSpawnPoint.x);
			fa.lastCheckpointY = Mathf.RoundToInt(xa.lastSpawnPoint.y);
			fa.lastCheckpointLvlIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

		}
		//wipe all temp coins
		fa.coinX = new List<int>();
		fa.coinY = new List<int>();
		fa.coinLvl = new List<int>();

		if (!this.enablePlayerTrail)
		{
			this.trailLength = 0.0f;
		}
		xa.player.GetComponent<NovaPlayerScript>().ConfigureTrail(this.trailLength, this.trailColor);
	}

	public static void PermaSaveCoins()
	{
		int numOfCoins = 0;
		string myCoinName = "";
		//make all temp coins perma saved
		for (int i = 0; i < fa.coinX.Count; i++)
		{
			numOfCoins++;
			myCoinName = "coinx" + fa.coinX[i] + "y" + fa.coinY[i] + "lvl" + fa.coinLvl[i];
			PlayerPrefs.SetInt(myCoinName, 1);
		}

		if (PlayerPrefs.HasKey("collectedCoins"))
		{
			int coins = PlayerPrefs.GetInt("collectedCoins", 0);
			coins += numOfCoins;
			PlayerPrefs.SetInt("collectedCoins", coins);
			fa.coinsCollected = coins;
		}
		else
		{
			PlayerPrefs.SetInt("collectedCoins", numOfCoins);
			fa.coinsCollected = numOfCoins;
		}
		PlayerPrefs.Save();
	}

	void Update()
	{
		MultiPlayerFuncs.CheckForMultiPlayers();
	}
}
