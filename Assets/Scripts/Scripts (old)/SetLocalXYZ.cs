using UnityEngine;
using System.Collections;

public class SetLocalXYZ : MonoBehaviour
{
	public GameObject go = null;
	public Vector3 setTo = Vector3.zero;
	void Update()
	{
		if (this.enabled)
		{
			go.transform.localPosition = setTo;
			this.enabled = false;
		}
	}
}
