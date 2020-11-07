using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCannonScript : MonoBehaviour
{
	bool active = false;//Hasn't seen the player yet
	public bool isTongueTurret = false;
	public GameObject bulletPrefab;
	public GameObject muzzlePoint;
	public GameObject firingSoundEffect;

	public HealthScript healthScript;
	public GameObject corpse;
	int uid = -1;

	public float firingDelay = 1.2f;
	float timeSet = 0;

	float oldHealth = 0;
	public GameObject puppet1;
	public GameObject deathSound;
	public GameObject gib1Prefab;
	public GameObject gib2Prefab;
	public GameObject gib1Muzzle;
	public GameObject gib2Muzzle;

	void Start()
	{
		if(isTongueTurret)
		{
			healthScript.health = FPSBalanceScript.FPSTurret;
		}
		else
		{
			healthScript.health = FPSBalanceScript.FPSCannon;
		}
		//add self to anyMonster list
		FPSMainScript.uids++;
		uid = FPSMainScript.uids;
		healthScript.uid = uid;
		FPSMainScript.anyMonster.Add(healthScript);

	}

	void Update()
	{
		if(fa.paused) {return; }
		if (oldHealth != healthScript.health)
		{
			oldHealth = healthScript.health;

			iTween.PunchRotation(puppet1, iTween.Hash("z", Random.Range(-45,45), "time", Random.Range(0.3f,0.5f), "easetype", iTween.EaseType.linear));
			//iTween.PunchRotation(puppet2, iTween.Hash("z", Random.Range(-45,45), "time", Random.Range(0.3f,0.5f), "easetype", iTween.EaseType.linear));
			/*
			if (healthScript.health > FPSBalanceScript.FPSCannonHurtHP)
			{
				puppet1.SetActive(true);
				puppet2.SetActive(false);
			}
			else
			{
				puppet1.SetActive(false);
				puppet2.SetActive(true);
			}*/
		}

		if (healthScript.health <= 0)
		{
			GameObject ds = Instantiate(deathSound, transform.position, deathSound.transform.rotation);

			GameObject g1 = Instantiate(gib1Prefab, gib1Muzzle.transform.position, gib1Muzzle.transform.rotation);
			GameObject g2 = Instantiate(gib2Prefab, gib2Muzzle.transform.position, gib2Muzzle.transform.rotation);

			
			for(int i = 0;i < FPSMainScript.anyMonster.Count;i++)
			{
				if(FPSMainScript.anyMonster[i].uid == uid) {FPSMainScript.anyMonster.RemoveAt(i);break;  }
			}
			Destroy(this.gameObject);
		}
		else
		{
			if (fa.time > (firingDelay + timeSet))
			{
				timeSet = fa.time;
				GameObject go = Instantiate(bulletPrefab, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
						
			}
		}
	}
}
