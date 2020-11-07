using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBallScript : MonoBehaviour
{
	public static ThrowingBallScript self;
	public TextMesh textMesh;
	public GameObject textMeshGo;
	public TextMesh daddysLoveLabelText;
	public GameObject ThrowingTentacle;
	public GameObject ThrowingTentacleMuzzlePoint;
	public GameObject Highfive1;
	public GameObject Highfive2;
	public GameObject Highfive3;
	public GameObject Highfive4;
	public GameObject HighfiveMuzzle1;
	public GameObject HighfiveMuzzle2;
	public GameObject HighfiveMuzzle3;
	public GameObject HighfiveMuzzle4;
	public GameObject ball;
	public GameObject ballBullet;
	public GameObject ballDeathExplo;
	public GameObject highfiveDeathExplo;
	public GameObject heartDeathExplo;
	public AnimateUsingMatsScript aniScript;
	public int oldIndex;
	State state = State.StartThrowing;
	float timeset = 0;
	float delay = 0;

	public int daddysLove = 5;
	public static int ReadOnly_DaddysLove = -1;
	//public static int WriteOnly_DaddysLove = -1;
	public static int currentDaddysLove = 5;//updated every frame in update, but not usuful for read/write, or before this script is running
	public GameObject[] hearts;

	bool caught = false;
	bool highfived = false;
	GameObject currentBall;
	GameObject currentHighfiveMuzzle;

	public enum State
	{
		StartThrowing,
		WaitBeforeThrowing,
		WaitThrowing,
		EndThrowing,
		WaitAfterThrowing,
		StartHighfive,
		WaitHighfive,
		EndHighfive,
		WaitAfterHighfive,
		End,
	}

	int highfiveIndex = 0;
	void Awake()
	{
		ReadOnly_DaddysLove = -1;//just in case
		self = this;
	}
	void Start()
	{
		iTween.FadeTo(textMeshGo, iTween.Hash("alpha", 0, "time", 0.01f));
		iTween.MoveBy(textMeshGo, iTween.Hash("y", -1, "time", 0.01f));
	}

	void Speak(string str)
	{
		textMesh.text = str;
		iTween.FadeTo(textMeshGo, iTween.Hash("alpha", 0, "time", 0.001f));
		iTween.MoveBy(textMeshGo, iTween.Hash("y", -1, "time", 0.001f));
		iTween.FadeTo(textMeshGo, iTween.Hash("delay", 0.1f, "alpha", 1, "time", 0.1f));
		iTween.MoveBy(textMeshGo, iTween.Hash("delay", 0.1f, "y", 1, "time", 0.1f));

	}

	public static void ResetSavedDaddysLove()//Called in RawFuncs on Restart OR on going to world map
	{

		SaveAbilitiesNodeScript.SavedDaddysLove = 5;
		if(self != null)
		{
			self.daddysLove = 5;
		}
		ReadOnly_DaddysLove = -1;
		currentDaddysLove = 5;
	}

	public void GetHurt()
	{
		Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart);
		daddysLove--;
		ReadOnly_DaddysLove = daddysLove;
		if (daddysLove >= 0)
		{
			Speak("Disappointing!");
			Instantiate(heartDeathExplo, hearts[daddysLove].transform.position, hearts[daddysLove].transform.rotation);
			hearts[daddysLove].SetActive(false);
		}
	}

	public static int externalAmount = -1;
	public static string externalReason = "";

	public static void External_SetToXDaddysLove(int amount, string fromWho)
	{
		//Debug.Log("SetExternal: " + amount + ", " + fromWho);
		externalAmount = amount;
		externalReason = fromWho;
	}

	void External_UpdateDaddysLove()
	{
		//Debug.Log("External amount: " + externalAmount + ", " + externalReason + ", Actually DL: " + daddysLove);

		if (daddysLove > externalAmount)
		{
			//go down
			while (daddysLove > externalAmount && daddysLove > 0)
			{
				daddysLove--;
				hearts[daddysLove].SetActive(false);
			}
		}
		if (daddysLove < externalAmount)
		{
			while (daddysLove < externalAmount && daddysLove < 4)
			{
				daddysLove++;
				hearts[daddysLove].SetActive(true);
			}
		}
		externalAmount = -1;
		externalReason = "";
	}

	void Update()
	{
		currentDaddysLove = daddysLove;
		if (externalAmount != -1) { External_UpdateDaddysLove(); }


		/*
		if (WriteOnly_DaddysLove != -1)
		{
			while (daddysLove > WriteOnly_DaddysLove)
			{
				daddysLove--;
				hearts[daddysLove].SetActive(false);
			}

			WriteOnly_DaddysLove = -1;
		}*/
		//ReadOnly_DaddysLove = daddysLove;


		/*
			switch(daddysLove)
			{
				case 0: daddysLoveLabelText.text = "Daddy's Bitter Disappointment:";break;
				case 1: daddysLoveLabelText.text = "Daddy's Bitter Disappointment:";break;
				case 2: daddysLoveLabelText.text = "Daddy's Pain-In-The-Ass:";break;
				case 3: daddysLoveLabelText.text = "Daddy's Love:";break;
				case 4: daddysLoveLabelText.text = "Daddy's Love:";break;
				case 5: daddysLoveLabelText.text = "Daddy's Love:";break;
			}
			*/
		//	if(Input.GetKeyDown(KeyCode.B)) {GetHurt(); }

		//Checking for catches & highfives (has to be outside the statemachine)
		if (currentBall != null)
		{
			if (Vector2.Distance(currentBall.transform.position, xa.player.transform.position) < 2)
			{
				caught = true;
				Speak("Nice one!");
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.BaseballBat);
				GameObject go = Instantiate(ballDeathExplo, currentBall.transform.position, currentBall.transform.rotation);
				go.transform.SetAngZ(414);
				Destroy(currentBall);
			}
		}

		if (currentHighfiveMuzzle != null && !highfived)
		{
			if (Vector2.Distance(currentHighfiveMuzzle.transform.position, xa.player.transform.position) < 2)
			{
				highfived = true;
				Speak("Right on!");
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Highfive);
				Instantiate(highfiveDeathExplo, currentHighfiveMuzzle.transform.position, currentHighfiveMuzzle.transform.rotation);

			}
		}





		//state machine
		switch (state)
		{
			case State.StartThrowing:
				{
					timeset = fa.time;
					delay = 0.3f;
					state = State.WaitBeforeThrowing;
					iTween.MoveTo(ThrowingTentacle, iTween.Hash("islocal", true, "x", 7.41f, "y", -6.58f, "time", 0.1f, "easetype", iTween.EaseType.easeInOutSine));

					Speak("Batter up!");

					break;
				}
			case State.WaitBeforeThrowing:
				{
					if (fa.time > (timeset + delay))
					{
						state = State.WaitThrowing;
						timeset = fa.time;
						delay = 0.3f;

						currentBall = Instantiate(ballBullet, ThrowingTentacleMuzzlePoint.transform.position, ThrowingTentacleMuzzlePoint.transform.rotation);
						currentBall.transform.SetAngZ(165);
						caught = false;
					}
					break;
				}
			case State.WaitThrowing:
				{
					if (fa.time > (timeset + delay))
					{
						state = State.EndThrowing;
					}
					break;
				}
			case State.EndThrowing:
				{
					iTween.MoveTo(ThrowingTentacle, iTween.Hash("islocal", true, "x", 7.41f, "y", -17, "time", 0.1f, "easetype", iTween.EaseType.easeInOutSine));
					state = State.WaitAfterThrowing;
					timeset = fa.time;
					delay = 2;
					break;
				}
			case State.WaitAfterThrowing:
				{
					if (fa.time > (timeset + delay))
					{
						state = State.StartHighfive;
					}
					break;
				}
			case State.StartHighfive:
				{
					Speak("Slap me five!");
					highfived = false;
					timeset = fa.time;
					delay = 3;
					state = State.WaitHighfive;
					float moveTime = 0.4f;
					if (highfiveIndex == 0)
					{
						currentHighfiveMuzzle = HighfiveMuzzle1;
						//Up: -9.4
						//Down: -17.58
						iTween.MoveTo(Highfive1, iTween.Hash("islocal", true, "x", -11.7f, "y", -9.4f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}
					if (highfiveIndex == 1)
					{
						currentHighfiveMuzzle = HighfiveMuzzle2;
						//Up x: -13.6
						//Down x: -26.73
						iTween.MoveTo(Highfive2, iTween.Hash("islocal", true, "x", -13.6f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}
					if (highfiveIndex == 2)
					{
						currentHighfiveMuzzle = HighfiveMuzzle3;
						//Up: 9.6
						//Down: 17.27
						iTween.MoveTo(Highfive3, iTween.Hash("islocal", true, "x", 5.7f, "y", 9.6f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}
					if (highfiveIndex == 3)
					{
						currentHighfiveMuzzle = HighfiveMuzzle4;
						//Up: -9.4
						//Down: -17.07
						iTween.MoveTo(Highfive4, iTween.Hash("islocal", true, "x", 5.3f, "y", -9.4f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}

					break;
				}
			case State.WaitHighfive:
				{
					if (fa.time > (timeset + delay) || highfived)
					{
						state = State.EndHighfive;
					}
					break;
				}
			case State.EndHighfive:
				{
					float moveTime = 0.4f;
					if (highfiveIndex == 0)
					{
						//Up: -9.4
						//Down: -17.58
						iTween.MoveTo(Highfive1, iTween.Hash("islocal", true, "x", -11.7f, "y", -17.58f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}
					if (highfiveIndex == 1)
					{
						//Up x: -15.8
						//Down x: -26.73
						iTween.MoveTo(Highfive2, iTween.Hash("islocal", true, "x", -26.73f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}
					if (highfiveIndex == 2)
					{
						//Up: 9.6
						//Down: 17.27
						iTween.MoveTo(Highfive3, iTween.Hash("islocal", true, "x", 5.7f, "y", 17.27f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}
					if (highfiveIndex == 3)
					{
						//Up: -9.4
						//Down: -17.07
						iTween.MoveTo(Highfive4, iTween.Hash("islocal", true, "x", 5.3f, "y", -17.07f, "time", moveTime, "easetype", iTween.EaseType.easeInOutSine));

					}

					highfiveIndex++;
					if (highfiveIndex == 4) { highfiveIndex = 0; }

					timeset = fa.time;
					delay = 0.6f;
					state = State.WaitAfterHighfive;
					break;
				}

			case State.WaitAfterHighfive:
				{
					if (fa.time > (timeset + delay))
					{
						if (highfived == false) { GetHurt(); }
						state = State.StartThrowing;
					}
					break;
				}

		}

		/*
		if (aniScript.index != oldIndex)
		{
			oldIndex = aniScript.index;

			//if (oldIndex == 4)
			//{
				//throw
			//	Instantiate(ballBullet, muzzlepoints[oldIndex].transform.position, muzzlepoints[oldIndex].transform.rotation);
				//ball.SetActive(false);
			//}
			//else
			//{
				//ball.SetActive(true);
				iTween.MoveTo(ball, iTween.Hash("x", muzzlepoints[oldIndex].transform.position.x, "y", muzzlepoints[oldIndex].transform.position.y, "time", 0.09f));

			//}



		}*/
	}
}
