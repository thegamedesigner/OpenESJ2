using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatan2BScript : MonoBehaviour
{

	public bool doBoss = false;
	bool startedBoss = false;
	public GameObject Controller_Landing;

	public GameObject landing_head;
	public GameObject landing_body;
	public GameObject landing_arms;
	public GameObject landing_legs;
	public GameObject blastPoint;
	public GameObject blastEffect;

	float timeSet = 0;
	float delay = 0;

	string toSay;
	bool firstYell = false;

	State lastState = State.None;
	State state = State.Intro;
	Intro intro = Intro.StartLanding;

	enum Intro
	{
		StartLanding,
		Impact,
		Yell,
		AfterYell,
		End
	}
	enum State
	{
		None,
		Intro,
		Waiting,
		After,
		End
	}

	void Start()
	{
		InitLanding();
	}

	void Update()
	{
		if (!xa.hasCheckpointed)
		{
			if (!startedBoss)
			{
				startedBoss = true;
				if (doBoss) { EnterLanding(); }
			}
			if (doBoss)
			{
				HandleBoss();
			}
		}
	}

	void HandleBoss()
	{
		switch (state)
		{
			case State.Intro:

				switch (intro)
				{
					case Intro.StartLanding:
						landing_legs.transform.SetY(-8.62f);
						landing_head.transform.SetY(-2f);
						landing_arms.transform.SetY(-4.44f);
						landing_body.transform.SetY(-4.47f);
						landing_legs.transform.AddY(23);
						landing_head.transform.AddY(23);
						landing_arms.transform.AddY(23);
						landing_body.transform.AddY(23);
					//	Debug.Log("HERE " + Time.timeSinceLevelLoad);
						iTween.MoveTo(landing_legs, iTween.Hash("delay", 0.3f, "y", -8.62f, "time", 0.3f, "easetype", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.none));
						iTween.MoveTo(landing_head, iTween.Hash("delay", 0.3f, "y", -2f, "time", 0.27f, "easetype", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.none));
						iTween.MoveTo(landing_arms, iTween.Hash("delay", 0.3f, "y", -4.44f, "time", 0.33f, "easetype", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.none));
						iTween.MoveTo(landing_body, iTween.Hash("delay", 0.3f, "y", -4.47f, "time", 0.3f, "easetype", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.none));
						intro = Intro.Impact;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 0.37f;
						break;
					case Intro.Impact:
						ScreenShakeCamera.Screenshake(2, 0.35f, ScreenShakeCamera.ScreenshakeMethod.Basic);
						GameObject go = Instantiate(blastEffect, blastPoint.transform.position, blastPoint.transform.rotation);
						intro = Intro.Yell;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 0.5f;
						break;
					case Intro.Yell:
						//Yell();
						intro = Intro.AfterYell;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 0.1f;
						break;
					case Intro.AfterYell:
						intro = Intro.End;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 0.1f;
						break;

				}

				break;
			case State.Waiting:
				if (intro != Intro.End)
				{
					if (fa.time > (timeSet + delay))
					{
						timeSet = 0;
						delay = 0;
						state = State.Intro;
					}

				}
				break;

		}
	}

	void EnterLanding()
	{
		Controller_Landing.SetActive(true);
	}

	void AllOff()
	{
		Controller_Landing.SetActive(false);
	}


	void InitLanding()
	{
		//Arms
		iTween.MoveBy(landing_arms, iTween.Hash("y", 0.5f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Head
		iTween.MoveBy(landing_head, iTween.Hash("x", -0.55f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//body
		iTween.MoveBy(landing_body, iTween.Hash("y", -0.3f, "time", 2, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}

}
