using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSExploScript : MonoBehaviour
{
	public Type type;
	public enum Type
	{
		None,
		ShotgunPelletBeam,
		End

	}

	void Start()
	{
		switch (type)
		{
			case Type.ShotgunPelletBeam:
				iTween.FadeTo(this.gameObject,iTween.Hash("alpha", 0, "time", 0.3f, "easetype", iTween.EaseType.easeInOutSine));
				break;
		}
	}

}
