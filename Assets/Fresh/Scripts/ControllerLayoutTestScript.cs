using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLayoutTestScript : MonoBehaviour
{
	public TextMesh textMesh;
	void Start()
	{
	}

	void Update()
	{
		textMesh.text = "Inputs:";
		//Test axis's
		for (int i = 1; i < 10; i++)
		{
			for (int a = 1; a < 10; a++)
			{
				float value = Input.GetAxis("Joy" + i + " Axis " + a);

				if (value != 0)
				{
					textMesh.text += "\nJoy" + i + "Axis" + a + ": " + value;

				}
			}

		}
	}
}
