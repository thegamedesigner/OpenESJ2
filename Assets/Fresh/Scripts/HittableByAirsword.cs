using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableByAirsword : MonoBehaviour
{
	public bool destroyWhenHealthIsZero = false;
	public HealthScript healthScript;
	public GameObject impactExplo;
	public GameObject impactExploPoint;
	public bool giveAirswordBoost = false;
	public int damage = 0;
	public GameObject destroyGO = null;
	public void HitByPlayer()
	{
		if (healthScript == null) { return; }//This isn't killable by airsword
		if (damage == 0)
		{
			healthScript.health = 0;
		}
		else
		{
			healthScript.health -= damage;
		}

		if (destroyGO != null) { Destroy(destroyGO); }

		if (impactExplo != null)
		{
			GameObject go = Instantiate(impactExplo, impactExploPoint.transform.position, impactExploPoint.transform.rotation);
			//set facing based on player direction
			if (xa.playerDir == 1)
			{
				go.transform.SetScaleX(1);
			}
			else
			{
				go.transform.SetScaleX(-1);
			}
		}
	}

	public void Update()
	{
		if (destroyWhenHealthIsZero)
		{
			bool dead = false;
			if (healthScript != null)
			{
				if (healthScript.health <= 0)
				{
					dead = true;
				}
			}
			else
			{
				dead = true;
			}

			if (dead)
			{
				if (impactExplo != null)
				{
					GameObject go = Instantiate(impactExplo, impactExploPoint.transform.position, impactExploPoint.transform.rotation);
				}
				if (destroyGO != null) { Destroy(destroyGO); }
			}
		}
	}
}
