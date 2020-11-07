using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMenuNode : MonoBehaviour
{

	void Start()
	{

	}

	void Update()
	{
		//UpdateFPS();
		//UpdateDeathCounterAndSpeedRunDisplay();


		bool openMenu = false;
		if (Controls.GetInputDown(Controls.Type.OpenMenu, 0)) { openMenu = true; }

		//if (RawFuncs.InRawMenu) { openMenu = false; }
		if (openMenu)
		{
			//Still here? Then toggle the IGMM.
			if (!fa.escapeDoesntTriggerInGameMainMenu)
			{
				if (RawFuncs.self != null)
				{
					if (!xa.fadingAtAll)
					{
						fa.paused = true;
						RawFuncs.self.MenuOn(RawInfo.MenuType.InGameHub);
					}
				}

			}
		}
	}
}