using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGodScript : MonoBehaviour {
	
	float r = 1f;
	float g = 0f;
	float b = 0f;
	float slowdown = 2f;
	
	public bool ignoreTrail = false;
	
	public enum SubColorMode{
		subR,
		subG,
		subB,
	}
	SubColorMode sMode = SubColorMode.subR;
	
	GameObject trailHandler = null;
	GameObject localNode = null;
	public GameObject skyObject = null;
	
	ManageTrailScript trail = null;
	LocalNodeScript lNode = null;

	void Awake(){
		if(!ignoreTrail){
			trailHandler = transform.Find("Trail Handler").gameObject;
			trail = trailHandler.GetComponent<ManageTrailScript>();
		}
	}
	
	void Update () {
		DoColors();
		if(lNode == null){
			lNode = xa.localNodeScript;
			return;
		}
	}
	
	
	private void DoColors(){
		//Set color limits.
		if(b > 1f){ b = 1f; }
		if(r > 1f){ r = 1f; }
		if(g > 1f){ g = 1f; }
		if(b < 0f){ b = 0f; }
		if(r < 0f){ r = 0f; }
		if(g < 0f){ g = 0f; }
		
		//From r to g.
		if(sMode == SubColorMode.subR){ 
			r -= fa.deltaTime/slowdown;
			g += fa.deltaTime/slowdown;
			if(r <= 0f){ r = 0f; }
			if(g >= 1f){ g = 1f; }
			if(r == 0f && g == 1f){ 
				sMode = SubColorMode.subG;
			}
		}
		//From g to b.
		if(sMode == SubColorMode.subG){
			g -= fa.deltaTime/slowdown;
			b += fa.deltaTime/slowdown;
			if(g <= 0f){ g = 0f; }
			if(b >= 1f){ b = 1f; }
			if(g == 0f && b == 1f){
				sMode = SubColorMode.subB;
			}
		}
		//From b to r.
		if(sMode == SubColorMode.subB){
			b -= fa.deltaTime/slowdown;
			r += fa.deltaTime/slowdown;
			if(b <= 0f){ b = 0f; }
			if(r >= 1f){ r = 1f; }
			if(b == 0f && r == 1f){
				sMode = SubColorMode.subR;
			}
		}
		
		//Apply colors.
		Color newCol = new Color(r, g, b, 1f);
		skyObject.GetComponent<MeshRenderer>().material.color = newCol;
		if(trail != null){ trail.newColor = newCol; }
		if(lNode != null){
			Color darkColor = new Color (newCol.r/2, newCol.g/2, newCol.b/2);
			lNode.lightColour = newCol;
			lNode.darkColour = darkColor;
		}
	}
}