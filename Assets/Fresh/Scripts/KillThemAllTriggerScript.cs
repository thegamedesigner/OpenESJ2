using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillThemAllTriggerScript : MonoBehaviour
{
	public GameObject[] GOs = new GameObject[0];
	public GameObject setThisGOActive;
	void Start()
	{

	}

	void Update()
	{
		int count = 0;
		for (int i = 0; i < GOs.Length; i++)
		{
			if (GOs[i] == null) { count++; }
		}
		if(count == GOs.Length) {setThisGOActive.SetActive(true); }
	}
}
