using UnityEngine;
using System.Collections;

public class InfLoveKillOnPortalling : MonoBehaviour
{
	int savedPortalStep = 0;

	void Start()
	{
		savedPortalStep = xa.portalStep;
	}
	void Update()
	{
		if (xa.portalStep > savedPortalStep)
		{
			Destroy(this.gameObject);
		}
	
	}
}
