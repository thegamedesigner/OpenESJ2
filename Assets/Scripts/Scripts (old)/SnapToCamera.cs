using UnityEngine;
using System.Collections;

public class SnapToCamera : MonoBehaviour
{
	public new GameObject camera;
	public Vector3 offset = Vector3.zero;
	public bool useLockToX = false;
	public bool useLockToY = false;
	public bool useLockToZ = false;
	public Vector3 lockTo = Vector3.zero;

	void Update()
	{
		xa.glx = camera.transform.position;
		xa.glx += offset;
		if (useLockToX) { xa.glx.x = lockTo.x; }
		if (useLockToY) { xa.glx.y = lockTo.y; }
		if (useLockToZ) { xa.glx.z = lockTo.z; }
		transform.position = xa.glx;
	}
}
