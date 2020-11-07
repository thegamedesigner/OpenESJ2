using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSZombieScript : MonoBehaviour
{
	public bool isHulk = false;
	public HealthScript healthScript;
	public GameObject corpse;
	public Material hurtMat;
	public Material hurtMat2;//only used for hulk
	public MeshRenderer meshRenderer;
	public GameObject puppet;
	public GameObject deathSound;
	public GameObject gib1Prefab;
	public GameObject gib2Prefab;
	public GameObject gib1Muzzle;
	public GameObject gib2Muzzle;

	bool active = false;//Hasn't seen the player yet

	float speed = 0;
	int zombieMinDam = 1;
	int zombieMaxDam = 3;
	float zombieAttackDelay = 0.4f;
	float zombieAttackTimeSet;
	Vector3 goal;
	Vector3 tempGoal;
	Vector3 offset;
	float offsetDelay = 1;
	float offsetTimeset;
	int uid = -1;
	bool hurt = false;
	bool hurt2 = false;
	bool wasHit = false;
	float oldHealth = 0;

	void Start()
	{
		healthScript.health = FPSBalanceScript.FPSZombieHP;
		if (isHulk) { healthScript.health = FPSBalanceScript.FPSHulkHP; }
		oldHealth = healthScript.health;

		//add self to zombie list
		FPSMainScript.uids++;
		uid = FPSMainScript.uids;
		FPSMainScript.zombies.Add(this);

		//dimension = Dimensions.currentDimension;
		speed = Random.Range(4f, 6f);
		goal = transform.position;
		tempGoal = transform.position;
	}

	void Update()
	{
		if(fa.paused) {return; }
		//getting hit
		if (oldHealth != healthScript.health)
		{
			oldHealth = healthScript.health;
			//got hit

			iTween.PunchRotation(puppet, iTween.Hash("z", Random.Range(-45, 45), "time", Random.Range(0.3f, 0.5f), "easetype", iTween.EaseType.linear));
		}

		if (isHulk)
		{
			if (!hurt && healthScript.health <= FPSBalanceScript.FPSHulkHurtHP1)
			{
				hurt = true;
				meshRenderer.material = hurtMat;
			}
			if (!hurt2 && healthScript.health <= FPSBalanceScript.FPSHulkHurtHP2)
			{
				hurt2 = true;
				meshRenderer.material = hurtMat2;
			}
		}
		else
		{
			if (!hurt && healthScript.health <= FPSBalanceScript.FPSZombieHurtHP)
			{
				hurt = true;
				meshRenderer.material = hurtMat;
			}
		}

		if (healthScript.health <= 0)
		{
			GameObject ds = Instantiate(deathSound, transform.position, deathSound.transform.rotation);

			GameObject g1 = Instantiate(gib1Prefab, gib1Muzzle.transform.position, gib1Muzzle.transform.rotation);
			GameObject g2 = Instantiate(gib2Prefab, gib2Muzzle.transform.position, gib2Muzzle.transform.rotation);

			for (int i = 0; i < FPSMainScript.zombies.Count; i++)
			{
				if (FPSMainScript.zombies[i].uid == uid) { FPSMainScript.zombies.RemoveAt(i); break; }
			}

			Destroy(this.gameObject);
		}
		else
		{
			if (FPSMainScript.FPSPlayer != null)
			{
				goal = FPSMainScript.FPSPlayer.transform.position;

			}

			if (fa.time >= (offsetTimeset + offsetDelay))
			{
				offsetTimeset = fa.time;
				offsetDelay = Random.Range(0.5f, 5f);
				offset.x = Random.Range(-2f, 2f);
				offset.z = Random.Range(-2f, 2f);
			}

			//Close enough to attack?
			if (Vector3.Distance(goal, transform.position) < 2)
			{
				if (fa.time > (zombieAttackTimeSet + zombieAttackDelay))
				{
					zombieAttackTimeSet = fa.time;
					HealthScript h = FPSMainScript.FPSPlayer.GetComponent<HealthScript>();
					h.health -= Random.Range(zombieMinDam, zombieMaxDam + 1);

					ScreenShakeCamera.Screenshake(0.2f, 0.1f, ScreenShakeCamera.ScreenshakeMethod.Basic);
				}
			}
			else
			{
				Vector3 oldAngles = transform.localEulerAngles;

				//ok, so move. Can I see the player?
				Ray ray = new Ray();
				RaycastHit hit;
				LayerMask mask = 1 << 19;
				float dist = 0;

				transform.LookAt(FPSMainScript.playerPos);
				ray.origin = transform.position;
				ray.direction = transform.forward;
				dist = Vector3.Distance(ray.origin, FPSMainScript.playerPos);

				if (!Physics.Raycast(ray, out hit, dist, mask))
				{
					//can see the player
					tempGoal = goal;
					active = true;
				}
				else
				{
					if (active)
					{
						//can't see the player. Find the node with the lowest steps, that I can see.
						int bestYet = 999;
						int bestIndex = -1;
						for (int i = 0; i < PF.nodes.Count; i++)
						{
							if (PF.nodes[i].steps < bestYet)//If it's worth checking if I can see it
							{
								transform.LookAt(PF.nodes[i].go.transform);
								ray.origin = transform.position;
								ray.direction = transform.forward;
								dist = Vector3.Distance(ray.origin, PF.nodes[i].go.transform.position);

								if (!Physics.Raycast(ray, out hit, dist, mask))
								{
									bestYet = PF.nodes[i].steps;
									bestIndex = i;
								}
							}
						}

						tempGoal = transform.position;
						if (bestIndex != -1)
						{
							tempGoal = PF.nodes[bestIndex].go.transform.position;
						}
					}
				}

				if (active)
				{
					transform.localEulerAngles = oldAngles;
					transform.LookAt(tempGoal);// + offset);
					transform.Translate(0, 0, speed * Time.deltaTime);
				}
			}
		}
	}
}
