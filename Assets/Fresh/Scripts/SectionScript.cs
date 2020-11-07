using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionScript : MonoBehaviour
{
	public GameObject controller;
	public float dist = 100;

	void Start()
	{

	}

	void Update()
	{
		if (Vector2.Distance(fa.cameraPos, transform.position) < (dist + 30))
		{
			controller.SetActive(true);
		}
		else
		{
			controller.SetActive(false);
		}
	}

	public bool CheckIfAmInYou(Vector3 pos)
	{
		if (Vector2.Distance(pos, transform.position) < dist) { return true; }
		return false;
	}

}
