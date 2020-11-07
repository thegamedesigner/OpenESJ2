using UnityEngine;
using System.Collections;

public class EnableBehaviorScript : MonoBehaviour
{
	public Behaviour[] scriptsToActivate;
	public float delayInSeconds = 0;
	public bool disableBehaviours = false;
	public bool waitForSendMessage = false;
	bool triggeredByMsg = false;
	float timeSet = -1;

	public void enableBehaviour()
	{
		triggeredByMsg = true;
	}

	void Update()
	{
		if (this.enabled && (!waitForSendMessage || (waitForSendMessage && triggeredByMsg)))
		{
			if (timeSet == -1) { timeSet = fa.time; }

			if (fa.time > (timeSet + delayInSeconds))
			{
				foreach (Behaviour co in scriptsToActivate)
				{
					if (disableBehaviours) { co.enabled = false; }
					else { co.enabled = true; }
				}
				this.enabled = false;
			}
		}
	}
}
