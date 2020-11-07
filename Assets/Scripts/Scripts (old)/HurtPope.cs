using UnityEngine;
using System.Collections;

public class HurtPope : MonoBehaviour
{

	public float damage = 0;

	void Start()
	{
		xa.popeHealth -= damage;
	
	}

}
