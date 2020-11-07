using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant2Script : MonoBehaviour
{
	public Info infoScript;
	public GameObject seed;
	public GameObject flower1;
	public GameObject seedJoint;

	enum State
	{
		None,
		WaitingToBeTriggered,
		Start,
		PlantSeed,
		WaitForSeedToGrow,
		StartFlower1,
		WaitForEnd,
		End
	}
	State state = State.WaitingToBeTriggered;

	float seedAngle = 0;
	float flower1Angle = 0;
	float seedTime = 1;
	float flower1Time = 1;
	float seedScale = 1;
	float flower1Scale = 1;

	float timeSet;
	float startingDelay = 0;
	float delayBetweenGrowths = 0.1f;

	void Start()
	{
		seed.transform.localScale = Vector3.zero;
		flower1.transform.localScale = Vector3.zero;

		startingDelay = Random.Range(0, 0.7f);

		seedAngle = Random.Range(-25, 25);
		flower1Angle = Random.Range(0, 360);

		seedScale = Random.Range(0.3f, 1.3f);
		flower1Scale = Random.Range(0.7f, 1.1f);

		seedTime = Random.Range(0.2f, 0.7f);
		flower1Time = Random.Range(0.2f, 0.7f);

		timeSet = fa.time;
	}

	void Update()
	{
		if (state == State.WaitingToBeTriggered)
		{
			if (infoScript.triggered) { state = State.Start; }
			else
			{
				return;
			}
		}

		flower1.transform.position = seedJoint.transform.position;
		switch (state)
		{
			case State.Start:
				if (fa.time > (timeSet + startingDelay))
				{
					state = State.PlantSeed;
				}
				break;
			case State.PlantSeed:
				seed.transform.SetAngZ(seedAngle);
				iTween.ScaleTo(seed, iTween.Hash("x", seedScale, "y", seedScale,"z", seedScale, "time", seedTime, "easetype", iTween.EaseType.easeInOutSine));
				timeSet = fa.time;
				state = State.WaitForSeedToGrow;
				break;
			case State.WaitForSeedToGrow:
				if (fa.time > (timeSet + delayBetweenGrowths))
				{
					state = State.StartFlower1;
				}
				break;
			case State.StartFlower1:
				flower1.transform.SetAngZ(flower1Angle);
				iTween.ScaleTo(flower1, iTween.Hash("x", flower1Scale, "y", flower1Scale, "z", flower1Scale, "time", flower1Time, "easetype", iTween.EaseType.easeInOutSine));
				state = State.WaitForEnd;
				timeSet = fa.time;
				break;
			case State.WaitForEnd:
				if (fa.time > (timeSet + 3))
				{
					state = State.End;
				}
				break;
			case State.End:
				this.enabled = false;
				break;

		}
	}
}
