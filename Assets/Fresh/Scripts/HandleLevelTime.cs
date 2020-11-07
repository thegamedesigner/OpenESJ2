using UnityEngine;
using System.Collections;

public class HandleLevelTime : MonoBehaviour
{
	/*
	// Gross static stuff so we can reset through Setup.updateSpeedRunTime()
	public static bool paused = false;
	public static string timerStr;//This is what Fresh_InGameMenus.TimerText.text gets set to
	
	public static void Init()
	{
		Reset(true);
	}

	public static void HandleUpdate()
	{
		if (!fa.paused)
		{
			if (!za.thisLevelShouldntHaveScoreOrTimers)
			{
				calcZaSpeedRunTimeInMil();
				timerStr = timeToString(za.speedrunTime);
			}
			else
			{
				timerStr = string.Empty;
			}

			if (Fresh_InGameMenus.self != null)
			{
				Fresh_InGameMenus.self.TimerText.text = timerStr;
			}
		}
	}


	public static void Reset(bool pause)
	{
		xa.startTimeThisLevel = fa.timeInSeconds;
		paused = pause;
	}

	public static string timeToString(int ms, int s, int m, int h)
	{
		System.Text.StringBuilder time = new System.Text.StringBuilder(16, 16);
		if (h > 0)
		{
			time.Append(h);
			time.Append(":");
		}

		if (m < 10)
		{
			time.Append(0);
		}
		time.Append(m);
		time.Append(":");

		if (s < 10)
		{
			time.Append(0);
		}
		time.Append(s);
		time.Append(":");

		if (ms < 10)
		{
			time.Append("00");
		}
		else if (ms < 100)
		{
			time.Append(0);
		}
		time.Append(ms);

		return time.ToString();
	}

	public static string timeToString(float seconds = -1)
	{
		int h = (int)(seconds / 3600);
		seconds -= h * 3600;
		int m = (int)(seconds / 60);
		seconds -= m * 60;
		int s = (int)(seconds);
		seconds -= s;
		int ms = (int)(seconds * 1000);
		return timeToString(ms, s, m, h);
	}

	public static void calcZaSpeedRunTimeInMil()
	{
		za.speedrunTime = (fa.timeInSeconds - za.speedrunTimeSet);
	}

	public static string StaticTimeToString(float time)
	{
		return timeToString(time);
	}

	*/
}
