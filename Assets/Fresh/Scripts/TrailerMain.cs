using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerMain : MonoBehaviour
{
	public bool DoStoryTelling = false;
	public GameObject[] reviews;

	// Use this for initialization
	float delay = 5;
	void Start()
	{
		if (DoStoryTelling)
		{
			for (int i = 0; i < reviews.Length; i++)
			{
			//	reviews[i].transform.LocalSetY(reviews[i].transform.localPosition.y - 20);
				
				reviews[i].transform.localScale = new Vector3(0, 0, 0.1f);
				reviews[i].transform.localEulerAngles = new Vector3(0, 0, -2f);
				iTween.RotateBy(reviews[i], iTween.Hash("z", 0.0111111111111111, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
				//iTween.MoveBy(reviews[i], iTween.Hash("delay", delay, "y", 20, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine));
				iTween.ScaleTo(reviews[i], iTween.Hash("delay", delay, "x", 0.1f, "y", 0.1f, "time", 0.2f, "easetype", iTween.EaseType.easeInExpo));

				delay += 0.1f;
			}
		}
		else
		{
			if (reviews.Length > 0)
			{

				for (int i = 0; i < reviews.Length; i++)
				{
					reviews[i].transform.localScale = new Vector3(0, 0, 0.1f);

					reviews[i].transform.localEulerAngles = new Vector3(0, 0, -3.5f);
					iTween.RotateBy(reviews[i], iTween.Hash("z", 0.0194444444444444, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

				}

				for (int i = 0; i < 3; i++)
				{
					delay += 0.1f;
					iTween.ScaleTo(reviews[i], iTween.Hash("delay", delay, "x", 0.1f, "y", 0.1f, "time", 0.2f, "easetype", iTween.EaseType.easeInExpo));

					iTween.ScaleTo(reviews[i], iTween.Hash("delay", delay + 1.7f, "x", 0, "y", 0, "time", 0.1f, "easetype", iTween.EaseType.easeOutExpo));

				}

				delay += 1.8f;

				for (int i = 3; i < 6; i++)
				{
					delay += 0.1f;
					iTween.ScaleTo(reviews[i], iTween.Hash("delay", delay, "x", 0.1f, "y", 0.1f, "time", 0.2f, "easetype", iTween.EaseType.easeInExpo));

					iTween.ScaleTo(reviews[i], iTween.Hash("delay", delay + 1.7f, "x", 0, "y", 0, "time", 0.1f, "easetype", iTween.EaseType.easeOutExpo));

				}
			}
		}

	}

	// Update is called once per frame
	void Update()
	{
		fa.UpdateTime();

		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}
}
