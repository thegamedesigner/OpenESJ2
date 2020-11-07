using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoofBulletScript : MonoBehaviour
{
	float lifespanInSeconds = 3;
	float lifeTimeSet;
	public float speed = 6;
	void Start()
	{
		lifeTimeSet = fa.time;
		iTween.FadeTo(this.gameObject,iTween.Hash("alpha",0,"time",2,"easetype",iTween.EaseType.linear));
	}

	void Update()
	{
		speed -= 5 * Time.deltaTime;
		if(speed < 0) {speed = 0; }
		transform.Translate(0,speed * Time.deltaTime,0);

		if(fa.time > (lifeTimeSet + lifespanInSeconds))
		{
			Destroy(this.gameObject);
		}
	}
}
