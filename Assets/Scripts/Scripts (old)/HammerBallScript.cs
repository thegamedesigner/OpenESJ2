using UnityEngine;
using System.Collections;

public class HammerBallScript : MonoBehaviour
{
	public float force = 0;
	public float friction = 0;
	public float delay = 0;
	public bool useRandom = false;
	public float ranMin = 0;
	public float ranMax = 0;
	public bool reverseDirection = false;

	float fuel = 0;
	float startingX = 0;
	bool jumping = false;
	float counter = 0;

	// Use this for initialization
	void Start()
	{
		startingX = transform.position.x;
		fuel = force;
		if (useRandom)
		{
			fuel = Random.Range(ranMin, ranMax);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!jumping)
		{
			counter += 10 * fa.deltaTime;
			if (counter > delay)
			{
				counter = 0;
				jumping = true;
			}
		}
		else
		{
			fuel -= friction * fa.deltaTime;

			xa.glx = transform.position;
			if (reverseDirection) { xa.glx.x += fuel * fa.deltaTime; }
			else { xa.glx.x -= fuel * fa.deltaTime; }
			transform.position = xa.glx;
	
			if (((transform.position.x >= startingX && !reverseDirection) || (transform.position.x <= startingX && reverseDirection)) && fuel < 0)
			//if (transform.position.x >= startingX && fuel < 0)
			{
				jumping = false;
				xa.glx = transform.position;
				xa.glx.x = startingX;
				transform.position = xa.glx;
				fuel = force;
				if (useRandom)
				{
					fuel = Random.Range(ranMin, ranMax);
				}
			}

		}
	}
}
