using UnityEngine;
using System.Collections;

public class MusicHandCodedBeatsScript : MonoBehaviour
{
	public int numOfItweens = 0;
	public int whichSong = 0;

	bool on = false;
	int index = 0;
	float stage = 0;
	float result = 0;
	float stage2 = 0;
	float amount = 0;
	int beatIndex = 0;
	
	float numOfBeats = 6;
	float startTime = 0;
	float beatTime = 0;
	bool hit = false;
	bool looped = false;
	
	bool isLooped()
	{
		return looped;
	}
	
	void Start()
	{
		if (whichSong == 1)
		{
			numOfBeats = 6;
			startTime = 1.49f;
			beatTime = 0.215f;
		}
		if (whichSong == 2)
		{
			numOfBeats = 6;
			startTime = 1.49f;
			beatTime = 0.215f;
		}
	}

	void Update()
	{
		on = false;

		if (whichSong == 1)
		{
			song1Func();
		}


		if (on)
		{
			//trigger iTweens
			index = 1;
			while (index <= numOfItweens)
			{
				iTweenEvent.GetEvent(this.gameObject, "trigger" + index).Play();
				index++;
			}
		}

	}

	void song2Func()
	{
		/*
		 * Song1 is the cut down version of the FireFrost song, for the trailer.
		 * The beat starts at 1.49, goes five times, then needs to be reset to an exact time.
		 * Those times are:
		 * 1.49
		 * 3.21
		 * 4.924
		 * 6.637
		 * 8.35
		 * 10.27
		 * 11.99
		 *
		 * */

		hit = false;
		if (xa.music_Time < 0.2) { stage = 0; stage2 = 0; beatIndex = 0; looped = true; startTime = 1.49f; }
		amount = 3.21f; if (xa.music_Time >= amount && stage2 == 0) { hit = true; }
		amount = 4.924f; if (xa.music_Time >= amount && stage2 == 1) { hit = true; }
		amount = 6.637f; if (xa.music_Time >= amount && stage2 == 2) { hit = true; }
		amount = 8.35f; if (xa.music_Time >= amount && stage2 == 3) { hit = true; }
		amount = 10.27f; if (xa.music_Time >= amount && stage2 == 4) { hit = true; }
		amount = 11.99f; if (xa.music_Time >= amount && stage2 == 5) { looped = false; hit = true; }
		if (hit)
		{
			beatIndex = 0;
			startTime = xa.music_Time;
			stage2++;
			stage = 0;
		}
		result = (startTime + (beatTime * beatIndex));
		if (xa.music_Time >= result && stage != result && beatIndex < numOfBeats)
		{
			stage = result;
			on = true;
			beatIndex++;

		}

	}

	void song1Func()
	{
		/*
		 * Song1 is the cut down version of the Jump song, for the trailer.
		 * The beat starts at 1.49, goes five times, then needs to be reset to an exact time.
		 * Those times are:
		 * 1.49
		 * 3.21
		 * 4.924
		 * 6.637
		 * 8.35
		 * 10.27
		 * 11.99
		 *
		 * */

		hit = false;
		if (xa.music_Time < 0.2) { stage = 0; stage2 = 0; beatIndex = 0; looped = true; startTime = 1.49f; }
		amount = 3.21f; if (xa.music_Time >= amount && stage2 == 0) { hit = true; }
		amount = 4.924f; if (xa.music_Time >= amount && stage2 == 1) { hit = true; }
		amount = 6.637f; if (xa.music_Time >= amount && stage2 == 2) { hit = true; }
		amount = 8.35f; if (xa.music_Time >= amount && stage2 == 3) { hit = true; }
		amount = 10.27f; if (xa.music_Time >= amount && stage2 == 4) { hit = true; }
		amount = 11.99f; if (xa.music_Time >= amount && stage2 == 5) { looped = false; hit = true; }
		if (hit)
		{
			beatIndex = 0;
			startTime = xa.music_Time;
			stage2++;
			stage = 0;
		}
		result = (startTime + (beatTime * beatIndex));
		if (xa.music_Time >= result && stage != result && beatIndex < numOfBeats)
		{
			stage = result;
			on = true;
			beatIndex++;

		}

	}

	
}
