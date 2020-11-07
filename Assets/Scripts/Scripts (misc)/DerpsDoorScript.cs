using UnityEngine;
using System.Collections;

public class DerpsDoorScript : MonoBehaviour
{
    public static int numberOfKeysCollected = 0;
    public static int numberOfKeysTotal = 0;
    public Behaviour enableThis = null;

    void Awake()
    {
        DerpsDoorScript.numberOfKeysCollected = 0;
        DerpsDoorScript.numberOfKeysTotal = 0;

    }

    void Update()
    {
        if (DerpsDoorScript.numberOfKeysCollected >= DerpsDoorScript.numberOfKeysTotal && fa.time > 1)
        {
            enableThis.enabled = true;
            this.enabled = false;
        }
    }
}
