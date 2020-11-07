using UnityEngine;
using System.Collections;

public class SendMessageOnRepeat : MonoBehaviour
{
	public bool loop = false;
	public GameObject[] sendMsgGOs = new GameObject[0];
	public string[] msgs = new string[0];
    public float[] delayInSeconds = new float[0];
    public bool ignoreFirstDelayOnce = false;
    public float startingDelay = 0;
    bool ignoredDelay = false;
	float timeSet = -1;
	int stage = 0;

	void Update()
	{
		if (this.enabled)
		{
			if (timeSet == -1) { timeSet = fa.time; }
            if ((startingDelay > 0 && fa.time > (timeSet + startingDelay)) || startingDelay <= 0)
            {
                if (ignoreFirstDelayOnce && !ignoredDelay) { timeSet = -999; ignoredDelay = true; }
                if (fa.time > (timeSet + delayInSeconds[stage]))
                {
                    timeSet = fa.time;
                    sendMsgGOs[stage].SendMessage(msgs[stage]);
                    stage++;
                }



                if (stage >= sendMsgGOs.Length)
                {
                    stage = 0;
                    if (!loop)
                    {
                        this.enabled = false;
                    }
                }
            }
		}
	}
}
