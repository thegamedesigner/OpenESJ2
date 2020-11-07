using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleController : MonoBehaviour
{
    public static CameraAngleController self;
    
    void Start()
    {
        self = this;
    }

    public static void AngleCamera(float angle, float t)
    {
        float a = (1f / 360f) * angle;
        iTween.RotateBy(self.gameObject,iTween.Hash("z", a, "time", t, "easetype", iTween.EaseType.easeInOutSine));
    }
}
