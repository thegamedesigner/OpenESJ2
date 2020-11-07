using UnityEngine;
using System.Collections;

public class DestroyIfNotAttractMode : MonoBehaviour
{

	void Update()
	{

		if (!xa.attractModeVersion)
		{
			Destroy(this.gameObject);
		}
	}

}
