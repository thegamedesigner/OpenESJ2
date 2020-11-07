using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedText : MonoBehaviour
{
	
	public TextMesh textMesh;
	public string text = "";
	void Start()
	{
		iTween.MoveBy(this.gameObject,iTween.Hash("delay", 0, "y", 0.7f, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine));
		iTween.FadeTo(this.gameObject,iTween.Hash("delay", 0, "alpha", 1, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine));


		iTween.MoveBy(this.gameObject,iTween.Hash("delay", 0.5f, "y", 0.7f, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine));
		iTween.FadeTo(this.gameObject,iTween.Hash("delay", 0.5f, "alpha", 0, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine));
	}

	void Update()
	{

	}
}
