using UnityEngine;
using System.Collections;

public class AutoTapOnce : MonoBehaviour
{

	public bool dontTriggerOnLoopingSecondTrack = false;
	public bool onlyTriggerOnLoopingSecondTrack = false;
	public int numOfITweens = 0;
	// Use this for initialization
	void Start()
	{
		if ((!dontTriggerOnLoopingSecondTrack && !onlyTriggerOnLoopingSecondTrack) ||
			(dontTriggerOnLoopingSecondTrack && !xa.playingLoopingSecondTrack) ||
			(onlyTriggerOnLoopingSecondTrack && xa.playingLoopingSecondTrack))
		{
			int index = 0;
			// trigger iTweens
			index = 1;
			while (index <= numOfITweens)
			{
				iTweenEvent.GetEvent(this.gameObject, "trigger" + index).Play();
				index++;
			}
		}

	}
}
