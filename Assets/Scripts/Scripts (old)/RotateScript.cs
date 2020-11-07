using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{
	public float speed = 0;
	public float tiltSpeed = 0;

	void Start()
	{

	}

	void Update()
	{
		xa.glx = transform.localEulerAngles;
		xa.glx.x += tiltSpeed * fa.deltaTime;
		xa.glx.y += speed * fa.deltaTime;
		transform.localEulerAngles = xa.glx;
	}
}
