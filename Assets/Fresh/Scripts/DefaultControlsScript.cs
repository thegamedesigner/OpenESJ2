using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultControlsScript : MonoBehaviour
{
	void Start()
	{
	string str = "";
		str += "Default controls:\n";
		str += "Jump - Z\n";
		str += "Ability - Z\n";
		str += "Move Left - Z\n";
		str += "Move Right - Z\n";
		str += "Drop off wall - Z\n";
		str += "Respawn - Z\n";
		str += "Restart level - Z\n";
		
		str += "\nFPS: \n";
		str += "Fire - Space / Mouse Left\n";
		str += "Cycle Weapon - E\n";
		str += "Forward - W\n";
		str += "Backward - S\n";
		str += "Strafe Left - A\n";
		str += "Strafe Right - D\n";
		str += "Look Up - Up Arrow / Mouse Up\n";
		str += "Look Down - Down Arrow / Mouse Down\n";
		str += "Look Left - Left Arrow / Mouse Left\n";
		str += "Look Right - Right Arrow / Mouse Right\n";
		
		str += "\nMenu: \n";
		str += "Select - Enter\n";
		str += "Open Menu - Escape\n";
		str += "Navigate Up - Up Arrow\n";
		str += "Navigate Down - Down Arrow\n";
		str += "Navigate Left - Left Arrow\n";
		str += "Navigate Right - Right Arrow\n";


		/*
Default controls:
Jump - Z
Ability - X
Move Left - Left Arrow
Move Right - Right Arrow
Drop off wall - Down Arrow
Respawn - R
Restart level - Q
		
FPS:
Fire - Space / Mouse Left
Cycle Weapon - E
Forward - W
"Backward - S
Strafe Left - A
Strafe Right - D
Look Up - Up Arrow / Mouse Up
Look Down - Down Arrow / Mouse Down
Look Left - Left Arrow / Mouse Left
Look Right - Right Arrow / Mouse Right
		
Menu:
Select - Enter
Open Menu - Escape
Navigate Up - Up Arrow
Navigate Down - Down Arrow
Navigate Left - Left Arrow
Navigate Right - Right Arrow
		*/
	}

	// Update is called once per frame
	void Update()
	{

	}
}
