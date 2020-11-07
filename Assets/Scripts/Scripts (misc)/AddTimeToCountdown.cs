using UnityEngine;
using System.Collections;

public class AddTimeToCountdown : MonoBehaviour
{
    public int addTime = 0;
    int timeAdded = 0;

    public GameObject createThisEffectAtCountdownDisplay = null;
    float timeSet = -5;//so there is no delay the first time
    float spawnDelay = 0.3f;

    void Update()
    {
        xa.countdownSecondsLeft += 1;
        xa.countdownSecondsTotal += 1;
        timeAdded += 1;

        if (createThisEffectAtCountdownDisplay)
        {
            if (fa.time >= timeSet + spawnDelay)
            {
                timeSet = fa.time;

                xa.glx = za.inworldCountdownPos;
                xa.glx.z = 30;
                xa.tempobj = (GameObject)(Instantiate(createThisEffectAtCountdownDisplay, xa.glx, createThisEffectAtCountdownDisplay.transform.rotation));
            }

        }

        if (timeAdded >= addTime)
        {
            this.enabled = false;
        }
    }
}
