using UnityEngine;
using System.Collections;

public class Gun_IceLaserScript : MonoBehaviour
{
	public GameObject bullet;
	public GameObject firingPtForward;
	public GameObject firingPtUp;
	public GameObject firingPtDown;

	bool snapToPlayer = false;
	bool left = false;
	bool right = false;
	bool up = false;
	bool down = false;

	void Start()
	{

	}

	void Update()
	{
		if (xa.player)
		{
			if (!snapToPlayer)
			{
				//find the player & snap to them
				transform.parent = xa.player.transform;
				transform.localPosition = Vector3.zero;
				snapToPlayer = true;
			}
			else
			{
				setDir();

				fireGun();
			}
		}
	}

	void setDir()
	{
		left = false;
		right = false;
		up = false;
		down = false;

		if (Input.GetKey(KeyCode.LeftArrow)) { left = true; }
		if (Input.GetKey(KeyCode.RightArrow)) { right = true; }
		if (Input.GetKey(KeyCode.UpArrow)) { up = true; }
		if (Input.GetKey(KeyCode.DownArrow)) { down = true; }

		if (left || right) { za.gunDir = 0; }
		if (up) { za.gunDir = 2; }
		if (down) { za.gunDir = 3; }

		if (left || right || up || down)
		{
			za.forceAimGun = true;
		}
		else
		{
			za.forceAimGun = false;
		}


	}

	void fireGun()
	{
		if (Input.GetKey(KeyCode.X))
		{
			xa.glx = transform.localScale;
			if (!za.playerFlipped) { xa.glx.x = 1; }
			else{ xa.glx.x = -1; }
			transform.localScale = xa.glx;
			if (za.gunDir == 0) { xa.glx = firingPtForward.transform.position; }
			if (za.gunDir == 2) { xa.glx = firingPtUp.transform.position; }
			if (za.gunDir == 3) { xa.glx = firingPtDown.transform.position; }

			xa.tempobj = (GameObject)(Instantiate(bullet, xa.glx, xa.null_quat));
			xa.glx = xa.tempobj.transform.localEulerAngles;
			if (za.gunDir == 0 && !za.playerFlipped) { xa.glx.z = 0; }
			if (za.gunDir == 0 && za.playerFlipped) { xa.glx.z = 180; }
			if (za.gunDir == 2) { xa.glx.z = 90; }
			if (za.gunDir == 3) { xa.glx.z = -90; }
			xa.tempobj.transform.localEulerAngles = xa.glx;
			if (xa.createdObjects) { xa.tempobj.transform.parent = xa.createdObjects.transform; }


		}
	}
}
