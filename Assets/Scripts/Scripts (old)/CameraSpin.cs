using UnityEngine;
using System.Collections;

public class CameraSpin : MonoBehaviour {

	public new GameObject camera; // warning CS0108: `CameraSpin.camera' hides inherited member `UnityEngine.Component.camera'. Use the new keyword if hiding was intended
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled)
		{
			iTweenEvent tween = camera.GetComponent<iTweenEvent>();
			tween.enabled = true;
			tween.Play();
			Destroy(this);
		}
	}
}
