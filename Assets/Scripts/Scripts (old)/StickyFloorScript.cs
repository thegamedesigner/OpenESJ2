using UnityEngine;
using System.Collections;

public class StickyFloorScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		 if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
			(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
			(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
			(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f)))
		{
			xa.inZoneStickyFloor++;
		}

	}
}
