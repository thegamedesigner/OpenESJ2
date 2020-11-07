using UnityEngine;
using System.Collections;

public class SwapCameraToMusicScript : MonoBehaviour
{
	public float time = 0;
	bool flipped = false;
	void Start()
	{

	}

	void Update()
	{
		if (xa.music_Time >= time)
		{
			if (!flipped)
			{
				flipped = true;
				xa.glx = Camera.main.GetComponent<Camera>().transform.localEulerAngles;
				xa.glx.z += 180;
				Camera.main.GetComponent<Camera>().transform.localEulerAngles = xa.glx;
			}

		}
		else
		{
			if (flipped)
			{
				flipped = false;
			}
		}
	}
}
