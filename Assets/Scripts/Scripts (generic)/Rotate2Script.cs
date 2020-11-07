using UnityEngine;
using System.Collections;

public class Rotate2Script : MonoBehaviour
{
	public Vector3 speed;

	void Update()
	{
		xa.glx = transform.localEulerAngles;
		//xa.glx = Vector3.Scale(transform.localEulerAngles, speed * fa.deltaTime);
		xa.glx.x += speed.x * fa.deltaTime;
		xa.glx.y += speed.y * fa.deltaTime;
		xa.glx.z += speed.z * fa.deltaTime;
		transform.localEulerAngles = xa.glx;
	}
}
