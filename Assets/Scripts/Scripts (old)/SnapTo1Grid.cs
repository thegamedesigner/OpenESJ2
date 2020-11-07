using UnityEngine;
using System.Collections;

public class SnapTo1Grid : MonoBehaviour
{

	void Start()
	{
		xa.glx = transform.position;
		xa.glx.x = Mathf.RoundToInt(xa.glx.x);
		xa.glx.y = Mathf.RoundToInt(xa.glx.y);
		transform.position = xa.glx;
		this.enabled = false;
	}

}
