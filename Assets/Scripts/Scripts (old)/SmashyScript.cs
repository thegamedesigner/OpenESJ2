using UnityEngine;
using System.Collections;

public class SmashyScript : MonoBehaviour
{
	public float upSpeed = 0;
	public float downSpeed = 0;
	public float upDelayTime = 0;
	public float downDelayTime = 0;
	public float upSpeedAdd = 0;
	public float downSpeedAdd = 0;
	public float distance = 0;


	int state = 0;
	Vector3 vec1 = Vector3.zero;
	Vector3 startPos = Vector3.zero;
	float upDelay = 0;
	float downDelay = 0;
	float currentUpSpeed = 0;
	float currentDownSpeed = 0;

	// Use this for initialization
	void Start()
	{
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (state == 0)
		{
			//going up
			currentUpSpeed += upSpeedAdd * fa.deltaTime;
			if (currentUpSpeed > upSpeed) { currentUpSpeed = upSpeed; }
			vec1 = transform.position;
			vec1.y += currentUpSpeed * fa.deltaTime;
			transform.position = vec1;

			if (Vector3.Distance(startPos, transform.position) > distance) { state = 1; }
		}
		else if (state == 1)
		{
			currentUpSpeed = 0;
			//wait a bit at the top
			upDelay += 10 * fa.deltaTime;
			if (upDelay > upDelayTime)
			{
				upDelay = 0;
				state = 2;
			}

		}
		else if (state == 2)
		{
			//going down
			currentDownSpeed += downSpeedAdd * fa.deltaTime;
			if (currentDownSpeed > downSpeed) { currentDownSpeed = downSpeed; }
			vec1 = transform.position;
			vec1.y -= currentDownSpeed * fa.deltaTime;
			transform.position = vec1;

			if (transform.position.y <= startPos.y)
			{
				vec1 = transform.position;
				vec1.y = startPos.y;
				transform.position = vec1;

				state = 3;
			}
		}
		else if (state == 3)
		{
			currentDownSpeed = 0;
			//wait a bit at the top
			downDelay += 10 * fa.deltaTime;
			if (downDelay > downDelayTime)
			{
				downDelay = 0;
				state = 0;
			}
		}
	}
}
