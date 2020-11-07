using UnityEngine;
using System.Collections;

public class SendMsgToGO : MonoBehaviour
{
	public GameObject[] go = null;
	public string[] msg;

	void Start()
	{
		int index = 0;
		while(index < go.Length)
		{
			if (go[index])
			{
				go[index].SendMessage(msg[index]);
			}
			index++;
		}
	}
}
