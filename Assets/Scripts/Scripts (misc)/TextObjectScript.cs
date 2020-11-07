using UnityEngine;
using System.Collections;

public class TextObjectScript : MonoBehaviour
{
    void Update()
    {
        //play some itweens
        iTween.MoveBy(this.gameObject, iTween.Hash("y", 0.61f, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine));
        iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 1, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine));
        this.enabled = false;
    }
}
