using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTweenEveryXSecondsScript : MonoBehaviour {
	
	public float triggerTime = 0.33f;
	float counter = 0f;
	public string tweenName = "";
	
	void Update(){
		counter -= Time.deltaTime;
		if(counter <= 0f){
			iTweenEvent.GetEvent(gameObject, tweenName).Play();
			counter = triggerTime;
		}
	}
}
