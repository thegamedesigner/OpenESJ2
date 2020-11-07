using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joysticks : MonoBehaviour
{
	public List<string> joyNames;
	public AxisState[,] joyStates;

	public class AxisState//The state of one axis
	{
		public int joyNum = -1;
		public int axisNum = -1;
		public float value = 0;
		public float deadZone = 0.4f;
		public bool pressed;//true if the abs of value is > than deadState

		public void UpdateMe()
		{
			//called every frame.
			if (Mathf.Abs(value) > deadZone) { pressed = true; } else { pressed = false; }
		}
	}

	void Awake()
	{
		//fill names list
		joyNames = new List<string>();
		for (int i = 1; i <= 10; i++)
		{
			for (int a = 1; a <= 10; a++)
			{
				joyNames.Add("Joy" + i + " Axis " + a);
			}
		}

		joyStates = new AxisState[11, 11];
		for (int j = 1; j <= 10; j++)
		{
			for (int a = 1; a <= 10; a++)
			{
				joyStates[j, a] = new AxisState();
				joyStates[j, a].joyNum = j;
				joyStates[j, a].axisNum = a;
			}
		}
	}

	void Update()
	{
		//Every frame, check every joystick. This is why this script needs to run before any others.
		for (int j = 1; j <= 10; j++)
		{
			for (int a = 1; a <= 10; a++)
			{
				//Note: JoyStates are zero based
				joyStates[j, a].value = Input.GetAxis("Joy" + j + " Axis " + a);
				joyStates[j, a].UpdateMe();
			}
		}
	}
}
