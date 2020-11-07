using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevFrogScript : MonoBehaviour
{
	public bool onlyFireIfOnScreen = false;
	public bool onlyFireIfTriggered = false;
	public Info infoScriptForTriggering;
	public GameObject bullet;
	public GameObject firingPoint;
	public FreshAni freshAniScript;
	public float delay = 1;
	float timeSet = 0;

	// Update is called once per frame
	void Update()
	{
		if (this.onlyFireIfTriggered)
		{
			if (this.infoScriptForTriggering == null  || !this.infoScriptForTriggering.triggered) {
				return;
			}
		}

		bool passed = false;

		if (!onlyFireIfOnScreen) {
			passed = true;
		}

		if (((transform.position.x - fa.cameraPos.x) < 14) &&
		    ((transform.position.y - fa.cameraPos.y) < 14))
		{
			passed = true;
		}

		if (passed)
		{
			if (this.bullet != null)
			{
				if (fa.time > (this.timeSet + this.delay))
				{
					this.timeSet = fa.time;
					this.freshAniScript.PlayAnimation(1);
					Vector3 pos = this.firingPoint.transform.position;
					pos.z = this.bullet.transform.position.z;
					GameObject.Instantiate<GameObject>(this.bullet, pos, this.firingPoint.transform.rotation);
				}
			}
		}
	}
}
