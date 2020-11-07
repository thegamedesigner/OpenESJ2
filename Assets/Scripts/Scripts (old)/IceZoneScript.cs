using UnityEngine;
using System.Collections;

public class IceZoneScript : MonoBehaviour
{
	//int oldState = 0;
	//int state = 0;
	void Start()
	{
	
	}

	void Update()
	{
		//NOTE*: Main sets xa.inZoneIce to 0 every frame(happens before this update)
        if (xa.player)
        {
            //This system is less efficient, but should solve the Ice Bug. I can't figure out the exact cause, but I think it's that the ice is at xa.isIceZone == -1, so adding to the ice zone variable just brings it up to zero.
            if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
                (transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
                (transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
                (transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f)))
            {
                xa.inZoneIce++;
            }
        }
		//else
		//{
		//	xa.inZoneIce = 0;
		//}
		////Debug.Log ("ICEZONE "+xa.inZoneIce);
		/*
		if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
			(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
			(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
			(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f)))
		{
			state = 1;
		}
		else
		{
			state = 0;
		}
		if (oldState != state)
		{
			oldState = state;

			if(state == 1)
			{
				xa.inZoneIce++; //Why wouldn't this just be xa.inZoneIce = 1; Am I missing something?
	
			}
			else
			{
				xa.inZoneIce--;
			}
		}
		*/
	  //  Setup.GC_DebugLog(xa.inZoneIce);
	}
}
