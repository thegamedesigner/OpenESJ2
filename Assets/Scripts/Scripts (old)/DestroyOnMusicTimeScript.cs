using UnityEngine;
using System.Collections;

public class DestroyOnMusicTimeScript : MonoBehaviour
{
	public GameObject go = null;
	public float timeInMusic = 0;
	bool triggered = false;
	public bool killInsteadOfDestroy = false;
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
				if (!killInsteadOfDestroy)
				{
					triggered = true;
					Destroy(go);
					this.enabled = false;//turn off this script.
				}
				else
				{
					HealthScript script = null;
					script = this.gameObject.GetComponent<HealthScript>();
					if (script)
					{
						triggered = true;
						script.health = 0;
						this.enabled = false;//turn off this script.
					}
				}
			}
		}

	}
}
