using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
	public bool tame = false;
	public GameObject seed;
	public GameObject branch1;
	public GameObject branch2;
	public GameObject branch3;
	public GameObject flower1;
	public GameObject seedJoint;
	public GameObject branch1Joint;
	public GameObject branch2Joint;
	public GameObject branch3Joint;

	enum State
	{
		None,
		StartingDelay,
		PlantSeed,
		WaitForSeedToGrow,
		StartBranch1,
		WaitForBranch1,
		StartBranch2,
		WaitForBranch2,
		StartBranch3,
		WaitForBranch3,
		StartFlower1,
		WaitForEnd,

		End
	}
	State state = State.None;

	float seedAngle = 0;
	float branch1Angle = 0;
	float branch2Angle = 0;
	float branch3Angle = 0;
	float flower1Angle = 0;
	float seedTime = 1;
	float branch1Time = 1;
	float branch2Time = 1;
	float branch3Time = 1;
	float flower1Time = 1;
	float seedScale = 1;
	float branch1Scale = 1;
	float branch2Scale = 1;
	float branch3Scale = 1;
	float flower1Scale = 1;

	float timeSet;
	float startingDelay = 0;
	float delayBetweenGrowths = 0.1f;

	void Start()
	{
		state = State.StartingDelay;

		seed.transform.localScale = Vector3.zero;
		branch1.transform.localScale = Vector3.zero;
		branch2.transform.localScale = Vector3.zero;
		branch3.transform.localScale = Vector3.zero;
		flower1.transform.localScale = Vector3.zero;

		startingDelay = Random.Range(0, 0.7f);

		seedAngle = Random.Range(-45, 45);
		branch1Angle = Random.Range(-45, 45);
		branch2Angle = Random.Range(-45, 45);
		branch3Angle = Random.Range(-45, 45);
		flower1Angle = Random.Range(0, 360);

		seedScale = Random.Range(0.8f, 1.2f);
		branch1Scale = Random.Range(0.5f, 1.5f);
		branch2Scale = Random.Range(0.5f, 1.5f);
		branch3Scale = Random.Range(0.5f, 1.5f);
		flower1Scale = Random.Range(0.7f, 2f);

		seedTime = Random.Range(0.2f, 0.7f);
		branch1Time = Random.Range(0.2f, 0.7f);
		branch2Time = Random.Range(0.2f, 0.7f);
		branch3Time = Random.Range(0.2f, 0.7f);
		flower1Time = Random.Range(0.2f, 0.7f);

		if (Random.Range(0f, 10f) < 1)
		{
			seedScale = Random.Range(0.8f, 1.2f);
			branch1Scale = Random.Range(1.5f, 2.5f);
			branch2Scale = Random.Range(1.5f, 2.5f);
			branch3Scale = Random.Range(1.5f, 2.5f);
			flower1Scale = Random.Range(1.7f, 2.5f);

		}

		if (tame)
		{
			seedAngle = Random.Range(-15, 15);
			branch1Angle = Random.Range(-15, 15);
			branch2Angle = Random.Range(-15, 15);
			branch3Angle = Random.Range(-15, 15);
			flower1Angle = Random.Range(0, 360);

			seedScale = Random.Range(0.5f, 1f);
			branch1Scale = Random.Range(0.5f, 1f);
			branch2Scale = Random.Range(0.5f, 1f);
			branch3Scale = Random.Range(0.5f, 1f);
			flower1Scale = Random.Range(0.5f, 1f);
		}

		timeSet = fa.time;
	}

	void Update()
	{
		branch1.transform.position = seedJoint.transform.position;
		branch2.transform.position = branch1Joint.transform.position;
		branch3.transform.position = branch2Joint.transform.position;
		flower1.transform.position = branch3Joint.transform.position;
		switch (state)
		{
			case State.StartingDelay:
				if (fa.time > (timeSet + startingDelay))
				{
					state = State.PlantSeed;
				}
				break;
			case State.PlantSeed:
				seed.transform.SetAngZ(seedAngle);
				iTween.ScaleTo(seed, iTween.Hash("x", seedScale, "y", seedScale, "time", seedTime, "easetype", iTween.EaseType.easeInOutSine));
				timeSet = fa.time;
				state = State.WaitForSeedToGrow;
				break;
			case State.WaitForSeedToGrow:
				if (fa.time > (timeSet + delayBetweenGrowths))
				{
					state = State.StartBranch1;
				}
				break;
			case State.StartBranch1:
				branch1.transform.SetAngZ(branch1Angle);
				iTween.ScaleTo(branch1, iTween.Hash("x", 1, "y", branch1Scale, "time", branch1Time, "easetype", iTween.EaseType.easeInOutSine));
				timeSet = fa.time;
				state = State.WaitForBranch1;
				break;
			case State.WaitForBranch1:
				if (fa.time > (timeSet + delayBetweenGrowths))
				{
					state = State.StartBranch2;
				}
				break;
			case State.StartBranch2:
				branch2.transform.SetAngZ(branch2Angle);
				iTween.ScaleTo(branch2, iTween.Hash("x", 1, "y", branch2Scale, "time", branch2Time, "easetype", iTween.EaseType.easeInOutSine));
				timeSet = fa.time;
				state = State.WaitForBranch2;
				break;

			case State.WaitForBranch2:
				if (fa.time > (timeSet + delayBetweenGrowths))
				{
					state = State.StartBranch3;
				}
				break;
			case State.StartBranch3:
				branch3.transform.SetAngZ(branch3Angle);
				iTween.ScaleTo(branch3, iTween.Hash("x", 1, "y", branch3Scale, "time", branch3Time, "easetype", iTween.EaseType.easeInOutSine));
				timeSet = fa.time;
				state = State.WaitForBranch3;
				break;
			case State.WaitForBranch3:
				if (fa.time > (timeSet + delayBetweenGrowths))
				{
					state = State.StartFlower1;
				}
				break;
			case State.StartFlower1:
				flower1.transform.SetAngZ(flower1Angle);
				iTween.ScaleTo(flower1, iTween.Hash("x", flower1Scale, "y", flower1Scale, "time", flower1Time, "easetype", iTween.EaseType.easeInOutSine));
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
