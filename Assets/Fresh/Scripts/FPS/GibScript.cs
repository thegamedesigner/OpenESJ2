using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibScript : MonoBehaviour
{
	public GameObject corpse;
	//For the FPS
	float timeSet = 0;
	float delay = 5;
	public bool dontDecay = false;//for satan's head
	void Start()
	{
		timeSet = Time.time;
		float speed = 10;
		GetComponent<Rigidbody>().velocity = Random.onUnitSphere * speed;
	}

	void Update()
	{
		if (Time.time > (timeSet + delay) && !dontDecay)
		{
			Vector3 vec1 = transform.position;
			vec1.y = -1.19f;
			GameObject go = Instantiate(corpse, vec1, corpse.transform.rotation);
			Destroy(this.gameObject);
		}
	}
}
