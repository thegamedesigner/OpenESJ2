using UnityEngine;
using System.Collections;

public class StoryTextItweenAwaaay : MonoBehaviour
{
	public float timeDelay = 0;
	public int moveToYPosition = 5;
	float offset = 0;

	void Start()
	{
		offset = fa.time;
	}

	void Update()
	{
		if (fa.time > (offset + 0.1f))
			{
			if (Controls.GetAnyKeyDownOnce() ||
				(fa.time - offset) > timeDelay)
			{
				iTween.MoveTo(this.gameObject, iTween.Hash("y", moveToYPosition, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "islocal", false));
				iTween.FadeTo(this.gameObject, iTween.Hash("amount", 0, "time", 1, "easetype", iTween.EaseType.easeInOutSine));
				Destroy(gameObject, 1.05f);
			}
		}
	}
}
