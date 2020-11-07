using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRocketScript : MonoBehaviour
{
	public GameObject exploPrefab;
	public GameObject puppet;
	public ParticleSystem particles;
	public GameObject exploSound;

	float minDam = 50;
	float maxDam = 150;

	Dimensions.Dimension dimension = Dimensions.Dimension.Red;

	float spd = 2;
	void Start()
	{
		dimension = Dimensions.currentDimension;
	}


	Ray ray = new Ray();
	RaycastHit hit;
	LayerMask mask = 1 << 19 | 1 << 20;
	Vector3 lastPos;

	void Update()
	{
		if(fa.paused) {return; }
		if (dimension != Dimensions.currentDimension)
		{
			//make puppet invisible
		}

		lastPos = transform.position;

		spd += spd + (1 * fa.deltaTime);
		if (spd > 55) { spd = 55; }
		transform.Translate(0, 0, spd * fa.deltaTime);

		ray.origin = lastPos;
		ray.direction = transform.forward;
		float dist = Vector3.Distance(transform.position, lastPos);

		if (Physics.Raycast(ray, out hit, dist, mask))
		{
			float amount = 0;
			if (Vector3.Distance(FPSPlayer.playerPos, hit.point) < 30) { amount = 0.1f; }
			if (Vector3.Distance(FPSPlayer.playerPos, hit.point) < 20) { amount = 0.3f; }
			if (Vector3.Distance(FPSPlayer.playerPos, hit.point) < 10) { amount = 0.7f; }
			ScreenShakeCamera.Screenshake(amount, 0.5f, ScreenShakeCamera.ScreenshakeMethod.Basic);
			//Hit something! 

			//Explode!
			GameObject es = Instantiate(exploSound, hit.point, transform.rotation);

			GameObject go;
			go = Instantiate(exploPrefab, hit.point, transform.rotation);

			//hurt everyone in a radius
			float damageRadius = 5;

			if (Vector3.Distance(transform.position, FPSMainScript.playerPos) < damageRadius)
			{
				FPSMainScript.FPSPlayerScript.healthScript.health -= Random.Range(minDam, maxDam + 1);
			}
			for (int i = 0; i < FPSMainScript.zombies.Count; i++)
			{
				if (Vector3.Distance(transform.position, FPSMainScript.zombies[i].transform.position) < damageRadius)
				{
					FPSMainScript.zombies[i].healthScript.health -= Random.Range(minDam, maxDam + 1);
				}
			}
			for (int i = 0; i < FPSMainScript.demons.Count; i++)
			{
				if (Vector3.Distance(transform.position, FPSMainScript.demons[i].transform.position) < damageRadius)
				{
					FPSMainScript.demons[i].healthScript.health -= Random.Range(minDam, maxDam + 1);
				}
			}
			for (int i = 0; i < FPSMainScript.anyMonster.Count; i++)
			{
				if (FPSMainScript.anyMonster[i] != null)
				{
					if (Vector3.Distance(transform.position, FPSMainScript.anyMonster[i].transform.position) < damageRadius)
					{
						FPSMainScript.anyMonster[i].health -= Random.Range(minDam, maxDam + 1);
					}
				}
			}

			//destroy self
			particles.transform.SetParent(null);
			particles.Stop();
			Destroy(this.gameObject);
		}

	}
}
