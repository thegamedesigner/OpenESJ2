using UnityEngine;
using System.Collections;

public class BirdPatrolScript : MonoBehaviour
{
	public float speed = 0;
	public float speedAdd = 0;
	float cSpd = 0;

	bool flipped = false;
	float currPatrolDist = 0;

	void Start()
	{
	
		cSpd = -speed;
	}

	void Update()
	{
		xa.glx = transform.position;
	
		if (!flipped)
		{
			if (cSpd > -speed) { cSpd -= speedAdd * fa.deltaTime; } else { flipped = !flipped; }
			xa.glx.x += cSpd * fa.deltaTime;
			currPatrolDist -= 10 * fa.deltaTime;
		}
		else
		{
			if (cSpd < speed) { cSpd += speedAdd * fa.deltaTime; } else { flipped = !flipped; }
			xa.glx.x += cSpd * fa.deltaTime;
			currPatrolDist -= 10 * fa.deltaTime;
		}
		transform.position = xa.glx;



	}
}
