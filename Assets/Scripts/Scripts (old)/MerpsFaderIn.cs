using UnityEngine;
using System.Collections;

public class MerpsFaderIn : MonoBehaviour
{
	//float fadeSpd = 1;
	float timeSet = -1;
	float fadeDelay = 1;//Time in seconds it takes to fade in (works correctly)
	float result = 0;

	void Awake()
	{
		xa.fadingOut = false;
		xa.fadingIn = true;
	}

	void Start()
	{
		xa.faderIn = this.gameObject;
		xa.allowPlayerInput = true;


		//Camera.main.gameObject.AddComponent<AspectUtility>();
		//AspectUtility.SetCamera();
	}

	public void fadeIn()
	{
		if (timeSet == -1) 
		{
			xa.tempColor = this.gameObject.GetComponent<Renderer>().material.color;
			xa.tempColor.a = 1;
			this.gameObject.GetComponent<Renderer>().material.color = xa.tempColor;
			timeSet = Time.realtimeSinceStartup;
		}
		result = (timeSet + fadeDelay) - Time.realtimeSinceStartup;
		if (result < 0) { result = 0; }
		result = result / fadeDelay;
		xa.tempColor = this.gameObject.GetComponent<Renderer>().material.color;
		xa.tempColor.a = result;
		this.gameObject.GetComponent<Renderer>().material.color = xa.tempColor;
		if (result <= 0)
		{
			if (xa.frozenCamera) { xa.frozenCamera = false; }
			xa.fadingIn = false;
			this.enabled = false;
			//Destroy(this.gameObject);
		}
	}

}
