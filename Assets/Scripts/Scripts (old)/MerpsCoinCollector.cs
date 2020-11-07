using UnityEngine;
using System.Collections;

public class MerpsCoinCollector : MonoBehaviour
{
	public GameObject coinSndEffect   = null;
	public static int totalMerpsCoins = 0;
	GameObject[] gos;
	GameObject[] exitDoors;
	float coinCollectionDist          = 1;

	void Start()
	{
		exitDoors = GameObject.FindGameObjectsWithTag("merpsExitDoor");
		gos = GameObject.FindGameObjectsWithTag("merpsCoin");
		totalMerpsCoins = gos.Length; // Lose that while loop! I was sent here by a warning of go was assigned but unused ;) -TM

		if (totalMerpsCoins <= 0)
		{
			triggerExitDoors();
		}
	}

	void Update()
	{
		foreach (GameObject go in gos)
		{
			if (go)//I don't update this array except on start, so some of the GO's may be null
			{
				if (go.tag == "merpsCoin")
				{
					xa.glx = go.transform.position;
					xa.glx.z = transform.position.z;
					if (Vector3.Distance(xa.glx, transform.position) < coinCollectionDist)
					{
						go.SendMessage("playAni1");
						xa.tempobj = (GameObject)(Instantiate(coinSndEffect, go.transform.position, coinSndEffect.transform.rotation));
						go.tag = "Untagged"; 
						totalMerpsCoins--;
						if (totalMerpsCoins <= 0)
						{
							triggerExitDoors();
							break;
						}
					}
				}
			}
		}
	}

	void triggerExitDoors()
	{
		foreach (GameObject go in exitDoors)
		{
			if (go)//I don't update this array except on start, so some of the GO's may be null
			{
				go.SendMessage("openDoor");
			}
		}
	}
}
