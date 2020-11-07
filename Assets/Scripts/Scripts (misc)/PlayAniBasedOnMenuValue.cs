using UnityEngine;
using System.Collections;

public class PlayAniBasedOnMenuValue : MonoBehaviour
{
    public AnimationScript_Generic animationScript = null;
    public bool playAni15OnNeg1Value = false;//so it can play an exit animation

    int oldSelectionBoxValue = -1;
    void Update()
    {
        if (oldSelectionBoxValue != za.menuSelectionBoxValue)
        {
            oldSelectionBoxValue = za.menuSelectionBoxValue;

            if (za.menuSelectionBoxValue != -1)
            {
                animationScript.playAnimation(za.menuSelectionBoxValue);
            }
            else
            {
                if (playAni15OnNeg1Value)
                {
                    animationScript.playAnimation(15);
                }
            }

        }
    }
}
