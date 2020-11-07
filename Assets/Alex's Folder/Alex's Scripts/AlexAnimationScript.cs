using UnityEngine;
using System.Collections;

public class AlexAnimationScript : MonoBehaviour {

	public Vector2[] framesOffset;
	public float pauseBetweenFrames;
	float counter, xScale, yScale;
	int frameAmount;
	[HideInInspector]
	public int currentFrame;
	public bool doNotLoop = false;
	public bool ignoreDeltaTime = false;
	public bool scaleXTexture = false;
	public bool scaleYTexture = false;
	
	void Awake () {
		currentFrame = 0;
		counter = pauseBetweenFrames;
		frameAmount = framesOffset.Length - 1;
		Vector3 s = gameObject.transform.localScale;
		Vector2 ms = gameObject.GetComponent<MeshRenderer>().material.mainTextureScale;
		if(scaleXTexture){ xScale = ms.x*s.x; } else { xScale = ms.x; }
		if(scaleYTexture){ yScale = ms.y*s.y; } { yScale = ms.y; }
		GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(xScale, yScale);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!ignoreDeltaTime){
			counter -= fa.deltaTime;
		} else {
			counter -= 1;
		}
		
		if(counter <= 0) {
			if(currentFrame < frameAmount) {
				currentFrame ++;
			} else {
				if(!doNotLoop){
					currentFrame = 0;
				}
			}
			counter = pauseBetweenFrames;
		}
		GetComponent<MeshRenderer>().material.mainTextureOffset = framesOffset[currentFrame];
	}
}
