using UnityEngine;
using System.Collections;

public class BasedOnRealTime_ScaleY : MonoBehaviour
{
	public float timeInSeconds = 1;//Time in seconds it takes to fade in
	float timeSet = -1;
	public float scaleYMulti = 2;//scale is 0 to 1, times this. (so if you want 3, x3)

	float result = 0;

	void Awake()
	{
	}

	void Update()
	{
		fadeIn();
	}

	public void fadeIn()
	{
		if (timeSet == -1)
		{
			timeSet = Time.realtimeSinceStartup;
		}
		// Setup.GC_DebugLog(MerpsSetup.basedOnRealTime(timeSet, timeInSeconds));


		result = 1 - MerpsSetup.basedOnRealTime(timeSet, timeInSeconds);
		xa.glx = transform.localScale;
		xa.glx.y = result * scaleYMulti;
		transform.localScale = xa.glx;
		if (xa.glx.y >= 1 * scaleYMulti) { this.enabled = false; }
	}
}
