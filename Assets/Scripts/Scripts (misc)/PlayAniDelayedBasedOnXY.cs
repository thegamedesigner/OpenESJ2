using UnityEngine;
using System.Collections;

public class PlayAniDelayedBasedOnXY : MonoBehaviour
{
    public bool playAniDelayedBasedOnXY = false;
    public int aniNumber = 0;
    public AnimationScript_Generic animationScript = null;
    public float delayInSecondsPerX = 0.1f;
    public float repeatDelay = 3f;

    float timeSet = -1;
    float timeSet2 = -1;
    float result1 = 0;
    bool waitForReset = false;
    //written so coins animations can be enabled based on X, so they do the wave

    void Update()
    {
        if (this.enabled)
        {
            if (!waitForReset)
            {
                if (timeSet == -1)
                {
                    timeSet = fa.time;
                    result1 = (21 + transform.position.x + (16 - transform.position.y)) * delayInSecondsPerX;
                }

                if (fa.time > (timeSet + result1))
                {
                    if (animationScript)
                    {
                        animationScript.playAnimation(aniNumber);
                        timeSet = -1;
                        timeSet2 = -1;
                        waitForReset = true;
                    }
                }
            }
            else
            {
                //count reset/repeat delay

                if (timeSet2 == -1)
                {
                    timeSet2 = fa.time;
                }

                if (fa.time > (timeSet + repeatDelay))
                {
                    waitForReset = false;
                }
            }
        }
    }

    void resetMe()
    {
    }
}
