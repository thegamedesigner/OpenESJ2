using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGOOnLoop : MonoBehaviour
{
	public GameObject go;
	public float delay;
	float timeSet;

	void Start()
	{

	}

	void Update()
	{
		if (fa.time >= (timeSet + delay))
		{
			timeSet = fa.time;
			GameObject g = Instantiate(go,transform.position,transform.rotation);
		}
	}
}
