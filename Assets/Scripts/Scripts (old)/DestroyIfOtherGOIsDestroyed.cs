using UnityEngine;
using System.Collections;

public class DestroyIfOtherGOIsDestroyed : MonoBehaviour
{
	public GameObject go = null;
	void Update()
	{
		if (go == null)
		{
			Destroy(this.gameObject);
		}
	}
}
