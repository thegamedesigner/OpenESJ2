using UnityEngine;
using System.Collections;

public class TimedEventsScript : MonoBehaviour
{
    public TimedEvent[] events = new TimedEvent[0];
    [System.Serializable]
    public class TimedEvent
    {
        public float timeInSeconds = 0;
        public Behaviour enableThis = null;
        public Behaviour disableThis = null;
        [HideInInspector]
        public bool toggle = false;

    }

    void Start()
    {

    }

    void Update()
    {
        foreach (TimedEvent t in events)
        {
            if (t.toggle == false && t.timeInSeconds <= fa.time)
            {
                t.toggle = true;
                if (t.enableThis) { t.enableThis.enabled = true; }
                if (t.disableThis) { t.disableThis.enabled = false; }
            }
        }
    }
}
