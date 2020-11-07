using UnityEngine;
using System.Collections;

public class DestroyIfAttractMode : MonoBehaviour
{

	void Update()
	{
		if (xa.attractModeVersion)
		{
			Destroy(this.gameObject);
		}
	}
}
