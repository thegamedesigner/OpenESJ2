using UnityEngine;
using System.Collections;

public class AimEyeballLaser : MonoBehaviour
{
	Vector3 ang1;
	Vector3 ang2;
	Vector3 ang3;
	Vector3 vec1;
	Vector3 vec2;
	Vector3 vec3;
	float result1;
	float result2;
	float result3;
	Vector3 target = Vector3.zero;
	float turnSpeed = 20;

	void Start()
	{
		xa.glx = transform.localEulerAngles;
		xa.glx.z = -55;
		transform.localEulerAngles = xa.glx;
	}
	void Update()
	{
		if (xa.player)
		{
			target = xa.player.transform.position;
			target.z = transform.position.z;

			ang1 = transform.localEulerAngles;
			ang1.z += 4;
			vec1 = Setup.projectVec(transform.position, ang1, 5, -Vector3.left);
			ang2 = transform.localEulerAngles;
			ang2.z -= 4;
			vec2 = Setup.projectVec(transform.position, ang2, 5, -Vector3.left);
			ang3 = transform.localEulerAngles;
			vec3 = Setup.projectVec(transform.position, ang3, 5, -Vector3.left);

			result1 = Vector3.Distance(target, vec1);
			result2 = Vector3.Distance(target, vec2);
			result3 = Vector3.Distance(target, vec3);

			if (result3 < result1 && result3 < result2)
			{
				//deadzone of half four on either side of center (-2,2)
			}
			else
			{
				if (result1 < result2)
				{
					xa.glx = transform.localEulerAngles;
					xa.glx.z += turnSpeed * fa.deltaTime;
					transform.localEulerAngles = xa.glx;
				}
				else
				{
					xa.glx = transform.localEulerAngles;
					xa.glx.z -= turnSpeed * fa.deltaTime;
					transform.localEulerAngles = xa.glx;
				}
			}

		}
	}
}
