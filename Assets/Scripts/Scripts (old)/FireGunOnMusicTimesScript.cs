using UnityEngine;
using System.Collections;

public class FireGunOnMusicTimesScript : MonoBehaviour
{
	public float[] times = new float[50];
	float[] localTimes = new float[50];

	GunScript gunScript;
	bool resetMe = false;
	int index = 0;

	void Start()
	{
		gunScript = this.gameObject.GetComponent<GunScript>();

		index = 0;
		while(index < times.Length)
		{
			localTimes[index] = times[index];
			index++;
		}
	}

	void Update()
	{
		if (xa.music_Time <= 1 && resetMe)
		{
			resetMe = false;
			index = 0;
			while (index < times.Length)
			{
				localTimes[index] = times[index];
				index++;
			}
		}
		if (xa.music_Time > 1 && !resetMe) { resetMe = true; }


		index = 0;
		while(index < localTimes.Length)
		{
			if (localTimes[index] != 0)
			{
				if (localTimes[index] <= xa.music_Time)
				{
					if (gunScript)
					{
						//Setup.GC_DebugLog(xa.music_Time);
						gunScript.fireABullet++;
					}
					localTimes[index] = 0;
				}
			}
			index++;
		}

	}
}
