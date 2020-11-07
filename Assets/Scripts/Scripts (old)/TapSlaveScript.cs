using UnityEngine;
using System.Collections;

public class TapSlaveScript : MonoBehaviour
{
	public int numOfITweens                     = 0;
	public bool useTap1                         = false;
	public bool useTap2                         = false;
	public bool useTap3                         = false;
	public bool useTap4                         = false;
	public bool useTap5                         = false;
	public bool dontTriggerOnLoopingSecondTrack = false;
	public bool onlyTriggerOnLoopingSecondTrack = false;
    public Behaviour enableThis = null;
	int index                                   = 0;

	void Start()
	{
		if ((!dontTriggerOnLoopingSecondTrack && !onlyTriggerOnLoopingSecondTrack) ||
			(dontTriggerOnLoopingSecondTrack && !xa.playingLoopingSecondTrack) ||
			(onlyTriggerOnLoopingSecondTrack && xa.playingLoopingSecondTrack))
		{
			//find blank spots
			index = 0;
			while (index < xa.tapSlavesCheck.Length)
			{
				if (!xa.tapSlavesCheck[index])
				{
					xa.tapSlavesCheck[index] = true;
					xa.tapSlaves[index] = this;
					break;
				}
				index++;
			}
		}
	}

	public void tapMe()
	{
		//trigger iTweens
		index = 1;
		while (index <= numOfITweens)
		{
			iTweenEvent.GetEvent(this.gameObject, "trigger" + index).Play();
			index++;
		}

        if (enableThis) { enableThis.enabled = true; }
	}
}
