using UnityEngine;
using System.Collections;

public class MerpsCameraGoalObjectScript : MonoBehaviour
{
	float vel;
	float dist;
	float result;

	void Start()
	{
		//za.cameraLimits[1] = 28.5f;//28.5 is a screen length
		//za.cameraLimits[3] = 16f;//16 is a screen height
	}

	void Update()
	{
		if (xa.player)
		{
			xa.glx = xa.player.transform.position;
			xa.glx.z = transform.position.z;

			dist = Vector3.Distance(transform.position, xa.glx);

			transform.LookAt(xa.glx);

			result = (dist) + 1;
			if (result > 5) { result = 5; }
			vel = result;

			if (dist > 0.3f)
			{
				transform.Translate(0, 0, vel * fa.deltaTime);
			}


			//lock (after movement to keep things smooth)
			xa.glx = transform.position;

			//player dist
			if (xa.player.transform.position.x > transform.position.x + 7) { xa.glx.x = xa.player.transform.position.x - 7; }
			if (xa.player.transform.position.x < transform.position.x - 7) { xa.glx.x = xa.player.transform.position.x + 7; }
			if (xa.player.transform.position.y > transform.position.y + 6) { xa.glx.y = xa.player.transform.position.y - 6; }
			if (xa.player.transform.position.y < transform.position.y - 6) { xa.glx.y = xa.player.transform.position.y + 6; }

			//camera limits
			if (xa.glx.x < za.cameraLimits[0]) { xa.glx.x = za.cameraLimits[0]; }
			if (xa.glx.x > za.cameraLimits[1]) { xa.glx.x = za.cameraLimits[1]; }
			if (xa.glx.y < za.cameraLimits[2]) { xa.glx.y = za.cameraLimits[2]; }
			if (xa.glx.y > za.cameraLimits[3]) { xa.glx.y = za.cameraLimits[3]; }

			transform.position = xa.glx;

		}
	}
}
