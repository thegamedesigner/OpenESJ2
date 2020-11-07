using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpikesScript : MonoBehaviour
{
	public GameObject hurtzone;
	public float offsetTime;
	public float timeDown;
	public float timeMovingUp;
	public float timeUp;
	public float timeMovingDown;
	public float peekSpeed;
	public float peekTime;

	State state = State.StartOffset;
	float timeSet = 0;
	float secondTimeSet = 0;
	enum State
	{
		StartOffset,
		WaitingForOffset,
		StartPopup,
		WaitingToPopDown,
		StartPopdown,
		WaitingToPeek,
		StartingToPeek,
		WaitingToPopUp,
		End
	}

	void Start()
	{

	}

	void Update()
	{
		switch (state)
		{
			case State.StartOffset:
				timeSet = fa.time + offsetTime;
				state = State.WaitingForOffset;
				break;
			case State.WaitingForOffset:
				if (fa.time >= timeSet)
				{
					state = State.StartPopdown;
				}
				break;
			case State.StartPopdown:
				hurtzone.SetActive(false);
				iTween.MoveBy(this.gameObject, iTween.Hash("y", -0.8f, "time", timeMovingDown, "easetype", iTween.EaseType.easeInSine));
				timeSet = fa.time + timeMovingDown + (timeDown - peekTime - peekSpeed);
				state = State.WaitingToPeek;
				break;
			case State.WaitingToPeek:
				if (fa.time >= timeSet)
				{
					state = State.StartingToPeek;
				}
				break;
			case State.StartingToPeek:
				iTween.MoveBy(this.gameObject, iTween.Hash("y", 0.15f, "time", peekSpeed, "easetype", iTween.EaseType.easeInSine));
				timeSet = fa.time + peekTime + peekSpeed;
				state = State.WaitingToPopUp;
				break;
			case State.WaitingToPopUp:
				if (fa.time >= timeSet)
				{
					state = State.StartPopup;
				}
				break;
			case State.StartPopup:
				iTween.MoveBy(this.gameObject, iTween.Hash("y", 0.65f, "time", timeMovingUp, "easetype", iTween.EaseType.easeInSine));
				timeSet = fa.time + timeMovingUp + timeUp;
				secondTimeSet = fa.time + (timeUp * 0.3f);
				state = State.WaitingToPopDown;
				break;
			case State.WaitingToPopDown:
				if (fa.time >= secondTimeSet && !hurtzone.activeSelf)
				{
					hurtzone.SetActive(true);
				}
				if (fa.time >= timeSet)
				{
					state = State.StartPopdown;
				}
				break;
		}
	}
}
