using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTextOnDelayScript : MonoBehaviour {

	public float delay = 0f;
	TextScript ts = null;
	GameObject triggerObj = null;
	bool triggered = false;
	float plBoxHeight, plBoxWidth;
	
	void Awake(){
		ts = GetComponent<TextScript>();
		ts.enabled = false;
		triggerObj = ts.triggerGO;
	}
	
	void Update () {
		//Does the player exist?
		if(xa.player){
			if(!triggered){
				//Get the width and height of the player.
				plBoxHeight = xa.playerBoxHeight;
				plBoxWidth = xa.playerBoxWidth;
				
				//Checking if player is in the zone and touching the ground.
				triggered =  ((triggerObj.transform.position.x + (triggerObj.transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
					(triggerObj.transform.position.x - (triggerObj.transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
					(triggerObj.transform.position.y + (triggerObj.transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
					(triggerObj.transform.position.y - (triggerObj.transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)));
			} else {
				if(delay <= 0){
					ts.enabled = true;
				} else {
					delay -= fa.deltaTime;
				}
			}
		}
	}
}
