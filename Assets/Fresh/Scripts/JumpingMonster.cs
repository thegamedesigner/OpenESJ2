using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMonster : MonoBehaviour
{
	float timeSet = 0;
	float delay = 0.93f;
	void Start()
	{

	}

	void Update()
	{
		if (Controls.GetInputDown(Controls.Type.Jump, 0))
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
