using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LagHackScript : MonoBehaviour
{
	int framesLagging = 0;
	void Start()
	{

	}

	void Update()
	{
		float fps = 1.0f / Time.deltaTime;
		if (fps < 50)
		{
			framesLagging++;
		}

		if (framesLagging > 60)
		{
			Destroy(this.gameObject);
			return;
		}
	}
}
