using UnityEngine;
using System.Collections;

public class GenericCameraBump : MonoBehaviour
{
	public bool useOnLeft = false;
	public bool useOnRight = false;
	public bool useOnUp = false;
	public bool useOnDown = false;

	public float offsetLeft = 9;
	public float offsetRight = 9;
	public float offsetUp = 9;
	public float offsetDown = 9;

	public float minY = 0;//ignores zero

	void Update()
	{
		if (xa.player)
		{
			if (useOnLeft)
			{
				if (xa.player.transform.position.x < (transform.position.x - offsetLeft) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + offsetLeft);
					transform.position = xa.glx;
				}
			}
			if (useOnRight)
			{
				if (xa.player.transform.position.x > (transform.position.x + offsetRight) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x - offsetRight);
					transform.position = xa.glx;
				}
			}
			if (useOnUp)
			{
				if (xa.player.transform.position.y > (transform.position.y + offsetUp) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - offsetUp);
					transform.position = xa.glx;
				}
			}
			if (useOnDown)
			{
				if (xa.player.transform.position.y < (transform.position.y - offsetDown) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y + offsetDown);
					if (xa.glx.y < minY && minY != 0) { xa.glx.y = minY; }
					transform.position = xa.glx;
				}
			}

		}
	}
}
