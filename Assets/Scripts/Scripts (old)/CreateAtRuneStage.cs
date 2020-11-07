using UnityEngine;
using System.Collections;

public class CreateAtRuneStage : MonoBehaviour
{
	public GameObject go = null;
	public int numOfRunesCollected = 0;

	void Update()
	{
		if (this.enabled)
		{
			if (xa.runesCollected == numOfRunesCollected)
			{
				Instantiate(go, transform.position, xa.null_quat);
				this.enabled = false;
			}
		}
	}
}
