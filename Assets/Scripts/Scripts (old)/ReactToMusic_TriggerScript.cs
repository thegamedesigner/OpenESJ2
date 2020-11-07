using UnityEngine;
using System.Collections;

public class ReactToMusic_TriggerScript : MonoBehaviour
{
	public float[] times = new float[45];
	float[] timesHit = new float[45];
	public bool[] addToPrevious = new bool[45];
	public bool useITweens = true;
	public int numOfItweens = 0;
	public bool rotateOnBeat = false;
	public float rotateAmount = 0;

	bool on = false;
	int index = 0;

	void Start()
	{
		index = 0;
		while (index < times.Length)
		{
			if (addToPrevious[index])
			{
				times[index] = times[index - 1] + times[index];
			}
			index++;
		}
	}

	void Update()
	{
		on = false;

		index = 0;
		while (index < times.Length)
		{
			if (xa.music_Time > times[index] && times[index] != 0 && timesHit[index] == 0) { on = true; timesHit[index] = 1; }

			if (xa.music_Time < 0.2) { timesHit[index] = 0; }//reset

			index++;
		}
	

			if (on)
			{
				if (useITweens)
				{
					//trigger iTweens
					index = 1;
					while (index <= numOfItweens)
					{
						iTweenEvent.GetEvent(this.gameObject, "trigger" + index).Play();
						index++;
					}
				}

				if (rotateOnBeat)
				{
					xa.glx = transform.localEulerAngles;
					xa.glx.z += rotateAmount;
					transform.localEulerAngles = xa.glx;
				}
			}

	}
}
