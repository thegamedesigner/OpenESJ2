using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBackroundScript : MonoBehaviour {
	
	public float riseSpeed = 2f;
	GameObject mainCam = null;
	
	void Start(){
			mainCam = transform.parent.gameObject;
	}
	
	void LateUpdate () {
			Vector3 pos = transform.localPosition;
			pos.y += Time.deltaTime * riseSpeed;
			if(pos.y >= 32){ pos.y = 24; }
			pos.x = 0;
			transform.localPosition = pos;
			transform.position = new Vector3(0, transform.position.y, 280);
	}
}
