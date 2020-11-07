using UnityEngine;
using System.Collections;

public class NeonLightScript : MonoBehaviour
{
    public Behaviour enableThisOnOn = null;
    float timeOn = 0.1f;
    float timeOff = 0.2f;
    float timeSet = 0;
    float delay = 0;
    bool On = false;
    void Start()
    {
        timeSet = fa.time;
        delay = timeOff;
        GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        if (fa.time > timeSet + delay)
        {
            if (!On)
            {
                //turn on
                On = true;
                delay = timeOn +Random.Range(0, 4f);
                GetComponent<Renderer>().enabled = true;
                timeSet = fa.time;
                if (xa.sn) { xa.sn.playSound(GC_SoundScript.Sounds.Neon); }
                if (enableThisOnOn) { enableThisOnOn.enabled = true; }
            }
            else
            {
                //turn off
                On = false;
                delay = timeOff +Random.Range(0, 0.1f);
                GetComponent<Renderer>().enabled = false;
                timeSet = fa.time;
            }

        }

    }
}
