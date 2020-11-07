using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoorScript : MonoBehaviour
{
	public Info infoScript;
	bool moving = false;

	float timeSet = 0;
	float delay = 1;
	float speedDown = 2.5f;
	float pauseAtTop = 0.3f;
	float pauseAtBottom = 0.5f;
	float verDist = 16;


	enum State
	{
		None,
		Waiting,
		Prepping_MovingDown,
		Prepping_WatingAtBottom,
		End
	}
	State state = State.Prepping_MovingDown;

	void Start()
	{
	}

	void Update()
	{
		if (infoScript.triggered == true)
		{
			infoScript.triggered = false;
			moving = true;
		}

		if (moving)
		{
			if (fa.time >= (timeSet + delay))
			{
				switch (state)
				{
					case State.Prepping_MovingDown:
						iTween.MoveBy(this.gameObject, iTween.Hash("y", -verDist, "time", speedDown, "easetype", iTween.EaseType.easeInCirc));
						delay = speedDown;
						timeSet = fa.time;
						state = State.Prepping_WatingAtBottom;
						break;
					case State.Prepping_WatingAtBottom:
						delay = pauseAtBottom;
						timeSet = fa.time;
						state = State.None;

						if (Setup.checkVecOnScreen(transform.position, false))
						{
							ScreenShakeCamera.Screenshake(1, 0.95f, ScreenShakeCamera.ScreenshakeMethod.Basic);
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RockImpact);
						}
						moving = false;
						break;



				}
			}
		}

	}
}
