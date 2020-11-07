using UnityEngine;
using System.Collections;

public class DeathExploScript : MonoBehaviour
{
	public GameObject coreObj;

	public float coreRotateSpeed = 0;
	public float fansRotateSpeed = 0;

	bool scaledIn = false;
	float startingXScale = 0;
	float startingCoreXScale = 0;

	void Start()
	{
		startingXScale = transform.localScale.x;
		startingCoreXScale = coreObj.transform.localScale.x;

		xa.glx = transform.localScale;
		xa.glx.x = 0.01f;
		xa.glx.y = 0.01f;
		xa.glx.z = 0.01f;
		transform.localScale = xa.glx;

		coreObj.transform.parent = null;
	}

	// Update is called once per frame
	void Update()
	{
		xa.glx = coreObj.transform.localEulerAngles;
		xa.glx.z += coreRotateSpeed * fa.deltaTime;
		coreObj.transform.localEulerAngles = xa.glx;

		xa.glx = transform.localEulerAngles;
		xa.glx.z += fansRotateSpeed * fa.deltaTime;
		transform.localEulerAngles = xa.glx;

		if (!scaledIn)
		{
			xa.glx = transform.localScale;
			xa.glx.x += 25 * fa.deltaTime;
			if (xa.glx.x > startingXScale) { xa.glx.x = startingXScale; scaledIn = true; }
			xa.glx.y = xa.glx.x;
			xa.glx.z = xa.glx.x;
			transform.localScale = xa.glx;

	

			xa.glx = coreObj.transform.localScale;
			xa.glx.x += 45 * fa.deltaTime;
			if (xa.glx.x > startingCoreXScale) { xa.glx.x = startingCoreXScale; }
			xa.glx.y = xa.glx.x;
			xa.glx.z = xa.glx.x;
			coreObj.transform.localScale = xa.glx;
		}
	}
}
