using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_AirSword : MonoBehaviour
{
	public NovaPlayerScript novaPlayerScript;

	float punchSpeed = 150;//855;
	float punchDist = 10;//How far, in blocks (whole units) can the player travel?
	float distSoFar = 0;
	Vector3 startPos;

	float swordSpeed = 700;//855;
	float timeSlowEffect = 0.5f;//0.7f;

	Vector3 swordDir = new Vector3(0, 0, 0);
	SwordState swordState = SwordState.Setup;
	//bool gotBoost = false;
	enum SwordState
	{
		Setup,
		Charge,
		WrapUp,
		End
	}
	float previousTime = 1;

	public void CheckHurtZones()
	{
		if (HurtZoneScript.HZs == null) { return; }
		Vector3 v1 = new Vector3(0, 0, 33);
		Vector3 v2 = new Vector3(transform.position.x, transform.position.y, 33);
		//Debug.DrawLine(v1, v2, Color.gray, 3);
		for (int i = 0; i < HurtZoneScript.HZs.Count; i++)
		{
			if (HurtZoneScript.HZs[i].dontHurtAirsword) { continue; }

			if ((HurtZoneScript.HZs[i].pos.x + (HurtZoneScript.HZs[i].size.x * 0.5f)) > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
				(HurtZoneScript.HZs[i].pos.x - (HurtZoneScript.HZs[i].size.x * 0.5f)) < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
				(HurtZoneScript.HZs[i].pos.y + (HurtZoneScript.HZs[i].size.y * 0.5f)) > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
				(HurtZoneScript.HZs[i].pos.y - (HurtZoneScript.HZs[i].size.y * 0.5f)) < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f)))
			{
				HurtZoneScript.StaticHurtFunc();
			}
		}
	}
	bool storedJump = false;
	bool hitDeadlyBoxCollider = false;

	public void SwordUpdate()
	{

		if (Controls.GetInputDown(Controls.Type.Jump, novaPlayerScript.playerNumber) && !xa.playerHasJetpack)
		{
			//Debug.Log("PRESSED JUMP WHILE AIRSWORDING");
			storedJump = true;
		}

		switch (swordState)
		{
			case SwordState.Setup:
				{
					hitDeadlyBoxCollider = false;
					swordDir = Vector3.zero;
					if (xa.playerControlsHeldDir < 0) { swordDir.x = -1; }
					if (xa.playerControlsHeldDir >= 0) { swordDir.x = 1; }
					swordState = SwordState.Charge;

					//play sound effect
					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.SwordSlice);

					ScreenSlash.ScreenSlashOn(transform.position.y);

					previousTime = Time.timeScale;
					Time.timeScale = timeSlowEffect;
					xa.playerAirSwording = true;
					//gotBoost = false;
					novaPlayerScript.Unstick();
				}
				break;
			case SwordState.Charge:
				{

					float dist = swordSpeed * fa.deltaTime;


					//raycast for blocks

					LayerMask swordMask = 1 << 19 | 1 << 21;//Only hits hitboxes on the NovaBlock layer
					Ray ray = new Ray();
					RaycastHit hit1 = new RaycastHit();
					RaycastHit hit2 = new RaycastHit();
					RaycastHit hit3 = new RaycastHit();
					ray.direction = swordDir;
					float tempSpeed = swordSpeed;
					bool hitSomething = false;
					bool raycastsHitSomething = false;
					float half = (novaPlayerScript.plHeight * 0.5f);// + 0.1f;//ghx
					Vector3 v1 = Vector3.zero;
					Vector3 v2 = Vector3.zero;


					float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);
					ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
					v1 = ray.origin; v1.z = 33;
					if (Physics.Raycast(ray, out hit1, dist, swordMask))
					{
						/* v2 = hit1.point; v2.z = 33; Debug.DrawLine(v1, v2, Color.yellow, 5);*/
						raycastsHitSomething = true;
					}


					ray.origin = new Vector3(transform.position.x, transform.position.y + half, raycastLayer);
					v1 = ray.origin; v1.z = 33;
					if (Physics.Raycast(ray, out hit2, dist, swordMask))
					{
						/* v2 = hit2.point; v2.z = 33; Debug.DrawLine(v1, v2, Color.yellow, 5);*/
						raycastsHitSomething = true;
					}



					ray.origin = new Vector3(transform.position.x, transform.position.y - half, raycastLayer);
					v1 = ray.origin; v1.z = 33;
					if (Physics.Raycast(ray, out hit3, dist, swordMask))
					{
						/* v2 = hit3.point; v2.z = 33;Debug.DrawLine(v1, v2, Color.yellow, 5); */
						raycastsHitSomething = true;
					}


					if (raycastsHitSomething)
					{
						float finalDist = 9999;
						Vector3 finalPoint = Vector3.zero;
						Collider finalCollider = null;
						int hitChoice = 0;
						float dist1 = 9999;
						float dist2 = 9999;
						float dist3 = 9999;

						if (hit1.collider != null) { dist1 = hit1.distance; }
						if (hit2.collider != null) { dist2 = hit2.distance; }
						if (hit3.collider != null) { dist3 = hit3.distance; }

						hitChoice = 3;
						if (dist1 <= dist2 && dist1 <= dist3) { hitChoice = 1; }
						if (dist2 <= dist1 && dist2 <= dist3) { hitChoice = 2; }
						if (dist3 <= dist1 && dist3 <= dist2) { hitChoice = 3; }
						//Debug.Log("Hit choice: " + hitChoice + ", D1: " + dist1 + ", D2: " + dist2 + ", D3: " + dist3);
						if (hitChoice == 1) { finalDist = hit1.distance; finalPoint = hit1.point; finalCollider = hit1.collider; }
						if (hitChoice == 2) { finalDist = hit2.distance; finalPoint = hit2.point; finalCollider = hit2.collider; }
						if (hitChoice == 3) { finalDist = hit3.distance; finalPoint = hit3.point; finalCollider = hit3.collider; }










						//	Debug.Log("HIT SOMETHING");
						tempSpeed = finalDist;
						hitSomething = true;
						transform.SetX(finalPoint.x);

						/*
						//Debug.Log("AirSwordHitSOmething: " + xa.playerBoxWidth + ", " + xa.playerBoxHeight);
						Vector3 a1 = transform.position;
						a1.z = 33;
						Vector2 a2;

						//draw up
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.y += xa.playerBoxHeight * 0.5f;
						//Debug.DrawLine(a1, a2, Color.green, 5);

						//draw down
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.y -= xa.playerBoxHeight * 0.5f;
						//Debug.DrawLine(a1, a2, Color.green, 5);

						//draw left
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.x -= xa.playerBoxWidth * 0.5f;
						//Debug.DrawLine(a1, a2, Color.green, 5);

						//draw right
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.x += xa.playerBoxWidth * 0.5f;
						Debug.DrawLine(a1, a2, Color.green, 5);


						//draw along the top
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.y += xa.playerBoxHeight * 0.5f;
						a2.x -= xa.playerBoxWidth * 0.5f;
						Debug.DrawLine(a1, a2, Color.green, 5);

						//draw along the bottom
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.y -= xa.playerBoxHeight * 0.5f;
						a2.x -= xa.playerBoxWidth * 0.5f;
						Debug.DrawLine(a1, a2, Color.green, 5);

						//draw along the left
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.y += xa.playerBoxHeight * 0.5f;
						a2.x += xa.playerBoxWidth * 0.5f;
						Debug.DrawLine(a1, a2, Color.green, 5);

						//draw along the right
						a2 = new Vector3(a1.x, a1.y, a1.z);
						a2.y -= xa.playerBoxHeight * 0.5f;
						a2.x += xa.playerBoxWidth * 0.5f;
						Debug.DrawLine(a1, a2, Color.green, 5);
						*/
						Info infoScript = null;
						if (hitChoice == 1) { infoScript = hit1.collider.GetComponent<Info>(); }
						if (hitChoice == 2) { infoScript = hit2.collider.GetComponent<Info>(); }
						if (hitChoice == 3) { infoScript = hit3.collider.GetComponent<Info>(); }
						if (infoScript != null)
						{
							if (infoScript.killPlayer) { hitDeadlyBoxCollider = true; }
						}

						HittableByAirsword hittableScript = finalCollider.gameObject.GetComponent<HittableByAirsword>();
						if (hittableScript != null)
						{
							hittableScript.HitByPlayer();
							/*if (hittableScript.giveAirswordBoost)
							{
								gotBoost = true;
							}*/
						}
					}

					if (hitSomething)
					{
						swordState = SwordState.WrapUp;
					}
					else
					{
						Vector3 swordVel = new Vector3();
						swordVel.x = (swordDir.x * tempSpeed) * fa.deltaTime;
						swordVel.y = (swordDir.y * tempSpeed) * fa.deltaTime;


						int split = 15;
						for (int m = split; m > 0; m--)
						{
							CheckForItems(dist * 1f, transform.position);
							CheckHurtZones();
							transform.Translate(swordVel / split);
						}

						//transform.Translate(swordVel);
					}




					if (!NovaPlayerScript.checkPlayerDeathBox(transform.position) && !novaPlayerScript.ThreeDee)
					{
						novaPlayerScript.hpScript.health = 0;
						swordState = SwordState.WrapUp;
					}

					if (novaPlayerScript.hpScript.health <= 0)
					{
						swordState = SwordState.WrapUp;
					}
				}
				break;

			case SwordState.WrapUp:
				{
					if (novaPlayerScript.altTrailPS_Normal != null)
					{
						novaPlayerScript.altTrailPS_Normal.Play();
						novaPlayerScript.altTrailPS_Airsword.Stop();
					}
					ScreenSlash.ScreenSlashOff();
					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RockImpact);
					ScreenShakeCamera.Screenshake(1, 0.25f, ScreenShakeCamera.ScreenshakeMethod.Basic);
					xa.playerAirSwording = false;
					Time.timeScale = previousTime;//1f;
												  //Time.timeScale = 0.7f;//previousTime;//1f;
					swordState = SwordState.Setup;
					novaPlayerScript.state = NovaPlayerScript.State.NovaPlayer;
					novaPlayerScript.DidAirSwordImpact = true;
					novaPlayerScript.vel.y = 0;

					if (hitDeadlyBoxCollider)
					{
						novaPlayerScript.hpScript.health = 0;
					}

					if (novaPlayerScript.hpScript.health <= 0)
					{
						if (novaPlayerScript.hpScript.setPosWhenKilled)
						{
							transform.SetX(novaPlayerScript.hpScript.posWhenKilled.x);
							transform.SetY(novaPlayerScript.hpScript.posWhenKilled.y);
						}
					}

					/*if (gotBoost)
					{
						novaPlayerScript.GotAirswordBoost();
					}*/

					if (storedJump)
					{
						storedJump = false;
						novaPlayerScript.ExternalPossibleJump();
					}
				}
				break;

		}
	}

	public void PunchUpdate()
	{
		if (Controls.GetInputDown(Controls.Type.Jump, novaPlayerScript.playerNumber) && !xa.playerHasJetpack)
		{
			storedJump = true;
		}

		switch (swordState)
		{
			case SwordState.Setup:
				{
					distSoFar = 0;
					startPos = transform.position;
					hitDeadlyBoxCollider = false;
					swordDir = Vector3.zero;
					if (xa.playerControlsHeldDir < 0) { swordDir.x = -1; }
					if (xa.playerControlsHeldDir >= 0) { swordDir.x = 1; }
					swordState = SwordState.Charge;

					//play sound effect
					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.SwordSlice);

					//ScreenSlash.ScreenSlashOn(transform.position.y);

					//previousTime = Time.timeScale;
					//Time.timeScale = timeSlowEffect;
					xa.playerAirSwording = true;
					//gotBoost = false;
					novaPlayerScript.Unstick();
				}
				break;
			case SwordState.Charge:
				{
					float dist = punchSpeed * fa.deltaTime;
					distSoFar += dist;


					//raycast for blocks

					LayerMask swordMask = 1 << 19 | 1 << 21;//Only hits hitboxes on the NovaBlock layer
					Ray ray = new Ray();
					RaycastHit hit1 = new RaycastHit();
					RaycastHit hit2 = new RaycastHit();
					RaycastHit hit3 = new RaycastHit();
					ray.direction = swordDir;
					float tempSpeed = punchSpeed * fa.deltaTime;
					bool hitSomething = false;
					bool raycastsHitSomething = false;
					float half = (novaPlayerScript.plHeight * 0.5f);// + 0.1f;//ghx
					Vector3 v1 = Vector3.zero;
					Vector3 v2 = Vector3.zero;


					float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);
					ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
					v1 = ray.origin; v1.z = 33;
					if (Physics.Raycast(ray, out hit1, dist, swordMask))
					{
						/* v2 = hit1.point; v2.z = 33; Debug.DrawLine(v1, v2, Color.yellow, 5);*/
						raycastsHitSomething = true;
					}


					ray.origin = new Vector3(transform.position.x, transform.position.y + half, raycastLayer);
					v1 = ray.origin; v1.z = 33;
					if (Physics.Raycast(ray, out hit2, dist, swordMask))
					{
						/* v2 = hit2.point; v2.z = 33; Debug.DrawLine(v1, v2, Color.yellow, 5);*/
						raycastsHitSomething = true;
					}



					ray.origin = new Vector3(transform.position.x, transform.position.y - half, raycastLayer);
					v1 = ray.origin; v1.z = 33;
					if (Physics.Raycast(ray, out hit3, dist, swordMask))
					{
						/* v2 = hit3.point; v2.z = 33;Debug.DrawLine(v1, v2, Color.yellow, 5); */
						raycastsHitSomething = true;
					}

					if (raycastsHitSomething)
					{
						float finalDist = punchDist;
						Vector3 finalPoint = Vector3.zero;
						Collider finalCollider = null;
						int hitChoice = 0;
						float dist1 = punchDist;
						float dist2 = punchDist;
						float dist3 = punchDist;

						if (hit1.collider != null) { dist1 = hit1.distance; }
						if (hit2.collider != null) { dist2 = hit2.distance; }
						if (hit3.collider != null) { dist3 = hit3.distance; }

						hitChoice = 3;
						if (dist1 <= dist2 && dist1 <= dist3) { hitChoice = 1; }
						if (dist2 <= dist1 && dist2 <= dist3) { hitChoice = 2; }
						if (dist3 <= dist1 && dist3 <= dist2) { hitChoice = 3; }
						//Debug.Log("Hit choice: " + hitChoice + ", D1: " + dist1 + ", D2: " + dist2 + ", D3: " + dist3);
						if (hitChoice == 1) { finalDist = hit1.distance; finalPoint = hit1.point; finalCollider = hit1.collider; }
						if (hitChoice == 2) { finalDist = hit2.distance; finalPoint = hit2.point; finalCollider = hit2.collider; }
						if (hitChoice == 3) { finalDist = hit3.distance; finalPoint = hit3.point; finalCollider = hit3.collider; }










						//	Debug.Log("HIT SOMETHING");
						tempSpeed = finalDist;
						hitSomething = true;
						transform.SetX(finalPoint.x);

						Info infoScript = null;
						if (hitChoice == 1) { infoScript = hit1.collider.GetComponent<Info>(); }
						if (hitChoice == 2) { infoScript = hit2.collider.GetComponent<Info>(); }
						if (hitChoice == 3) { infoScript = hit3.collider.GetComponent<Info>(); }
						if (infoScript != null)
						{
							if (infoScript.killPlayer) { hitDeadlyBoxCollider = true; }
						}

						HittableByAirsword hittableScript = finalCollider.gameObject.GetComponent<HittableByAirsword>();
						if (hittableScript != null)
						{
							hittableScript.HitByPlayer();
							/*if (hittableScript.giveAirswordBoost)
							{
								gotBoost = true;
							}*/
						}
					}

					float distCheck = Vector3.Distance(startPos,transform.position);
					if (hitSomething || distCheck > punchDist)
					{
						swordState = SwordState.WrapUp;
						
						if (distCheck > punchDist)//if you moved too far (too little, you probably hit something)
						{
							if(startPos.x < transform.position.x)
							{
								float moveBack = punchDist - distCheck;
								Debug.Log(moveBack);
								transform.Translate(new Vector3(moveBack,0,0));
							}
							else
							{
								float moveBack = punchDist - distCheck;
								Debug.Log(moveBack);
								transform.Translate(new Vector3(-moveBack,0,0));
							}
						}
						//Debug.Log(Vector3.Distance(startPos,transform.position));
						
					}
					else
					{
						Vector3 swordVel = new Vector3();
						swordVel.x = (swordDir.x * tempSpeed);
						swordVel.y = (swordDir.y * tempSpeed);

						//transform.SetX(ray.GetPoint(dist).x);

						int split = 15;
						for (int m = split; m > 0; m--)
						{
							CheckForItems(dist * 1f, transform.position);
							CheckHurtZones();
							transform.Translate(swordVel / split);

							
						
							if (Vector3.Distance(startPos, transform.position) > punchDist)
							{
							//	Debug.Log(m);
								//float d = Vector3.Distance(startPos, transform.position) - punchDist;
								//transform.Translate(new Vector3(-d,0,0));
								break;
							}

						}
						//transform.Translate(swordVel);


					}




					if (!NovaPlayerScript.checkPlayerDeathBox(transform.position) && !novaPlayerScript.ThreeDee)
					{
						novaPlayerScript.hpScript.health = 0;
						swordState = SwordState.WrapUp;
					}

					if (novaPlayerScript.hpScript.health <= 0)
					{
						swordState = SwordState.WrapUp;
					}
				}
				break;

			case SwordState.WrapUp:
				{
					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RockImpact);
					xa.playerAirSwording = false;
					swordState = SwordState.Setup;
					novaPlayerScript.state = NovaPlayerScript.State.NovaPlayer;
					novaPlayerScript.DidAirSwordImpact = true;
					novaPlayerScript.vel.y = 0;

					if (hitDeadlyBoxCollider)
					{
						novaPlayerScript.hpScript.health = 0;
					}

					if (novaPlayerScript.hpScript.health <= 0)
					{
						if (novaPlayerScript.hpScript.setPosWhenKilled)
						{
							transform.SetX(novaPlayerScript.hpScript.posWhenKilled.x);
							transform.SetY(novaPlayerScript.hpScript.posWhenKilled.y);
						}
					}

					/*if (gotBoost)
					{
						novaPlayerScript.GotAirswordBoost();
					}*/

					if (storedJump)
					{
						storedJump = false;
						novaPlayerScript.ExternalPossibleJump();
					}
				}
				break;

		}
	}

	void CheckForItems(float dist, Vector3 pos)
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("item");


		foreach (GameObject go in gos)
		{
			xa.glx = go.transform.position;
			xa.glx.z = pos.z;
			if (Vector3.Distance(xa.glx, pos) < 10)//just check theyre not, like, on the other side of the map
			{
				float tempDist = dist;
				ItemScript itemScript = go.GetComponent<ItemScript>();
				if (itemScript != null) { if (itemScript.itemActivationRadiusOverride != -1) { tempDist = itemScript.itemActivationRadiusOverride; } }

				if (Vector3.Distance(xa.glx, pos) < tempDist)
				{
					if (itemScript != null && itemScript.type != "bounceCoin")
					{
						UseFoundItem(go);
					}
					if (itemScript != null && itemScript.type == "bouncePad")
					{
						UseFoundItem(go);
					}
				}
			}
		}

		ShrineScript.CheckShrines(pos);
	}

	void UseFoundItem(GameObject go)
	{
		ItemScript itemScript;
		itemScript = go.GetComponent<ItemScript>();
		if (itemScript)
		{
			itemScript.pickUpItem();

		}
	}

}
