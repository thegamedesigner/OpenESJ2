using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInfoScript : MonoBehaviour
{
	public Info[] infoScripts;
	public bool setTo = true;

	void Start()
	{
		for (int i = 0; i < infoScripts.Length; i++)
		{
			if(infoScripts[i] != null) {infoScripts[i].triggered = setTo; }
		}

	}
}
