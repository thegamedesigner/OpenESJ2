using UnityEngine;
using System.Collections;

public class DestroyBlocksInCollider : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("solidThing");
	
		float left = GetComponent<Collider>().bounds.center.x - GetComponent<Collider>().bounds.size.x*0.5f;
		float right = GetComponent<Collider>().bounds.center.x + GetComponent<Collider>().bounds.size.x*0.5f;
		float top = GetComponent<Collider>().bounds.center.y - GetComponent<Collider>().bounds.size.y*0.5f;
		float bottom = GetComponent<Collider>().bounds.center.y + GetComponent<Collider>().bounds.size.y*0.5f;
	
		float hw;
		float hh;
		foreach (GameObject go in gos) {
			hw = go.GetComponent<Collider>().bounds.size.x*0.5f;
			hh = go.GetComponent<Collider>().bounds.size.y*0.5f;
	
			if (!(left > go.GetComponent<Collider>().bounds.center.x + hw ||
				right < go.GetComponent<Collider>().bounds.center.x - hw ||
				top > go.GetComponent<Collider>().bounds.center.y + hh ||
				bottom < go.GetComponent<Collider>().bounds.center.y - hh)) {
	
				Destroy(go);
			}
	
		}
	}
}
