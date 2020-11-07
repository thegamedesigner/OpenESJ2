using UnityEngine;
using System.Collections;

public class BulletTweakingScript : MonoBehaviour
{
	public float xairFrictionX = 0.03f;
	public float xgravity = 3f;
	public float xmaxFallingVel = -99;
	public float xstartingVelX = 1;
	public float xstartingVelY = 5;

	public static float airFrictionX = 0.03f;
	public static float velXMinimum = 0.04f;
	public static float gravity = 1.3f;
	public static float maxFallingVel = -1;
	public static float startingVelX = 10;
	public static float startingVelY = 10;

	void Update()
	{
		airFrictionX = xairFrictionX;
		gravity = xgravity;
		maxFallingVel = xmaxFallingVel;
		startingVelX = xstartingVelX;
		startingVelY = xstartingVelY;
	}

}
