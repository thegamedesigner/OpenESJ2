using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffectNodeScript : MonoBehaviour
{
	public GameObject prefab;

	float delay = 3;
	float timeset;
	void Start()
	{
		timeset = fa.time + 2;
	}

	void Update()
	{
		if (fa.time > (timeset + delay))
		{
			timeset = fa.time;
			GameObject go = Instantiate(prefab, transform.position,transform.rotation);
		}

	}
}
