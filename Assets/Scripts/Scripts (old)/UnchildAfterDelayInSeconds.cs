using UnityEngine;
using System.Collections;

public class UnchildAfterDelayInSeconds : MonoBehaviour
{
	public float delayInSeconds = 0;
	float counter = 0;
	// Use this for initialization
	void Start()
	{

		counter = fa.time;
	}

	// Update is called once per frame
	void Update()
	{
		if ((counter + delayInSeconds) < fa.time)
		{
			transform.parent = null;
			this.enabled = false;
		}
	}
}
