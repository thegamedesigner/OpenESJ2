using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemScript : MonoBehaviour
{
	public bool isCoin = false;
	public bool redDoorKey;
	public bool pinkDoorKey;
	public bool orangeDoorKey;
	public bool purpleDoorKey;

	public float killSelfInXSeconds = -1;
	public float timeSet = -1;

	public string type = "";
	public int starIndex = -1;
	public bool createPickedUpEffect = false;
	public GameObject pickedUpEffect;
	public bool createAsChild = false;
	public bool useToHurtBoss = false;

	public bool useTriggeredTween1 = false;
	public bool useTriggeredTween2 = false;
	public bool useTriggeredTween3 = false;

	public bool useRemoteTriggeredTween1 = false;
	public GameObject remoteGO1;

	public Fresh_SoundEffects.Type playSound = Fresh_SoundEffects.Type.None;

	public Behaviour enableThis = null;

	public bool isBouncePad = false;
	public bool isKey = false;
	public float itemActivationRadiusOverride = -1;//Normally is itemDist in playerscript, which is 1, unless this has a value that is != -1
	public GameObject door = null;
	public GameObject killZone = null;

	[HideInInspector]
	public bool usedUp = false;
	float counter = 0;

	bool pickedUp = false;
	string myCoinName = "";

	
	void Start()
	{
		// xa.tempobj.transform.parent = xa.createdObjects.transform;
		//if (type == "coin")
		//{
		//Destroy(this.gameObject);
		//}
		if (isCoin)
		{
			//Get my unique id

			myCoinName = "coinx" + Mathf.RoundToInt(transform.position.x) + "y" + Mathf.RoundToInt(transform.position.y) + "lvl" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			//Debug.Log("Coin Name: " + myCoinName);
			if (PlayerPrefs.HasKey(myCoinName))
			{
				int coinResult = 0;
				coinResult = PlayerPrefs.GetInt(myCoinName, 0);
				if (coinResult == 1)
				{
					Destroy(this.gameObject);//Player has already collected it.
				}
			}
			//	if(fa.devMode) {pickUpItem(); PlayerSpawnerScript.PermaSaveCoins();}
		}
		if (type == "star")
		{
			if (starIndex < 0)
			{
				Destroy(this.gameObject);
				//Debug.LogError("Invalid star on this level placed without index. Set it in properties of ItemScript");
			}

			int lvlId = LevelInfo.getSceneNumFromName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

			//Checkpointed star
			if ((xa.checkpointedStarsThisLevel & (0x1 << starIndex)) != 0)
			{
				Destroy(this.gameObject);
			}

			//If star is saved
			if ((xa.playerStars[lvlId] & (0x1 << starIndex)) != 0)
			{
				for (int i = 0; i < this.transform.childCount; i++)
				{
					this.transform.GetChild(i).GetComponent<Renderer>().material.color *= new Color(0.5f, 0.5f, 0.5f, 0.5f);
				}
			}
		}
	}

	void Update()
	{
		if (killSelfInXSeconds > 0 && timeSet > 0 && fa.timeInSeconds > (timeSet + killSelfInXSeconds))
		{
			Destroy(this.gameObject);
			return;
		}

		if (redDoorKey && SaveAbilitiesNodeScript.redDoorOpened) { Destroy(this.gameObject); }
		if (pinkDoorKey && SaveAbilitiesNodeScript.pinkDoorOpened) { Destroy(this.gameObject); }
		if (orangeDoorKey && SaveAbilitiesNodeScript.orangeDoorOpened) { Destroy(this.gameObject); }
		if (purpleDoorKey && SaveAbilitiesNodeScript.purpleDoorOpened) { Destroy(this.gameObject); }

		//spawnStompHitbox();
		if (isBouncePad)
		{
			if (usedUp)
			{
				counter += 10 * fa.deltaTime;
				if (counter > 1)
				{
					counter = 0;
					usedUp = false;
				}
			}
		}

	}

	bool spawnedStompHitbox = false;
	void spawnStompHitbox()
	{
		if (spawnedStompHitbox) { return; }

		if (xa.de)
		{
			spawnedStompHitbox = true;
			if (isBouncePad)
			{
				xa.tempobj = (GameObject)(Instantiate(xa.de.zeroAngleStompHitbox, transform.position, xa.null_quat));

			}
			else if (type == "bounceCoin")
			{
				xa.tempobj = (GameObject)(Instantiate(xa.de.oneUseStompHitbox, transform.position, xa.null_quat));
			}
			else
			{
				xa.tempobj = (GameObject)(Instantiate(xa.de.stompHitbox, transform.position, xa.null_quat));
			}
			xa.tempobj.transform.parent = this.gameObject.transform;
		}
	}

	public void pickUpItem()
	{
		////Debug.Log ("Picking Up " + name);
		if (pickedUp) { return; }
		pickedUp = true;

		if (killSelfInXSeconds > 0)
		{
			timeSet = fa.timeInSeconds;
		}

		if (isCoin)
		{
			//Debug.Log("PickUp, Coin Name: " + myCoinName);
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Coin);

			//save into temp list (gets saved into playerpref files on checkpointing)
			fa.coinX.Add(Mathf.RoundToInt(transform.position.x));
			fa.coinY.Add(Mathf.RoundToInt(transform.position.y));
			fa.coinLvl.Add(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

			/*
			PlayerPrefs.SetInt(myCoinName, 1);

			if (PlayerPrefs.HasKey("collectedCoins"))
			{
				int coins = PlayerPrefs.GetInt("collectedCoins", 0);
				coins++;
				PlayerPrefs.SetInt("collectedCoins", coins);

			}
			else
			{
				PlayerPrefs.SetInt("collectedCoins", 1);
			}
			PlayerPrefs.Save();*/
		}
		//if (type == "coin")
		//{
		//	Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Coin);
		//}

		if (type == "goldenButt")
		{
			//Debug.Log("BUTTTTT 1");
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Butt);
		}

		if (type == "star")
		{
			int starBitMask = 0x1 << starIndex;
			//Setup.GC_DebugLog("you got star. star bits: " + starBitMask);
			xa.carryingStars = starBitMask | xa.carryingStars;
		}
		if (type == "rainbowAmmo")//from pope battle
		{
			xa.rainbowAmmo--;
			xa.rainbowAmmoLoaded++;
		}
		if (type == "rune")//from glorg summoning
		{
			xa.runesExisting--;
			xa.runesCollected++;
			xa.runes++;
			if (xa.runesExisting < 0) { xa.runesExisting = 0; }
		}

		if (!isBouncePad) { tag = "Untagged"; }

		if (createPickedUpEffect)
		{
			xa.glx = transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.tempobj = (GameObject)(Instantiate(pickedUpEffect, xa.glx, xa.null_quat));
			if (createAsChild)
			{
				xa.tempobj.transform.parent = Camera.main.GetComponent<Camera>().transform;
			}
			else
			{
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}
		}

		if (useTriggeredTween1) { iTweenEvent.GetEvent(this.gameObject, "triggeredTween1").Play(); }
		if (useTriggeredTween2) { iTweenEvent.GetEvent(this.gameObject, "triggeredTween2").Play(); }
		if (useTriggeredTween3) { iTweenEvent.GetEvent(this.gameObject, "triggeredTween3").Play(); }

		if (useRemoteTriggeredTween1)
		{
			iTweenEvent.GetEvent(remoteGO1, "remoteTween1").Play();

			if (redDoorKey) { SaveAbilitiesNodeScript.potential_redDoorOpened = true; }
			if (pinkDoorKey) { SaveAbilitiesNodeScript.potential_pinkDoorOpened = true; }
			if (orangeDoorKey) { SaveAbilitiesNodeScript.potential_orangeDoorOpened = true; }
			if (purpleDoorKey) { SaveAbilitiesNodeScript.potential_purpleDoorOpened = true; }


		}



		if (playSound != Fresh_SoundEffects.Type.None) { Fresh_SoundEffects.PlaySound(playSound); }

		if (enableThis) { enableThis.enabled = true; }

		if (isKey)
		{
			iTweenEvent.GetEvent(door, "triggered1").Play();

			//play sound
			Setup.playSound(Setup.snds.Key);

			//turn off spawner
			SpawnerScript script;
			script = door.GetComponent<SpawnerScript>();
			script.enabled = false;

			//Destroy kill zone
			PointerScript pointerScript;
			pointerScript = door.GetComponent<PointerScript>();
			pointerScript.enabled = false;
			Destroy(pointerScript.killZone);
		}

	}
}
