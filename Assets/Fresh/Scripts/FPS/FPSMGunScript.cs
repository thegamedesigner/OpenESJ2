using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMGunScript : MonoBehaviour
{
	public Dimensions.Dimension dimension = Dimensions.Dimension.Red;
	public GameObject bulletPrefab;
	public GameObject muzzlePoint;
	public FreshAni aniScript;
	public FreshAni aniScript2;
	public GameObject gatling_snd;
	public HealthScript healthScript;
	public GameObject corpse;
	public GameObject puppet1;
	public GameObject puppet2;
	public GameObject deathSound;
	public GameObject gib1Prefab;
	public GameObject gib2Prefab;
	public GameObject gib1Muzzle;
	public GameObject gib2Muzzle;


	enum State
	{
		None,
		StartIdle,
		IdleWaiting,
		Alert,
		StartWarmUp,
		WarmingUp,
		StartFiring,
		Firing,
		StartReloading,
		Reloading,
		Dead,
		End
	}

	State state = State.None;

	float idle_timeset;
	float warmUp_timeset;
	float firing_timeset;
	float reloading_timeset;
	float alert_sightDist = 100;
	float firingSpeed_TimeSet;
	int uid = -1;
	float oldHealth = 0;

	void Start()
	{
		healthScript.health = FPSBalanceScript.FPSMgunHP;
		if (dimension == Dimensions.Dimension.Blue) { transform.AddY(-500); }
		state = State.Alert;

		//add self to demon list
		FPSMainScript.uids++;
		uid = FPSMainScript.uids;
		FPSMainScript.demons.Add(this);

	}

	void Update()
	{
		if(fa.paused) {return; }
		if (oldHealth != healthScript.health)
		{
			oldHealth = healthScript.health;

				iTween.PunchRotation(puppet1, iTween.Hash("z", Random.Range(-45,45), "time", Random.Range(0.3f,0.5f), "easetype", iTween.EaseType.linear));
				iTween.PunchRotation(puppet2, iTween.Hash("z", Random.Range(-45,45), "time", Random.Range(0.3f,0.5f), "easetype", iTween.EaseType.linear));
			
			if (healthScript.health > FPSBalanceScript.FPSMgunHurtHP)
			{
				puppet1.SetActive(true);
				puppet2.SetActive(false);
			}
			else
			{
				puppet1.SetActive(false);
				puppet2.SetActive(true);
			}
		}

		if (Dimensions.currentDimension != dimension) { return; }
		//text.text = "" + healthScript.health + "(" + state + ")";

		if (healthScript.health <= 0)
		{
			GameObject ds = Instantiate(deathSound, transform.position, deathSound.transform.rotation);
			
			GameObject g1 = Instantiate(gib1Prefab, gib1Muzzle.transform.position, gib1Muzzle.transform.rotation);
			GameObject g2 = Instantiate(gib2Prefab, gib2Muzzle.transform.position, gib2Muzzle.transform.rotation);

			state = State.Dead;
			//GameObject go = Instantiate(corpse, transform.position, corpse.transform.rotation);
			//go.transform.SetY(0); 

			for (int i = 0; i < FPSMainScript.demons.Count; i++)
			{
				if (FPSMainScript.demons[i].uid == uid) { FPSMainScript.demons.RemoveAt(i); break; }
			}
			Destroy(this.gameObject);
		}
		else
		{
			switch (state)
			{
				case State.Alert:
					{
						idle_timeset = 0;
						warmUp_timeset = 0;
						firing_timeset = 0;
						reloading_timeset = 0;

						//Can I see the player?
						Vector3 oldAngles = transform.localEulerAngles;
						float dist;
						Ray ray = new Ray();
						RaycastHit hit;
						LayerMask mask = 1 << 19;

						transform.LookAt(FPSMainScript.playerPos);
						ray.origin = transform.position;
						ray.direction = transform.forward;
						dist = Vector3.Distance(transform.position, FPSMainScript.playerPos);
						transform.localEulerAngles = oldAngles;

						if (!Physics.Raycast(ray, out hit, dist, mask))
						{
							state = State.StartWarmUp;
						}
					}
					break;
				case State.StartWarmUp:
					{
						aniScript.PlayAnimation(1);
						aniScript2.PlayAnimation(1);
						warmUp_timeset = fa.time;
						state = State.WarmingUp;

						//GameObject go = Instantiate(gatling_snd, transform);
					}
					break;
				case State.WarmingUp:
					{
						if (fa.time > (warmUp_timeset + 0.64f))
						{
							state = State.StartFiring;
						}
					}
					break;
				case State.StartFiring:
					{
						firing_timeset = fa.time;
						state = State.Firing;

					}
					break;
				case State.Firing:
					{
						if (fa.time > (firingSpeed_TimeSet + 0.1f))
						{
							firingSpeed_TimeSet = fa.time;
							GameObject go = Instantiate(bulletPrefab, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
						}

						if (fa.time > (firing_timeset + 1f))
						{
							state = State.StartReloading;
						}
					}
					break;
				case State.StartReloading:
					{
						aniScript.PlayAnimation(2);
						aniScript2.PlayAnimation(2);
						reloading_timeset = fa.time;
						state = State.Reloading;
					}
					break;
				case State.Reloading:
					{
						if (fa.time > (firing_timeset + 0.64f))
						{
							state = State.Alert;
						}
					}
					break;
			}
		}
	}
}
