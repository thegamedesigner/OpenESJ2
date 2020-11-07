using UnityEngine;
using System.Collections;

public class OldGenericTriggerScript : MonoBehaviour
{
	public float startingDelayInSeconds = 0;
	public bool dontLoop = false;
	public bool disableOnEnd = false;
	public int stopLoopingAfterXLoops = 0;
	public float[] firstDelayInSeconds;
	public Behaviour[] enableThisScript;
	public Behaviour[] disableThisScript;
	public string[] sendMsg;
	public GameObject[] sendMsgGO;
	public string[] itweenToPlay;
	public GameObject[] itweenGO;
	public bool[] waitForItweenToComplete;
	public float[] secondDelayInSeconds;

	float delay = 0;
	int loopCount = 0;

	void Start()
	{
		delay = startingDelayInSeconds + fa.time;
	}

	int instructionBlockIndex = 0;
	int instructionBlockPhase = 0;

	void Update()
	{
		if (fa.time < delay)
		{
			//wait...
		}
		else
		{
			if (instructionBlockPhase == 0)
			{
				//first delay
				if (firstDelayInSeconds.Length > instructionBlockIndex)
				{
					delay = firstDelayInSeconds[instructionBlockIndex] + fa.time;
				}
				else
				{
					delay = 0;
				}
				instructionBlockPhase = 1;
			}
			else if (instructionBlockPhase == 1)
			{
				//trigger script
				instructionBlockPhase = 3;
				if (enableThisScript.Length > instructionBlockIndex)
				{
					if (enableThisScript[instructionBlockIndex])
					{
						enableThisScript[instructionBlockIndex].enabled = true;
					}
				}

				if (disableThisScript.Length > instructionBlockIndex)
				{
					if (disableThisScript[instructionBlockIndex]) { disableThisScript[instructionBlockIndex].enabled = true; }
				}
				if (sendMsgGO.Length > instructionBlockIndex)
				{
					if (sendMsgGO[instructionBlockIndex]) { sendMsgGO[instructionBlockIndex].SendMessage(sendMsg[instructionBlockIndex]); }
				}
				if (itweenGO.Length > instructionBlockIndex)
				{
					if (itweenGO[instructionBlockIndex])
					{
						iTweenEvent.GetEvent(itweenGO[instructionBlockIndex], itweenToPlay[instructionBlockIndex]).Play();
						if (waitForItweenToComplete[instructionBlockIndex]) { instructionBlockPhase = 2; }
					}
				}


			}
			else if (instructionBlockPhase == 2)
			{
				//waiting for itween to complete
			}
			else if (instructionBlockPhase == 3)
			{
				//second delay
				if (secondDelayInSeconds.Length > instructionBlockIndex)
				{
					delay = secondDelayInSeconds[instructionBlockIndex] + fa.time;
				}
				else
				{
					delay = 0;
				}
				instructionBlockPhase = 4;
			}
			else if (instructionBlockPhase == 4)
			{
				//move to next instruction block (or loop)
				instructionBlockPhase = 0;
				instructionBlockIndex++;
				if (instructionBlockIndex >= firstDelayInSeconds.Length)
				{
					if (!dontLoop && (stopLoopingAfterXLoops == 0 || (loopCount < stopLoopingAfterXLoops))) { instructionBlockIndex = 0; loopCount++; }//loop
					if (disableOnEnd) { this.enabled = false; }
				}
			}
		}


	}

	public void itweenComplete()
	{
		if (instructionBlockPhase == 2) { instructionBlockPhase = 3; }
	}
}
