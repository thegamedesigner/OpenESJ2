using UnityEngine;
using System.Collections;

public class SetToRandom360Angle : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		xa.glx = transform.localEulerAngles;
		xa.glx.z = Random.Range(0, 360);
		transform.localEulerAngles = xa.glx;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
