using UnityEngine;
using System.Collections;

public class KillAllMonstersOnGenericBossDeath : MonoBehaviour
{

	void Update()
	{

		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("dieWithGenericBoss");
		HealthScript healthScript = null;
		foreach (GameObject go in gos)
		{
			healthScript = null;
			healthScript = go.GetComponent<HealthScript>();
			if (healthScript)
			{
				healthScript.health = 0;
			}
		}
		this.enabled = false;

		GameObject[] gos2;
		gos2 = GameObject.FindGameObjectsWithTag("monster");
		foreach (GameObject go in gos2)
		{
			healthScript = null;
			healthScript = go.GetComponent<HealthScript>();
			if (healthScript)
			{
				healthScript.health = 0;
			}
		}
		this.enabled = false;
	}
}
