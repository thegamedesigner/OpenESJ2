using UnityEngine;
using System.Collections;

public class PlayerPuppetScript : MonoBehaviour
{
	public GameObject leg1;
	public GameObject leg2;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (xa.playerDir == -1)
		{
			xa.glx = transform.localEulerAngles;
			xa.glx.y = 0;
			transform.localEulerAngles = xa.glx;
		}
		if (xa.playerDir == 1)
		{
			xa.glx = transform.localEulerAngles;
			xa.glx.y = 180;
			transform.localEulerAngles = xa.glx;
		}

	}
}
