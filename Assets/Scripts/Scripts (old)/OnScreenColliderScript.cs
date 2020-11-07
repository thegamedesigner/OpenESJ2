using UnityEngine;
using System.Collections;

public class OnScreenColliderScript : MonoBehaviour
{
	void OnLevelWasLoaded()
	{
		setToScale();
		attachToCamera();
	}

	void Update()
	{
		setToScale();
	}

	void setToScale()
	{
		float size = Camera.main.orthographicSize * 2;
		//Debug.Log("Size: " + size);
		xa.glx.x                = size * Camera.main.GetComponent<Camera>().aspect;
		xa.glx.y                = size;
		xa.glx.z                = 1f;
		if (this == xa.onScreenCollider) xa.glx *= 1.3f;
		transform.localScale    = xa.glx;
		//xa.onScreenObjectsDirty = true;
	}

	void attachToCamera()
	{
		transform.parent = Camera.main.GetComponent<Camera>().transform;
		xa.glx = Vector3.zero;
		xa.glx.z = 10f;
		transform.localPosition = xa.glx;
	}
}
