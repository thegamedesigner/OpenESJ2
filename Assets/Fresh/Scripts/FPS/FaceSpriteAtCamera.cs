using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSpriteAtCamera : MonoBehaviour
{
	public GameObject useThisGOInstead;

	void Start()
	{

	}

	void Update()
	{
		Vector3 goal = Vector3.zero;
		if (useThisGOInstead != null)
		{
			goal = useThisGOInstead.transform.position;
		}
		else
		{
			goal = FPSMainScript.FPSCam.transform.position;
		}
		transform.LookAt(goal);
		transform.SetAngX(0);

	}
}
