using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	
	GameObject mainCamera;
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled)
		{
			mainCamera.GetComponent<CameraScript>().cameraFollowsPlayer = true;
			mainCamera.GetComponent<CameraScript>().cameraFollowsPlayerY = true;
			Destroy(this);
		}
	}
}
