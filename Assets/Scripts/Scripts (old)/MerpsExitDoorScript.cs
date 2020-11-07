using UnityEngine;
using System.Collections;

public class MerpsExitDoorScript : MonoBehaviour
{
	public GameObject aniGO = null;
	public Behaviour winTriggerZone = null;
	
	

	void Update()
	{

	}

	public void openDoor()
	{
		aniGO.SendMessage("playAni1");
		winTriggerZone.enabled = true;
	}
}
