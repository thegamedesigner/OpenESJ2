using UnityEngine;
using System.Collections;

public class InfLoveDestroyBeforePortalSteps : MonoBehaviour
{

	public int minPortals = 0;
	
	// Update is called once per frame
	void Update ()
	{
		if(xa.portalStep <= minPortals)
			Destroy (this.gameObject);
	
	}
}
