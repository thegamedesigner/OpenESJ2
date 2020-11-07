using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealPinwheelsAtTimeScript : MonoBehaviour {
	
	float timeInSong = 0f;
	public float timeToAppear = 38.9f;
	bool scaledThisInstance = false;
	List<GameObject> pinwheels = new List<GameObject>();
	float bandaidDelay = 0.5f;
	
	void Start(){
		foreach (Transform child in gameObject.transform) {
			pinwheels.Add(child.gameObject);
		}
		
		for(int i = 0; i < pinwheels.Count; i++){
			pinwheels[i].transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		}
	}
	
	void Update () {
		if(bandaidDelay <= 0f){
			if(xa.music_Time < timeToAppear){
				for(int i = 0; i < pinwheels.Count; i++){
					pinwheels[i].transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
				}
			} else {
				for(int i = 0; i < pinwheels.Count; i++){
					iTween.ScaleTo(pinwheels[i], Vector3.one, 0.5f);
				}
				scaledThisInstance = true;
			}
		} else {
			bandaidDelay -= Time.deltaTime;
		}
	}
}
