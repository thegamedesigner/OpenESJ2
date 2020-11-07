using UnityEngine;
using System.Collections;

public class DestroyEverythingWithBossMissileScriptOnEnabled : MonoBehaviour
{
    public bool setToThis = false;
    void Update()
    {
        za.destroyBossMissiles = setToThis;
        this.enabled = false;
    }
}
