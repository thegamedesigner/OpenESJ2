using UnityEngine;
using System.Collections;

public class DestroyBasedOnPGMode : MonoBehaviour
{
    public bool destroyIfPG_ON = false;
    public bool destroyIfPG_OFF = false;

    void Start()
    {
        if (destroyIfPG_ON && xa.pgMode)
        {
            Destroy(this.gameObject);
        }
        if (destroyIfPG_OFF && !xa.pgMode)
        {
            Destroy(this.gameObject);
        }
    }
}
