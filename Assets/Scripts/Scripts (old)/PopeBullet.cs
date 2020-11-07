using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopeBullet : MonoBehaviour
{
	public float damage = 0;
	public GameObject deathExplo = null;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//hit pope
		if (xa.pope)
		{
		   // if (transform.position.x > xa.pope.collider.bounds.min.x &&
			//	transform.position.x < xa.pope.collider.bounds.max.x &&
			//	transform.position.y > xa.pope.collider.bounds.min.y &&
			//	transform.position.y < xa.pope.collider.bounds.max.y)
			xa.glx = transform.position;
			xa.glx2 = xa.pope.transform.position;
			xa.glx2.z = xa.glx.z;
			if(Vector3.Distance(xa.glx,xa.glx2) < 3.32f)
			{
				xa.popeHealth -= damage;
				xa.glx = transform.position;
				xa.tempobj = (GameObject)(Instantiate(deathExplo, xa.glx, xa.null_quat));
				if (xa.createdObjects) { xa.tempobj.transform.parent = xa.createdObjects.transform; }
				Destroy(this.gameObject);
				this.enabled = false;
				return;
			}
		}

		//GameObject[] gos;
		//gos = GameObject.FindGameObjectsWithTag("solidThing");
		//List<GameObject> gos = xa.onScreenSolid;
		GameObject[] gos = xa.onScreenSolid;
		foreach (GameObject go in gos)
		{
			if (transform.position.x > go.GetComponent<Collider>().bounds.min.x &&
				transform.position.x < go.GetComponent<Collider>().bounds.max.x &&
				transform.position.y > go.GetComponent<Collider>().bounds.min.y &&
				transform.position.y < go.GetComponent<Collider>().bounds.max.y)
			{
				xa.glx = transform.position;
				xa.tempobj = (GameObject)(Instantiate(deathExplo, xa.glx, xa.null_quat));
				if (xa.createdObjects) { xa.tempobj.transform.parent = xa.createdObjects.transform; }
				Destroy(this.gameObject);
				this.enabled = false;
				return;
			}
		}
	}
}
