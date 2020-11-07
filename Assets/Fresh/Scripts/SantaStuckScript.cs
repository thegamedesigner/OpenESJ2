using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaStuckScript : MonoBehaviour
{
	public GameObject endPortal;
	public GameObject nextSanta;
	public bool dontSetNextSanta = false;
	public GameObject[] missile;
	public GameObject[] muzzlePoint;
	public HealthScript healthScript;
	public StateBasedCamera camScript;
	public FreshAni aniScript;
	public Vector2 pos;
	public Vector2 santaPos;
	bool dead = false;
	float deathTimeSet;
	public bool attacking = false;

	float missileTimeSet;
	public float missileFiringDelay = 3;
	float finishedTimeSet = -1;
	public bool setSelfInactive = false;

	void Awake()
	{
		if (nextSanta != null && !dontSetNextSanta)
		{
			SantaStuckScript script = nextSanta.GetComponent<SantaStuckScript>();
			script.setSelfInactive = true;
		}
	}

	void Start()
	{
		if (setSelfInactive) { this.gameObject.SetActive(false); }
	}

	void Update()
	{
		if (fa.time >= (finishedTimeSet + 3) && finishedTimeSet != -1)
		{
			if (nextSanta != null) { nextSanta.SetActive(true); }
			this.gameObject.SetActive(false);

		}

		if (dead)
		{
			if (fa.time >= (deathTimeSet + 5))
			{
				dead = false;
				aniScript.PlayAnimation(0);
			}
		}
		else
		{
			if (attacking)
			{
				if (fa.time >= (missileTimeSet + missileFiringDelay))
				{
					missileTimeSet = fa.time;

					for (int i = 0; i < muzzlePoint.Length; i++)
					{
						Instantiate(missile[i], muzzlePoint[i].transform.position, muzzlePoint[i].transform.rotation);
					}
				}
			}


			if (healthScript.health <= 0)
			{
				iTween.MoveTo(this.gameObject, iTween.Hash("x", santaPos.x, "y", santaPos.y, "time", 3, "easetype", iTween.EaseType.easeOutSine));
				finishedTimeSet = fa.time;
				if (endPortal != null) { endPortal.SetActive(true); }
				camScript.pos = pos;
				camScript.state = StateBasedCamera.stateTypes.Pos;
				healthScript.health = 100;
				aniScript.PlayAnimation(1);
				deathTimeSet = fa.time;
				dead = true;
			}
		}

	}
}
