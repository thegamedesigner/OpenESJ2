using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatanLegsScript : MonoBehaviour
{
	public GameObject leg;
	float timeset;
	float delay;
	public int stage = 1;
	int waitingStage = -1;
	float spd = 0.1f;

	void Start()
	{

	}

	void Update()
	{
		switch (stage)
		{
			//Waiting
			case 0: 
				if(fa.time > (timeset + delay))
				{
					timeset = 0;
					delay = 0;
					stage = waitingStage;
				}
				break;

			//Backward
			case 1: 
				leg.transform.LocalSetY(-2.96f);
				leg.transform.LocalSetX(-0.18f);
				timeset = fa.time;
				delay = spd;
				waitingStage = 2;
				stage = 0;
				iTween.MoveBy(leg, iTween.Hash("x", 2.5f, "time", spd, "easetype", iTween.EaseType.easeInOutSine));
				break;
				
			//Up
			case 2: 
				timeset = fa.time;
				delay = spd;
				waitingStage = 3;
				stage = 0;
				iTween.MoveBy(leg, iTween.Hash("y", 1.73f, "time", spd, "easetype", iTween.EaseType.easeInSine));
				break;

			//Forward
			case 3: 
				timeset = fa.time;
				delay = spd;
				waitingStage = 4;
				stage = 0;
				iTween.MoveBy(leg, iTween.Hash("x", -2.5f, "time", spd, "easetype", iTween.EaseType.easeInOutSine));
				break;

			//Down
			case 4: 
				timeset = fa.time;
				delay = spd;
				waitingStage = 1;
				stage = 0;
				iTween.MoveBy(leg, iTween.Hash("y", -1.73f, "time", spd, "easetype", iTween.EaseType.easeInSine));
				break;
			
		}
	}
}
