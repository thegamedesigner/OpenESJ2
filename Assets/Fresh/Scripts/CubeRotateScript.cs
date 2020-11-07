using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotateScript : MonoBehaviour
{
	float delay = 0;
	float timeset = 0;

	void Start()
	{
	}
	

	void Update()
	{
		if (Time.time > (timeset + delay))
		{
			delay = Random.Range(1f,2f);
			timeset = Time.time;
			iTween.RotateTo(this.gameObject,iTween.Hash("x",Random.Range(-360f,360f),"y",Random.Range(-360f,360f),"z",Random.Range(-360f,360f),"time", delay,"easetype", iTween.EaseType.linear));
		}

	}
}
