using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheScript : MonoBehaviour
{
	public bool InfCamScythe = false;
	public bool CameraBounceScythe = false;
	public GameObject puppet;
	public Vector3 vel;
	float timeset;
	public float lifespan = 5.5f;
	public GameObject deathExplo;
	void Start()
	{
		iTween.RotateBy(puppet, iTween.Hash("z", 1, "time", 0.3f, "looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.linear));
		timeset = fa.time;
	}

	void Update()
	{
		if (fa.time > (timeset + lifespan) && !InfCamScythe)
		{
			if (deathExplo != null)
			{
				Instantiate(deathExplo, transform.position, transform.rotation);
			}
			Destroy(this.gameObject);
		}
		transform.position += vel * Time.deltaTime * fa.pausedFloat;


		if (!CameraBounceScythe)
		{
			if (transform.position.y > 10) { vel.y = -vel.y; }
			if (transform.position.y < -10) { vel.y = -vel.y; }
			if (transform.position.x > 15.5f) { vel.x = -vel.x; }
			if (transform.position.x < -23) { vel.x = -vel.x; }
		}
		if (CameraBounceScythe)
		{
			if (transform.position.y > Camera.main.transform.position.y + 10) { vel.y = -vel.y; }
			if (transform.position.y < Camera.main.transform.position.y + -10) { vel.y = -vel.y; }
			

		}

		//transform.Translate(speed * fa.deltaTime, 0, 0);
	}
}
