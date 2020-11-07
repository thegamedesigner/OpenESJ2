using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicDeathPinwheel : MonoBehaviour
{

    void Start()
    {
        ScreenShakeCamera.Screenshake(1, 0.2f, ScreenShakeCamera.ScreenshakeMethod.Basic);
        Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RockImpact);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
