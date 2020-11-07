using UnityEngine;
using System.Collections;

public class MerpsExploBlockScript : MonoBehaviour
{
	public float delayInSeconds = 0;

	public GameObject animateThisGO = null;
	public string triggeredAni = "";

	public GameObject sendMsgOnExploGO = null;
	public string sendMsgOnExplo = "";

	float timeSet = 0;

	bool triggered = false;
	GameObject exploPtr;

	void Start()
	{
	}

	void Update()
	{
		if (triggered)
		{
			if(fa.time > (timeSet + delayInSeconds))
			{
				if (sendMsgOnExploGO) { sendMsgOnExploGO.SendMessage(sendMsgOnExplo); }
			}
		}
	}

	public void triggerMe()
	{
		if (triggered) { return; }
		triggered = true;
		timeSet = fa.time;
		if (animateThisGO) { animateThisGO.SendMessage(triggeredAni); }
	}

}
