using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlantByDistanceScript : MonoBehaviour {
		
	public enum FlowerType{
		BigFlower,
		SmallFlower,
        GardenTree,
	}
	public FlowerType flower;
	
	public float distance = 5f;
	PlantScript ps = null;
	Info inf = null;
	List<GameObject> childObjs = new List<GameObject>();
	bool didTask = false;

	
	
	void Awake(){
		if(flower == FlowerType.BigFlower){
			ps = GetComponent<PlantScript>();
			ps.enabled = false;
			
			foreach (Transform child in gameObject.transform) {
				childObjs.Add(child.gameObject);
			}
			
			for(int i = 0; i < childObjs.Count; i++){
				childObjs[i].SetActive(false);
			}
		} else if(flower == FlowerType.SmallFlower) {
			inf = GetComponent<Info>();
			inf.triggered = false;
		} else if(flower == FlowerType.GardenTree) {
            foreach (Transform child in gameObject.transform) {
				childObjs.Add(child.gameObject);
			}
			
			for(int i = 0; i < childObjs.Count; i++){
				childObjs[i].SetActive(false);
			}
        }
	}

	void Update () {
		
		Vector2 playerPos = new Vector2(xa.player.transform.position.x, xa.player.transform.position.y);
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		float d = Vector2.Distance(pos, playerPos);
				
		if(flower == FlowerType.BigFlower){
			if(xa.player && !didTask){
				if(d <= distance){
					for(int i = 0; i < childObjs.Count; i++){
						childObjs[i].SetActive(true);
					}
					ps.enabled = true;
					didTask = true;
				}
			}
		} else if( flower == FlowerType.SmallFlower){
			if(d <= distance && !didTask){
				inf.triggered = true;
			}
		} else if(flower == FlowerType.GardenTree) {
            if(d <= distance){
				for(int i = 0; i < childObjs.Count; i++){
					childObjs[i].SetActive(true);
				}
				didTask = true;
			}
        }
	}
}
