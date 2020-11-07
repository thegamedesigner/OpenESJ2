using UnityEngine;
using System.Collections;

public class FallingStarScript : MonoBehaviour
{
	public GameObject puppet;
	Color tempColor;
	Vector3 rot;
	Vector3 pos;
	float dt;
	public float fadeInSpeed    = 0;
	public float fadeOutSpeed   = 0;
	public float lifespan       = 0;
	public float maxAlpha       = 0;
	public float fallingSpeed   = 0;
	public float rotationSpeed  = 0;
	public float phase2         = 0;
	public float rotationSpeed2 = 0;
	bool fadedIn                = false;
	bool fadeOut                = false;

	void Start()
	{
		tempColor                      = puppet.GetComponent<Renderer>().material.color;
		tempColor.a                    = 0;
		puppet.GetComponent<Renderer>().material.color = tempColor;
		rot                            = transform.localEulerAngles;
		pos                            = transform.position;
	}

	void Update()
	{
		//rot                     = transform.localEulerAngles;
		//rot.z                  += rotationSpeed2 * fa.deltaTime;
		//transform.localEulerAngles = rot;
		dt = fa.deltaTime;
		if (xa.music_Time < phase2)
		{
			//rot                     = transform.localEulerAngles;
			rot.z                  += rotationSpeed * dt;
			transform.localEulerAngles = rot;
			//xa.glx                     = transform.position;
			pos.y                  -= fallingSpeed * dt;
			transform.position         = pos;

			if (!fadedIn)
			{
				//tempColor                   = puppet.renderer.material.color;
				tempColor.a                += fadeInSpeed * dt;
				puppet.GetComponent<Renderer>().material.color = tempColor;
				if (tempColor.a >= maxAlpha)
				{
					tempColor.a = maxAlpha;
					fadedIn = true;
				}
			}
			else
			{
				if (!fadeOut)
				{
					lifespan -= 10 * dt;
					if (lifespan <= 0)
					{
						fadeOut = true;
					}
				}
				else
				{
					//tempColor                    = puppet.renderer.material.color;
					tempColor.a                   -= fadeOutSpeed * dt;
					puppet.GetComponent<Renderer>().material.color = tempColor;
					if (tempColor.a <= 0)
					{
						Destroy(puppet.GetComponent<Renderer>().material);
						Destroy(this.gameObject);
					}
				}
			}
		}
	}
}
