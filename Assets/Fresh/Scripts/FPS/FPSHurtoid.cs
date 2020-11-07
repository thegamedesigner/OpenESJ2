using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHurtoid : MonoBehaviour
{
	public bool square = false;

	Vector3 goal;
	int zombieMinDam = 15;
	int zombieMaxDam = 35;
	float zombieAttackDelay = 0.4f;
	float zombieAttackTimeSet;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
		if(fa.paused) {return; }
		if (FPSMainScript.FPSPlayer != null)
		{
			goal = FPSMainScript.FPSPlayer.transform.position;

		}
		//Close enough to attack?
		bool hit = false;

		if (square)
		{
			if (goal.x > transform.position.x - 1.2f &&
				goal.x < transform.position.x + 1.2f &&
				goal.z > transform.position.z - 1.2f &&
				goal.z < transform.position.z + 1.2f)
			{
				hit = true;
			}
		}
		else
		{
			if (Vector3.Distance(goal, transform.position) < 1f)//cubes are normally 3 across
			{ hit = true; }
		}


		if (hit)
		{
			if (fa.time > (zombieAttackTimeSet + zombieAttackDelay))
			{
				zombieAttackTimeSet = fa.time;
				HealthScript h = FPSMainScript.FPSPlayer.GetComponent<HealthScript>();
				h.health -= Random.Range(zombieMinDam, zombieMaxDam + 1);

				ScreenShakeCamera.Screenshake(0.5f, 0.3f, ScreenShakeCamera.ScreenshakeMethod.Basic);
			}
		}
	}
}
