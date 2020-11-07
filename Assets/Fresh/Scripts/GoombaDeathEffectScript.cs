using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaDeathEffectScript : MonoBehaviour
{
	public GameObject secondaryEffect;
	void Start()
	{
		if (secondaryEffect != null)
		{
			Instantiate(secondaryEffect, transform.position, transform.rotation);
		}

		iTween.ScaleTo(this.gameObject, iTween.Hash("x", 1, "y", 1, "time", 0.3f, "easetype", iTween.EaseType.easeInOutSine));

		iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 0, "time", 7, "easetype", iTween.EaseType.easeInOutSine));


		iTween.MoveBy(this.gameObject, iTween.Hash("delay", 3, "y", 40, "time", 15, "easetype", iTween.EaseType.easeInSine));
		this.enabled = false;
	}

	void Update()
	{

	}
}
