using UnityEngine;
using System.Collections;

public class StayAtAngle : MonoBehaviour
{
	public Vector3 angle = Vector3.zero;
	void Update()
	{
		transform.localEulerAngles = angle;
	}
}
