using UnityEngine;
using System.Collections;

public class TriggerItweenOnGO : MonoBehaviour
{
	public GameObject go;
	public string itweenName = "";

	void Update()
	{
		if (this.enabled)
		{
			//Setup.GC_DebugLog("Called itween: " + itweenName);
			iTweenEvent.GetEvent(go, itweenName).Play();
			this.enabled = false;
		}
	}

}
