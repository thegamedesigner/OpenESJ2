using UnityEngine;
using System.Collections;

public class InfLoveFader : MonoBehaviour
{
	bool fadingOut = false;
	bool fadingIn = false;
	float fadeSpeed = 3.5f;

	// Use this for initialization
	void Start ()
	{
		if(xa.infLoveFader != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			xa.infLoveFader = this;
			xa.tempColor = GetComponent<Renderer>().material.color;
			xa.tempColor.a = 0;
			GetComponent<Renderer>().material.color = xa.tempColor;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(fadingOut || fadingIn)
		{
			xa.tempColor = GetComponent<Renderer>().material.color;
			if(fadingOut)
			{
				//xa.tempColor.a += fadeSpeed * fa.deltaTime * 10f;
				xa.tempColor.a = 1f;
				if(xa.tempColor.a >= 1f) // Done
				{
					xa.tempColor.a = 1f;
					fadingOut = false;
					fadingIn = true;
				}
			}
			else if(fadingIn)
			{
				xa.tempColor.a -= fadeSpeed * fa.deltaTime;
				if (xa.tempColor.a <= 0) // Done
				{
					xa.tempColor.a = 0;
					fadingIn = false;
				}
			}
			GetComponent<Renderer>().material.color = xa.tempColor;
		}
	}

	public void beginFade()
	{
		fadingIn = false;
		fadingOut = true;
	}
}
