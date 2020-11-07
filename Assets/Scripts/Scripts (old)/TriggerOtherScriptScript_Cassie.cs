using UnityEngine;
using System.Collections;

public class TriggerOtherScriptScript_Cassie : MonoBehaviour
{
	public Behaviour[] scriptsToActivate;
	public bool triggerWhenPlayerIsInScaleBox = false;
	public bool triggerOnDistFromGO = false;
	public bool triggerOnDistFromPlayer = false;
	public bool triggerOnStart = false;
	public GameObject distGO = null;
	public float distTrigger = 0;
	public bool disableBehaviours = false;

	public float delayInSeconds = 0;

	public bool useSendMsg = false;
	public string[] msgToSend;
	public GameObject[] GOToSendTo;
	public bool showDebugMsg = false;
	public string debugMsg = "Triggered!";
	public bool disableMeOnTriggering = false;

	int triggered = 0;
	//bool permaTriggered = false;
	float counter = 0;

	void Start()
	{
		if (triggerOnStart) { triggeredFunc(); }
	}

	void Update()
	{
		if (triggered > 0)
		{
			checkTriggered();
		}
		else
		{
			if (triggerOnDistFromPlayer)
			{
				if (xa.player)
				{
					xa.glx = transform.position;
					xa.glx2 = xa.player.transform.position;
					xa.glx.z = xa.glx2.z;
					if (Vector3.Distance(xa.glx, xa.glx2) < distTrigger)
					{
						if (triggered == 0) { triggeredFunc(); }
					}
				}

			}
			if (triggerOnDistFromGO)
			{
				xa.glx = transform.position;
				xa.glx2 = distGO.transform.position;
				xa.glx.z = xa.glx2.z;
				if (Vector3.Distance(xa.glx, xa.glx2) < distTrigger)
				{
					if (triggered == 0) { triggeredFunc(); }
				}
			}
			if (triggerWhenPlayerIsInScaleBox)
			{
				if (xa.player)
				{
					if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x) &&
						(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x) &&
						(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y) &&
						(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y))
					{
						//  Setup.GC_DebugLog("GOT HERE");
						if (triggered == 0) { triggeredFunc(); }
					}
				}
			}

		}
	}


	public void triggeredFunc()
	{
		counter = fa.time;
		triggered = 1;
		//permaTriggered = true;
	}

	void checkTriggered()
	{
		if (triggered < 2)
		{
			if ((counter + delayInSeconds) < fa.time)
			{



					triggered = 0;
					enableStuff();
			}
		}
	}

	void enableStuff()
	{
		foreach (Behaviour co in scriptsToActivate)
		{
			if (disableBehaviours) { co.enabled = false; }
			else { co.enabled = true; }
		}
		if (useSendMsg)
		{
			int index = 0;
			while (index < GOToSendTo.Length)
			{
				if (GOToSendTo[index])
				{
					GOToSendTo[index].SendMessage(msgToSend[index]);
				}
				index++;
			}
		}
		if (disableMeOnTriggering) { this.enabled = false; }
	}

}
