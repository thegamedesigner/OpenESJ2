using UnityEngine;
using System.Collections;

public class PopeIgnoreMissilesHack : MonoBehaviour
{
	public Transform invincibilityPoint;
	public static bool ignoreMissiles = false;

	void Start ()
	{
        if (invincibilityPoint == null)
        { 	//Debug.LogError("Pope Invincibility Point not initialized!");
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(invincibilityPoint)
		{
			if(Vector3.Distance(transform.position, invincibilityPoint.position) < 0.5f)
			{
				if(!PopeIgnoreMissilesHack.ignoreMissiles)
					PopeIgnoreMissilesHack.ignoreMissiles = true;
			}
			else
			{
				if(PopeIgnoreMissilesHack.ignoreMissiles)
					PopeIgnoreMissilesHack.ignoreMissiles = false;
			}
		}
	}
}
