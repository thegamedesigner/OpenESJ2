using UnityEngine;
using System.Collections;

public class SetParentToNullIfPlayerIsDead : MonoBehaviour
{

    void Update()
    {
        if (xa.playerDead)
        {
            transform.parent = null;
            this.enabled = false;
        }
    }
}
