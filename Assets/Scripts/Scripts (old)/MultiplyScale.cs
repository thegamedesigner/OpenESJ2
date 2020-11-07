using UnityEngine;
using System.Collections;

public class MultiplyScale : MonoBehaviour
{
	public GameObject go = null;
	public Vector3 multiplier = Vector3.zero;
	void Update()
	{
		if (this.enabled)
		{
			xa.glx = go.transform.localScale;
			xa.glx.x *= multiplier.x;
			xa.glx.y *= multiplier.y;
			xa.glx.z *= multiplier.z;
			go.transform.localScale = xa.glx;
			this.enabled = false;
		}
	}
}
