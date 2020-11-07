using UnityEngine;

public class ToggleVolume : MonoBehaviour
{
    void Update()
    {
        if (xa.muteSound == 0 && xa.muteMusic == 0)
        {
            xa.muteSound = 1;
            xa.muteMusic = 1;
        }
        else
        {
            xa.muteSound = 0;
            xa.muteMusic = 0;
        }
        /*
        if (za.skaldScript)
        {

            if (za.skaldScript.isPaused())
            {
                za.skaldScript.unpauseSkald();
                Setup.GC_DebugLog("MUTE: OFF");
            }
            else
            {
                za.skaldScript.pauseSkald();
                Setup.GC_DebugLog("MUTE: ON");
            }
        }

        */
        this.enabled = false;
    }
}
