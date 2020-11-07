using UnityEngine;
using System.Collections;

public class DestroyGOOnEnabled : MonoBehaviour
{
	public GameObject GO = null;
	public bool ifNullIgnore = false;
	void Update()
	{
		if (!ifNullIgnore || GO)
		{
			//Setup.GC_DebugLog("Doing stuff!");
			if (!GO) { GO = this.gameObject; }
			Destroy(GO);
			this.enabled = false;
		}
	}
}
