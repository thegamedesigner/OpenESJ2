using UnityEngine;
using System.Collections;

public class NessieBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(fa.deltaTime, Mathf.Sin(fa.deltaTime)*0.1f, 0);
	}
}
