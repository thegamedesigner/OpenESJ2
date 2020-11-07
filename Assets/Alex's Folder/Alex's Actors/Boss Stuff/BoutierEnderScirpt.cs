using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutierEnderScirpt : MonoBehaviour {
	
	public GameObject face = null;
	public GameObject portal = null;
	GameObject aliveBatch, deadBatch;
	
	float smoothness = 3f;
	float waitToReavealFaceTime = 1.5f;
	float waitToRevealPortalTime = 3f;
	
	bool cutBalloon = false;
	
	Color faceColor = Color.clear;
	
	void Awake(){
		aliveBatch = transform.Find("Alive Text Batch").gameObject;
		deadBatch = transform.Find("Dead Batch").gameObject;
		portal.SetActive(false);
	}
	

	void Update () {
		if(!cutBalloon){
			faceColor = Color.Lerp(face.GetComponent<MeshRenderer>().material.color, Color.clear, smoothness * fa.deltaTime);
			if(xa.player){
				NovaPlayerScript nps = xa.player.GetComponent<NovaPlayerScript>();
				Vector3 pPos = xa.player.transform.position;
				if(pPos.y > 339.5f && pPos.y < 341.5f && nps.state == NovaPlayerScript.State.SwordState){
					cutBalloon = true;
				}
			}
		} else {
			if(waitToReavealFaceTime > 0){
				waitToReavealFaceTime -= fa.deltaTime;
			} else {
				faceColor = Color.Lerp(face.GetComponent<MeshRenderer>().material.color, Color.white, smoothness * fa.deltaTime);
			}
			
			if(waitToRevealPortalTime > 0){
				waitToRevealPortalTime -= fa.deltaTime;
			} else {
				portal.SetActive(true);
			}
		}
		
		aliveBatch.SetActive(!cutBalloon);
		deadBatch.SetActive(cutBalloon);

		face.GetComponent<MeshRenderer>().material.color = faceColor;
	}
}
