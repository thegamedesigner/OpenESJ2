using UnityEngine;
using System.Collections;

public class InfLoveTriggerChanges : MonoBehaviour
{
	// Set in the editor on TriggerZoneStart objects.
	public Material newSkyMat;
	public GameObject[] needDisableGameObjects = null;
	
	// Update is called once per frame
	void Update ()
	{
		if(enabled)
		{
			//Sky
			if(newSkyMat != null)
				GameObject.Find("Sky").GetComponent<Renderer>().material = newSkyMat;
			
			//Enable these (THEY GET DISABLED IN TeleporterScript)
			if(needDisableGameObjects != null)
			{
				for(int i = 0; i < needDisableGameObjects.Length; i++)
				{
					needDisableGameObjects[i].SetActive(true);
				}
				xa.enabledInfLoveGameObjects = needDisableGameObjects;
				xa.onScreenObjectsDirty = true;
			}
			enabled = false;
		}
	}
}
