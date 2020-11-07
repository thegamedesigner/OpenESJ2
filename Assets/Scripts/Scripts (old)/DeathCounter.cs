using UnityEngine;
using System.Collections;

public class DeathCounter : MonoBehaviour
{

	void Start()
	{
		if (!xa.deathCounter)
		{
			DontDestroyOnLoad(this.gameObject);
			xa.deathCounter = this.gameObject;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void Update()
	{
		if (xa.showDeathCounter && transform.localPosition.y < -200)
		{
			xa.glx = transform.localPosition;
			xa.glx.y = 0;
			transform.localPosition = xa.glx;
		}
		if (!xa.showDeathCounter && transform.localPosition.y > -200)
		{
			xa.glx = transform.localPosition;
			xa.glx.y = -300;
			transform.localPosition = xa.glx;
		}
	}
}
