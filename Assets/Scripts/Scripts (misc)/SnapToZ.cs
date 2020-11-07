using UnityEngine;
using System.Collections;

public class SnapToZ : MonoBehaviour
{
    public float z = 0;
    public bool snapLate = false;

    void Start()
    {
        if (!snapLate)
        {
            xa.glx = transform.position;
            xa.glx.z = z;
            transform.position = xa.glx;
            this.enabled = false;
        }
    }
    void Update()
    {
        if (snapLate)
        {
            snapLate = false;
            xa.glx = transform.position;
            xa.glx.z = z;
            transform.position = xa.glx;
            this.enabled = false;
        }
    }

}
