using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMegaSatanScript : MonoBehaviour
{
	public GameObject IdleController;
	public GameObject ChargingController;
	public GameObject portal;

	public GameObject IdleHead;
	public GameObject IdleBody;
	public GameObject IdleLegs;
	public GameObject IdleArms;

	public GameObject ChargingHead;
	public GameObject ChargingBody;
	public GameObject ChargingLeg1;
	public GameObject ChargingLeg2;
	public GameObject ChargingArms;

	float stateChargeTime = 8;
	float stateFiringTime = 2;
	float stateTimeSet = 0;
	float firingSpeed_TimeSet = 0;
	float firingSpeed_Delay = 0.5f;

	public GameObject muzzlePoint;
	public GameObject bulletPrefab;
	public GameObject zombiePrefab;
	bool active = false;//Hasn't seen the player yet
	float speed = 0;
	int minDam = 1;
	int maxDam = 3;
	float attackDelay = 0.4f;
	float attackTimeSet;
	Vector3 goal;
	Vector3 tempGoal;
	public HealthScript healthScript;
	public GameObject corpse;
	int uid = -1;
	
	float oldHealth = 0;
	public GameObject GenericGib1;
	public GameObject GenericGib2;
	public GameObject HeadGib;
	public GameObject HeadGibMuzzle;
	public GameObject GenericGibMuzzle1;
	public GameObject GenericGibMuzzle2;
	public GameObject GenericGibMuzzle3;
	public GameObject GenericGibMuzzle4;
	public GameObject GenericGibMuzzle5;
	public GameObject GenericGibMuzzle6;
	public MeshRenderer Mesh_FPSHead;
	public MeshRenderer Mesh_IdleHead;
	public Material FPSHeadHurtMat1;
	public Material FPSHeadHurtMat2;
	public Material IdleHeadHurtMat1;
	public Material IdleHeadHurtMat2;

	enum State
	{
		None,
		Firing,//Standing still, shooting fireballs out his groin
		Charging,//Roaring, charging at you. (Zombie movement, but won't leave the main room)
		End
	}
	State state = State.Firing;//starts inactive, so it doesn't matter

	void Start()
	{
		healthScript.health = FPSBalanceScript.MegaSatanHP;
		InitItweens();


		//add self to zombie list
		FPSMainScript.uids++;
		uid = FPSMainScript.uids;
		FPSMainScript.anyMonster.Add(healthScript);

		//dimension = Dimensions.currentDimension;
		speed = Random.Range(4f, 6f);
		goal = transform.position;
		tempGoal = transform.position;
	}

	void InitItweens()
	{
		iTween.MoveBy(IdleHead, iTween.Hash("x", 0.6f, "y", 0.3f, "time", 0.3f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(IdleArms, iTween.Hash("x", 0.15f, "y", -0.3f, "time", 0.3f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(IdleBody, iTween.Hash("y", -0.3f, "time", 0.3f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		iTween.MoveBy(ChargingHead, iTween.Hash("x", 0.4f, "y", 0.5f, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(ChargingArms, iTween.Hash("x", 0.15f, "y", -0.5f, "time", 0.3f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(ChargingBody, iTween.Hash("y", -0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		iTween.MoveTo(ChargingLeg1, iTween.Hash("y", 0.12f, "islocal", true, "time", 0.15f, "easetype", iTween.EaseType.easeInOutCubic, "looptype", iTween.LoopType.pingPong));
		iTween.MoveTo(ChargingLeg2, iTween.Hash("y", -0.33, "islocal", true, "time", 0.15f, "easetype", iTween.EaseType.easeInOutCubic, "looptype", iTween.LoopType.pingPong));


	}
	bool hurt1;
	bool hurt2; 
	void Update()
	{
		if(fa.paused) {return; }
		//changing hurt state

			if (!hurt1 && healthScript.health <= FPSBalanceScript.MegaSatanHurtHP1)
			{
				hurt1 = true;
				Mesh_IdleHead.material = IdleHeadHurtMat1;
				Mesh_FPSHead.material = FPSHeadHurtMat1;
			}
			if (!hurt2 && healthScript.health <= FPSBalanceScript.MegaSatanHurtHP2)
			{
				hurt2 = true;
				Mesh_IdleHead.material = IdleHeadHurtMat2;
				Mesh_FPSHead.material = FPSHeadHurtMat2;
			}
		//getting hit
		if (oldHealth != healthScript.health)
		{
			oldHealth = healthScript.health;
			//got hit

			iTween.PunchRotation(IdleHead, iTween.Hash("z", Random.Range(-45, 45), "time", Random.Range(0.3f, 0.5f), "easetype", iTween.EaseType.linear));
			iTween.PunchRotation(IdleBody, iTween.Hash("z", Random.Range(-45, 45), "time", Random.Range(0.3f, 0.5f), "easetype", iTween.EaseType.linear));
			iTween.PunchRotation(ChargingHead, iTween.Hash("z", Random.Range(-45, 45), "time", Random.Range(0.3f, 0.5f), "easetype", iTween.EaseType.linear));
			iTween.PunchRotation(ChargingBody, iTween.Hash("z", Random.Range(-45, 45), "time", Random.Range(0.3f, 0.5f), "easetype", iTween.EaseType.linear));
		}

		float delay = 1;
		if (state == State.Charging) { delay = stateChargeTime; }
		if (state == State.Firing) { delay = stateFiringTime; }
		if (fa.time >= (delay + stateTimeSet))
		{
			stateTimeSet = fa.time;
			if (state == State.Charging)
			{
				state = State.Firing;
				ChargingController.SetActive(false);
				IdleController.SetActive(true);
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.FPSMS_Scream1);
			}
			else
			{
				state = State.Charging;
				ChargingController.SetActive(true);
				IdleController.SetActive(false);
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.FPSMS_Scream2);
			}
		}


		if (state == State.Firing)
		{
			if (fa.time > (firingSpeed_TimeSet + firingSpeed_Delay))
			{
				Vector3 target = Vector3.zero;

				if (FPSMainScript.FPSPlayer != null)
				{
					target = FPSMainScript.FPSPlayer.transform.position;

				}
				muzzlePoint.transform.LookAt(target);// + offset);
				firingSpeed_TimeSet = fa.time;
				GameObject go = Instantiate(zombiePrefab, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
				go = Instantiate(bulletPrefab, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
			}
		}


		if (healthScript.health <= 0)
		{
			GameObject g1 = Instantiate(HeadGib, HeadGibMuzzle.transform.position, HeadGibMuzzle.transform.rotation);
			Instantiate(GenericGib1, GenericGibMuzzle1.transform.position, GenericGibMuzzle1.transform.rotation);
			Instantiate(GenericGib1, GenericGibMuzzle2.transform.position, GenericGibMuzzle2.transform.rotation);
			Instantiate(GenericGib1, GenericGibMuzzle3.transform.position, GenericGibMuzzle3.transform.rotation);
			Instantiate(GenericGib2, GenericGibMuzzle4.transform.position, GenericGibMuzzle4.transform.rotation);
			Instantiate(GenericGib2, GenericGibMuzzle5.transform.position, GenericGibMuzzle5.transform.rotation);
			Instantiate(GenericGib2, GenericGibMuzzle6.transform.position, GenericGibMuzzle6.transform.rotation);

			portal.transform.SetX(transform.position.x);
			portal.transform.SetZ(transform.position.z);
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.FPSMS_Death);
			GameObject go = Instantiate(corpse, transform.position, corpse.transform.rotation);

			for (int i = 0; i < FPSMainScript.anyMonster.Count; i++)
			{
				if (FPSMainScript.anyMonster[i].uid == uid) { FPSMainScript.anyMonster.RemoveAt(i); break; }
			}

			Destroy(this.gameObject);
		}
		else
		{
			if (FPSMainScript.FPSPlayer != null)
			{
				goal = FPSMainScript.FPSPlayer.transform.position;

			}

			//Close enough to melee attack?
			if (Vector3.Distance(goal, transform.position) < 2)
			{
				if (fa.time > (attackTimeSet + attackDelay))
				{
					attackTimeSet = fa.time;
					HealthScript h = FPSMainScript.FPSPlayer.GetComponent<HealthScript>();
					h.health -= Random.Range(minDam, maxDam + 1);

					ScreenShakeCamera.Screenshake(0.1f, 0.1f, ScreenShakeCamera.ScreenshakeMethod.Basic);
				}
			}
			else
			{
				if (state == State.Charging)//if moving
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
						if (active == false)
						{
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.FPSMS_Intro);
						}
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
						if (tempGoal.x > 36) { tempGoal.x = 36; }
						if (tempGoal.x < 12) { tempGoal.x = 12; }
						if (tempGoal.z > -72) { tempGoal.z = -72; }
						if (tempGoal.z < -96) { tempGoal.z = -96; }
						ScreenShakeCamera.Screenshake(0.01f, 0.1f, ScreenShakeCamera.ScreenshakeMethod.Basic);
						transform.localEulerAngles = oldAngles;
						transform.LookAt(tempGoal);// + offset);
						transform.Translate(0, 0, speed * Time.deltaTime);
						transform.localEulerAngles = oldAngles;

					}
				}
			}
		}
	}
}
