using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationScript : MonoBehaviour
{

	public GameObject sky2;
	public ParticleSystem fire1;
	public ParticleSystem fire2;
	public ParticleSystem fire3;
	public ParticleSystem snowflakes;
	public ParticleSystem hellflakes;
	public GameObject S;
	public GameObject A;
	public GameObject N;
	public GameObject T;
	public GameObject A2;
	public GameObject santaPuppet;
	public GameObject satanPuppet;
	public GameObject portal;

	public GameObject head;
	public GameObject body;
	public GameObject arms;

	float delay = 9;
	float timeset = 0;

	float blinkDelay = 1f;
	float blinkDelayOff = 0.03f;
	float blinkSet;
	bool blinkState = true;

	float switchDelay = 13;
	float switchSet = 0;

	void Start()
	{

		iTween.MoveBy(head, iTween.Hash("x", -0.5f, "y", -0.3, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(arms, iTween.Hash("y", 0.4f, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(body, iTween.Hash("x", 0.4f, "time", 2f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		switchSet = fa.timeInSeconds;
		timeset = fa.timeInSeconds;
		blinkSet = fa.timeInSeconds;
		S.transform.LocalSetY(-0.25f);
		A.transform.LocalSetY(0.25f);
		N.transform.LocalSetY(-0.25f);
		T.transform.LocalSetY(0.25f);
		A2.transform.LocalSetY(-0.25f);
		//satanPuppet.transform.SetY(-18.17f);
		iTween.MoveBy(S, iTween.Hash("y", 0.5f, "time", 2, "islocal", true, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(A, iTween.Hash("y", -0.5f, "time", 2, "islocal", true, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(N, iTween.Hash("y", 0.5f, "time", 2, "islocal", true, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(T, iTween.Hash("y", -0.5f, "time", 2, "islocal", true, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(A2, iTween.Hash("y", 0.5f, "time", 2, "islocal", true, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	// Update is called once per frame
	void Update()
	{
		if (fa.timeInSeconds > (switchDelay + switchSet) && blinkDelay != -1)
		{
			blinkDelay = -1;
			santaPuppet.SetActive(false);
			satanPuppet.SetActive(true);
			portal.SetActive(true);
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.MegaSatan_Scream);
			//iTween.MoveTo(santaPuppet, iTween.Hash("y", -13.5f, "time", 0.2f, "easetype", iTween.EaseType.easeInSine));

			iTween.MoveTo(portal, iTween.Hash("x", 20, "y", -7.7, "delay", 3f, "time", 3f, "easetype", iTween.EaseType.easeOutSine));

		}

		float localDelay = blinkDelay;
		if (!blinkState) { localDelay = blinkDelayOff; }

		if (fa.timeInSeconds > (blinkSet + localDelay) && blinkDelay != -1)
		{
			blinkState = !blinkState;
			santaPuppet.SetActive(blinkState);
			blinkDelay -= 0.05f;
			if (blinkDelay < 0.1f)
			{
				blinkDelay = 0.1f;
			}
			blinkSet = fa.timeInSeconds;
		}



		if (fa.timeInSeconds > (timeset + delay) && delay != -1)
		{
			delay = -1;
			fire1.Play();
			fire2.Play();
			fire3.Play();
			hellflakes.Play();
			snowflakes.Stop();
			
			//iTween.MoveTo(sky2, iTween.Hash("y", 200, "time", 0.5f, "easetype", iTween.EaseType.easeInSine));
			iTween.FadeTo(sky2, iTween.Hash("alpha", 1, "time", 1f, "easetype", iTween.EaseType.easeInSine));


			iTween.MoveTo(N, iTween.Hash("x", 6, "time", 1, "islocal", true, "easetype", iTween.EaseType.easeInOutSine));
			iTween.MoveTo(T, iTween.Hash("x", 0, "time", 1, "islocal", true, "easetype", iTween.EaseType.easeInOutSine));
			iTween.MoveTo(A2, iTween.Hash("x", 3, "time", 1, "islocal", true, "easetype", iTween.EaseType.easeInOutSine));

			iTween.RotateBy(S, iTween.Hash("z", 1, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
			iTween.RotateBy(A, iTween.Hash("z", 1, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));
			iTween.RotateBy(N, iTween.Hash("z", 1, "time", 0.8f, "easetype", iTween.EaseType.easeInOutSine));
			iTween.RotateBy(T, iTween.Hash("z", 1, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));
			iTween.RotateBy(A2, iTween.Hash("z", 1, "time", 1.2f, "easetype", iTween.EaseType.easeInOutSine));

		}
	}
}
