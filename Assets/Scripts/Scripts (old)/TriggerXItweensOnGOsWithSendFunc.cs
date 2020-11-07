using UnityEngine;
using System.Collections;

public class TriggerXItweensOnGOsWithSendFunc : MonoBehaviour
{
	public GameObject[] go = new GameObject[0];
	public string[] itweenName = new string[0];

	public void triggerItweenX(string input)
	{
	   // input.to
		int i = 0;
		i = int.Parse(input);
		//needs to be an object that I cast as a string
		 iTweenEvent.GetEvent(go[i], itweenName[i]).Play();
	}
}
