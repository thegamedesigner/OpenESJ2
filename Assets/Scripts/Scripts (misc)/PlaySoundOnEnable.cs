using UnityEngine;
using System.Collections;

public class PlaySoundOnEnable : MonoBehaviour
{
    public GC_SoundScript.Sounds sound;

    void Update()
    {
        if (xa.sn) { xa.sn.playSound(sound); }
        this.enabled = false;
    }
}
