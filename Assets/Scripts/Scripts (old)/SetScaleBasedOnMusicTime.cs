using UnityEngine;
using System.Collections;

public class SetScaleBasedOnMusicTime : MonoBehaviour
{
	public float startTime = 0;
	public float endTime = 0;
	public Vector3 scaleBefore = Vector3.zero;
	public Vector3 scaleDuring = Vector3.zero;
	public Vector3 scaleAfter = Vector3.zero;
	int triggeredStage = 0;

	void Start()
	{
		transform.localScale = scaleBefore;
	}

	// Update is called once per frame
	void Update()
	{
		if (xa.music_Time >= startTime && triggeredStage == 0)
		{
			triggeredStage = 1;
			transform.localScale = scaleDuring;
		}
		if (xa.music_Time >= endTime && triggeredStage == 1)
		{
			triggeredStage = 2;
			transform.localScale = scaleAfter;
		}
		if (xa.music_Time <= startTime && triggeredStage == 2)
		{
			triggeredStage = 0;
			transform.localScale = scaleBefore;
		}
	}
}
