using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfXBehindCamera : MonoBehaviour
{
	public float XDist = 15;
	public bool callLoseDaddysLoveFunc = false;
	void Start()
	{

	}

	void Update()
	{
		if (fa.mainCameraObject != null)
		{
			if ((fa.mainCameraObject.transform.position.x - transform.position.x) > XDist)
			{
				ThrowingBallScript.self.GetHurt();
				Destroy(this.gameObject);
			}

		}
	}
}
