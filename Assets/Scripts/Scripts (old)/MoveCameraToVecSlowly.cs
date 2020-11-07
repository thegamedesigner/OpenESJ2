using UnityEngine;
using System.Collections;

public class MoveCameraToVecSlowly : MonoBehaviour
{
	CameraScript cameraScript;
	public Vector3 goal = Vector3.zero;
	public float speed = 0;
	public bool dontMoveToZeros = false;
	void Start()
	{
		moveCameraToVecSlowly();
	}

	public void moveCameraToVecSlowly()
	{
		if (goal.x == 0 && dontMoveToZeros) { goal.x = Camera.main.GetComponent<Camera>().transform.position.x; }
		if (goal.y == 0 && dontMoveToZeros) { goal.y = Camera.main.GetComponent<Camera>().transform.position.y; }
		if (goal.z == 0 && dontMoveToZeros) { goal.z = Camera.main.GetComponent<Camera>().transform.position.z; }
		cameraScript = Camera.main.GetComponent<Camera>().GetComponent<CameraScript>();
		cameraScript.useGoToVec = true;
		cameraScript.goToVec = goal;
		cameraScript.goToVecSpeed = speed;
	}
}
