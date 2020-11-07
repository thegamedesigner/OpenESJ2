using UnityEngine;
using System.Collections;

public class RotateBackAndForth : MonoBehaviour
{

	public float speed = 0;
	public float amount = 0;

	float curveSpd = 0;
	public float curveMax = 0;
	public float curveMin = 0;
	public float curveAdd = 0;

	bool flip = false;
	bool curveFlip = false;
	float amountsCurr = 0;
	void Update()
	{
		if (curveFlip) { curveSpd -= curveAdd * fa.deltaTime; }
		if (!curveFlip) { curveSpd += curveAdd * fa.deltaTime; }
		if (curveSpd > curveMax) { curveSpd = curveMax; }
		if (curveSpd < curveMin) { curveSpd = curveMin; }
		xa.glx = transform.localEulerAngles;
		if (!flip)
		{
			xa.glx.z += speed * fa.deltaTime;
			amountsCurr += curveSpd * fa.deltaTime;
		}
		else
		{
			xa.glx.z -= speed * fa.deltaTime;
			amountsCurr -= speed * fa.deltaTime;
		}
		if (amountsCurr > amount && amount != 0) { curveSpd = 0; curveFlip = false; flip = true; }
		if (amountsCurr > (amount * 0.5f) && amount != 0 && flip == false) { curveFlip = true; }
		if (amountsCurr > -(amount * 0.5f) && amount != 0 && flip == true) { curveFlip = true; }
		if (amountsCurr < -amount && amount != 0) { curveSpd = 0; curveFlip = false; flip = false; }
		transform.localEulerAngles = xa.glx;
	}
}
