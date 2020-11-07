using UnityEngine;
using System.Collections;

public class CubeTurretScript : MonoBehaviour
{
	public GameObject bullet;
	public GameObject firingPoint;
	public GameObject muzzleFlash;

	public float speedOfFiring = 1;//In seconds
	float timeSet = 0;

	void Start()
	{
		timeSet = -999;
	}

	void Update()
	{
		//point at player
		if (xa.player)
		{
			transform.SetAng(0, 0, Setup.ReturnAngleTowardsVec(transform.position, xa.player.transform.position));

			//Fire bullets
			if (fa.time > (timeSet + speedOfFiring))
			{
				timeSet = fa.time;
				xa.tempobj = (GameObject)(Instantiate(bullet, firingPoint.transform.position, firingPoint.transform.rotation));

				if (muzzleFlash != null)
				{
					xa.tempobj = (GameObject)(Instantiate(muzzleFlash, firingPoint.transform.position, firingPoint.transform.rotation));
				}
			}
		}


	}
}
