using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingObjScript : MonoBehaviour
{
	void Start()
	{
		transform.SetAng(Random.Range(0f,360f),Random.Range(0f,360f),Random.Range(0f,360f));

	}

	void Update()
	{
		float localFreq = xa.beat_Freq;
		float multi = 15;
		float scaleSpd = 4;

		Vector3 goal = new Vector3(localFreq * multi,localFreq * multi,localFreq * multi);
		
		
		if(transform.localScale.x < goal.x)
		{
			Vector3 scale = transform.localScale;
			scale.x += scaleSpd * fa.deltaTime;
			transform.localScale = scale;
		}
		else
		{
			Vector3 scale = transform.localScale;
			scale.x -= scaleSpd * fa.deltaTime;
			transform.localScale = scale;
		}

		if(transform.localScale.y < goal.y)
		{
			Vector3 scale = transform.localScale;
			scale.y += scaleSpd * fa.deltaTime;
			transform.localScale = scale;
		}
		else
		{
			Vector3 scale = transform.localScale;
			scale.y -= scaleSpd * fa.deltaTime;
			transform.localScale = scale;
		}

		if(transform.localScale.z < goal.z)
		{
			Vector3 scale = transform.localScale;
			scale.z += scaleSpd * fa.deltaTime;
			transform.localScale = scale;
		}
		else
		{
			Vector3 scale = transform.localScale;
			scale.z -= scaleSpd * fa.deltaTime;
			transform.localScale = scale;
		}

		

		//rotate 
		float rotationSpd = 45f;
		Vector3 rotationVec = transform.localEulerAngles;
		rotationVec.x = rotationSpd * fa.deltaTime;
		rotationVec.y = rotationSpd * fa.deltaTime;
		rotationVec.z = rotationSpd * fa.deltaTime;
		transform.Rotate(rotationVec);

	}

}
