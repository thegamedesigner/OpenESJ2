using UnityEngine;
using System.Collections;

public class CameraBumpForPope2 : MonoBehaviour
{
	public float forceOffset = 9;
	public bool useOnXAxis = false;

	public bool useOnXAndYAxis = false;
	public bool bothDirectionsXAxis = false;
	public bool bothDirectionsYAxis = false;

	void Update()
	{
		if (xa.player)
		{
			if (useOnXAxis || useOnXAndYAxis)
			{
				if (xa.player.transform.position.x > (transform.position.x + forceOffset) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x - forceOffset);
					transform.position = xa.glx;
				}
				if (bothDirectionsXAxis && xa.player.transform.position.x < (transform.position.x - forceOffset) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + forceOffset);
					transform.position = xa.glx;
				}
			}
			if(!useOnXAxis || useOnXAndYAxis)
			{
				if (xa.player.transform.position.y > (transform.position.y + forceOffset) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - forceOffset);
					transform.position = xa.glx;
				}
				if (bothDirectionsYAxis && xa.player.transform.position.y < (transform.position.y - forceOffset) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y + forceOffset);
					transform.position = xa.glx;
				}
			}
		}
	}
}
