using UnityEngine;
using System.Collections;

public class SpikeyThingShooterScript : MonoBehaviour
{
	public GameObject createChargeUpEffect = null;
	public GameObject createBullet = null;

	public float timeInSecondsReloading = 0;
	public float timeInSecondsChargingUp = 0;
	float timeSave = 0;
	int state = 0;
	GameObject storedChargingUpEffect = null;


	void Start()
	{
		timeSave = fa.time;
	}

	void Update()
	{
		if (xa.runesCollected >= 4 && xa.runesCollected <= 9)
		{

			if (state == 0)
			{
				if (fa.time >= (timeSave + timeInSecondsReloading))
				{
					timeSave = fa.time;
					state = 1;
					if (createChargeUpEffect) { storedChargingUpEffect = (GameObject)(Instantiate(createChargeUpEffect, transform.position, xa.null_quat)); }

				}
			}
			else if (state == 1)
			{
				if (fa.time >= (timeSave + timeInSecondsChargingUp))
				{
					timeSave = fa.time;
					if (createBullet) { Instantiate(createBullet, transform.position, xa.null_quat); }
					if (storedChargingUpEffect) { Destroy(storedChargingUpEffect); }
					storedChargingUpEffect = null;
					state = 0;
				}
			}
		}
	}
}
