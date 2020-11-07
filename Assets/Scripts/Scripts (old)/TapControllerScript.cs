using UnityEngine;
using System.Collections;

public class TapControllerScript : MonoBehaviour
{
    public bool useRobotMusicOffset = false;
    public float robotMusicOffset = 0;

	public float[] tap1Times = new float[200];
	public float[] tap2Times = new float[200];
	public float[] tap3Times = new float[200];
	public float[] tap4Times = new float[200];
	public float[] tap5Times = new float[200];

	public float tap1Offset = 0;//applied to all times in the array, so to shift it a little bit you don't need to reenter them all
	public float tap2Offset = 0;
	public float tap3Offset = 0;
	public float tap4Offset = 0;
    public float tap5Offset = 0;
    float[] offsets = new float[5];

	public bool triggerTap1 = true;
	public bool triggerTap2 = true;
	public bool triggerTap3 = true;
	public bool triggerTap4 = true;
	public bool triggerTap5 = true;

	float[] tap1Hits = new float[200];
	float[] tap2Hits = new float[200];
	float[] tap3Hits = new float[200];
	float[] tap4Hits = new float[200];
	float[] tap5Hits = new float[200];


	int index = 0;
	bool on1 = false;
	bool on2 = false;
	bool on3 = false;
	bool on4 = false;
	bool on5 = false;
	bool resetOnLoop = false;

	void Start()
	{

	}

	void Update()
	{
		//loop system
		if (xa.music_Time > 2 && !resetOnLoop) { resetOnLoop = true; }
		if (resetOnLoop && xa.music_Time < 0.5)
		{
			resetOnLoop = false;
			index = 0;
			while (index < tap1Hits.Length)
			{
				tap1Hits[index] = 0;
				index++;
			}
		}

		//check for taps/beats
		on1 = false;
		on2 = false;
		on3 = false;
		on4 = false;
		on5 = false;

        offsets[0] = tap1Offset;
        offsets[1] = tap2Offset;
        offsets[2] = tap3Offset;
        offsets[3] = tap4Offset;
        offsets[4] = tap5Offset;

        //this is so that the music effects on Ruined City work with both the Explo version & the normal version of the fall
        if (useRobotMusicOffset)
        {
            if (za.skaldScript)
            {
                if (za.skaldScript.state == SkaldScript.State.NormalMusic || za.skaldScript.state == SkaldScript.State.StartNormalMusic)
                {
                        offsets[0] += robotMusicOffset;
                        offsets[1] += robotMusicOffset;
                        offsets[2] += robotMusicOffset;
                        offsets[3] += robotMusicOffset;
                        offsets[4] += robotMusicOffset;
                }
            }
        }

		index = 0;
		while (index < tap1Times.Length)
		{
			if (xa.music_Time > (tap1Times[index] + offsets[0]) && tap1Times[index] != 0 && tap1Hits[index] == 0) { on1 = true; tap1Hits[index] = 1; }
            if (xa.music_Time > (tap2Times[index] + offsets[1]) && tap2Times[index] != 0 && tap2Hits[index] == 0) { on2 = true; tap2Hits[index] = 1; }
            if (xa.music_Time > (tap3Times[index] + offsets[2]) && tap3Times[index] != 0 && tap3Hits[index] == 0) { on3 = true; tap3Hits[index] = 1; }
            if (xa.music_Time > (tap4Times[index] + offsets[3]) && tap4Times[index] != 0 && tap4Hits[index] == 0) { on4 = true; tap4Hits[index] = 1; }
            if (xa.music_Time > (tap5Times[index] + offsets[4]) && tap5Times[index] != 0 && tap5Hits[index] == 0) { on5 = true; tap5Hits[index] = 1; }


			index++;
		}

		if (!triggerTap1) { on1 = false; }
		if (!triggerTap2) { on2 = false; }
		if (!triggerTap3) { on3 = false; }
		if (!triggerTap4) { on4 = false; }
		if (!triggerTap5) { on5 = false; }

		//trigger a beat
		if (on1 || on2 || on3 || on4 || on5)
		{
			index = 0;
			while (index < xa.tapSlavesCheck.Length)
			{
				if (xa.tapSlavesCheck[index])
				{
					if (xa.tapSlaves[index])
					{
						if (on1 && xa.tapSlaves[index].useTap1) { xa.tapSlaves[index].tapMe(); }
						if (on2 && xa.tapSlaves[index].useTap2) { xa.tapSlaves[index].tapMe(); }
						if (on3 && xa.tapSlaves[index].useTap3) { xa.tapSlaves[index].tapMe(); }
						if (on4 && xa.tapSlaves[index].useTap4) { xa.tapSlaves[index].tapMe(); }
						if (on5 && xa.tapSlaves[index].useTap5) { xa.tapSlaves[index].tapMe(); }
					}
				}
				index++;
			}
		}
	}
}
