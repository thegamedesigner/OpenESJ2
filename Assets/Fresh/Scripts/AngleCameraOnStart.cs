using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCameraOnStart : MonoBehaviour
{
    public float angle = 0;
    public float time = 3;
    
    void Update()
    {
        CameraAngleController.AngleCamera(angle, time);
        this.enabled = false;
    }
}
