using UnityEngine;
using System.Collections;

public class TwistOnEnterScreen : MonoBehaviour
{
	public GameObject twistMe = null;//destroys the gameobject that the script is on be default, but can be forced to destroy a custom gameobject.

	public float ySpeed = 0F;
	public int to_ZAngle = 0;
	public float rotSpeed = 0.1F;

	bool twisting;
	bool doneTwisting;
	
	// Use this for initialization
	void Start ()
	{
		twisting = false;
		doneTwisting = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!twistMe || doneTwisting) { } // don't do anything if target is invalid or have already twisted.
		else if (twisting)
		{
			Vector3 v3 = twistMe.transform.localEulerAngles;
			float cur_ZAngle = (v3.z + 180) % 360 - 180;
			if (Mathf.Abs(cur_ZAngle - to_ZAngle) < Mathf.Abs(rotSpeed))
			{
				v3.z = to_ZAngle;
				twistMe.transform.localEulerAngles = v3;
				doneTwisting = true;
			}
			else
			{
				v3.z += (to_ZAngle < v3.z ? -rotSpeed : rotSpeed) * 10f * fa.deltaTime;
				twistMe.transform.localEulerAngles = v3;
			}

			if (ySpeed != 0)
			{
				twistMe.transform.Translate(new Vector3(0,ySpeed * 10f * fa.deltaTime));
			}
		}
		else if (xa.music_Time >= 15.1 && transform.position.x < xa.frontEdgeOfScreen)//buffer zone in front of screen
		{
			twisting = true;
		}
	}
}
