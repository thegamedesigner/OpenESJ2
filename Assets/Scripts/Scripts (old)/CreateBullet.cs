using UnityEngine;
using System.Collections;

public class CreateBullet : MonoBehaviour
{
	public GameObject[] bullet;

	void fireBullet()
	{
		foreach (GameObject go in bullet)
		{
			Instantiate(go, transform.position, transform.rotation);
		}
	}


}
