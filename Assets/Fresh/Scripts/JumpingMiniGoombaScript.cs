using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMiniGoombaScript : MonoBehaviour
{
	public float startingDelay = 0;
	float startingTimeSet = 0;
	float timeSet = 0;
	float delay = 1.2f;
	bool unlocked = false;

	void Start()
	{
		startingDelay = transform.position.x;
		while(startingDelay > 4) {startingDelay -= 4; }
		startingTimeSet = fa.time;
	}

	void Update()
	{
		if (!unlocked)
		{
			if (fa.time >= (startingTimeSet + startingDelay))
			{
				unlocked = true;
			}
		}
		else
		{
			if (fa.time >= (timeSet + delay))
			{
				timeSet = fa.time;
				Jump();
			}
		}

	}
	void Jump()
	{
		iTween.MoveBy(this.gameObject, iTween.Hash("y", 5, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveBy(this.gameObject, iTween.Hash("delay", 0.42f, "y", -5, "time", 0.5f, "easetype", iTween.EaseType.easeInSine));
	}
}
