using UnityEngine;
using System.Collections;

public class SendMessagesScript : MonoBehaviour
{
	public GameObject[] sendMsgGOs = new GameObject[0];
	public string[] messages = new string[0];
	public bool[] useParameters = new bool[0];
	public string[] parameters = new string[0];
	int index = 0;
	void Update()
	{
		if (this.enabled)
		{
			index = 0;
			while(index < sendMsgGOs.Length)
			{
				if (index < useParameters.Length)
				{
					if (useParameters[index])
					{
						sendMsgGOs[index].SendMessage(messages[index], (object)(parameters));
					}
				}
				else
				{
					sendMsgGOs[index].SendMessage(messages[index]);
				}
				index++;
			}
			this.enabled = false;
		}
	}
}
