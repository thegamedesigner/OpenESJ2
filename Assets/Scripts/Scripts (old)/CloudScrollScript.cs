using UnityEngine;
using System.Collections;

public class CloudScrollScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-fa.deltaTime,0,0, Space.Self);
	}
}
