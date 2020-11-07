using UnityEngine;
using System.Collections;

public class FollowerEyeballScript : MonoBehaviour
{
	float distX = 1;
	float distY = 3;
	int stateX = 0;//0=centered
	int stateY = 0;//0=centered
	int oldStateX = 0;
	int oldStateY = 0;
	float result;

	void Update()
	{
		if (xa.player)
		{
			xa.glx = transform.position;
			xa.glx.y = xa.player.transform.position.y;
			xa.glx.z = xa.player.transform.position.z;
			result = Vector3.Distance(xa.player.transform.position, xa.glx);
			distY = result;
			if (distY < 3) { distY = 3; }
			//if (result > 6) { distY = 6; }
			//if (result <= 6) { distY = 3; }


			if (xa.player.transform.position.x <= (transform.position.x + distX) && xa.player.transform.position.x >= (transform.position.x - distX)) { stateX = 0; }
			if (xa.player.transform.position.x > (transform.position.x + distX)) { stateX = 1; }
			if (xa.player.transform.position.x < (transform.position.x - distX)) { stateX = -1; }

			if (xa.player.transform.position.y > (transform.position.y + distY)) { stateY = 2; }
			if (xa.player.transform.position.y < (transform.position.y - distY)) { stateY = -1; }

			if (xa.player.transform.position.y <= (transform.position.y + distY) && xa.player.transform.position.y > transform.position.y) { stateY = 1; }
			if (xa.player.transform.position.y >= (transform.position.y - distY) && xa.player.transform.position.y <= transform.position.y) { stateY = 0; }
		  

			if (oldStateX != stateX || oldStateY != stateY)
			{
				if (stateX == 0)
				{
					if (stateY > 0) { this.gameObject.SendMessage("playAni5"); }//looking up centered
					if (stateY <= 0) { this.gameObject.SendMessage("playAni6"); }//looking down centered
				}
				else
				{
					if (stateX == 1) { setScale(-4); }
					if (stateX == -1) { setScale(4); }

					if (stateY == 2) { this.gameObject.SendMessage("playAni1"); }//looking up 2
					if (stateY == 1) { this.gameObject.SendMessage("playAni4"); }//looking up 1
					if (stateY == 0) { this.gameObject.SendMessage("playAni2"); }//looking middle
					if (stateY == -1) { this.gameObject.SendMessage("playAni3"); }//looking down
				}
			}
			
			//Setup.GC_DebugLog("dist: " + result);
			oldStateX = stateX;
			oldStateY = stateY;

		}
	}

	void setScale(float a)
	{
		xa.glx = transform.localScale; xa.glx.x = a; transform.localScale = xa.glx;
	}
}
