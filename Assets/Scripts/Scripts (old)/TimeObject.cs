using UnityEngine;
using System.Collections;

public class TimeObject : MonoBehaviour
{

	void Start()
	{
		if (!xa.timeObject)
		{
			DontDestroyOnLoad(this.gameObject);
			xa.timeObject = this.gameObject;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void Update()
	{
		if (xa.showTimer && !fa.isMenuLevel)
		{
			transform.LocalSetX(0);
		}
		else
		{
			transform.LocalSetX(-300);
		}
	}
}
