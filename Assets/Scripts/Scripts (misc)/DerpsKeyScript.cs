using UnityEngine;
using System.Collections;

public class DerpsKeyScript : MonoBehaviour
{
    public float dist = 1;
    public Behaviour enableThis = null;

    void Start()
    {
        DerpsDoorScript.numberOfKeysTotal++;
    }

    void Update()
    {
        if (xa.player)
        {
            xa.glx.x = transform.position.x;
            xa.glx.y = transform.position.y;
            xa.glx.z = xa.player.transform.position.z;
            if (Vector3.Distance(xa.player.transform.position, xa.glx) < dist)
            {
                DerpsDoorScript.numberOfKeysCollected++;
                enableThis.enabled = true;
                this.enabled = false;
            }
        }
    }
}
