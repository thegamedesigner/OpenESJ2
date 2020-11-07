using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMonsterBulletScript : MonoBehaviour
{
	public float speed = 5;
	public float actualSpeed = 5;
	public int minDam = 20;
	public int maxDam = 30;
	public GameObject exploPrefab;
	bool dead = false;

	void Start()
	{

	}

	Ray ray = new Ray();
	RaycastHit hit;
	LayerMask mask = 1 << 19;
	Vector3 lastPos;

	void Update()
	{
		if(fa.paused) {return; }
		if (!dead)
		{

			lastPos = transform.position;
			transform.Translate(0, 0, actualSpeed * fa.deltaTime);

			if (Vector3.Distance(FPSMainScript.playerPos, transform.position) < 1)
			{
				FPSMainScript.FPSPlayerScript.healthScript.health -= Random.Range(minDam, maxDam + 1);
				ScreenShakeCamera.Screenshake(0.2f, 0.1f, ScreenShakeCamera.ScreenshakeMethod.Basic);
				GameObject go;
				go = Instantiate(exploPrefab, hit.point, transform.rotation);
				dead = true;
				Destroy(this.gameObject);
			}

			ray.origin = lastPos;
			ray.direction = transform.forward;
			float dist = Vector3.Distance(transform.position, lastPos);

			if (Physics.Raycast(ray, out hit, dist, mask))
			{
				dead = true;
				//Hit something! 

				//Explode!
				GameObject go;
				go = Instantiate(exploPrefab, hit.point, transform.rotation);

				//destroy self
				Destroy(this.gameObject);
			}
		}
	}
}
