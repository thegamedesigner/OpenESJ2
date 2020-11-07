using UnityEngine;
using System.Collections;

public class JumpingScript : MonoBehaviour
{
	public float force = 0;
	public float friction = 0;
	public float delay = 0;
	public bool useRandom = false;
	public float ranMin = 0;
	public float ranMax = 0;
	public bool reverseDirection = false;
	public bool jumpWhenPlayerJumps = false;

	float fuel = 0;
	float startingY = 0;
	bool jumping = false;
	float counter = 0;

	// Use this for initialization
	void Start()
	{
		startingY = transform.position.y;
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
			if (jumpWhenPlayerJumps)
			{
				if (xa.playerJumped)
				{
					jumping = true;
				}
			}
			else
			{

				counter += 10 * fa.deltaTime;
				if (counter > delay)
				{
					counter = 0;
					jumping = true;
				}
			}
		}
		else
		{
			fuel -= friction * fa.deltaTime;

			xa.glx = transform.position;
			if (reverseDirection) { xa.glx.y -= fuel * fa.deltaTime; }
			else { xa.glx.y += fuel * fa.deltaTime; }
			transform.position = xa.glx;

			if (((transform.position.y <= startingY && !reverseDirection) || (transform.position.y >= startingY && reverseDirection)) && fuel < 0)
			{
				jumping = false;
				xa.glx = transform.position;
				xa.glx.y = startingY;
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
