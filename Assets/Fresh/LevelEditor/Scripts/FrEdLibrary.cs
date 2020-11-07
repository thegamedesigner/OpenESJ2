using System.Collections;
using UnityEngine;

public class FrEdLibrary : MonoBehaviour
{
	public static int uIds             = 0;
	public static FrEdLibrary instance = null;
	public Mat[] mats                  = new Mat[0];
	public LibraryItem[] library       = new LibraryItem[0];
	public Track[] tracks              = new Track[0];
	public MeshClass[] meshes          = new MeshClass[0];
	public BehaviourClass[] behaviours = new BehaviourClass[0];
	public Material customSheetMat     = null;

	public enum Type
	{
		None,
		PlayerSpawn,
		Block,
		SpikeBall,
		Portal,
		SetSky,
		SetMusic,
		Frog,
		NinjaStar,
		HomingMissile,
		ShrineDoubleJump,
		ShrineTripleJump,
		ShrineQuintupleJump,
		ShrineButtSmash,
		ShrineAirSword,
		ShrineRemoveAbilities,
		Laser,
		QuadLaser,
		Coin,
		BounceArrow,
		SpikyHomingMissile,
		RoundBullet,
		Firestick,
		CameraTrigger,
		Particles,
		Decoration,
		Animation,
		MovingSpikePlatform,
		Buffalo,
		DetailBlock,
		ExplodingPlatform,
		MissilePlatform,
		Unicorn,
		Charger,
		Blocker,
		BouncePad,
		StickyWall,

		//MovingPlatform,
		/*
		HomingMissile,
		HomingMissileFast,
		HomingMissileSpiky,
		RoundBullet,
		FlyingFrog,
		SpikyFrog,
		BounceStar,
		MonsterSpawner,
		SpikedCutie,
		SpikedBeast,
		TinyImp,
		NPC_MinorDevil,
		NPC_Raver1,
		NPC_Raver2,
		NPC_Raver3,
		NPC_RaverBald,
		NPC_BoogieBro,
		NPC_BoogieBro2,
		HurtZone,
		Spikes,
		Spikes_NoHurtZone,
		SpikeBall_NoHurtZone,
		*/

		End
	}

	public enum TrackType
	{
		None,
		ENV_1 = 1,
		ENV_5 = 2,
		ENV_8222 = 3,
		ENV_AbsoluteZero = 4,
		ENV_Darkas = 5,
		ENV_FutureShock = 6,
		ENV_GenericTechno = 7,
		ENV_GreenWithMe = 8,
		ENV_Rokits = 9,
		ENV_Shakestopper = 10,
		ENV_ThisSound = 11,
		ENV_TwoStep = 12,
		ENV_WobbleWobble = 13,
		ENV_Valkyrie = 14,
		GetSix_NoInterruptions = 15,
		GetSix_Valhalla = 16,
		Reptiore_Butter = 17,
		Reptiore_GatorRaid = 18,
		Reptiore_OmegaHauzer = 19,
		Reptiore_SomeRegrettablyOffensiveShit = 20,
		ENV_Osiris = 21,
		GetSix_Helix = 22,
		Reptiore_Tentacles = 23,
		End
	}

	public enum MatType
	{
		None,
		sky_skyblue,
		sky_black,
		sky_dkpink,
		sky_deeppinkflat,
		sky_deeppinkwhite,
		sky_dkbluedesat,
		sky_dkbluebright,
		sky_dkred,
		sky_dkred2,
		sky_paleorange,
		sky_flatyellow,
		sky_greendarktop,
		sky_ltgreen,
		sky_grey,
		sky_flatred,
		sky_pink1,
		sky_purple1,
		sky_snowypink,
		sky_stormyblue,
		sky_whitegreen,
		sky_white,
		sky_yellow,
		sky_flatpink,
		sky_flatdullpurple,
		sky_snowypink2,
		sky_red1,
		sky_purplered,
		sky_flatpurple,
		sky_palepurpletan,
		sky_pink2,
		sky_greenstripes,
		sky_9,
		sky_10,
		sky_11,
		sky_11b,
		sky_12,
		sky_13,
		sky_15,
		sky_16,
		sky_17,
		sky_18,
		sky_19,
		sky_20,
		sky_21,
		sky_22,
		sky_23,
		sky_24,
		sky_25,
		sky_26,
		sky_27,
		sky_28,
		sky_29,
		sky_30,
		sky_32,
		sky_33,
		sky_34,
		sky_35a,
		sky_35b,
		sky_37,
		sky_38,
		sky_39,
		sky_40,
		sky_41,
		sky_43,


		End
	}

	public enum MeshType
	{
		None,
		Cube,
		Square,
		Star,
		FourPointStar,
		Diamond,
		Circle,
		Triangle,
		End
	}

	[System.Serializable]
	public class LibraryItem
	{
		public string label;
		public Type type;
		public GameObject prefab;
		public Sprite sprite;
	}

	[System.Serializable]
	public class Mat
	{
		public string label;
		public MatType type;
		public Material material;
	}

	[System.Serializable]
	public class MeshClass
	{
		public string label;
		public MeshType type;
		public Mesh mesh;
	}

	[System.Serializable]
	public class Track
	{
		public string label;
		public TrackType type;
		public AudioClip track;
	}

	[System.Serializable]
	public class BehaviourClass
	{
		public string label;
		public Type type;
		public Object component;
	}

	public LibraryItem GetLibraryItem(Type type)
	{
		for (int i = 0; i < library.Length; i++)
		{
			if (library[i].type == type) {
				return library[i];
			}
		}
		return null;
	}

	public GameObject GetPrefab(Type type)
	{
		for (int i = 0; i < library.Length; i++)
		{
			if (library[i].type == type) { return library[i].prefab; }
		}
		Debug.Log("Library item not found: " + type);
		return null;
	}

	public Object GetBehaviour(Type type)
	{
		for (int i = 0; i < behaviours.Length; i++)
		{
			if (behaviours[i].type == type) { return behaviours[i].component; }
		}
		Debug.Log("Behaviour not found: " + type);
		return null;
	}

	public Material GetMat(MatType type)
	{
		for (int i = 0; i < mats.Length; i++)
		{
			if (mats[i].type == type) { return mats[i].material; }
		}
		Debug.Log("Mat not found: " + type);
		return null;
	}

	public Mesh GetMesh(MeshType type)
	{
		for (int i = 0; i < meshes.Length; i++)
		{
			if (meshes[i].type == type) { return meshes[i].mesh; }
		}
		Debug.Log("Mesh not found: " + type);
		return null;
	}

	public AudioClip GetTrack(TrackType type)
	{
		for (int i = 0; i < tracks.Length; i++)
		{
			if (tracks[i].type == type) { return tracks[i].track; }
		}
		Debug.Log("Track not found: " + type);
		return null;
	}

	public IEnumerator LoadCustomMat()
	{
		WWW localFile = null;
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "CustomSheet.png");
		using (localFile = new WWW(filePath)) {
			yield return localFile;
			customSheetMat.mainTexture = localFile.texture;
		}
	}

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		IEnumerator c;
		c = LoadCustomMat();
		StartCoroutine(c);
	}



}
