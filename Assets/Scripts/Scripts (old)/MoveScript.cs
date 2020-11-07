using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{
	public float speedMin = 0;
	public float speedMax = 0;
	public bool keepVisualAngleAtZero = false;
	public bool giveRandom360ZAngle = false;

	Vector3 actualAng = Vector3.zero;
	Vector3 visualAng = Vector3.zero;
	// Use this for initialization
	void Start()
	{
		visualAng = transform.localEulerAngles;
		if (giveRandom360ZAngle)
		{
			xa.glx = transform.localEulerAngles;
			xa.glx.z = Random.Range(0, 360);
			transform.localEulerAngles = xa.glx;
		}
		actualAng = transform.localEulerAngles;
		speedMin = Random.Range(speedMin, speedMax);
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.localEulerAngles != Vector3.zero)
		{
			actualAng = transform.localEulerAngles;
		}
		transform.localEulerAngles = actualAng;
		//Move me
		transform.Translate(0, speedMin * fa.deltaTime, 0);
		transform.localEulerAngles = visualAng;
	}
}
