using UnityEngine;
using System.Collections;

public class MatchPlayerYScript : MonoBehaviour
{
	public float maxSpeed = 0;
	public float add = 0;
	float spd = 0;
	public bool onlyActiveBetweenMusicStartAndStop = false;
	public float musicStart = 0;
	public float musicStop = 0;
	public bool dontTriggerOnLoopingSecondTrack = false;
	public bool onlyTriggerOnLoopingSecondTrack = false;
	public bool AlwaysFollowOnLoopingSecondTrack = false;
	public bool usePopeBehaviour = false;

	public float popeDelayInSeconds = 0;
	float popeCounter = 0;

	void Start()
	{

	}

	void Update()
	{
		if ((!dontTriggerOnLoopingSecondTrack && !onlyTriggerOnLoopingSecondTrack) ||
			(dontTriggerOnLoopingSecondTrack && !xa.playingLoopingSecondTrack) ||
			(onlyTriggerOnLoopingSecondTrack && xa.playingLoopingSecondTrack) ||
			AlwaysFollowOnLoopingSecondTrack && xa.playingLoopingSecondTrack)
		{
			if ((AlwaysFollowOnLoopingSecondTrack && onlyActiveBetweenMusicStartAndStop && xa.playingLoopingSecondTrack) || !onlyActiveBetweenMusicStartAndStop || (onlyActiveBetweenMusicStartAndStop && xa.music_Time >= musicStart && xa.music_Time <= musicStop))
			{

				if (xa.player && !xa.playerDead)
				{
					if (usePopeBehaviour)
					{
						popeBehaviour();
					}
					else
					{
						wizardBehaviour();
					}
				}
			}
		}
	}

	void popeBehaviour()
	{
		if ((popeCounter + popeDelayInSeconds) < fa.time)
		{
			xa.glx = xa.player.transform.position;
			xa.glx.z = transform.position.z;
			xa.glx.x = transform.position.x;
			iTween.MoveTo(this.gameObject, iTween.Hash("position", xa.glx, "time", maxSpeed));
			popeCounter = fa.time;
		}
		else
		{

		}
	}

	void wizardBehaviour()
	{
		xa.glx = transform.position;

		if (xa.glx.y < xa.player.transform.position.y)
		{
			spd += add; // * fa.deltaTime;
		}
		if (xa.glx.y >= xa.player.transform.position.y)
		{
			spd -= add; // * fa.deltaTime;
		}
		if (spd < -maxSpeed) { spd = -maxSpeed; }
		if (spd > maxSpeed) { spd = maxSpeed; }
		xa.glx.y += spd * Time.timeScale * fa.deltaTime;
		transform.position = xa.glx;
	}
}
