using UnityEngine;
using System.Collections;

public class MerpsCameraScript : MonoBehaviour
{
	public bool useOnLeft = false;
	public bool useOnRight = false;
	public bool useOnUp = false;
	public bool useOnDown = false;

	public float offsetLeft = 9;
	public float offsetRight = 9;
	public float offsetUp = 9;
	public float offsetDown = 9;

	void Start()
	{
		transform.position = xa.cameraStartPos;
	}



	void Update()
	{
		if (xa.player && !xa.playerDead)
		{
			if (useOnLeft)
			{
				if (xa.player.transform.position.x < (transform.position.x - offsetLeft) && transform.position.x >= za.cameraLimits[0])
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + offsetLeft);
					transform.position = xa.glx;
				}
			}
			if (useOnRight)
			{
				if (xa.player.transform.position.x > (transform.position.x + offsetRight) && transform.position.x <= za.cameraLimits[1])
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x - offsetRight);
					transform.position = xa.glx;
				}
			}
			if (useOnUp)
			{
				if (xa.player.transform.position.y > (transform.position.y + offsetUp) && transform.position.y <= za.cameraLimits[2])
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - offsetUp);
					transform.position = xa.glx;
				}
			}
			if (useOnDown)
			{
				if (xa.player.transform.position.y < (transform.position.y - offsetDown) && transform.position.y >= za.cameraLimits[3])
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y + offsetDown);
					transform.position = xa.glx;
				}
			}


			//cap at limits
		   // xa.glx = transform.position;
		   // if (xa.glx.x < za.cameraLimits[0]) { xa.glx.x = za.cameraLimits[0]; }
		   // i/f (xa.glx.x > za.cameraLimits[1]) { xa.glx.x = za.cameraLimits[1]; }
		   // if (xa.glx.y < za.cameraLimits[2]) { xa.glx.y = za.cameraLimits[2]; }
		   // if (xa.glx.y > za.cameraLimits[3]) { xa.glx.y = za.cameraLimits[3]; }
			//transform.position = xa.glx;

		}
	}

}
