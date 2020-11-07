using UnityEngine;
using System.Collections;

public class SetSkaldStateToNone : MonoBehaviour
{
    public bool useForceThisState = false;
    public SkaldScript.State forceThisState = SkaldScript.State.None;
    void Update()
    {
        if (za.skaldScript)
        {
            if (useForceThisState)
            {
                za.skaldScript.forceState(forceThisState);
            }
            else
            {
                za.skaldScript.forceState(SkaldScript.State.None);
            }
            this.enabled = false;
        }
    }
}
