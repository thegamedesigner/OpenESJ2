using UnityEngine;
using System.Collections;

public class DelayScript : MonoBehaviour
{
	public float delay = 0;
	public string msg = "";
	public GameObject go;
	float timeSave = 0;
	// Use this for initialization
	void Start()
	{
		timeSave = fa.time;
	}

	// Update is called once per frame
	void Update()
	{
		if(timeSave + delay <= fa.time)
		{
			go.SendMessage(msg);
			this.enabled = false;
		}
	}
}
