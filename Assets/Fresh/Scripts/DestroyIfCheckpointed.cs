using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfCheckpointed : MonoBehaviour
{
	public bool destroyIfNotCheckpointed = false;

	void Start()
	{
		if (xa.hasCheckpointed && !destroyIfNotCheckpointed)
		{
			Destroy(this.gameObject);
		}
		if (!xa.hasCheckpointed && destroyIfNotCheckpointed)
		{
			Destroy(this.gameObject);
		}
	}

	void Update()
	{

	}
}
