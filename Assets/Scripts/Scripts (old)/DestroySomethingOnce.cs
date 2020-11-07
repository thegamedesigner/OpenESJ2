using UnityEngine;
using System.Collections;

public class DestroySomethingOnce : MonoBehaviour
{
	public GameObject something = null;

	void Update()
	{
		Destroy(something);
		this.enabled = false;
	}

}
