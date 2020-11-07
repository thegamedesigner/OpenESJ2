using UnityEngine;
using System.Collections;

public class MatchCameraZPosition : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		if(transform.position.z != Camera.main.gameObject.transform.position.z)
		{
			xa.glx = transform.position;
			xa.glx.z = Camera.main.gameObject.transform.position.z;
			transform.position = xa.glx;
		}
	}
}
