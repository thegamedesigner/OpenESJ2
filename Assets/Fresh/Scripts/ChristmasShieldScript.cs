using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasShieldScript : MonoBehaviour
{
	public GameObject exploEffect;
	public ElfScript[] elves;

	void Start()
	{

	}

	void Update()
	{
		bool success = true;
		for (int i = 0; i < elves.Length; i++)
		{
			if (!elves[i].rescued)
			{
				success = false;
				break;
			}
		}
		if (success)
		{
			GameObject go = Instantiate(exploEffect, transform.position, transform.rotation);
			this.gameObject.SetActive(false);
		}
	}
}
