using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutierManagerScript : MonoBehaviour {
	
	GameObject boutier = null;
	GameObject cameraObj = null;
	GameObject sideBalloonBro = null;
	GameObject ballonBroTextHolder = null;
	
	[HideInInspector]
	public List<GameObject> balloonTexts = new List<GameObject>();
	
	Vector3 boutierPos = Vector3.zero;
	Vector3 balloonBroTargetPos = Vector3.zero;
	
	void Awake(){
		boutier = transform.Find("Boutier").gameObject;
		cameraObj = Camera.main.gameObject;
		sideBalloonBro = transform.Find("Side Hanging Balloon Bro").gameObject;
		ballonBroTextHolder = transform.Find("Balloon Bro Text Holder").gameObject;
	}
	
	void Start(){
		foreach (Transform child in ballonBroTextHolder.transform){
			balloonTexts.Add(child.gameObject);
		}
	}
	
	void Update () {
		//Manage position of the smoke monster.
		boutierPos = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, 12);
		boutier.transform.position = boutierPos;
		
		//Manage the position of the side balloon bro.
		Vector3 camPos = cameraObj.transform.position;
		if(camPos.y > 17 && camPos.y < 258){
			balloonBroTargetPos.x = 15.5f;
		} else {
			balloonBroTargetPos.x = 20f;
		}
		balloonBroTargetPos.y = 198;
		balloonBroTargetPos.z = 50;
		float smooth = 3f;
		sideBalloonBro.transform.position = Vector3.Lerp(sideBalloonBro.transform.position, balloonBroTargetPos, smooth*Time.deltaTime);
		
		//Manage position of each balloon guy texts.
		Vector3 balloonTextPos = new Vector3(12, camPos.y + 4, 28);
		for(int i = 0; i < balloonTexts.Count; i++){
			balloonTexts[i].transform.position = balloonTextPos;
		}
	}
}
