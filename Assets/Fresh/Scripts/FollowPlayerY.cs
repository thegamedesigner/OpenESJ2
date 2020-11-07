using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerY : MonoBehaviour
{
	public float minY = -999;
	public float maxY = 999;
	public float speedUp = 5;
	public float speedDown = 5;
	void Start()
	{

	}

	void Update()
	{
		if (xa.player)
		{
			if (xa.player.transform.position.y > transform.position.y)
			{
				transform.AddY(speedUp * fa.deltaTime);
				if (xa.player.transform.position.y < transform.position.y) {transform.SetY(xa.player.transform.position.y); }
			}
			if (xa.player.transform.position.y < transform.position.y)
			{
				transform.AddY(-speedDown * fa.deltaTime);
				if (xa.player.transform.position.y > transform.position.y) {transform.SetY(xa.player.transform.position.y); }
			}
			
			if(transform.position.y < minY) {transform.SetY(minY); }
			if(transform.position.y > maxY) {transform.SetY(maxY); }
		}
	}
}
