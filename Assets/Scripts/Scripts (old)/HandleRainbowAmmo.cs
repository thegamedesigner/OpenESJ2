using UnityEngine;
using System.Collections;

public class HandleRainbowAmmo : MonoBehaviour
{
	public GameObject rainbowAmmo;
	public GameObject NPC;

	int lastSpawnId = -1;

	float counter = 0;

	void Start()
	{
		getNPCScripts();
		xa.rainbowAmmo = 0;
		xa.rainbowAmmoLoaded = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (xa.popeHits <= 11)
		{
			if (xa.rainbowAmmo <= 0)
			{
				if (counter < 15)
				{
					counter += 10 * fa.deltaTime;
				}
				else
				{
					counter = 0;
					spawnNewRainbowAmmo();
				}
			}
			if (xa.rainbowAmmoLoaded > 0)
			{
				xa.rainbowAmmoLoaded--;
				fireFromNpc();
			}
		}
	}

	void spawnNewRainbowAmmo()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("ammoSpawn");
		int result = (int)(Random.Range(0, gos.Length));
		if (lastSpawnId == result) { result += 1; if (result >= gos.Length) { result = 0; } }
		lastSpawnId = result;
		xa.glx = gos[result].transform.position;
		xa.tempobj = (GameObject)(Instantiate(rainbowAmmo,xa.glx,xa.null_quat));
		xa.rainbowAmmo += 1;
	}

	GunScript script;
	SpawnBasedOnDistFromPlayer script2;
		AniScript_TriggeredAni script3;

	void getNPCScripts()
	{
		script = NPC.GetComponent<GunScript>();
		script2 = NPC.GetComponent<SpawnBasedOnDistFromPlayer>();
		script3 = NPC.GetComponent<AniScript_TriggeredAni>();
	}

	void fireFromNpc()
	{
		script.fireABullet++;

		script2.createObject();

		script3.triggerAni2();

	}
}
