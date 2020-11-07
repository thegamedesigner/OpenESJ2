using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAniController : MonoBehaviour
{
	[HideInInspector]
	public LegController2Script.aniTypes aniType;
	[HideInInspector]
	public float dir = 2;

	public GameObject puppet;
	public TextMesh name;
	public AnimationScript_Generic aniScript;

	LegController2Script.aniTypes aniOld = LegController2Script.aniTypes.None;

	void Start()
	{

	}

	void Update()
	{
		if (aniOld != aniType)
		{
			//Debug.Log("PlayAni: " + aniType);
			aniOld = aniType;
			int result = GetIntForType(aniType);
			if (result != -1)
			{
				aniScript.playAnimation(result);
			}
		}
	}

	int GetIntForType(LegController2Script.aniTypes type)
	{
		switch (type)
		{
			case LegController2Script.aniTypes.Stumble1: return 0;
			case LegController2Script.aniTypes.Walk: return 1;
			case LegController2Script.aniTypes.Stand: return 2;
			case LegController2Script.aniTypes.Ascend: return 3;
			case LegController2Script.aniTypes.Fall: return 4;
			case LegController2Script.aniTypes.Dead: return 5;
			case LegController2Script.aniTypes.Wall: return 6;
			case LegController2Script.aniTypes.Hover: return 7;
			case LegController2Script.aniTypes.Stumble2: return 8;
			case LegController2Script.aniTypes.Land1: return 9;
			case LegController2Script.aniTypes.Land2: return 10;
			case LegController2Script.aniTypes.OffWall: return 11;
			case LegController2Script.aniTypes.Crouch: return 12;
			case LegController2Script.aniTypes.AirSword: return 13;
			case LegController2Script.aniTypes.WallFloat: return 14;
		}
		return -1;
	}

}
