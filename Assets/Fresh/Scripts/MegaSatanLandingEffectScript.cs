using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatanLandingEffectScript : MonoBehaviour
{
    public GameObject backdrop;
    public GameObject center;

    float timeset;

    void Start()
    {
        //handle backdrop
        if (backdrop != null)
        {
            iTween.ScaleTo(backdrop, iTween.Hash("x", 1, "y", 1, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine));
            iTween.ScaleTo(backdrop, iTween.Hash("delay", 0.16f, "x", 2, "y", 2, "time", 2, "easetype", iTween.EaseType.easeInOutSine));
            iTween.FadeTo(backdrop, iTween.Hash("alpha", 0, "time", 2f, "easetype", iTween.EaseType.easeInOutSine));

        }

        //handle center
        if (center != null)
        {
            iTween.ScaleTo(center, iTween.Hash("x", 1, "y", 1, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine));
            iTween.ScaleTo(backdrop, iTween.Hash("delay", 0.16f, "x", 2, "y", 2, "time", 2, "easetype", iTween.EaseType.easeInOutSine));
            iTween.FadeTo(center, iTween.Hash("alpha", 0, "time", 2f, "easetype", iTween.EaseType.easeInOutSine));

        }
        timeset = fa.time;
    }

    void Update()
    {
        if (fa.time > (timeset + 3))
        {
            Destroy(this.gameObject);
        }
    }
}
