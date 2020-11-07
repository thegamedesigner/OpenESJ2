using UnityEngine;
using System.Collections;

public class LevelNameBannerScript : MonoBehaviour
{
	bool beenPaused = false;
	
	void Update()
	{
		if (Time.timeScale <= 0) { beenPaused = true; }

		if (beenPaused && Time.timeScale != 0)
		{
			xa.glx = transform.localScale;
			xa.glx.y = 0;//such a hack, but itweens aren't working for me right now.
			transform.localScale = xa.glx;
		}
	}
}
