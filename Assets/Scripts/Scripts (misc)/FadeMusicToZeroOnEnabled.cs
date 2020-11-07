using UnityEngine;
using System.Collections;

public class FadeMusicToZeroOnEnabled : MonoBehaviour
{
    //Only works for the basic music. Doesn't work for the second stage of seamless music loops
    public float speed = 1;
    void Update()
    {
        za.artificalMusicVolumeCap -= speed * fa.deltaTime;
        if (za.artificalMusicVolumeCap <= 0)
        {
            if (za.skaldScript) { za.skaldScript.forceState(SkaldScript.State.None); }
            za.artificalMusicVolumeCap = 0;
            this.enabled = false;
        }

    }
}
