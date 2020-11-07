using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyCamPickupScript : MonoBehaviour {

	//1f = 360 degrees;    -1f = -360 degrees;   0.25f = 90 degrees;  0.5f = 180 degrees;   etc.
	[Range(-1f, 1f)]
	public float rotAddition;

	float dissapearTime = 1f;
	
	GameObject puppet, cameraObj;
	
	bool triggered = false;
	
	void Awake(){
		puppet = transform.Find("puppet").gameObject;
		cameraObj = Camera.main.gameObject;
	}
	
	void Update(){
		if(xa.player && !triggered){
		
			//Check if the player is inside me!
			Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
			Vector2 playerPos = new Vector2(xa.player.transform.position.x, xa.player.transform.position.y);			
			float d = Vector2.Distance(myPos, playerPos);
			
			//if the player is inside me, do the rotation stuff.
			if(d <= 1.5f){
				//The pickup dissapear effect.
				iTween.ScaleTo(puppet, new Vector3(4, 4, 1), dissapearTime);
				iTween.ColorTo(puppet, new Color(1f, 1f, 1f, 0f), dissapearTime);
				
				//Rotate the camera.
				iTween.RotateBy(cameraObj, iTween.Hash(
					"z", rotAddition,
					"time", dissapearTime*2f,
					"looptype", iTween.LoopType.none,
					"easetype", iTween.EaseType.easeInOutSine
				));
				triggered = true;
			}
			
		}
	}
}
