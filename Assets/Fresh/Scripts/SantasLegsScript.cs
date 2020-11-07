using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantasLegsScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		float speed = 10;
		GetComponent<Rigidbody>().velocity = Random.onUnitSphere * speed;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
