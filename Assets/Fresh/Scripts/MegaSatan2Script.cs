using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatan2Script : MonoBehaviour
{
	public bool doBoss = false;
	bool startedBoss = false;

	public GameObject Controller_Idle;
	public GameObject Controller_Landing;
	public GameObject Controller_Charging;
	public GameObject Controller_ChargingSubController;

	public TextMesh IdleText;
	public TextMesh ChargeText;

	public GameObject idle_head;//These are for idle, before labels were used.
	public GameObject idle_body;
	public GameObject idle_arms;
	public GameObject idle_legs;

	public GameObject landing_head;
	public GameObject landing_body;
	public GameObject landing_arms;
	public GameObject landing_legs;
	public GameObject blastPoint;
	public GameObject blastEffect;

	public GameObject charging_head;
	public GameObject charging_body;
	public GameObject charging_arms;

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
		Decide,
		Waiting,
		Idle,
		Charging,
		End
	}

	int choice = 0;

	void Start()
	{
		InitLanding();
		InitIdle();
		InitCharging();
	}

	void Update()
	{
		if (xa.hasCheckpointed)
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

	void YellIntro()
	{

	}

	void HandleBoss()
	{
		float leftX = Camera.main.gameObject.transform.position.x - 25;
		float rightX = Camera.main.gameObject.transform.position.x + 31;
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
			case State.Decide:

				
				state = State.Charging;


				if (choice == 0)
				{
					choice = 1;
					state = State.Idle;
				}
				else if (choice == 1)
				{
					state = State.Charging;
					choice = 0;
				}
				break;
			case State.Waiting:
				if (fa.time > (timeSet + delay))
				{
					timeSet = 0;
					delay = 0;
					if (intro != Intro.End)
					{//If the intro isn't finished, go back to the intro
						state = State.Intro;
					}
					else
					{
						state = State.Decide;
					}
				}
				break;
			case State.Idle:
				timeSet = fa.time;
				delay = 0.8f;
				EnterIdle();
				state = State.Waiting;
				break;
			case State.Charging:

				EnterCharging();
				
				iTween.MoveTo(Controller_Charging, iTween.Hash("x", leftX, "time", 2f, "easetype", iTween.EaseType.linear));
				iTween.MoveTo(Controller_Charging, iTween.Hash("delay", 2f, "x", rightX, "time", 2f, "easetype", iTween.EaseType.linear));
				iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 2f, "x", -1, "time", 0));
				iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 4f, "x", 1, "time", 0));

				Controller_Idle.transform.SetX(rightX);

				timeSet = fa.time;
				delay = 4f;
				state = State.Waiting;
				break;

		}
	}

	void EnterIdle()
	{
		AllOff();
		Controller_Idle.SetActive(true);
	}
	void EnterLanding()
	{
		AllOff();
		Controller_Landing.SetActive(true);
	}
	void EnterCharging()
	{
		AllOff();
		Controller_Charging.SetActive(true);
	}


	void AllOff()
	{
		Controller_Idle.SetActive(false);
		Controller_Landing.SetActive(false);
		Controller_Charging.SetActive(false);
	}


	void InitLanding()
	{
		//Arms
		iTween.MoveBy(landing_arms, iTween.Hash("y", 0.1f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Head
		iTween.MoveBy(landing_head, iTween.Hash("x", -0.15f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}

	void InitIdle()
	{
		//Head
		iTween.MoveBy(idle_head, iTween.Hash("x", 0.6f, "y", 0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(idle_arms, iTween.Hash("x", 0.15f, "y", -0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(idle_body, iTween.Hash("y", -0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}

	void InitCharging()
	{
		//Head
		iTween.MoveBy(charging_head, iTween.Hash("x", 0.6f, "y", 0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(charging_arms, iTween.Hash("y", 0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(charging_body, iTween.Hash("y", -0.2f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}


}
