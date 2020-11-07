using UnityEngine;
using System.Collections;

public class SendMessageScript : MonoBehaviour
{
	public GameObject sendMsgGO = null;
	public string msg = "";
	public bool useParameters = false;
	public string parameters;
	void Update()
	{
		if (this.enabled)
		{
			if (useParameters)
			{
				sendMsgGO.SendMessage(msg,(object)(parameters));
			}
			else
			{
				sendMsgGO.SendMessage(msg);
			}
			this.enabled = false;
		}
	}
}
