using UnityEngine;
using System.Collections;

public class EnableOnLoop : MonoBehaviour
{

    [Multiline]
    public string label = "Delay, then Enable, then Loop.";
    public float delayInSeconds = 0;
    public Behaviour behaviour = null;
    public bool skipFirstDelay = false;
    public float forceThisAsFirstDelay = 0;
    public int maxLoops = 0;
    int loops = 0;

    float timeSet = 0;

    void Start()
    {
        timeSet = fa.time;
    }

    void Update()
    {
        if (fa.time > timeSet + delayInSeconds || skipFirstDelay || (forceThisAsFirstDelay != 0 && fa.time > (timeSet + forceThisAsFirstDelay)))
        {
            timeSet = fa.time;
            behaviour.enabled = true;
            skipFirstDelay = false;
            loops++;
            if (maxLoops > 0) { if (loops >= maxLoops) { this.enabled = false; } }
        }
    }
}
