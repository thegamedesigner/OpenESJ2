using UnityEngine;
using System.Collections;

public class DestroyWhenFallingTooLow : MonoBehaviour
{
	public float YAmount = 0;

	void Update()
	{
		if (transform.position.y < YAmount)
		{
			Destroy(this.gameObject);
		}
	}
}
