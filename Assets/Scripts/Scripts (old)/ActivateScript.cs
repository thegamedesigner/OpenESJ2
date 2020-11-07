using UnityEngine;
using System.Collections;

public class ActivateScript : MonoBehaviour
{
	public bool enableScripts = false;
	public Behaviour[] scriptsToEnable;

	public bool callFuncInScript = false;
	public string nameOfFunc = "";
	public GameObject gameObjectWithScript = null;

	//add to this script: activating itweens, and creating gameobjects

	//This script is called by certain other scripts, generically (as it were).
	public void activateFunc()
	{
		if (enableScripts)
		{
			foreach (Behaviour co in scriptsToEnable)
			{
				co.enabled = true;
			}
		}

		if (callFuncInScript)
		{
			gameObjectWithScript.SendMessage(nameOfFunc);
		}
	}

}
