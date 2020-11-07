using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonControllerScript : MonoBehaviour
{
	public GameObject[] balloons = new GameObject[0];
	public GameObject load;

	float gravityFast = 0.1f;
	float gravityVel = 0;
	float startingY = 0;

	void Start()
	{
		startingY = transform.position.y;
	}

	void Update()
	{
		int balloonsLost = 0;
		for (int i = 0; i < balloons.Length; i++)
		{
			if (balloons[i] == null) { balloonsLost++; }
		}

		if (balloonsLost == 1)
		{
			transform.AddY(-0.5f * fa.deltaTime);
		}
		if (balloonsLost == balloons.Length)//no balloons
		{
			gravityVel += gravityFast;
			transform.AddY(-gravityVel * fa.deltaTime);
		}
		
		if (startingY > (transform.position.y + 15))
		{
			Destroy(this.gameObject);
		}
		if (startingY < (transform.position.y - 15))
		{
			Destroy(this.gameObject);
		}

		
		if(load == null)
		{
			gravityVel += gravityFast;
			transform.AddY(gravityVel * fa.deltaTime);
		}


	}
}
