using UnityEngine;
using System.Collections;

public class TriggerGenericAnimationOnEnabled : MonoBehaviour
{
    //Triggers an animation in the script given, on enabled.
    public AnimationScript_Generic animationScript_Generic = null;
    public int animationNumber = 0;
    void Update()
    {
        animationScript_Generic.playAnimation(animationNumber);
        this.enabled = false;
    }
}
