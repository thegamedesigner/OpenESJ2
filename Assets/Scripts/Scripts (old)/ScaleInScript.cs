using UnityEngine;
using System.Collections;

public class ScaleInScript : MonoBehaviour
{
	//This script scales something up, until it's reached it's max scales, then it turns off

	public bool SnapRelevantScalesToVeryTiny = false;
	public bool ScaleOnX = false;
	public bool ScaleOnY = false;
	public bool ScaleOnZ = false;
	public float ScaleXSpeed = 0;
	public float ScaleYSpeed = 0;
	public float ScaleZSpeed = 0;
	public float ScaleXMax = 0;
	public float ScaleYMax = 0;
	public float ScaleZMax = 0;

	bool done = false;
	// Use this for initialization
	void Start()
	{
		if (SnapRelevantScalesToVeryTiny)
		{
			xa.glx = transform.localScale;
			if (ScaleOnX) { xa.glx.x = 0.01f; }
			if (ScaleOnY) { xa.glx.y = 0.01f; }
			if (ScaleOnZ) { xa.glx.z = 0.01f; }
			transform.localScale = xa.glx;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(!done)
		{
			xa.glx = transform.localScale;
			xa.glx.x += ScaleXSpeed * fa.deltaTime;
			xa.glx.y += ScaleYSpeed * fa.deltaTime;
			xa.glx.z += ScaleZSpeed * fa.deltaTime;
			if (ScaleOnX) { xa.glx.x = Mathf.Clamp(xa.glx.x, 0, ScaleXMax);}
			if (ScaleOnY) { xa.glx.y = Mathf.Clamp(xa.glx.y, 0, ScaleYMax); }
			if (ScaleOnZ) { xa.glx.z = Mathf.Clamp(xa.glx.z, 0, ScaleZMax); }

			if (((xa.glx.x >= ScaleXMax && ScaleOnX) || (!ScaleOnX)) && ((xa.glx.y >= ScaleYMax && ScaleOnY) || (!ScaleOnY)) && ((xa.glx.z >= ScaleZMax && ScaleOnZ) || (!ScaleOnZ)))
			{
				done = true;
				this.enabled = false;
			}
			transform.localScale = xa.glx;
		}

	}
}
