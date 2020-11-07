using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlatformScript : MonoBehaviour
{
	public Info infoScript;
	public FreshAni aniScript;
	bool triggered = false;
	public float timeUntilExplode = 0.45f;
	float timeSet = 0;
	public GameObject deathEffect;

	public GameObject[] createGOs;
	public GameObject[] muzzlePoints;


	void Start()
	{

	}

	void Update()
	{
		if (triggered)
		{
			//Debug.Log("fa.time: " + fa.time + ", timeSet: " + timeSet + ", delay: " + timeUntilExplode);
			if (fa.time >= (timeSet + timeUntilExplode))
			{
				Instantiate(deathEffect, transform.position, transform.rotation);
				Destroy(this.gameObject);

				for (int i = 0; i < createGOs.Length; i++)
				{
					Instantiate(createGOs[i], muzzlePoints[i].transform.position, muzzlePoints[i].transform.rotation);
				}
			}
		}
		else
		{
			if (infoScript)
			{
				if (infoScript.stoodOnByPlayer)
				{
					triggered = true;
					aniScript.PlayAnimation(1);
					timeSet = fa.time;

				}
			}
		}
	}
}
