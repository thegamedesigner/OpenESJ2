using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpStarExploScript : MonoBehaviour
{
	public MeshRenderer meshRenderer;
	void Start()
	{
		iTween.ScaleTo(this.gameObject,iTween.Hash("x",3, "y", 3, "time", 0.1f,"easetype", iTween.EaseType.easeInOutSine));
		iTween.RotateBy(this.gameObject,iTween.Hash("z",0.2f, "time", 0.5f,"easetype", iTween.EaseType.easeInOutSine));
		
	}

	void Update()
	{
		Color c = meshRenderer.material.color;
		if(c.a <= 0)
		{
			Destroy(this.gameObject);
			return;
		}
		//Debug.Log(c.a);
		c.a -= 2 * Time.deltaTime;
		meshRenderer.material.color = c;

		
	}
}
