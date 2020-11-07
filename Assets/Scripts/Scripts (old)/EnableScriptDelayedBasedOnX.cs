using UnityEngine;
using System.Collections;

public class EnableScriptDelayedBasedOnX : MonoBehaviour
{
	public Behaviour scriptToActivate;
	public float delayInSecondsPerX = 0;

	float timeSet = -1;
	float result1 = 0;
	//written so coins animations can be enabled based on X, so they do the wave


	void Update()
	{
		if (this.enabled)
		{
			if (timeSet == -1) 
			{ 
				timeSet = fa.time;
				result1 = (21 + transform.position.x + (16 - transform.position.y)) * delayInSecondsPerX;
			}

			if (fa.time > (timeSet + result1))
			{
				scriptToActivate.enabled = true;
				this.enabled = false;
			}
		}
	}
}
