using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateUsingMatsScript : MonoBehaviour
{
	public bool start = false;
	public bool loop = false;
	public Renderer myRenderer;
	public Material[] mats;
	public float[] times;

	float delay = 1;
	float timeset;
	public int index = 0;

	void Start()
	{

	}

	void Update()
	{
		if (start)
		{
			if (fa.time > (timeset + delay))
			{
				timeset = fa.time;
				delay = times[index];
				myRenderer.material = mats[index];

				index++;

				if (index >= times.Length)
				{
					if (loop)
					{
						index = 0;
						delay = times[index];

					}
					else
					{
						start = false;
					}
				}
			}
		}
	}

	public void Play()
	{
		start = true;
		delay = times[index];
		myRenderer.material = mats[index];
	}

}
