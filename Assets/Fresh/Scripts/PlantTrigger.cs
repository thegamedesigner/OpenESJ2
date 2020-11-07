using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrigger : MonoBehaviour
{
	GameObject[] gos;

	void Start()
	{
		gos = GameObject.FindGameObjectsWithTag("triggerablePlant");
	}

	void Update()
	{
		if (xa.player == null) { return; }
		for (int i = 0; i < gos.Length; i++)
		{
			if(gos[i] == null) {continue; }
			if (Vector2.Distance(xa.player.transform.position, gos[i].transform.position) < 5)
			{
				gos[i].GetComponent<Info>().triggered = true;
				gos[i] = null;
			}


		}
	}
}
