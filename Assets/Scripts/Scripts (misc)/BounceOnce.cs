using UnityEngine;
using System.Collections;

public class BounceOnce : MonoBehaviour
{
    public Vector3 startingScale;
    float timeSet = -1;

    float pulse(float time, float freq)// Frequency in Hz
    {
        const float pi = 3.14f;
        return 0.5f * (1 + Mathf.Sin(2 * pi * freq * time));
    }
    //1/10 (10 = frequency) = 0.1 second is the period, which is the time between 1's.

    float result;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSet == -1)
        {
            timeSet = fa.time;
            transform.localScale = startingScale * 2f;
        }
        result = pulse(fa.time - timeSet, 1);
        result = (0.5f - result);
        transform.localScale = startingScale * result;
    }
}
