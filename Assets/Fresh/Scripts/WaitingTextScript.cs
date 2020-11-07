using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingTextScript : MonoBehaviour
{
	public Text text;
	float timeset;
	float delay = 0.3f;
	int index = 0;

	void Start()
	{
		timeset = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time > (timeset + delay))
		{
			timeset = Time.time;
			text.text = "Waiting";
			for (int i = 0; i < index; i++)
			{
				text.text += ".";
			}
			index++;
			if(index > 3) {index = 0; }
		}
	}
}
