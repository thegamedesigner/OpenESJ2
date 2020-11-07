using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatan2ChargingScript : MonoBehaviour
{
	public bool doBoss = false;

	public GameObject Controller_Charging;
	public GameObject Controller_ChargingSubController;

	public TextMesh ChargeText;

	public GameObject charging_head;
	public GameObject charging_body;
	public GameObject charging_arms;

	float timeSet = 0;
	float delay = 0;

	string toSay;
	bool firstYell = false;

	State lastState = State.None;
	State state = State.Charging;

	enum State
	{
		None,
		Waiting,
		Charging,
		End
	}

	int choice = 0;

	void Start()
	{
		InitCharging();
	}

	void Update()
	{
		if (doBoss && xa.hasCheckpointed)
		{
			HandleBoss();
		}
	}
	int textIndex = 0;
	bool updateHalfWay = false;
	void HandleBoss()
	{
		float leftX = Camera.main.gameObject.transform.position.x - 25;
		float rightX = Camera.main.gameObject.transform.position.x + 31;
		switch (state)
		{
			case State.Waiting:
				if (fa.time > (timeSet + delay))
				{
					timeSet = 0;
					delay = 0;
					state = State.Charging;
				}
				if (!updateHalfWay && fa.time > (timeSet + 4))
				{
					updateHalfWay = true;
					UpdateText();

				}
				break;
			case State.Charging:
				UpdateText();
				EnterCharging();
				updateHalfWay = false;
				iTween.MoveTo(Controller_Charging, iTween.Hash("x", 30, "time", 3f, "easetype", iTween.EaseType.linear, "islocal", true));
				iTween.MoveTo(Controller_Charging, iTween.Hash("delay", 4f, "x", -30, "time", 3f, "easetype", iTween.EaseType.linear, "islocal", true));

				iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 3f, "x", 1, "time", 0));
				iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 7f, "x", -1, "time", 0));

				timeSet = fa.time;
				delay = 8f;
				state = State.Waiting;
				break;

		}
	}

	void UpdateText()
	{
		if (xa.pgMode)
		{
				textIndex ++;
				switch(textIndex)
				{
					case 0:  ChargeText.text = "Boo!"; break;
					case 1:  ChargeText.text = "Coming through!"; break;
					case 2:  ChargeText.text = "Outta my way!"; break;
					case 3:  ChargeText.text = "Dancin'"; break;
					case 4:  ChargeText.text = "Crushing!"; break;
					case 5:  ChargeText.text = "Boop!"; break;
					case 6:  ChargeText.text = "Heckin' Yeah!"; break;
					case 7:  ChargeText.text = "Incoming!"; break;
					case 8:  ChargeText.text = "Come down here!"; break;
					case 9:  ChargeText.text = "Cray cray!"; break;
					case 10:  ChargeText.text = "Coming through!"; break;
					case 11:  ChargeText.text = "Charging up!"; break;
					case 12:  ChargeText.text = "Whew!"; break;
					case 13:  ChargeText.text = "Arrrgg!"; break;
					case 14:  ChargeText.text = "Bwaarg!"; break;
					case 15:  ChargeText.text = "Yaarrg!"; break;
				}
			if(textIndex > 15) { ChargeText.text = " ";}
		}

		else
		{
			textIndex++;
			switch (textIndex)
			{
				case 0: ChargeText.text = "Boo!"; break;
				case 1: ChargeText.text = "Coming through!"; break;
				case 2: ChargeText.text = "Outta my way!"; break;
				case 3: ChargeText.text = "Dancin'"; break;
				case 4: ChargeText.text = "Crushing!"; break;
				case 5: ChargeText.text = "I'm Mega-Satan!"; break;
				case 6: ChargeText.text = "Horny devil!"; break;
				case 7: ChargeText.text = "Incoming!"; break;
				case 8: ChargeText.text = "Come down here!"; break;
				case 9: ChargeText.text = "Motherfucker!"; break;
				case 10: ChargeText.text = "Coming through!"; break;
				case 11: ChargeText.text = "Charging up!"; break;
				case 12: ChargeText.text = "Whew!"; break;
				case 13: ChargeText.text = "Arrrgg!"; break;
				case 14: ChargeText.text = "Bwaarg!"; break;
				case 15: ChargeText.text = "Yaarrg!"; break;
			}
			if (textIndex > 15) { ChargeText.text = " "; }
		}

	}
	void EnterCharging()
	{
		AllOff();
		Controller_Charging.SetActive(true);
	}


	void AllOff()
	{
		Controller_Charging.SetActive(false);
	}

	void InitCharging()
	{

		//		iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 2f, "x", -1, "time", 0));
		//		iTween.ScaleTo(Controller_ChargingSubController, iTween.Hash("delay", 4f, "x", 1, "time", 0));


		//Head
		iTween.MoveBy(charging_head, iTween.Hash("x", 0.6f, "y", 0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Arms
		iTween.MoveBy(charging_arms, iTween.Hash("y", 0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		//Body
		iTween.MoveBy(charging_body, iTween.Hash("y", -0.2f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

	}


}
