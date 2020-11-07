using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKeyScript : MonoBehaviour {
	
	GameObject puppet, doorHinge, eruptingSmoke, screenFillingSmoke, portal;
	float minDist = 1.75f;
	float levelTransitionTimer = 5f;
	bool triggered = false;
	string scaleTweenName = "scale", colorTweenName = "color", hingeTweenName = "hinge";
	
	void Awake(){
		puppet = transform.Find("puppet").gameObject;
		doorHinge = transform.Find("Door Hinge").gameObject;
		eruptingSmoke = transform.Find("Erupting Smoke").gameObject;
		screenFillingSmoke = transform.Find("Screen Filling Smoke").gameObject;
		portal = transform.Find("Portal").gameObject;
		
	}
	
	void Update(){
		if(xa.player){
			Vector2 keyPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
			Vector2 playerPos = new Vector2(xa.player.transform.position.x, xa.player.transform.position.y);
			
			float dist = Vector2.Distance(keyPos, playerPos);
			
			if(dist <= minDist && !triggered){
				
				iTweenEvent.GetEvent(puppet, scaleTweenName).Play();
				iTweenEvent.GetEvent(puppet, colorTweenName).Play();
				iTweenEvent.GetEvent(doorHinge, hingeTweenName).Play();
				
				eruptingSmoke.GetComponent<ParticleSystem>().Play();
				screenFillingSmoke.GetComponent<ParticleSystem>().Play();
				
				Vector3 screenSmokePos = screenFillingSmoke.transform.position;
				screenSmokePos.z = 10;
				screenFillingSmoke.transform.position = screenSmokePos;
				
				triggered = true;
			}
			
			if(triggered){
				levelTransitionTimer -= fa.deltaTime;
				if(levelTransitionTimer <= 0){
					portal.transform.position = xa.player.transform.position;
				}
			}
		}
	}
}
