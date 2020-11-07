using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectController : MonoBehaviour
{
	public static KnownControllers controller = KnownControllers.Keyboard;

	public enum KnownControllers
	{
		Mouse,
		Keyboard,
		Xbox360,
		Custom,
		End
	}

	public static void DetectCurrentController()//Called every frame
	{
		if (Input.GetAxis("Mouse X") != 0.0f ||
			Input.GetAxis("Mouse Y") != 0.0f)
		{
			controller = KnownControllers.Mouse;
		}

		//detect keyboard
		if (Input.GetKey((KeyCode)1))
		{
			Debug.Log("GOT IT HERE");
		}

	}
}
