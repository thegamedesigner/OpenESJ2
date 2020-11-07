using UnityEngine;
using System.Collections;

public class TriggerItweenOnMusicTimeScript : MonoBehaviour
{
	public string itweenName = "";
	public float timeInMusic = 0;
	bool triggered = false;

	public bool dontTriggerOnLoopingSecondTrack = false;
	public bool onlyTriggerOnLoopingSecondTrack = false;

	void Update()
	{
		if ((!dontTriggerOnLoopingSecondTrack && !onlyTriggerOnLoopingSecondTrack) ||
			(dontTriggerOnLoopingSecondTrack && !xa.playingLoopingSecondTrack) ||
			(onlyTriggerOnLoopingSecondTrack && xa.playingLoopingSecondTrack))
		{
			if (xa.music_Time >= timeInMusic && !triggered)
			{
				triggered = true;
				iTweenEvent.GetEvent(this.gameObject, itweenName).Play();
			}
		}

	}
}
