using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStringToControl : MonoBehaviour
{
	public string strBefore = "Press ";
	public string strAfter = " to Jump";
	public TextMesh textMesh;
	public Controls.Type control = Controls.Type.Jump;

	void Start()
	{
		if (control == Controls.Type.Jump)//handle jump
		{
			textMesh.text = strBefore + Controls.GetLabelForJumpKey() + strAfter;
		}
		else if (control == Controls.Type.Ability1)//handle ability1
		{
			textMesh.text = strBefore + Controls.GetLabelForAbility1Key() + strAfter;
		}
		else if (control == Controls.Type.MoveDown)
		{
			textMesh.text = strBefore + Controls.GetLabelForMoveDownKey() + strAfter;
		}
		else
		{
			string temp = Controls.GetLabelForControlType(control);
			if (temp == "JoystickButton0") { temp = "A"; }
			if (temp == "JoystickButton1") { temp = "B"; }
			textMesh.text = strBefore + temp + strAfter;
		}
	}

}
