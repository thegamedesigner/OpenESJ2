using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyEffect1Script : MonoBehaviour
{
	public GameObject innerInnerStar;

	void Start()
	{
	}

	void Update()
	{
		float localFreq = xa.beat_Freq;
		float multi = 5;
		float scaleSpdUp = 2;
		float scaleSpdDown = 2;

		Vector3 goal = new Vector3(localFreq * multi, localFreq * multi, localFreq * multi);
		goal *= 5;
		
		Vector3 scale;

		if (transform.localScale.x < goal.x)
		{
			scale = transform.localScale;
			scale.x += scaleSpdUp * fa.deltaTime;
			transform.localScale = scale;
		}
		else
		{
			scale = transform.localScale;
			scale.x -= scaleSpdDown * fa.deltaTime;
			transform.localScale = scale;
		}

		scale = transform.localScale;
		//if (scale.x < 0.1f) { scale.x = 0.1f; }
		//if (scale.x > 2.5f) { scale.x = 2.5f; }
		scale.y = scale.x;
		transform.localScale = scale;
		
		scale.x = (1 - scale.x);
		if(scale.x > 0) {scale.x = 0; }
		scale.y = scale.x;

		scale *= 32;
		innerInnerStar.transform.localScale = scale;
		

	}
}
