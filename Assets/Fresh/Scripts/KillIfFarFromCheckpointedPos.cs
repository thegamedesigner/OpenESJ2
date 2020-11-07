using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillIfFarFromCheckpointedPos : MonoBehaviour
{
	public float dist = 3;
	public bool killOnNotCheckpointed = false;

	void Update()
	{
		if (xa.hasCheckpointed)
		{
			if (Vector2.Distance(xa.lastSpawnPoint, transform.position) > dist)
			{
				Destroy(this.gameObject);
			}
			else
			{
				this.gameObject.tag = "Mom";
				this.enabled = false;
			}
		}
		else
		{
			if (killOnNotCheckpointed)
			{
				Destroy(this.gameObject);
			}
		}

	}
}
