using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFacingBasedOnMovement : MonoBehaviour
{
	Vector3 lastPos;
	Vector3 scale;
	public bool invert = false;
	float invertVar = 1;
	float dir = 1;
	void Start()
	{
		scale = transform.localScale;
		if (invert) { invertVar = -1; }
	}

	float timeSet = 0;
	float delay = 0.5f;
	void Update()
	{
		//if(fa.time > (timeSet + delay))
		//{
			timeSet = fa.time;
			if (transform.position.x < lastPos.x) { dir = 1; }
			if (transform.position.x > lastPos.x) { dir = -1; }

			transform.localScale = new Vector3((scale.x * dir) * invertVar,scale.y,scale.z);

			lastPos = transform.position;
		//}
	}
}
