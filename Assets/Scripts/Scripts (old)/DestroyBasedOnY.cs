using UnityEngine;
using System.Collections;

public class DestroyBasedOnY : MonoBehaviour
{
	public float YAmount = 0;
	public bool destroyIfAboveY = false;
	public bool destroyIfBelowY = false;

	void Update()
	{
		if (transform.position.y < YAmount && destroyIfBelowY) { Destroy(this.gameObject); }
		if (transform.position.y > YAmount && destroyIfAboveY) { Destroy(this.gameObject); }
	}
}
