using UnityEngine;
using System.Collections;

public class TriggerGlorgScript : MonoBehaviour {
	
	public GameObject GlorgGoesHere;
	
	bool setBoxStats = false;
	bool triggered = false;
	
	float plBoxHeight = 0;
	float plBoxWidth = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (triggered) {
			return;
		}
	
		if (xa.player)
		{
			if (!setBoxStats)
			{
				setBoxStats = true;
				plBoxHeight = xa.playerBoxHeight;
				plBoxWidth = xa.playerBoxWidth;
			}
	
			if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
				(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
				(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
				(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)))
			{
				triggered = true;
				iTweenEvent glorgTween = GlorgGoesHere.GetComponent<iTweenEvent>();
				glorgTween.Play();
	
				GlorgGoesHere.gameObject.transform.parent = Camera.main.transform;
			}
		}
	}
}
