using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumScript : MonoBehaviour {

	GameObject plumTriggerOne = null;
	GameObject plumTriggerTwo = null;
	GameObject plum = null;
	
	LineRenderer line = null;
	
	float plBoxHeight, plBoxWidth;
	
	bool inMe1, inMe2, triggered;
	
	
	void Awake() {
		plumTriggerOne = transform.Find("Trigger One").gameObject;
		plumTriggerTwo = transform.Find("Trigger Two").gameObject;
		plum = transform.Find("Plum").gameObject;
		
		GameObject lineObj = transform.Find("Vine").gameObject;
		line = lineObj.GetComponent<LineRenderer>();
	}
	
	void Update () {
		//Does the player exist?
		if(xa.player){
			//Get the width and height of the player.
			plBoxHeight = xa.playerBoxHeight;
			plBoxWidth = xa.playerBoxWidth;
			
			//Checking if player is in the zone and touching the ground.
			inMe1 =  ((plumTriggerOne.transform.position.x + (plumTriggerOne.transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
				(plumTriggerOne.transform.position.x - (plumTriggerOne.transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
				(plumTriggerOne.transform.position.y + (plumTriggerOne.transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
				(plumTriggerOne.transform.position.y - (plumTriggerOne.transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)));
			inMe2 =  ((plumTriggerTwo.transform.position.x + (plumTriggerTwo.transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
				(plumTriggerTwo.transform.position.x - (plumTriggerTwo.transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
				(plumTriggerTwo.transform.position.y + (plumTriggerTwo.transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
				(plumTriggerTwo.transform.position.y - (plumTriggerTwo.transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)));
			//Debug.Log("In me: " + inMe.ToString());
			
			if((inMe1 || inMe2) && !triggered){
				line.enabled = false;
				triggered = true;
			}
			
			if(triggered){
				Vector3 pos = transform.position;
				pos.y -= fa.deltaTime*8f;
				transform.position = pos;
				
				if(pos.y < Camera.main.gameObject.transform.position.y - 12){
					Destroy(gameObject);
				}
			}
		}
	}
}
