using UnityEngine;
using System.Collections;

public class TriggerZoneScript : MonoBehaviour
{
	public Behaviour[] scriptsToActivate;
    public GameObject GOToSendTo = null;
    public string msgToSend = string.Empty;
	public bool useSendMsg       = false;
	bool isInfLove               = xa.gameMode == Main.GameMode.INFINITE_LOVE;
	
	// Update is called once per frame
	void Update()
	{
		if(!isInfLove || (isInfLove && xa.infLoveTriggerZone != this))
		{
			if (xa.player && !xa.playerDead)
			{
				float x = transform.position.x;
				float y = transform.position.y;
				float px = xa.player.transform.position.x;
				float py = xa.player.transform.position.y;
				Vector3 halfScale = transform.localScale * 0.5f;
				
				if ((x + halfScale.x) > (px - (xa.playerBoxWidth * 0.5f)) &&
					(x - halfScale.x) < (px + (xa.playerBoxWidth * 0.5f)) &&
					(y + halfScale.y) > (py - (xa.playerBoxHeight * 0.5f)) &&
					(y - halfScale.y) < (py + (xa.playerBoxHeight * 0.5f)))
				{
					if (useSendMsg)
					{
                            GOToSendTo.SendMessage(msgToSend);
					}
					
					foreach (Behaviour co in scriptsToActivate)
					{
						co.enabled = true;
					}
					
					if (!isInfLove)
					{
						Destroy(this);
					}
					else
					{
						xa.infLoveTriggerZone = this;
					}
				}
			}
		}
	}
}
