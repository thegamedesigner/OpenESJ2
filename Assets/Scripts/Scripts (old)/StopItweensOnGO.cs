using UnityEngine;
using System.Collections;

public class StopItweensOnGO : MonoBehaviour
{
	public GameObject[] gos = null;
	int index = 0;
	void Update()
	{
		if (this.enabled)
		{
			index = 0;
			while (index < gos.Length)
			{
				iTween.Stop(gos[index]);
				index++;
			}
			this.enabled = false;
		}
	}
}
