using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItweensScript : MonoBehaviour
{
	public Type type = Type.None;
	public enum Type
	{
		None,
		WhiteCircle,
		End
	}

	void Start()
	{
		switch (type)
		{
			case Type.WhiteCircle:
				iTween.ScaleTo(this.gameObject,iTween.Hash("x", 22, "y", 22, "time", 2.5f, "easetype", iTween.EaseType.easeOutSine));
				iTween.FadeTo(this.gameObject,iTween.Hash("delay", 1, "alpha", 0, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				break;
		}
	}

	void Update()
	{
		switch (type)
		{
			case Type.WhiteCircle:
				break;
		}
	}
}
