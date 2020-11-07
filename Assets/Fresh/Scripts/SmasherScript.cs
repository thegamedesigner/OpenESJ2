using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherScript : MonoBehaviour
{
    float timeSet = 0;
    float delay = 1;
    float speedDown = 1;
    float speedUp = 2;
    float pauseAtTop = 0.3f;
    float pauseAtBottom = 0.5f;
    float verDist = 8;
    public bool startAtBottom = false;
    enum State
    {
        None,
        Waiting,
        Prepping_MovingUp,
        Prepping_MovingDown,
        Prepping_WatingAtBottom,
        Prepping_WaitingAtTop,
        End
    }
    State state = State.Prepping_MovingDown;

    void Start()
    {
        if(startAtBottom) {state = State.Prepping_MovingUp; }
    }

    void Update()
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
                    state = State.Prepping_MovingUp;

                    if (Setup.checkVecOnScreen(transform.position, false))
                    {
                        ScreenShakeCamera.Screenshake(1, 0.15f, ScreenShakeCamera.ScreenshakeMethod.Basic);
                        Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RockImpact);
                    }
                    break;
                case State.Prepping_MovingUp:
                    iTween.MoveBy(this.gameObject, iTween.Hash("y", verDist, "time", speedUp, "easetype", iTween.EaseType.easeInOutSine));
                    delay = speedUp;
                    timeSet = fa.time;
                    state = State.Prepping_WaitingAtTop;
                    break;
                case State.Prepping_WaitingAtTop:
                    delay = pauseAtTop;
                    timeSet = fa.time;
                    state = State.Prepping_MovingDown;
                    break;


            }
        }

    }
}
