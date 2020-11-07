using UnityEngine;
using System.Collections;

public class ParalyzePlayer : MonoBehaviour
{
    public bool setTo = false;
    void Update()
    {
        za.paralyzePlayerForCutscenes = setTo;
        this.enabled = false;
    }
}
