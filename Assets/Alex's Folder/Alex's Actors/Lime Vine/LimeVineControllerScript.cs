using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimeVineControllerScript : MonoBehaviour {
	
	GameObject vinePointHolder;
	LineRenderer vineLine;
	List <Vector3> vinePoints = new List <Vector3>();
	int zPos = 276;
	[HideInInspector]
	public int currentVinePointIndex = 0;
	[HideInInspector]
	public int maxVinePointIndex = 1;
	float moveSpeed = 10f;
	
	void Awake(){
		vineLine = transform.Find("Vine Line").gameObject.GetComponent<LineRenderer>();
		vinePointHolder = transform.Find("Vine Points").gameObject;
		
		vineLine.useWorldSpace = true;
	}
	
	void Start(){
		//Add vine points.
		foreach(Transform child in vinePointHolder.transform){
			vinePoints.Add(new Vector3(child.position.x, child.position.y, zPos));
		}
		
		//Set amount of line points.
		vineLine.positionCount = vinePoints.Count;
		
		//Preemtively set vine points Z position.
		for(int i = 0; i < vinePoints.Count; i++){
			vineLine.SetPosition(i, new Vector3(0, 0, zPos));
		}
	}
	
	void Update(){
		if(currentVinePointIndex <= maxVinePointIndex){
			//Debug.Log("Current Point is under maximum");
			//Set above line points to the their target position.
			for(int i = 0; i < vinePoints.Count; i++){
				//Debug.Log("Moving points");
				if(i == currentVinePointIndex){
					vineLine.SetPosition(currentVinePointIndex, Vector3.MoveTowards(vineLine.GetPosition(currentVinePointIndex), new Vector3(vinePoints[currentVinePointIndex].x, vinePoints[currentVinePointIndex].y, zPos), moveSpeed * fa.deltaTime));
				} else if( i > currentVinePointIndex){
					vineLine.SetPosition(i, vineLine.GetPosition(currentVinePointIndex));
				}
			}
			
			if(vineLine.GetPosition(currentVinePointIndex) == new Vector3(vinePoints[currentVinePointIndex].x, vinePoints[currentVinePointIndex].y, zPos)){
				currentVinePointIndex++;
			}
		}
	}	
}