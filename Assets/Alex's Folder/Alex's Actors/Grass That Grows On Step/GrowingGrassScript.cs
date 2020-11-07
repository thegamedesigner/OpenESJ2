using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingGrassScript : MonoBehaviour {

	public Texture[] tPool;

	float plBoxHeight = 0;
	float plBoxWidth = 0;

	bool activated = false;

	void Awake(){
		GetComponent<AlexAnimationScript>().enabled = false;
		GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2((1f/5f), 1f);
		GetComponent<MeshRenderer>().material.mainTextureOffset = Vector2.zero;
		GetComponent<MeshRenderer>().material.mainTexture = tPool[Random.Range(0, tPool.Length)];
	}
	
	void Update () {
		if (xa.player && !activated)
		{
			plBoxHeight = xa.playerBoxHeight;
			plBoxWidth = xa.playerBoxWidth;
			
			if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
			    (transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
			    (transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
			    (transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)))
			{
				GetComponent<AlexAnimationScript>().enabled = true;
				activated = true;
			}
		}
	}
}
