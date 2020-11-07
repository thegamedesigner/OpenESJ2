using UnityEngine;
using System.Collections;

public class ScrollMetaNodes : MonoBehaviour
{
	public float min = 0;
	public float max = 0;

	public GameObject centerOnThis = null;
	public GameObject MoveThis = null;
	//float dist = 5;
	//float timeSet = 0;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//Setup.GC_DebugLog(xa.levelSelectCurrentNumber);

		if (xa.levelSelectCurrentNumber >= 1)
		{
			xa.glx = transform.localPosition;
			xa.glx.x = 30 - (xa.levelSelectCurrentNumber * 4);
			MoveThis.transform.localPosition = xa.glx;
		}
		/*
		if ((xa.levelSelectCurrentNumber > 8 && xa.levelSelectCurrentNumber < 17) || (xa.levelSelectCurrentNumber > 24))
		{
			xa.glx = transform.localPosition;
			xa.glx.x = -20;
			MoveThis.transform.localPosition = xa.glx;
		}
		else
		{
			xa.glx = transform.localPosition;
			xa.glx.x = 12;
			MoveThis.transform.localPosition = xa.glx;
		}
		*/


		// {
		//    xa.glx = MoveThis.transform.position;
		//
		//     MoveThis.transform.position = xa.glx;
		// }
		// xa.glx = MoveThis.transform.position;
		// xa.glx.x = centerOnThis.transform.position.x;
		// MoveThis.transform.position = xa.glx;

		//xa.glx = transform.position;
		//xa.glx.x = centerOnThis.transform.position.x;// +dist;
		//transform.position = xa.glx;
		// if (centerOnThis.transform.position.x > transform.position.x)
		// {
		//    xa.glx = MoveThis.transform.position;
		//
		//     MoveThis.transform.position = xa.glx;
		// }
		// if (transform.position.x > (centerOnThis.transform.position.x - dist))
		// {
		//     Setup.GC_DebugLog("Right");
		// }
		/*

		if (!CustomControls.GetKey(CustomControls.Action.LEFT) && !CustomControls.GetKey(CustomControls.Action.RIGHT))
		{
			timeSet = 0;
		}

		if (CustomControls.GetKey(CustomControls.Action.LEFT))
		{
			if (fa.time > (timeSet + 0.2f))
			{
				xa.glx = MoveThis.transform.position;
				xa.glx.x += 4;
				MoveThis.transform.position = xa.glx;
				timeSet = fa.time;
			}
		}
		if (CustomControls.GetKey(CustomControls.Action.RIGHT))
		{
			if (fa.time > (timeSet + 0.2f))
			{
				xa.glx = MoveThis.transform.position;
				xa.glx.x -= 4;
				MoveThis.transform.position = xa.glx;
				timeSet = fa.time;
			}
		}*/
	}

	void scrollLeft()
	{
		xa.glx = transform.localPosition;
		xa.glx.x += 5 * fa.deltaTime;
		if (xa.glx.x > max) { xa.glx.x = max; }
		if (xa.glx.x < min) { xa.glx.x = min; }
		transform.localPosition = xa.glx;
	}
	void scrollRight()
	{
		xa.glx = transform.localPosition;
		xa.glx.x -= 5 * fa.deltaTime;
		if (xa.glx.x > max) { xa.glx.x = max; }
		if (xa.glx.x < min) { xa.glx.x = min; }
		transform.localPosition = xa.glx;
	}
}
