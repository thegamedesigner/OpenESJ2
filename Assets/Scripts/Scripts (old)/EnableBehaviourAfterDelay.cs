using UnityEngine;
using System.Collections;

public class EnableBehaviourAfterDelay : MonoBehaviour
{
	public Behaviour behaviour = null;
	public float delayInSeconds = 0;
	float timeSave = 0;

	void Start()
	{
		timeSave = fa.time;
	}

	void Update()
	{
		if (fa.time >= (timeSave + delayInSeconds))
		{
			enableBehaviour();
		}
	}

	public void enableBehaviour()
	{
		behaviour.enabled = true;
	}
}
