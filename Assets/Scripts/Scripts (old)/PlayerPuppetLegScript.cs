using UnityEngine;
using System.Collections;

public class PlayerPuppetLegScript : MonoBehaviour
{
	public float walkSpd = 0;
	public float stompSpd = 0;
	public float backRange = 0;
	public float frontRange = 0;
	public float legLength = 0;
	public float releasePoint = 0;

	public GameObject otherLeg;
	public bool leadLeg;
	PlayerPuppetLegScript script;

	bool backwards = false;

	int phase = 1;
	void Start()
	{
		script = otherLeg.GetComponent<PlayerPuppetLegScript>();
		if (!leadLeg)
		{
			walkSpd = script.walkSpd;
			stompSpd = script.stompSpd;
			backRange = script.backRange;
			frontRange = script.frontRange;
			legLength = script.legLength;
			releasePoint = script.releasePoint;

			phase = 3;
		}

		//xa.glx = transform.localPosition;
		//xa.glx.y = -0.5f;
		//transform.localPosition = xa.glx;


	}

	void Update()
	{
		backwards = false;
		if (xa.playerDir == -1) { backwards = true; }

		if (!leadLeg)
		{
			walkSpd = script.walkSpd;
			stompSpd = script.stompSpd;
			backRange = script.backRange;
			frontRange = script.frontRange;
			legLength = script.legLength;
			releasePoint = script.releasePoint;
		}

		if (phase == 1)
		{
			//push backwards

			if (!backwards)
			{
				xa.glx = transform.localPosition;
				xa.glx.x -= walkSpd * fa.deltaTime;
				transform.localPosition = xa.glx;

				if (transform.localPosition.x < backRange) { phase = 2; }
			}
			else
			{
				xa.glx = transform.localPosition;
				xa.glx.x += walkSpd * fa.deltaTime;
				transform.localPosition = xa.glx;

				if (transform.localPosition.x > -backRange) { phase = 2; }
			}
		}
		if (phase == 2)
		{
			xa.glx = transform.localPosition;
			xa.glx.y += stompSpd * fa.deltaTime;
			transform.localPosition = xa.glx;

			if (transform.localPosition.y > -0.22f)
			{
				xa.glx = transform.localPosition;
				xa.glx.y = -0.22f;
				transform.localPosition = xa.glx;
				phase = 3;
			}
		}
		if (phase == 3)
		{
			xa.glx = transform.localPosition;
			if (!backwards) { xa.glx.x = frontRange; }
			else { xa.glx.x = -frontRange; }
			transform.localPosition = xa.glx;

			if (leadLeg)
			{
				phase = 4;
			}
			else
			{
				if (script.phase == 1 && otherLeg.transform.localPosition.x < releasePoint) { phase = 4; }
			}
		}
		if (phase == 4)
		{
			xa.glx = transform.localPosition;
			xa.glx.y -= stompSpd * fa.deltaTime;
			transform.localPosition = xa.glx;

			if (transform.localPosition.y < -0.5f)
			{
				xa.glx = transform.localPosition;
				xa.glx.y = legLength;
				transform.localPosition = xa.glx;
				phase = 1;
			}
		}

	}
}
