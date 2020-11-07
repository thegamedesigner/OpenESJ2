using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsTestScript : MonoBehaviour
{
    public Text display;
    /*
    A test script, for testing some controls / input concepts
    */
    float deadzone = 0.2f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetAxis("Joy1 Axis 1") > deadzone)
        {
            display.text = "Pressed";
        }
        else
        {
            
            display.text = "Not Pressed";
        }

        string[] s = Input.GetJoystickNames();
        for(int i = 0;i < s.Length;i++)
        {
            display.text += "\n" + s[i];
        }

    }
}
