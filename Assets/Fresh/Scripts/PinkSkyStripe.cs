using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkSkyStripe : MonoBehaviour
{

	void Start()
	{

	}

	void Update()
	{

		float localFreq = xa.beat_Freq;
		float multi = 15;
		float scaleSpdUp = 4;
		float scaleSpdDown = 4;

		Vector3 goal = new Vector3(localFreq * multi, localFreq * multi, localFreq * multi);
		
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
		if (scale.x < 0.1f) { scale.x = 0.1f; }
		if (scale.x > 2.5f) { scale.x = 2.5f; }
		transform.localScale = scale;
	}
}
