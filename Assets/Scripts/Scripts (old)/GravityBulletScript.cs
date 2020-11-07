using UnityEngine;
using System.Collections;

public class GravityBulletScript : MonoBehaviour
{
	public GameObject aniPuppet = null;
	public Vector3 vel = Vector3.zero;
	float airFrictionX = 0.03f;
	float gravity = 9f;
	float maxFallingVel = -44;
	float startingVelY = 11;
	//float startingVelX = 3;

	void Start()
	{
		vel.x = transform.localEulerAngles.z - 90;
		xa.glx = Vector3.zero;
		transform.localEulerAngles = xa.glx;
		//vel.x = Random.RandomRange(-startingVelX, startingVelX);
		vel.y = startingVelY;

		if (vel.x < 0)
		{
			xa.glx = transform.localScale;
			xa.glx.x = -xa.glx.x;
			transform.localScale = xa.glx;
		}
	}

	void Update()
	{
		handleVel();

		handleAni();
	}
	int stage = 0;
	void handleAni()
	{
		if (stage == 0)
		{
			if (vel.y < 4) { aniPuppet.SendMessage("playAni1"); }
			if (vel.y < 2) { aniPuppet.SendMessage("playAni2"); }
			if (vel.y < 0.75) { aniPuppet.SendMessage("playAni3"); stage++; }
		}
		if (stage == 1)
		{
			if (vel.y < 0) { aniPuppet.SendMessage("playAni4"); }
			if (vel.y < -0.75) { aniPuppet.SendMessage("playAni5"); }
			if (vel.y < -2) { aniPuppet.SendMessage("playAni6"); }
			if (vel.y < -4) { aniPuppet.SendMessage("playAni7"); stage++; }
		}

	}

	void handleVel()
	{
		//gravity
		vel.y -= gravity * fa.deltaTime;
		if (vel.y < maxFallingVel) { vel.y = maxFallingVel; }

		//X air friction

		if (vel.x > airFrictionX)
		{
			vel.x -= airFrictionX * fa.deltaTime;
		}
		if (vel.x < -airFrictionX)
		{
			vel.x += airFrictionX * fa.deltaTime;
		}

		transform.position += vel * fa.deltaTime;
	}
}
