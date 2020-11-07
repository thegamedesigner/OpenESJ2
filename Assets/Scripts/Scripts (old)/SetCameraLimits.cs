using UnityEngine;
using System.Collections;

public class SetCameraLimits : MonoBehaviour
{
	public bool isLeft = false;
	public bool isRight = false;
	public bool isUp = false;
	public bool isDown = false;
	float halfCameraWidth = 10.5f;
	float halfCameraHeight = 6;
	void Start()
	{
		if (isLeft) { za.cameraLimits[0] = transform.position.x + halfCameraWidth; }
		if (isRight) { za.cameraLimits[1] = transform.position.x - halfCameraWidth; }
		if (isUp) { za.cameraLimits[2] = transform.position.y - halfCameraHeight; }
		if (isDown) { za.cameraLimits[3] = transform.position.y + halfCameraHeight; }
	}

}
