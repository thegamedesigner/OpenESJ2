using UnityEngine;
using System.Collections;

public class BeatSlave : MonoBehaviour
{
	public float	delay							= 0;
	public float	start							= 0;
	public float	stop							= 0;
	public float	beat							= 0;
	public int		numOfItweens					= 0;
	public bool		startLoop						= false;
	public bool		fireGun							= false;
	public bool		triggerAnimation				= false;
	public bool		triggerTriggeredAnimation		= false;
	public bool		dontTriggerOnLoopingSecondTrack = false;
	public bool		onlyTriggerOnLoopingSecondTrack = false;
    public Behaviour enableThis = null;

	float					localStart	= 0;
	float					localStop	= 0;
	int						index		= 0;
	float					counter		= 0;
	bool					startedLoop	= false;
	bool					resetMe		= false;
	GunScript				gunScript	= null;
	AniScript_playOnce		aniScript	= null;
	AniScript_TriggeredAni	aniScript2	= null;

	void Start()
	{
		localStart	= start;
		localStop	= stop;
		counter		= localStart;

		if (fireGun)
		{
			gunScript = this.gameObject.GetComponent<GunScript>();
		}
		if (triggerAnimation)
		{
			aniScript = this.gameObject.GetComponent<AniScript_playOnce>();
		}
		if (triggerTriggeredAnimation)
		{
			aniScript2 = this.gameObject.GetComponent<AniScript_TriggeredAni>();
		}
	}

	void Update()
	{
		if ((!dontTriggerOnLoopingSecondTrack && !onlyTriggerOnLoopingSecondTrack) ||
			(dontTriggerOnLoopingSecondTrack && !xa.playingLoopingSecondTrack) ||
			(onlyTriggerOnLoopingSecondTrack && xa.playingLoopingSecondTrack))
		{
			if (fa.time >= delay)
			{
				delay = 0;
				if (xa.music_Time <= 1 && resetMe)
				{
					resetMe = false;
					startedLoop = false;
					localStart = start;
					localStop = stop;
					counter = localStart;
					iTween.Stop(this.gameObject);
				}

				if (xa.music_Time > 1 && !resetMe)
				{
					resetMe = true;
				}

				index = 0;
				if (xa.music_Time >= localStart && xa.music_Time <= localStop)
				{
					if (xa.music_Time >= counter)
					{
                        counter = xa.music_Time + beat;
						if (counter <= beat && xa.music_Time > beat * 2)
						{
							/**
							 * So the deal here, is that when the beatslave is created,
							 * but xa.music_Time is > 0 (ie. we respawned and the music didn't restart)
							 * the counter thinks it's wayyy behind and creates a bunch 
							 * of tweens until counter == xa.music_Time resulting in a lag spike
							 * that is proportional to how far along we are in the music.
							 * 
							 * Now if the music is 2 beats ahead, we just automatically catch up.
							 */
							counter = xa.music_Time;
						}

						if (!startLoop || (startLoop && !startedLoop))
						{
							//activate tweens
							if (numOfItweens > 0)
							{
								index = 1;
								while (index <= numOfItweens)
								{
									startedLoop = true;
									//iTween.Resume(this.gameObject);
									iTweenEvent.GetEvent(this.gameObject, "trigger" + index).Play();
									index++;
								}
							}

							if (fireGun)
							{
								if (gunScript)
								{
									gunScript.fireABullet++;
									//Setup.GC_DebugLog("fired bullet " + fa.time + "-" + beat + "-" + counter);
								}
							}

							if (triggerAnimation)
							{
								if (aniScript)
								{
									aniScript.playing++;
								}
							}
							if (triggerTriggeredAnimation)
							{
								if (aniScript2)
								{
									aniScript2.triggerAni2();
								}
							}

                            if (enableThis)
                            {
                                enableThis.enabled = true;
                            }
						}

					}
				}

				if (startedLoop && startLoop && xa.music_Time > localStop)
				{
					iTween.Pause(this.gameObject);
					iTween.Stop(this.gameObject);
				}
			}
		}
	}

}
