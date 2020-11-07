using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeCamera : MonoBehaviour
{
    public static float screenshakeTimeSet;
    public static float screenshakeDelay;
    public static float screenshakeAmount;
    public static int shakeForXFrames;
    public static Vector3 posOffset = Vector3.zero;

    void Start()
    {
    }

    void Update()
    {
		
		if(fa.paused) {return; }
        if (fa.time < (screenshakeTimeSet + screenshakeDelay))
        {
            //if (shakeForXFrames > 0) { shakeForXFrames--; }
            Vector2 shakePos = Random.insideUnitCircle * screenshakeAmount;
            posOffset = new Vector3(shakePos.x, shakePos.y, 0);
        }
        else
        {
            posOffset = Vector3.zero;
            screenshakeDelay = 0;
            screenshakeAmount = 0;
        }

        transform.localPosition = posOffset;

    }


    public enum ScreenshakeMethod { None, Basic, PerFrame, End }
    public static void Screenshake(float amount, float fTime, ScreenshakeMethod method)
    {
		if(fa.paused) {return; }
        float time = fTime * fa.screenshakeMultiplier;
        if (time <= 0) { return; }
        if (amount <= 0) { return; }
        screenshakeTimeSet = fa.time;
        screenshakeDelay = time;
        screenshakeAmount = amount;

    }

}
