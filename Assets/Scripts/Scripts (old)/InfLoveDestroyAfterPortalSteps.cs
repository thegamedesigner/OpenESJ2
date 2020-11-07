using UnityEngine;
using System.Collections;

public class InfLoveDestroyAfterPortalSteps : MonoBehaviour
{
	public int maxPortals = -1;
	
	// Update is called once per frame
	void Update ()
	{
		if(xa.portalStep > maxPortals && maxPortals != -1)
			Destroy (this.gameObject);
	
	}
}
