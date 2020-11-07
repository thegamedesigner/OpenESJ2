using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatanScript : MonoBehaviour
{
	public bool doBoss = false;
	bool startedBoss = false;

	public GameObject aboutToFlipPrefab;
	public GameObject Controller_Atk;
	public GameObject Controller_Atk2;
	public GameObject Controller_Idle;
	public GameObject Controller_Stunned;
	public GameObject Controller_Big;
	public GameObject Controller_Landing;
	public GameObject Controller_Charging;
	public GameObject Controller_ChargingSubController;
	public GameObject Controller_Outro;
	public GameObject Controller_CuteGhost;
	public GameObject Controller_DieToGhost;

	public TextMesh IdleText;
	public TextMesh ChargeText;
	public TextMesh StunnedText;
	public TextMesh AttackingText;
	public TextMesh DieToGhostText;
	public TextMesh OutroText;

	public GameObject finalSkull;
	public GameObject finalArms;
	public GameObject outroMuzzlePoint;
	public GameObject flameDeathExplo;
	public GameObject sparksDeathExplo;
	public GameObject epicDeathPinwheel;
	public GameObject portal;
	public GameObject goldenButt;

	public GameObject head;//These are for idle, before labels were used.
	public GameObject body;
	public GameObject arms;
	public GameObject legs;

	public GameObject atk_head;
	public GameObject atk_body;
	public GameObject atk_arms;
	public GameObject atk_legs;
	public GameObject scythPrefab;
	public GameObject muzzlePoint1;
	public GameObject muzzlePoint2;
	public GameObject muzzlePoint3;

	public GameObject atk2_head;
	public GameObject atk2_body;
	public GameObject atk2_arms;
	public GameObject atk2_legs;

	public GameObject ghostSkull;

	public GameObject stunned_head;
	public GameObject stunned_body;
	public GameObject stunned_arms;
	public GameObject stunned_legs;
	public GameObject stunnedMuzzlePoint;

	public GameObject big_head;
	public GameObject big_body;
	public GameObject big_arms;

	public GameObject landing_head;
	public GameObject landing_body;
	public GameObject landing_arms;
	public GameObject landing_legs;
	public GameObject blastPoint;
	public GameObject blastEffect;

	public GameObject charging_head;
	public GameObject charging_body;
	public GameObject charging_arms;

	public AnimateUsingMatsScript dieToGhost_AniScript;
	public GameObject dieToGhost_Quad;
	public GameObject dieToGhost_Ghost;
	public GameObject dieToGhost_Corpse;
	public GameObject dieToGhost_CorpseMuzzlePoint;

	public HealthScript healthScript;

	float timeSet = 0;
	float delay = 0;
	int nonRandomCuteGhostCycle = 0;

	string toSay;
	bool firstYell = false;

	State lastState = State.None;
	State state = State.Intro;
	Intro intro = Intro.StartLanding;
	Outro outro = Outro.NotStartedYet;
	ChoiceStage choiceStage = ChoiceStage.Phase1_ChargeAndScythe;

	enum ChoiceStage
	{
		Intro,
		Phase1_ChargeAndScythe,
		Phase2_Fireballs,
		Phase3_CrazyCharge,
		Phase4_Vanish,
		Phase5_CuteGhost,
		Phase99_DerpyGhostDeath,
		End
	}

	enum Intro
	{
		StartLanding,
		Impact,
		Yell,
		AfterYell,
		End
	}
	enum Outro
	{
		NotStartedYet,
		StartTweening,
		Yell,
		AfterYell,
		DropButt,
		Explode,
		Portal,
		End
	}

	enum State
	{
		None,
		Intro,
		Decide,
		Waiting,
		Idle,
		Stunned,
		Charging,
		Attacking,
		Attacking2,
		LongIdle,
		Outro,
		Dead,
		CuteGhost,
		Big,
		DieToGhost,
		DieToGhost2,
		End
	}

	int choice = 0;

	void Start()
	{
		InitLanding();
		InitIdle();
		InitAtk();
		InitAtk2();
		InitStunned();
		InitBig();
		InitCharging();
		InitOutro();

		//if (doBoss) { EnterOutro(); }
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
		//Debug.Log("State: " + state + ", Intro: " + intro + ", Outro: " + outro + ", choice: " + choice + ", ChoicePhase: " + choiceStage);
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


				if (healthScript.health > 100 && healthScript.health <= 125 && choiceStage != ChoiceStage.Phase2_Fireballs) { choice = 0; choiceStage = ChoiceStage.Phase2_Fireballs; }
				if (healthScript.health > 0 && healthScript.health <= 100 && choiceStage != ChoiceStage.Phase5_CuteGhost) { choice = 0; choiceStage = ChoiceStage.Phase4_Vanish; }
				if (healthScript.health <= 0 && choiceStage != ChoiceStage.Phase99_DerpyGhostDeath) { choice = 0; choiceStage = ChoiceStage.Phase99_DerpyGhostDeath; }


				if (choiceStage == ChoiceStage.Phase1_ChargeAndScythe)
				{
					if (choice == 0)
					{
						choice = 1;
						state = State.Idle;
					}
					else if (choice == 1)
					{
						state = State.Charging;
						if (healthScript.health < 180) { choice = 2; }
						else { choice = 3; }

					}
					else if (choice == 2)
					{
						state = State.Attacking;
						choice = 3;
					}
					else if (choice == 3)
					{
						choice = 4;
						state = State.Stunned;
					}
					else if (choice == 4)
					{
						choice = 0;
						state = State.LongIdle;
					}

				}
				else if (choiceStage == ChoiceStage.Phase2_Fireballs)
				{
					if (choice == 0)
					{
						choice = 1;
						state = State.Idle;
					}
					else if (choice == 1)
					{
						choice = 2;
						state = State.Attacking2;
					}
					else if (choice >= 2)
					{
						choice = 0;
						state = State.Stunned;
					}

				}
				else if (choiceStage == ChoiceStage.Phase4_Vanish)
				{
					choice = 0;
					state = State.DieToGhost;
					choiceStage = ChoiceStage.Phase5_CuteGhost;
				}
				else if (choiceStage == ChoiceStage.Phase5_CuteGhost)
				{
					state = State.CuteGhost;



				}
				else if (choiceStage == ChoiceStage.Phase99_DerpyGhostDeath)
				{
					state = State.Outro;
					outro = Outro.StartTweening;
					EnterOutro();
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
					else if (outro != Outro.End && outro != Outro.NotStartedYet)
					{
						state = State.Outro;
					}
					else if (lastState == State.DieToGhost)
					{
						state = State.DieToGhost2;
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
			case State.LongIdle:
				timeSet = fa.time;
				delay = 1f;
				EnterIdle();
				state = State.Waiting;
				break;
			case State.Stunned:
				timeSet = fa.time;
				delay = 5;
				EnterStunned();
				GameObject g4 = Instantiate(aboutToFlipPrefab, stunnedMuzzlePoint.transform.position, stunnedMuzzlePoint.transform.rotation);

				state = State.Waiting;
				break;
			case State.Attacking:
				timeSet = fa.time;
				delay = 3;
				EnterAttacking();
				state = State.Waiting;

				GameObject g1 = Instantiate(scythPrefab, muzzlePoint1.transform.position, muzzlePoint1.transform.rotation);
				//GameObject g2 = Instantiate(scythPrefab,muzzlePoint2.transform.position,muzzlePoint2.transform.rotation);
				GameObject g3 = Instantiate(scythPrefab, muzzlePoint3.transform.position, muzzlePoint3.transform.rotation);
				ScytheScript ss = null;
				ss = g1.GetComponent<ScytheScript>();
				ss.vel = new Vector3(-6, -6, 0);
				//ss = g2.GetComponent<ScytheScript>();
				//ss.vel = new Vector3(-14,11,0);
				ss = g3.GetComponent<ScytheScript>();
				ss.vel = new Vector3(-6, 4, 0);

				break;
			case State.Attacking2:
				timeSet = fa.time;
				delay = 5;
				EnterAttacking2();
				state = State.Waiting;

				break;
			case State.Big:
				timeSet = fa.time;
				delay = 3;
				EnterBig();
				state = State.Waiting;

				break;
			case State.Charging:

				EnterCharging();
				iTween.MoveTo(Controller_Charging, iTween.Hash("x", -21.5f, "time", 1f, "easetype", iTween.EaseType.linear));
				iTween.MoveTo(Controller_Charging, iTween.Hash("delay", 1f, "x", 10.27f, "time", 1f, "easetype", iTween.EaseType.linear));
				iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 1f, "x", -1, "time", 0));
				iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 2f, "x", 1, "time", 0));

				timeSet = fa.time;
				delay = 2f;
				state = State.Waiting;
				break;
			case State.DieToGhost:
				EnterDieToGhost();
				dieToGhost_AniScript.Play();

				dieToGhost_Ghost.transform.SetX(3.5f);
				dieToGhost_Ghost.transform.SetY(-14.86f);
				iTween.MoveTo(dieToGhost_Ghost, iTween.Hash("delay", 1f, "y", 0.14, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));

				lastState = State.DieToGhost;
				timeSet = fa.time;
				delay = 2f;
				state = State.Waiting;
				break;
			case State.DieToGhost2:
				if(xa.pgMode)
				{
					DieToGhostText.text = "Cray cray, bro bro!";
				}
				else
				{
					DieToGhostText.text = "Fuck it!";
				}
				iTween.MoveTo(dieToGhost_Ghost, iTween.Hash("delay", 1f, "y", 18.11, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));

				Instantiate(dieToGhost_Corpse, dieToGhost_CorpseMuzzlePoint.transform.position, dieToGhost_CorpseMuzzlePoint.transform.rotation);

				lastState = State.None;
				timeSet = fa.time;
				delay = 3f;
				state = State.Waiting;
				break;
			case State.CuteGhost:

				EnterCuteGhost();

				if (nonRandomCuteGhostCycle == 0)
				{
					nonRandomCuteGhostCycle = 1;
					ghostSkull.transform.position = new Vector3(19f, 9.5f, 55);
					iTween.MoveTo(ghostSkull, iTween.Hash("x", -28.9f, "y", -9.1f, "time", 5f, "easetype", iTween.EaseType.linear));
				}
				else if (nonRandomCuteGhostCycle == 1)
				{
					nonRandomCuteGhostCycle = 2;
					ghostSkull.transform.position = new Vector3(4.1f, 16.2f, 55);
					iTween.MoveTo(ghostSkull, iTween.Hash("x", -3.7f, "y", -13f, "time", 5f, "easetype", iTween.EaseType.linear));
				}
				else if (nonRandomCuteGhostCycle == 2)
				{
					nonRandomCuteGhostCycle = 3;
					ghostSkull.transform.position = new Vector3(21f, 5f, 55);
					iTween.MoveTo(ghostSkull, iTween.Hash("x", -30f, "y", 5f, "time", 5f, "easetype", iTween.EaseType.linear));
				}
				else if (nonRandomCuteGhostCycle == 3)
				{
					nonRandomCuteGhostCycle = 0;
					ghostSkull.transform.position = new Vector3(21f, -4.4f, 55);
					iTween.MoveTo(ghostSkull, iTween.Hash("x", -30f, "y", -4.4f, "time", 5f, "easetype", iTween.EaseType.linear));
				}


				timeSet = fa.time;
				delay = 6f;
				state = State.Waiting;
				break;

			case State.Outro:

				switch (outro)
				{
					case Outro.StartTweening:
						toSay = "Nooooooooooooo!\nMy beautiful butt!";
						OutroText.text = "";
						firstYell = false;
						finalSkull.transform.SetX(31);
						finalSkull.transform.SetY(-9);

						iTween.MoveTo(finalSkull, iTween.Hash("x", -4f, "y", -2f, "time", 2f, "easetype", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.none));
						outro = Outro.Yell;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 2.2f;
						break;
					case Outro.Yell:
						if (!firstYell)
						{
							firstYell = true;
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.MegaSatan_Scream);
						}
						if (toSay.Length > 0)
						{
							string chop = "" + toSay[0];
							toSay = toSay.Remove(0, 1);
							OutroText.text += chop;

							outro = Outro.Yell;
							delay = 0.05f;
						}
						else
						{

							outro = Outro.AfterYell;
							delay = 0.5f;
						}

						state = State.Waiting;
						timeSet = fa.time;
						break;
					case Outro.AfterYell:
						outro = Outro.DropButt;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 0.1f;
						break;
					case Outro.DropButt:

						goldenButt.transform.SetParent(null, true);
						//goldenButt.transform.SetScaleX(12);
						//goldenButt.transform.SetScaleY(12);
						iTween.MoveTo(goldenButt, iTween.Hash("x", -2f, "y", -8.9f, "time", 1f, "easetype", iTween.EaseType.easeOutBounce));
						iTween.RotateBy(goldenButt, iTween.Hash("z", 1, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));

						Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.MegaSatan_Bounce);

						outro = Outro.Explode;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 2f;
						break;
					case Outro.Explode:

						Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.MegaSatan_Explo);
						//ScreenShakeCamera.Screenshake(2, 0.45f, ScreenShakeCamera.ScreenshakeMethod.Basic);
						Instantiate(flameDeathExplo, outroMuzzlePoint.transform.position, outroMuzzlePoint.transform.rotation);
						Instantiate(sparksDeathExplo, outroMuzzlePoint.transform.position, outroMuzzlePoint.transform.rotation);
						Instantiate(epicDeathPinwheel, outroMuzzlePoint.transform.position, outroMuzzlePoint.transform.rotation);



						outro = Outro.Portal;
						state = State.Waiting;
						timeSet = fa.time;
						delay = 3f;
						break;
					case Outro.Portal:
						portal.SetActive(true);
						outro = Outro.End;
						state = State.Dead;
						timeSet = fa.time;
						delay = 1f;
						break;

				}

				break;

		}
	}

	void EnterStunned()
	{
		AllOff();
		Controller_Stunned.SetActive(true);
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

	void EnterAttacking()
	{
		AllOff();
		Controller_Atk.SetActive(true);
	}

	void EnterAttacking2()
	{
		AllOff();
		Controller_Atk2.SetActive(true);
	}

	void EnterOutro()
	{
		AllOff();
		Controller_Outro.SetActive(true);
	}

	void EnterCuteGhost()
	{
		AllOff();
		Controller_CuteGhost.SetActive(true);
	}

	void EnterBig()
	{
		AllOff();
		Controller_Big.SetActive(true);
	}

	void EnterDieToGhost()
	{
		AllOff();
		Controller_DieToGhost.SetActive(true);
	}

	void AllOff()
	{
		Controller_Idle.SetActive(false);
		Controller_Stunned.SetActive(false);
		Controller_Atk.SetActive(false);
		Controller_Atk2.SetActive(false);
		Controller_Big.SetActive(false);
		Controller_Landing.SetActive(false);
		Controller_Charging.SetActive(false);
		Controller_Outro.SetActive(false);
		Controller_CuteGhost.SetActive(false);
		Controller_DieToGhost.SetActive(false);
	}


	void InitLanding()
	{
		//Arms
		iTween.MoveBy(landing_arms, iTween.Hash("y", 0.1f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Head
		iTween.MoveBy(landing_head, iTween.Hash("x", -0.15f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}

	void InitOutro()
	{
		//Arms
		iTween.MoveBy(finalSkull, iTween.Hash("y", 0.2f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Head
		iTween.MoveBy(finalArms, iTween.Hash("y", -0.1f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}

	void InitStunned()
	{
		//Head
		iTween.MoveBy(stunned_head, iTween.Hash("x", -0.5f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(stunned_legs, iTween.Hash("x", -0.15f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Legs
		iTween.MoveBy(stunned_arms, iTween.Hash("y", 0.4f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(stunned_body, iTween.Hash("y", -0.1f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void InitAtk()
	{
		//Head
		iTween.MoveBy(atk_head, iTween.Hash("x", -0.1f, "y", 0.6f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(atk_arms, iTween.Hash("x", -0.15f, "y", 0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(atk_body, iTween.Hash("y", -0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void InitAtk2()
	{
		//Head
		iTween.MoveBy(atk2_head, iTween.Hash("x", -0.1f, "y", 0.6f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(atk2_arms, iTween.Hash("x", -0.15f, "y", 0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(atk2_body, iTween.Hash("y", -0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void InitIdle()
	{
		//Head
		iTween.MoveBy(head, iTween.Hash("x", 0.6f, "y", 0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(arms, iTween.Hash("x", 0.15f, "y", -0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(body, iTween.Hash("y", -0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

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

	void InitBig()
	{
		//Head
		iTween.MoveBy(big_head, iTween.Hash("x", -0.5f, "y", -0.3, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(big_arms, iTween.Hash("y", 0.4f, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(big_body, iTween.Hash("x", 0.4f, "time", 2f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}


}
