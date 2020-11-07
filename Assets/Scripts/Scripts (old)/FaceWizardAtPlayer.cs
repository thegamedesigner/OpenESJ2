using UnityEngine;
using System.Collections;

public class FaceWizardAtPlayer : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (xa.player)
		{
			if (xa.player.transform.position.x < transform.position.x)
			{
				xa.glx = transform.localScale;
				xa.glx.x = -4;
				transform.localScale = xa.glx;
			}
			else
			{
				xa.glx = transform.localScale;
				xa.glx.x = 4;
				transform.localScale = xa.glx;
			}
		}
	}
}
