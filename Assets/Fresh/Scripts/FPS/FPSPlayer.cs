using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class FPSPlayer : MonoBehaviour
{
	//monster stats



	List<Vector3> lastSafePos = new List<Vector3>();


	public static Vector3 playerPos;
	public static int ammo;
	public GameObject shotgunModel;
	public GameObject rocketLauncherModel;

	public GameObject hurtOverlay1;
	public GameObject hurtOverlay2;
	public GameObject hurtOverlay3;
	public GameObject muzzlePoint;
	public GameObject fakeMuzzlePoint;
	public GameObject uzi_exploPrefab;
	public GameObject uzi_beamPrefab;
	public GameObject rocketPrefab;
	public HealthScript healthScript;

	//public GameObject bloodPS;

	float uzi_timeSet;
	float uzi_firingDelay = 0.08f;
	float uzi_raycastDist = 200;
	float uzi_accuracy = 2f;

	public GameObject sg_exploPrefab;
	public GameObject sg_beamPrefab;
	float sg_timeSet;
	float sg_firingDelay = 0.08f;
	float sg_raycastDist = 200;
	float sg_accuracy = 4f;
	int shotgunMinDam = 3;
	int shotgunMaxDam = 4;
	float rl_timeSet;
	float rl_firingDelay = 0.5f;

	int uziBulletsLeftToFire = 0;
	int bulletCount = 0;

	public int shotgunAmmo = 8;
	public int rocketAmmo = 3;

	bool dead = false;
	bool respawned = false;
	public GameObject camObj;
	public GameObject camControllerObj;

	float deadTimeset = 0;

	public enum Weapon
	{
		None,
		Uzi,
		Shotgun,
		RocketLauncher,
		End
	}
	public Weapon weapon = Weapon.Shotgun;
	float hurtOverlay_min = 0;
	float hurtOverlay_max = 1;
	float hurtOverlaySpeed = 0.4f;
	bool hurtOverlayDir = false;

	[HideInInspector]
	public int playerNumber = 0;//0 = player1


	Vector3 vel = Vector3.zero;
	float angVel = 0;
	float friction = 0.7f;
	float angFriction = 0.9f;
	float turnSpeed = 5;
	float moveSpeed = 5;
	float strafeSpeed = 5;
	float mouseSensitivity = 5;

	float speed = 10f;//5f;
	float maxVelY = 25f;
	float plWidth = 0.8f;
	float plHalfWidth = 0.8f * 0.5f;//plWidth * 0.5f;
	float plHeight = 1.6f;
	float plHalfHeight = 1.6f * 0.5f;//plHeight * 0.5f;
	float plQuarterHeight = 1.6f * 0.25f;//plHeight * 0.25f;
	float yaw = 0.0f;
	float pitch = 0.0f;
	public GameObject cam;
	void Start()
	{
		xa.fadingAtAll = false;
		yaw = transform.localEulerAngles.y;
		FPSMainScript.FPSPlayer = this.gameObject;
		FPSMainScript.FPSPlayerScript = this;

		
		//YOU ARE ON THIS LEVEL. Save that you've unlocked it.
		string level = SceneManager.GetActiveScene().name;
		UnlockSystemScript.UnlockThisLevel(level);


	}

	void Update()
	{
		if(fa.paused) {return; }
		playerPos = transform.position;

		if (healthScript.health > 30 && healthScript.health <= 60)
		{
			hurtOverlay1.SetActive(true);
			hurtOverlay2.SetActive(false);
			hurtOverlay3.SetActive(false);
		}
		if (healthScript.health > 0 && healthScript.health <= 30)
		{
			hurtOverlay1.SetActive(false);
			hurtOverlay2.SetActive(true);
			hurtOverlay3.SetActive(false);
		}
		if (healthScript.health < 0)
		{
			hurtOverlay1.SetActive(false);
			hurtOverlay2.SetActive(false);
			hurtOverlay3.SetActive(true);
		}
		if (healthScript.health > 60)
		{
			hurtOverlay1.SetActive(false);
			hurtOverlay2.SetActive(false);
		}

		if (Controls.GetInputDown(Controls.Type.FPSCycleWeapon, 0))
		{
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.BaseballBat);

			shotgunModel.SetActive(false);
			rocketLauncherModel.SetActive(false);

			if (weapon == Weapon.Shotgun) { rocketLauncherModel.SetActive(true); weapon = Weapon.RocketLauncher; }
			else { shotgunModel.SetActive(true); weapon = Weapon.Shotgun; }
		}

		if (weapon == Weapon.Shotgun) { ammo = shotgunAmmo; }
		else { ammo = rocketAmmo; }

		if (!dead)
		{

			if (healthScript.health <= 0)
			{
				deadTimeset = fa.time;
				dead = true;
				iTween.MoveBy(camControllerObj, iTween.Hash("y", -1.8f, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
				iTween.RotateTo(camControllerObj, iTween.Hash("x", -45, "time", 1.5f, "easetype", iTween.EaseType.easeInOutSine));
			}
		}
		else
		{
			if (fa.time > (deadTimeset + 2))
			{
				if (!respawned)
				{
					respawned = true;
					xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, SceneManager.GetActiveScene().name);
				}
			}
		}
		if (!dead)
		{

			HandleMovement();
			HandleWeapon();
			HandleCameraLook();
		}

		int hp = Mathf.RoundToInt(healthScript.health);
		if (hp < 0) { hp = 0; }
		if (healthScript.health > 0 && hp <= 0) { hp = 1; }
		/*	if (Fresh_InGameMenus.self != null)
			{
				Fresh_InGameMenus.self.FPS_HealthText.text = "" + hp;
				int ammo = 0;
				if (weapon == Weapon.Shotgun) { ammo = shotgunAmmo; }
				if (weapon == Weapon.RocketLauncher) { ammo = rocketAmmo; }
				Fresh_InGameMenus.self.FPS_AmmoText.text = "" + ammo;// + "/" + maxAmmo;

				if (hp > 30) { hurtOverlay_max = 0; hurtOverlay_min = 0; }
				else if (hp > 20) { hurtOverlay_max = 0.3f; hurtOverlay_min = 0.1f; }
				else if (hp > 10) { hurtOverlay_max = 0.5f; hurtOverlay_min = 0.3f; }
				else if (hp > 0) { hurtOverlay_max = 1f; hurtOverlay_min = 0.7f; }
				else { hurtOverlay_max = 1f; hurtOverlay_min = 0.9f; }

				Image i = Fresh_InGameMenus.self.FPS_HurtOverlay;
				float a = i.material.color.a;
				if (hurtOverlayDir)
				{
					if (a < hurtOverlay_max) { a += hurtOverlaySpeed * fa.deltaTime; } else { hurtOverlayDir = false; }
				}
				else
				{
					if (a > hurtOverlay_min) { a -= hurtOverlaySpeed * fa.deltaTime; } else { hurtOverlayDir = true; }
				}

				if (hurtOverlay_max == 0) { a = 0; }
				i.material.color = new Color(i.material.color.r, i.material.color.g, i.material.color.b, a);
			}
			*/

	}

	void HandleMovement()
	{
		HandleVoxelMovement();
		/*
		vel.x *= friction;
		vel.z *= friction;
		if (Controls.GetInput(Controls.Type.FPSForward, playerNumber)) { vel.z += moveSpeed * Time.deltaTime; }
		if (Controls.GetInput(Controls.Type.FPSBackward, playerNumber)) { vel.z -= moveSpeed * Time.deltaTime; }
		if (Controls.GetInput(Controls.Type.FPSLeft, playerNumber)) { vel.x -= strafeSpeed * Time.deltaTime; }
		if (Controls.GetInput(Controls.Type.FPSRight, playerNumber)) { vel.x += strafeSpeed * Time.deltaTime; }
		transform.Translate(vel);
		transform.AddAngY(Input.GetAxis("Horizontal") * mouseSensitivity);
		*/

	}

	void HandleWeapon()
	{
		if (weapon == Weapon.RocketLauncher)
		{
			if (Controls.GetInputDown(Controls.Type.FPSFire, 0) || Input.GetMouseButtonDown(0))

			//if(Controls.GetKeyDown(Controls.Type.Ability1))
			{
				FireRocketLauncher();
			}

		}
		if (weapon == Weapon.Shotgun)
		{
			if (Controls.GetInputDown(Controls.Type.FPSFire, 0) || Input.GetMouseButtonDown(0))
			//if(Controls.GetKeyDown(Controls.Type.Ability1))
			{
				FireShotgun();
			}

		}
	}


	public void FireShotgun()
	{

		if (fa.time > (sg_timeSet + sg_firingDelay))
		{
			if (shotgunAmmo <= 0)
			{
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.ShotgunEmpty);
			}
			else
			{
				shotgunAmmo--;
				sg_timeSet = fa.time;
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.ShotgunFiring);

				for (int i = 0; i < 6; i++)
				{
					FireShotgunPellet();
				}
			}

		}
	}

	public void FireRocketLauncher()
	{

		if (fa.time > (rl_timeSet + rl_firingDelay))
		{

			if (rocketAmmo <= 0)
			{
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RocketLauncherEmpty);
			}
			else
			{
				rocketAmmo--;
				rl_timeSet = fa.time;
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.UziFiring);

				GameObject go = Instantiate(rocketPrefab, fakeMuzzlePoint.transform.position, muzzlePoint.transform.rotation);
			}
		}
	}

	void FireShotgunPellet()
	{
		Ray ray = new Ray();
		RaycastHit hit;
		LayerMask mask = 1 << 19 | 1 << 20;
		muzzlePoint.transform.AddAngX(Random.Range(-sg_accuracy, sg_accuracy));
		muzzlePoint.transform.AddAngY(Random.Range(-sg_accuracy, sg_accuracy));
		ray.origin = muzzlePoint.transform.position;
		ray.direction = muzzlePoint.transform.forward;
		muzzlePoint.transform.localEulerAngles = Vector3.zero;
		//Debug.DrawLine(muzzlePoint.transform.position, ray.GetPoint(100),Color.red, 12);
		//	muzzlePoint.transform.AddAngY(Random.Range(-uzi_accuracy, uzi_accuracy));
		Vector3 hitPos;

		if (Physics.Raycast(ray, out hit, uzi_raycastDist, mask))
		{
			hitPos = hit.point;

			Info info = hit.transform.gameObject.GetComponent<Info>();
			if (info != null)
			{
				info.healthScript.health -= Random.Range(shotgunMinDam, shotgunMaxDam + 1);
			}
		}
		else
		{
			hitPos = ray.GetPoint(sg_raycastDist);
		}

		GameObject go;
		go = Instantiate(sg_exploPrefab, hitPos, sg_exploPrefab.transform.rotation);


		float dist = Vector3.Distance(fakeMuzzlePoint.transform.position, hitPos);
		fakeMuzzlePoint.transform.LookAt(hitPos);
		ray.origin = fakeMuzzlePoint.transform.position;
		ray.direction = fakeMuzzlePoint.transform.forward;
		Vector3 halfway = ray.GetPoint(dist * 0.5f);
		go = Instantiate(sg_beamPrefab);
		go.transform.position = halfway;
		go.transform.LookAt(hitPos);
		go.transform.SetScaleZ(dist);

		//go = Instantiate(bloodPS, hitPos, bloodPS.transform.rotation);

	}

	public void FireUzi()
	{

		if (fa.time > (uzi_timeSet + uzi_firingDelay))
		{
			uziBulletsLeftToFire--;
			uzi_timeSet = fa.time;
			//	Debug.Log("BANG! " + fa.timeInSeconds);

			if (bulletCount == 0)
			{
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.UziFiring);
			}
			//bulletCount++;
			//if (bulletCount == 3) { bulletCount = 0; }

			Ray ray = new Ray();
			RaycastHit hit;
			LayerMask mask = 1 << 19;
			muzzlePoint.transform.AddAngX(Random.Range(-uzi_accuracy, uzi_accuracy));
			muzzlePoint.transform.AddAngY(Random.Range(-uzi_accuracy, uzi_accuracy));
			ray.origin = muzzlePoint.transform.position;
			ray.direction = muzzlePoint.transform.forward;
			muzzlePoint.transform.localEulerAngles = Vector3.zero;
			//Debug.DrawLine(muzzlePoint.transform.position, ray.GetPoint(100),Color.red, 12);
			//	muzzlePoint.transform.AddAngY(Random.Range(-uzi_accuracy, uzi_accuracy));
			Vector3 hitPos;

			if (Physics.Raycast(ray, out hit, uzi_raycastDist, mask))
			{
				hitPos = hit.point;

				Info info = hit.transform.gameObject.GetComponent<Info>();
				if (info != null)
				{
					info.healthScript.health -= Random.Range(shotgunMinDam, shotgunMaxDam + 1);
				}
			}
			else
			{
				hitPos = ray.GetPoint(uzi_raycastDist);
			}

			GameObject go;
			go = Instantiate(uzi_exploPrefab, hitPos, uzi_exploPrefab.transform.rotation);

			/*
			float dist = Vector3.Distance(muzzlePoint.transform.position, hitPos);
			Vector3 halfway = ray.GetPoint(dist * 0.5f);
			go = Instantiate(uzi_beamPrefab);
			go.transform.position = halfway;
			go.transform.LookAt(hitPos);
			go.transform.SetScaleZ(dist);

	*/

		}
	}







	void HandleCameraLook()
	{
		if (cam == null) { return; }
		yaw += 125 * Input.GetAxis("Mouse X") * Time.deltaTime;
		pitch -= 125 * Input.GetAxis("Mouse Y") * Time.deltaTime;

		if (Controls.GetInput(Controls.Type.FPSLookLeft, 0)) { yaw -= 125 * Time.deltaTime; }
		if (Controls.GetInput(Controls.Type.FPSLookRight, 0)) { yaw += 125 * Time.deltaTime; }
		if (Controls.GetInput(Controls.Type.FPSLookUp, 0)) { pitch -= 65 * Time.deltaTime; }
		if (Controls.GetInput(Controls.Type.FPSLookDown, 0)) { pitch += 65 * Time.deltaTime; }

		pitch -= 125 * Input.GetAxis("Mouse Y") * Time.deltaTime;

		if (pitch > 70) { pitch = 70; }
		if (pitch < -50) { pitch = -50; }
		cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		cam.transform.position = transform.position;
		cam.transform.AddY(0.5f);

	}

	void HandleVoxelMovement()
	{


		//Debug.Log(Camera.main.transform.localEulerAngles);
		if (xa.emptyObj == null) { return; }//might be the case for the first frame or so
		xa.emptyObj.transform.position = transform.position;
		xa.emptyObj.transform.rotation = transform.rotation;
		xa.emptyObj.transform.SetAngY(Camera.main.transform.eulerAngles.y);

		if (Controls.GetInput(Controls.Type.FPSForward, 0)) { xa.emptyObj.transform.Translate(0, 0, speed); }
		if (Controls.GetInput(Controls.Type.FPSBackward, 0)) { xa.emptyObj.transform.Translate(0, 0, -speed); }
		if (Controls.GetInput(Controls.Type.FPSLeft, 0)) { xa.emptyObj.transform.Translate(-speed, 0, 0); }
		if (Controls.GetInput(Controls.Type.FPSRight, 0)) { xa.emptyObj.transform.Translate(speed, 0, 0); }

		vel.x += xa.emptyObj.transform.position.x - transform.position.x;
		vel.z += xa.emptyObj.transform.position.z - transform.position.z;

		if (vel.y > maxVelY) { vel.y = maxVelY; }
		if (vel.y < -maxVelY) { vel.y = -maxVelY; }

		vel.x *= friction;
		vel.z *= friction;

		if (vel.x > -0.01f && vel.x < 0.01f) { vel.x = 0; }
		if (vel.z > -0.01f && vel.z < 0.01f) { vel.z = 0; }


		ResolveVel();
	}

	void ResolveVel()
	{
		Ray ray = new Ray();
		LayerMask mask = 1 << 19;
		RaycastHit hit;
		float dist;

		//raycast for walls
		Vector3 hitPoint = Vector3.zero;

		if (vel.x > 0)
		{
			ray.direction = new Vector3(1, 0, 0);
			dist = plHalfWidth;
			RaycastX(ray, mask, dist, new Vector3(0.1f, 0, 0));
			RaycastX(ray, mask, dist, new Vector3(-0.1f, 0, 0));
		}
		if (vel.x < 0)
		{
			ray.direction = new Vector3(-1, 0, 0);
			dist = plHalfWidth;
			RaycastX(ray, mask, dist, new Vector3(0.1f, 0, 0));
			RaycastX(ray, mask, dist, new Vector3(-0.1f, 0, 0));
		}
		if (vel.z > 0)
		{
			ray.direction = new Vector3(0, 0, 1);
			dist = plHalfWidth;
			RaycastZ(ray, mask, dist, new Vector3(0, 0, 0.1f));
			RaycastZ(ray, mask, dist, new Vector3(0, 0, -0.1f));
		}
		if (vel.z < 0)
		{
			ray.direction = new Vector3(0, 0, -1);
			dist = plHalfWidth;
			RaycastZ(ray, mask, dist, new Vector3(0, 0, 0.1f));
			RaycastZ(ray, mask, dist, new Vector3(0, 0, -0.1f));
		}

		//Debug.Log(vel);
		transform.position += vel * Time.deltaTime;



		//can I see a node?
		bool canSeePlayer = false;
		for (int i = 0; i < PF.nodes.Count; i++)
		{
			PF.nodes[i].go.transform.LookAt(FPSMainScript.playerPos);
			ray.origin = PF.nodes[i].go.transform.position;
			ray.direction = PF.nodes[i].go.transform.forward;
			dist = Vector3.Distance(ray.origin, transform.position);

			if (!Physics.Raycast(ray, out hit, dist, mask))
			{
				Debug.DrawRay(ray.origin, ray.direction, Color.green, 1);
				lastSafePos.Add(transform.position);
				//if (lastSafePos.Count > 100) { lastSafePos.RemoveAt(0); }
				canSeePlayer = true;
			}
		}

		if (!canSeePlayer)
		{
			//Debug.Log("OUTSIDE, teleporting!" + Time.time);
			//transform.position = new Vector3(ray.origin.x,transform.position.y,ray.origin.z);
			//transform.position = lastSafePos;

			//transform.position -= vel * Time.deltaTime;


			//rewind
			for (int a = lastSafePos.Count - 1; a > 0; a--)
			{
				transform.position = lastSafePos[a];
				//Can this see a node?
				canSeePlayer = false;
				for (int i = 0; i < PF.nodes.Count; i++)
				{
					PF.nodes[i].go.transform.LookAt(FPSMainScript.playerPos);
					ray.origin = PF.nodes[i].go.transform.position;
					ray.direction = PF.nodes[i].go.transform.forward;
					dist = Vector3.Distance(ray.origin, transform.position);

					if (!Physics.Raycast(ray, out hit, dist, mask))
					{
						canSeePlayer = true;
						break;
					}
				}
				if (canSeePlayer) { break; }
			}
		}
		else
		{
			//Debug.Log("CAN SEE NODE " + Time.time);
		}
	}

	void RaycastX(Ray ray, LayerMask mask, float dist, Vector3 offset)
	{
		RaycastHit hit;
		ray.origin = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z + offset.z);
		if (Physics.Raycast(ray, out hit, dist, mask)) { hitWallX(hit); }
	}

	void RaycastZ(Ray ray, LayerMask mask, float dist, Vector3 offset)
	{
		RaycastHit hit;
		ray.origin = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z + offset.z);
		if (Physics.Raycast(ray, out hit, dist, mask)) { hitWallZ(hit); }
	}

	void hitWallX(RaycastHit hit)
	{
		//transform.SetX(hit.point.x + plHalfWidth);
		vel.x = 0;
	}
	void hitWallZ(RaycastHit hit)
	{
		//transform.SetZ(hit.point.z + plHalfWidth);
		vel.z = 0;
	}
}
