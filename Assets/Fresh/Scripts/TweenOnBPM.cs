using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenOnBPM : MonoBehaviour
{
	public GameObject goToTween;
	public float start = 0;
	public float end = 0;
	public float timeDelay = 0;//The exact amount of time to repeat after
	float goal = 0;

	void Start()
	{
		goal = start;
	}

	void Update()
	{
		if (xa.music_Time >= start && xa.music_Time <= end)
		{
			if (xa.music_Time >= goal)
			{
				goal = xa.music_Time + timeDelay;
				iTweenEvent.GetEvent(goToTween, "RemoteItween1").Play();

			}
		}
	}
}
