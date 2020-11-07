using UnityEngine;
using System.Collections;

public class BounceBlockPuppetScript : MonoBehaviour
{

	public float speed = 0;
	float counter = 0;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		counter += 10 * fa.deltaTime;
		if (counter > speed)
		{
			counter = 0;

			xa.glx = transform.localEulerAngles;
			xa.glx.z += 45;
			transform.localEulerAngles = xa.glx;
		}

	}
}
