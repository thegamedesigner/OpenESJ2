using UnityEngine;
using System.Collections;

public class PopeQuoteScript : MonoBehaviour
{
    public AnimationScript_Generic script = null;
    public GameObject chatObject = null;
    float delay = 3;//X seconds
    float timeSet = 0;
    int stage = 0;

    void Start()
    {
        timeSet = fa.time;

    }

    void Update()
    {
        if (stage == 0)
        {
            if (fa.time > (timeSet + delay))
            {
                script.playAni1();
                timeSet = fa.time;
                stage = 1;
            }

        }
        if (stage == 1)
        {
            chatObject.SendMessage("sayNextThing");
            stage = 2;
        }
    }
}
