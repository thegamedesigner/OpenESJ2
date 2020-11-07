using UnityEngine;
using System.Collections;

public class EnableBasedOnDistFromPlayer : MonoBehaviour
{
    public float dist = 1;
    public Behaviour[] enableThese;

    void Update()
    {
        if (xa.player)
        {
            if (Vector2.Distance(xa.player.transform.position, transform.position) <= dist)
            {
                foreach (Behaviour b in enableThese)
                {
                    b.enabled = true;
                }
                this.enabled = false;
            }
        }
    }
}
