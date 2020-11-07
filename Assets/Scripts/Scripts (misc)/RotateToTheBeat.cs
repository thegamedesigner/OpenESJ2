using UnityEngine;
using System.Collections;

public class RotateToTheBeat : MonoBehaviour
{
    public float minimumFreq = 0;
    bool flip = false;
    void Start()
    {

    }

    void Update()
    {
        if (xa.beat_Freq > minimumFreq && !flip)
        {
            flip = true;
            transform.AddToAng(0, 0, 15);
        }
        else
        {
            flip = false;
        }
    }
}
