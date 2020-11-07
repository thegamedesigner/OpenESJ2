using UnityEngine;
using System.Collections;

public class TriggerBasedOnPhase : MonoBehaviour
{
	public float[] delaysInSec;

	public string[] msgs;
	public GameObject[] gos;
	public bool[] emptyPhases;

	public int loops = 0;

	float counter = 0;
	int phase = 0;
	float saveTime = 0;

	void Start()
	{

	}

	void Update()
	{
		if (this.enabled && phase < msgs.Length)
		{
			if (counter != 0)
			{
				if ((counter + saveTime) <= fa.time)
				{
					counter = 0;
					saveTime = fa.time;
					if (!emptyPhases[phase]) { gos[phase].SendMessage(msgs[phase]); }
					phase++;
					if (phase >= msgs.Length) { if (loops > 0) { phase = 0; loops--; } }
	
				}
			}
			else
			{
				//start a delay
				counter = delaysInSec[phase];
				saveTime = fa.time;
				//Setup.GC_DebugLog("Starting phase " + phase);
			}

		}
	}
}
