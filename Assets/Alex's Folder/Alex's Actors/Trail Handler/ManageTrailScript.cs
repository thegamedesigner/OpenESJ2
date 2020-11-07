using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTrailScript : MonoBehaviour {
	
	public enum HandleType {
		CompletelyDestroy,
		ChangeColor,
	}
	public HandleType howShouldTrailBeChanged = HandleType.CompletelyDestroy;
	
	GameObject trailHolder, fakeTrail, gapTrail, stompTrail, stars;
	
	public Color newColor = Color.white;
	
	public bool hideStars = false;
	
	void Update(){
		//Find needed pieces.
		if(trailHolder == null){ 	trailHolder = xa.player.transform.Find("altTrail").gameObject; return; }
		
		if(howShouldTrailBeChanged != HandleType.CompletelyDestroy){  //We don't need to bother searching for these pieces if we are hiding the trail altogether.
			fakeTrail = trailHolder.transform.Find("fakeTrail").gameObject;
			gapTrail = trailHolder.transform.Find("gapTrail").gameObject;
			stompTrail = trailHolder.transform.Find("stompTrail").gameObject;
			stars = trailHolder.transform.Find("stars").gameObject;
		}
		
		//What to do with found pieces.
		if(howShouldTrailBeChanged == HandleType.CompletelyDestroy){
			trailHolder.SetActive(false);
		} else if (howShouldTrailBeChanged == HandleType.ChangeColor){
			//Find main modules of particle systems.  Don't know the name of them....  good thing "var" exists.   ;)
			var fakePS = fakeTrail.GetComponent<ParticleSystem>().main;
			var gapPS = gapTrail.GetComponent<ParticleSystem>().main;
			var stompPS = stompTrail.GetComponent<ParticleSystem>().main;
			
			//Applying new colors.
			fakePS.startColor = newColor;
			gapPS.startColor = newColor;
			stompPS.startColor = newColor;
		} else {
			//Debug.Log("Wut lol");
		}
		
		//Should we hide the stars?
		if(stars != null){ stars.SetActive(!hideStars); }
	}
}
