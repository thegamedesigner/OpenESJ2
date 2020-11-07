using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerScript : MonoBehaviour
{
	public bool onlyIfTriggered = false;
	public Info infoScriptToTrigger;
	public GameObject creationExplo;
	public GameObject creationPoint;
	public GameObject[] monsterLineup;
	public float[] delays;
	float timeSet;
	int index;

	void Start()
	{

	}

	void Update()
	{
		if (onlyIfTriggered)
		{
			if(infoScriptToTrigger == null) {return; }
			if(!infoScriptToTrigger.triggered) { return;}
		}

		if (fa.time >= (timeSet + delays[index]))
		{
			if(creationExplo != null) {Instantiate(creationExplo,creationPoint.transform.position,creationPoint.transform.rotation); }
			
			GameObject go = Instantiate(monsterLineup[index],creationPoint.transform.position,creationPoint.transform.rotation);
			
			index++;
			if(index >= monsterLineup.Length) {index = 0; }

			timeSet = fa.time;
		}
	}
}
