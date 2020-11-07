using UnityEngine;
using System.Collections;

public class PartyTimeSlave : MonoBehaviour
{
    public Behaviour[] enableThese;

    void Start()
    {
        if (PartyTimeController.selfScript)
        {
            PartyTimeController.selfScript.scripts.Add(this);
        }
    }

    public void TriggerMe()
    {
        foreach (Behaviour b in enableThese)
        {
            b.enabled = true;
        }
    }

}
