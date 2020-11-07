using UnityEngine;
using System.Collections;

public class OverlayCameraScript : MonoBehaviour
{

	void Start()
	{
		if (!xa.overlayCamera)
		{
			DontDestroyOnLoad(this.gameObject);
			xa.overlayCamera = this.gameObject;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

}
