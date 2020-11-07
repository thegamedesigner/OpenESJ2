using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrthagraphicSizeTriggerScript : MonoBehaviour {

	public float newSize = 5f, changeSpeed = 3f;

	bool isAbove = false;
	public bool snap = false;
	
	Camera mainCam = null;
	

	void Start(){
        mainCam = Camera.main;
        float startOrth = mainCam.orthographicSize; 
		if(!snap){
			if(startOrth < newSize){
				isAbove = true;
			} else {
				isAbove = false;
			}
        } else { 
			CleanUp();
		}
	}
	
	
	
	void Update () {
		if(!snap){
			if(isAbove){
				mainCam.orthographicSize += Time.deltaTime*changeSpeed;
				if(mainCam.orthographicSize >= newSize){
					CleanUp();
				}
			} else {
				mainCam.orthographicSize -= Time.deltaTime*changeSpeed;
				if(mainCam.orthographicSize <= newSize){
					CleanUp();
				}
			}
		}
	}
	
	void CleanUp(){
		mainCam.orthographicSize = newSize;
		Destroy(gameObject);
	}
}
