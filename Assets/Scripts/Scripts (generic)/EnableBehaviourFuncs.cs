using UnityEngine;
using System.Collections;

public class EnableBehaviourFuncs : MonoBehaviour
{
	public Behaviour behaviour = null;

    public void enableBehaviour()
    {
        behaviour.enabled = true;
    }

    public void disableBehaviour()
    {
        behaviour.enabled = false;
    }
}
