using UnityEngine;
using System.Collections;

public class ExpandOnScreenColliderScript : MonoBehaviour
{
	public bool updateEveryFrame = false;

	void Start()
	{
		setToScale();
	}

	void Update()
	{
		if (!updateEveryFrame) { return; }

		setToScale();
	}

	void setToScale()
	{
		float size = Camera.main.orthographicSize * 2;
		//Debug.Log("Size: " + size);
		xa.glx.x = size * Camera.main.GetComponent<Camera>().aspect;
		xa.glx.y = size;
		xa.glx.z = 1f;
		xa.glx *= 1.1f;
		transform.localScale = xa.glx;

	}
}
