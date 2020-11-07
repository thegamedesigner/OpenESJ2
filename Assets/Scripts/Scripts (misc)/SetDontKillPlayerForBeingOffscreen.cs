using UnityEngine;
using System.Collections;

public class SetDontKillPlayerForBeingOffscreen : MonoBehaviour
{
    public bool setToThis = false;
    void Update()
    {
        za.dontKillPlayerForBeingOffscreen = setToThis;
        this.enabled = false;
    }
}
