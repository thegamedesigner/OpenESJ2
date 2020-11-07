using UnityEngine;
using System.Collections;

public class TriggerItweenOnGOWithSendFunc : MonoBehaviour
{
	public GameObject go;
	public GameObject go2;
	public string itweenName = "";
	public string itweenName2 = "";

	public void triggerItween()
	{
		iTweenEvent.GetEvent(go, itweenName).Play();
	}

	public void triggerItween2()
	{
		iTweenEvent.GetEvent(go2, itweenName2).Play();
	}
}
