using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMainScript : MonoBehaviour
{
	public static FPSMainScript self;
	public static Camera FPSCam = null;
	public static GameObject FPSPlayer = null;
	public static FPSPlayer FPSPlayerScript = null;
	public static Vector3 playerPos = Vector3.zero;
	public static int uids = 0;
	public static List<FPSZombieScript> zombies;
	public static List<FPSMGunScript> demons;
	public static List<HealthScript> anyMonster;//Added after having each type be it's own list was a pain. 

	public Behaviour fpsControllerScript;
	public GameObject mainCam;
	public Material redWall1;
	public Material blueWall1;
	public Material redCeiling1;
	public Material blueCeiling1;
	public Material redFloor1;
	public Material blueFloor1;




	void Awake()
	{
		zombies = new List<FPSZombieScript>();
		demons = new List<FPSMGunScript>();
		anyMonster = new List<HealthScript>();
		self = this;
		FPSCam = Camera.main;
		//Fresh_InGameMenus.self.FPS_HealthIcon.gameObject.SetActive(true);
		//Fresh_InGameMenus.self.FPS_HealthText.gameObject.SetActive(true);

		
		Setup.SetCursor(Setup.C.NotVisible, Setup.C.Locked);
	}

	void Start()
	{
		PF.InitNodes();
	}

	// Update is called once per frame
	void Update()
	{
		if(fa.paused) {return; }
		PF.CalcAllPaths();
		if (FPSPlayer != null)
		{
			playerPos = FPSPlayer.transform.position;
		}

	}
}
