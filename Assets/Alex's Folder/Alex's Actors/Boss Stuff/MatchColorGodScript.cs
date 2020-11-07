using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchColorGodScript : MonoBehaviour {

	public bool useLightColour = false;
	public bool useDarkColour  = false;
	public bool useCustomAlpha = false;
	
	public float customAlphaValue = 0f;
	
	private void Update()
	{
		Color newCol = Color.white;
		
		if (useLightColour) {
			newCol = xa.localNodeScript.lightColour;
		} else if (useDarkColour) {
			newCol = xa.localNodeScript.darkColour;
		}
		
		if (useCustomAlpha){
			newCol = new Color(newCol.r, newCol.g, newCol.b, customAlphaValue);
		}
		
		GetComponent<Renderer>().material.color = newCol;
	}
}
