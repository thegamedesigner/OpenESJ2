using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRotateScript : MonoBehaviour
{
	public Vector3 rotateVec;

	void Start()
	{

	}

	void Update()
	{
		transform.Rotate(rotateVec * fa.deltaTime);
	}
}
