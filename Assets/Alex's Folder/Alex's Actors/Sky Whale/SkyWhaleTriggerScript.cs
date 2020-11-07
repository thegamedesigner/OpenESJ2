using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWhaleTriggerScript : MonoBehaviour {

	NovaMovingBlock nmb = null;
	AlexAnimationScript anim = null;
	GameObject puppet, zzz;
	
	void Awake(){
		puppet = transform.Find("puppet").gameObject;
		zzz = transform.Find("ZZZ").gameObject;
		
		nmb = GetComponent<NovaMovingBlock>();
		anim = puppet.GetComponent<AlexAnimationScript>();
		nmb.enabled = false;
		anim.enabled = false;
		
		zzz.GetComponent<ParticleSystem>().Play();
		puppet.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0.8f, 0f);
	}
	
	void Update () {
		if(xa.player && !nmb.enabled){
			Vector3 pos = xa.player.transform.position;
			if(pos.y < -2.5){
				zzz.GetComponent<ParticleSystem>().Stop();
				anim.enabled = true;
				nmb.enabled = true;
			}
		}
	}
}
