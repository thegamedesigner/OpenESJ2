using UnityEngine;
using System.Collections;

public class DestroyWhenPlayerIsPastX : MonoBehaviour
{
	public GameObject GO1 = null;
	public float useThisX = 0;

	void Start()
	{
	}

	void Update()
	{
		if(xa.player)
		{
			if(xa.player.transform.position.x > useThisX)
			{
				Destroy(GO1);
			}
		}
	
	}
}
