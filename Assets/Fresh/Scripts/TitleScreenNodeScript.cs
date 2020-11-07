using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenNodeScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		if (RawFuncs.self != null) { RawFuncs.self.AllMenusOff(); }
		fa.paused = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Controls.GetAnyKeyDown() && !Input.GetKey(KeyCode.Escape))
		{
			xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "Warnings");
		}
	}
}
