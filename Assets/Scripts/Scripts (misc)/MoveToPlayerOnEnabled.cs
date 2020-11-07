using UnityEngine;
using System.Collections;

public class MoveToPlayerOnEnabled : MonoBehaviour
{

    void Update()
    {
        if (xa.player)
        {
            transform.position = xa.player.transform.position;
            this.enabled = false;
        }
    }
}
