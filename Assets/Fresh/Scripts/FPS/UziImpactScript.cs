using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziImpactScript : MonoBehaviour
{
	public GameObject controller;
	public GameObject starPuppet;

	float timeSet = 0;

	void Start()
	{
		float scaleTo = Random.Range(0.7f,1.3f);
		controller.transform.localScale = new Vector3(scaleTo, scaleTo, scaleTo);
		iTween.ScaleTo(controller, iTween.Hash("x", scaleTo * 2, "y", scaleTo * 2, "z", scaleTo * 2, "time", 1, "easetype", iTween.EaseType.easeInOutSine));

		controller.transform.localEulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
		
		iTween.FadeTo(starPuppet, iTween.Hash("alpha", 0, "time", 1, "easetype", iTween.EaseType.easeInOutSine));

		timeSet = fa.time;
	}

	void Update()
	{
		if (fa.time >= (timeSet + 1))
		{
			Destroy(this.gameObject);
		}
	}
}
