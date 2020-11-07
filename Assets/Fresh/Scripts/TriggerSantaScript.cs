using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSantaScript : MonoBehaviour
{
	public SantaStuckScript santaStuckScript;
	public SantaHoveringScript santaScript;

	void Start()
	{
		if (santaScript != null) { santaScript.attacking = true; }
		if (santaStuckScript != null) { santaStuckScript.attacking = true; }
	}

	void Update()
	{

	}
}
