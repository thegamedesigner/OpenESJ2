using UnityEngine;
using System.Collections;

public class ExploScript : MonoBehaviour
{
	public float FadeInSpeed = 0;
	public float Lifespan = 0;
	public float FadeOutSpeed = 0;
	public float AlphaMax = 0;
	public float ScaleMin = 0;
	public float ScaleMax = 0;
	public float ScaleInSpeed = 0;
	public bool random360angle = false;
	public bool random360ZOnlyangle = false;
	public bool killSelfOnly = true;
	public bool dontDie = false;
	public bool keepAlpha = true;

	bool fadedIn = false;
	bool scaledIn = false;
	float targetScale;

	void Start()
	{
		xa.glx = transform.localScale;
		xa.glx.x = 0.1f;
		xa.glx.y = 0.1f;
		xa.glx.z = 0.1f;
		transform.localScale = xa.glx;

		xa.tempColor = transform.GetComponent<Renderer>().material.color;
		if (!keepAlpha) { xa.tempColor.a = 0; }
		transform.GetComponent<Renderer>().material.color = xa.tempColor;

		targetScale = Random.Range(ScaleMin, ScaleMax);

		if(random360angle)
		{
			xa.glx = transform.localEulerAngles;
			xa.glx.x = Random.Range(0,360);
			xa.glx.y = Random.Range(0,360);
			xa.glx.z = Random.Range(0,360);
			transform.localEulerAngles = xa.glx;
		}
		if (random360ZOnlyangle)
		{
			xa.glx = transform.localEulerAngles;
			xa.glx.z = Random.Range(0, 360);
			transform.localEulerAngles = xa.glx;
		}
	}

	void Update()
	{


		if (!fadedIn)
		{
			xa.tempColor = transform.GetComponent<Renderer>().material.color;
			xa.tempColor.a += FadeInSpeed * fa.deltaTime;
			if (xa.tempColor.a >= AlphaMax) { fadedIn = true; xa.tempColor.a = AlphaMax; }
			transform.GetComponent<Renderer>().material.color = xa.tempColor;
		}

		if (!scaledIn)
		{
			xa.glx = transform.localScale;
			xa.glx.x += ScaleInSpeed * fa.deltaTime;
			xa.glx.y += ScaleInSpeed * fa.deltaTime;
			xa.glx.z += ScaleInSpeed * fa.deltaTime;
			if (xa.glx.x >= targetScale)
			{
				scaledIn = true;
				xa.glx.x = targetScale;
				xa.glx.y = targetScale;
				xa.glx.z = targetScale;
			}
			transform.localScale = xa.glx;
		}

		if (Lifespan > 0)
		{
			Lifespan -= 5 * fa.deltaTime;
		}

		if (Lifespan <= 0 && !dontDie)
		{
			xa.tempColor = transform.GetComponent<Renderer>().material.color;
			xa.tempColor.a -= FadeOutSpeed * fa.deltaTime;
			if (xa.tempColor.a <= 0)
			{
				if (killSelfOnly)
				{
					Destroy(this.gameObject);
				}
				else
				{
					Destroy(transform.parent.gameObject);
				}
			}
			transform.GetComponent<Renderer>().material.color = xa.tempColor;
		}

	}
}
