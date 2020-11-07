using UnityEngine;
using System.Collections;

public class TriggerItweensOnGOs : MonoBehaviour
{
	public GameObject[] gos;
	public string[] names;

	int index = 0;
	void Update()
	{
		if (this.enabled)
		{
			index = 0;
			while (index < gos.Length)
			{
				iTweenEvent.GetEvent(gos[index], names[index]).Play();
				index++;
			}

			this.enabled = false;
		}
	}
}
