using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableParticleEmissionTriggerScript : MonoBehaviour {

	public GameObject particleSystemObject;
	float  plBoxHeight = 0f, plBoxWidth = 0f;
	bool inMe = false, triggered = false;
	
	// Update is called once per frame
	void Update () {
		//Does the player exist?
		if(xa.player){
			//Get the width and height of the player.
			plBoxHeight = xa.playerBoxHeight;
			plBoxWidth = xa.playerBoxWidth;
			
			//Checking if player is in the zone and touching the ground.
			inMe =  ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
				(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
				(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
				(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)));
			
			//Debug.Log("In me: " + inMe.ToString());
			
			if(inMe && !triggered){
				particleSystemObject.GetComponent<ParticleSystem>().Play();
				triggered = true;
			}
		}
	}
}
