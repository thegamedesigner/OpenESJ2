using UnityEngine;
using System.Collections;

public class StickToEdgeOfScreenScript : MonoBehaviour
{
	float lastY = 0;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		xa.glx = Camera.main.GetComponent<Camera>().transform.position;
		//xa.glx.x = transform.position.x;
		xa.glx.z = transform.position.z;

		int index;
		index = 0;
		while (index < 50)
		{
			//vec1 = Camera.main.camera.transform.position;
			xa.glx.y += 5f;
			if (!Camera.main.GetComponent<Camera>().pixelRect.Contains(Camera.main.GetComponent<Camera>().WorldToScreenPoint(xa.glx)))
			{ break; }
			else { lastY = xa.glx.y; }
			index++;
		}
		xa.glx.y = lastY;
		index = 0;
		while (index < 50)
		{
			//vec1 = Camera.main.camera.transform.position;
			xa.glx.y += 1f;
			if (!Camera.main.GetComponent<Camera>().pixelRect.Contains(Camera.main.GetComponent<Camera>().WorldToScreenPoint(xa.glx)))
			{ break; }
			else { lastY = xa.glx.y; }
			index++;
		}
		xa.glx.y = lastY;
		index = 0;
		while (index < 50)
		{
			//vec1 = Camera.main.camera.transform.position;
			xa.glx.y += 0.1f;
			if (!Camera.main.GetComponent<Camera>().pixelRect.Contains(Camera.main.GetComponent<Camera>().WorldToScreenPoint(xa.glx)))
			{ break; }
			else { lastY = xa.glx.y; }
			index++;
		}
		xa.glx.x = transform.position.x;
		transform.position = xa.glx;

	}
}
