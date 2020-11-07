using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEmbersScript : MonoBehaviour {
	
	//1:51.6
	public float timeToAppear = 111.6f;
	bool flamedThisInstance = false;
	List<GameObject> particleObjs = new List<GameObject>();
	float bandaidDelay = 0.5f;
	
	void Start(){
		foreach (Transform child in gameObject.transform) {
			particleObjs.Add(child.gameObject);
		}
		
		for(int i = 0; i < particleObjs.Count; i++){
			particleObjs[i].GetComponent<ParticleSystem>().Stop();
		}
	}
	
	void Update () {
		if(bandaidDelay <= 0f){
			if(!flamedThisInstance){
				if(xa.music_Time < timeToAppear){
					for(int i = 0; i < particleObjs.Count; i++){
						particleObjs[i].GetComponent<ParticleSystem>().Stop();
					}
				} else {
					for(int i = 0; i < particleObjs.Count; i++){
						particleObjs[i].GetComponent<ParticleSystem>().Play();
					}
					flamedThisInstance = true;
				}
			}
			//Debug.Log("xa.music_Time: " + xa.music_Time);
		} else {
			bandaidDelay -= Time.deltaTime;
		}
	}
}
