using System.Collections.Generic;
using Structs;
using UnityEngine;

// TODO: Refactor this into smaller classes
[System.Diagnostics.DebuggerDisplay("{type} Prefab:{prefabId} Behaviour:{behaviourId}")]
public class ButtEntity
{
	public GameObject sceneReference               = null;
	public Vector2 pos                             = new Vector2(0, 0);
	public float zPos                              = -1.0f;
	public Vector2 scale                           = Vector2.one;
	public float tilt                              = 0.0f;

	public int prefabId                            = -1;
	public int behaviourId                         = -1;
	public FrEdLibrary.Type type                   = FrEdLibrary.Type.None;

	public float createLate                        = -1;
	public string destinationLevel                 = null;//for Portals
	public string destinationButt                  = null;//for Portals
	public bool noSound                            = false;//For portals
	public int uId                                 = -1;
	public int int1                                = 0;
	public int easing                              = 0;
	public float speed                             = float.NegativeInfinity;//speed of anything that moves in a direction
	public float lifespan                          = float.NegativeInfinity;//lifespan for things that die
	public int ammoId                              = -1;//id to the prefab that is the ammo type
	public float firingDelay                       = -1;
	public float turningSpeed                      = float.NegativeInfinity;//turning speed of homing missile
	public StateBasedCamera.stateTypes cameraState = StateBasedCamera.stateTypes.None;
	public Vector2 nodePos                         = Vector2.negativeInfinity;
	public Vector2 min                             = new Vector2(float.MinValue, float.MinValue);
	public Vector2 max                             = new Vector2(float.MaxValue, float.MaxValue);
	public float track                             = 0.0f;
	public List<Structs.FFrame> frames             = new List<Structs.FFrame>();
	public int aniId                               = -1;//the id of the animation behaviour
	public bool checkFloor                         = true;

	public FrEdLibrary.MeshType mesh               = FrEdLibrary.MeshType.None;
	public float emissionPerSecond                 = 10.0f;
	public float startSpeedMin                     = 5.0f;
	public float startSpeedMax                     = 5.0f;
	public float startSizeMin                      = 5.0f;
	public float startSizeMax                      = 5.0f;
	public float startLifespanMin                  = 5.0f;
	public float startLifespanMax                  = 5.0f;
	public ParticleSystemShapeType shape           = ParticleSystemShapeType.Box;
	public Vector3 shapeScale                      = Vector3.one;
	public Color color1                            = Color.black;
	public Color color2                            = Color.black;
	public int skyBands                            = 0;
	public float alpha1                            = 0.0f;
	public float alpha2                            = 0.0f;
	public float alpha3                            = 0.0f;
	public float alpha4                            = 0.0f;
	public float alpha5                            = 0.0f;
	public float rotationMin                       = 0.0f;
	public float rotationMax                       = 0.0f;

	public void Initialize()//For setting type-specfic default stats (like zLayers)
	{
		float gribblyFrontLayer    = xa.GetLayer(xa.layers.GribblyFront);
		float gribblyBehindLayer   = xa.GetLayer(xa.layers.GribblyBehind);
		float playerAndBlocksLayer = xa.GetLayer(xa.layers.PlayerAndBlocks);
		float monsterLayer         = xa.GetLayer(xa.layers.Monsters);
		float homingMissileLayer   = xa.GetLayer(xa.layers.HomingMissile);

		switch (this.type) {
			case FrEdLibrary.Type.Block:
				if (this.zPos == -1) this.zPos = playerAndBlocksLayer;
				break;
			case FrEdLibrary.Type.PlayerSpawn:
				if (this.zPos == -1) this.zPos = playerAndBlocksLayer;
				if (this.createLate == -1) this.createLate = 0.02f;
				break;
			case FrEdLibrary.Type.SpikeBall:
				if (this.zPos == -1) this.zPos = monsterLayer;
				break;
			case FrEdLibrary.Type.Portal:
				if (this.zPos == -1) this.zPos = xa.GetLayer(xa.layers.GribblyBehind2);
				if (string.IsNullOrEmpty(this.destinationLevel)) this.destinationLevel = "MegaMetaWorld";
				break;
			case FrEdLibrary.Type.Frog:
				if (this.zPos == -1) this.zPos = monsterLayer;
				if (this.firingDelay == -1) this.firingDelay = 2.0f;
				break;
			case FrEdLibrary.Type.NinjaStar:
				if (this.zPos == -1) this.zPos = playerAndBlocksLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 5.0f;
				if (float.IsNegativeInfinity(this.lifespan)) this.lifespan = 2.0f;
				break;
			case FrEdLibrary.Type.RoundBullet:
				if (this.zPos == -1) this.zPos = playerAndBlocksLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 5.0f;
				if (float.IsNegativeInfinity(this.lifespan)) this.lifespan = 3.0f;
				break;
			case FrEdLibrary.Type.HomingMissile:
				if (this.zPos == -1) this.zPos = homingMissileLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 7.0f;
				if (float.IsNegativeInfinity(this.lifespan)) this.lifespan = 8.0f;
				if (float.IsNegativeInfinity(this.turningSpeed)) this.turningSpeed = 55.0f;
				break;
			case FrEdLibrary.Type.SpikyHomingMissile:
				if (this.zPos == -1) this.zPos = homingMissileLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 7.0f;
				if (float.IsNegativeInfinity(this.lifespan)) this.lifespan = 8;
				if (float.IsNegativeInfinity(this.turningSpeed)) this.turningSpeed = 55;
				break;
			case FrEdLibrary.Type.ShrineDoubleJump:
				if (this.zPos == -1) this.zPos = gribblyBehindLayer;
				break;
			case FrEdLibrary.Type.ShrineTripleJump:
				if (this.zPos == -1) this.zPos = gribblyBehindLayer;
				break;
			case FrEdLibrary.Type.ShrineQuintupleJump:
				if (this.zPos == -1) this.zPos = gribblyBehindLayer;
				break;
			case FrEdLibrary.Type.ShrineAirSword:
				if (this.zPos == -1) this.zPos = gribblyBehindLayer;
				break;
			case FrEdLibrary.Type.ShrineButtSmash:
				if (this.zPos == -1) this.zPos = gribblyBehindLayer;
				break;
			case FrEdLibrary.Type.ShrineRemoveAbilities:
				if (this.zPos == -1) this.zPos = gribblyBehindLayer;
				break;
			case FrEdLibrary.Type.Laser:
				if (this.zPos == -1) this.zPos = monsterLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 0.0f;
				break;
			case FrEdLibrary.Type.QuadLaser:
				if (this.zPos == -1) this.zPos = monsterLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 15.0f;
				break;
			case FrEdLibrary.Type.BounceArrow:
				if (this.zPos == -1) this.zPos = gribblyFrontLayer;
				break;
			case FrEdLibrary.Type.Coin:
				if (this.zPos == -1) this.zPos = gribblyFrontLayer;
				break;
			case FrEdLibrary.Type.Firestick:
				if (this.zPos == -1)  this.zPos = gribblyFrontLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 200.0f;
				break;
			case FrEdLibrary.Type.CameraTrigger:
				if (this.zPos == -1) this.zPos = xa.GetLayer(xa.layers.GribblyFront3);
				break;
			case FrEdLibrary.Type.Particles:
				if (this.zPos == -1) this.zPos = xa.GetLayer(xa.layers.Background4);
				break;
			case FrEdLibrary.Type.Decoration:
				if (this.zPos == -1) this.zPos = xa.GetLayer(xa.layers.GribblyBehind3);
				break;
			case FrEdLibrary.Type.Buffalo:
				if (this.zPos == -1) this.zPos = monsterLayer;
				if (float.IsNegativeInfinity(this.speed)) this.speed = 0.0f;
				break;
		}
	}

	public int SetProperty(string label, string value)//returns true if it's a prefab
	{
		switch (label) {
			case "prefab":
				int.TryParse(value, out this.prefabId);
				if (this.prefabId > 0) {
					return 1;
				}
			return 0;
			case "behaviour":
				int.TryParse(value, out this.behaviourId);
				if (this.behaviourId > 0) {
					return 2;
				}
			return 0;
			case "type": this.type = value.ToEnum<FrEdLibrary.Type>(); return 0;
			case "x": float.TryParse(value, out this.pos.x); return 0;
			case "y": float.TryParse(value, out this.pos.y); return 0;
			case "pos": this.pos = this.GetVec2(value); return 0;
			case "xScale": float.TryParse(value, out this.scale.x); return 0;
			case "yScale": float.TryParse(value, out this.scale.y); return 0;
			case "scale": this.scale = this.GetVec2(value); return 0;
			case "tilt": float.TryParse(value, out this.tilt); return 0;
			case "destinationLevel": this.destinationLevel = value; return 0;
			case "destinationButt": this.destinationButt = value; return 0;
			case "noSound": bool.TryParse(value, out this.noSound); return 0;
			case "createLate": float.TryParse(value, out this.createLate); return 0;
			case "int1": int.TryParse(value, out this.int1); return 0;
			case "easing": int.TryParse(value, out this.easing); return 0;
			case "speed": float.TryParse(value, out this.speed); return 0;
			case "lifespan": float.TryParse(value, out this.lifespan); return 0;
			case "ammo": int.TryParse(value, out this.ammoId); return 0;
			case "firingDelay": float.TryParse(value, out this.firingDelay); return 0;
			case "turningSpeed": float.TryParse(value, out this.turningSpeed); return 0;
			case "cameraState": this.cameraState = value.ToEnum<StateBasedCamera.stateTypes>(); return 0;
			case "nodePosX": float.TryParse(value, out this.nodePos.x); return 0;
			case "nodePosY": float.TryParse(value, out this.nodePos.y); return 0;
			case "nodePos": this.nodePos = this.GetVec2(value); return 0;
			case "track": float.TryParse(value, out this.track); return 0;
			case "minX": float.TryParse(value, out this.min.x); return 0;
			case "minY": float.TryParse(value, out this.min.y); return 0;
			case "min": this.min = this.GetVec2(value); return 0;
			case "maxX": float.TryParse(value, out this.max.x); return 0;
			case "maxY": float.TryParse(value, out this.max.y); return 0;
			case "max": this.max = this.GetVec2(value); return 0;
			case "ani": int.TryParse(value, out this.aniId); return 0;
			case "frame": this.frames.Add(this.GetFFrame(value)); return 0;
			case "checkFloor": bool.TryParse(value, out this.checkFloor); return 0;

			case "mesh": this.mesh = value.ToEnum<FrEdLibrary.MeshType>(); return 0;
			case "emissionPerSecond": float.TryParse(value, out this.emissionPerSecond); return 0;
			case "speedMin": float.TryParse(value, out this.startSpeedMin); return 0;
			case "speedMax": float.TryParse(value, out this.startSpeedMax); return 0;
			case "startSizeMin": float.TryParse(value, out this.startSizeMin); return 0;
			case "startSizeMax": float.TryParse(value, out this.startSizeMax); return 0;
			case "lifespanMin": float.TryParse(value, out this.startLifespanMin); return 0;
			case "lifespanMax": float.TryParse(value, out this.startLifespanMax); return 0;
			case "shape": this.shape = value.ToEnum<ParticleSystemShapeType>(); return 0;
			case "shapeScaleX": float.TryParse(value, out this.shapeScale.x); return 0;
			case "shapeScaleY": float.TryParse(value, out this.shapeScale.y); return 0;
			case "shapeScaleZ": float.TryParse(value, out this.shapeScale.z); return 0;
			case "shapeScale": this.shapeScale = this.GetVec3(value); return 0;
			case "colour1": this.color1 = this.GetColor(value); return 0;
			case "colour2": this.color2 = this.GetColor(value); return 0;
			case "color1": this.color1 = this.GetColor(value); return 0;
			case "color2": this.color2 = this.GetColor(value); return 0;
			case "skyBands" : int.TryParse(value, out this.skyBands); return 0;
			case "alpha1": float.TryParse(value, out this.alpha1); return 0;
			case "alpha2": float.TryParse(value, out this.alpha2); return 0;
			case "alpha3": float.TryParse(value, out this.alpha3); return 0;
			case "alpha4": float.TryParse(value, out this.alpha4); return 0;
			case "alpha5": float.TryParse(value, out this.alpha5); return 0;
			case "rotationMin": float.TryParse(value, out this.rotationMin); return 0;
			case "rotationMax": float.TryParse(value, out this.rotationMax); return 0;
		}

		return 0;
	}

	// Refactor this so that GameObject is an entity of some type and does its own thing
	public void ApplyToGameObject(GameObject go)
	{
		if (this.type == FrEdLibrary.Type.Buffalo) {
			GoomaScript buffalo    = go.GetComponent<GoomaScript>();
			buffalo.speed          = this.speed;
			buffalo.checkForFloors = this.checkFloor;
			buffalo.move           = buffalo.speed != 0.0f;
			if (!buffalo.move) {
				buffalo.checkForFloors = false;
				this.checkFloor        = false;
			}
		}

		if (this.type == FrEdLibrary.Type.MovingSpikePlatform) {
			NovaMovingBlock block = go.GetComponent<NovaMovingBlock>();
			//block.InitFromEditor(this.nodePos, this.speed, this.easing);
		}

		//apply stats for decoration
		if (this.type == FrEdLibrary.Type.Decoration) {
			if (this.aniId != -1) {
				FrEdAniScript aniScript = go.AddComponent<FrEdAniScript>();
				FrEdInfo infoScript     = go.GetComponent<FrEdInfo>();
				aniScript.go            = infoScript.puppet;
				aniScript.meshFilter    = aniScript.go.GetComponent<MeshFilter>();
				aniScript.aniId         = this.aniId;
			}
		}

		//apply stats for particle system
		if (this.type == FrEdLibrary.Type.Particles) {
			ParticleSystem ps = go.GetComponentInChildren<ParticleSystem>();
			var em = ps.emission;
			em.rateOverTime = this.emissionPerSecond;

			var ma = ps.main;
			var sz = ma.startSize;
			sz.mode = ParticleSystemCurveMode.TwoConstants;
			sz.constantMin = this.startSizeMin;
			sz.constantMax = this.startSizeMax;
			ma.startSize = sz;
			//Debug.Log("setting size: " + this.startSizeMin + ", " + this.startSizeMax);

			var sp = ma.startSpeed;
			sp.constantMin = this.startSpeedMin;
			sp.constantMax = this.startSpeedMax;
			sp.mode = ParticleSystemCurveMode.TwoConstants;
			ma.startSpeed = sp;

			var lf = ma.startLifetime;
			lf.constantMin = this.startLifespanMin;
			lf.constantMax = this.startLifespanMax;
			lf.mode = ParticleSystemCurveMode.TwoConstants;
			ma.startLifetime = lf;

			var sh = ps.shape;
			sh.shapeType = this.shape;
			sh.scale = this.shapeScale;

			var cl = ps.colorOverLifetime;
			cl.enabled = true;
			Gradient grad = new Gradient();
			grad.SetKeys(
				new GradientColorKey[] {
					new GradientColorKey(this.color1, 0.0f),
					new GradientColorKey(this.color2, 1.0f)
				},
				new GradientAlphaKey[] {
					new GradientAlphaKey(this.alpha1, 0.0f),
					new GradientAlphaKey(this.alpha2, 0.25f),
					new GradientAlphaKey(this.alpha3, 0.5f),
					new GradientAlphaKey(this.alpha4, 0.75f),
					new GradientAlphaKey(this.alpha5, 1.0f)
				}
			);
			cl.color = grad;

			var rt = ps.rotationOverLifetime;
			rt.enabled = true;
			rt.separateAxes = true;
			var rtz = rt.z;
			rtz.constantMin = this.rotationMin;
			rtz.constantMax = this.rotationMax;
			rtz.mode = ParticleSystemCurveMode.TwoConstants;
			rt.z = rtz;

			ParticleSystemRenderer psr = go.GetComponentInChildren<ParticleSystemRenderer>();
			psr.mesh = FrEdLibrary.instance.GetMesh(this.mesh);
		}

		//apply stats for camera trigger
		if (this.type == FrEdLibrary.Type.CameraTrigger) {
			SetStateCameraState script = go.GetComponentInChildren<SetStateCameraState>();
			script.state = this.cameraState;
			script.pos.x = this.nodePos.x;
			script.pos.y = this.nodePos.y;
			script.scrollSpeed = this.speed;
			script.trackXOrY = this.track;
			script.minX = this.min.x;
			script.maxX = this.max.x;
			script.minY = this.min.y;
			script.maxY = this.max.y;

			script.setState = true;
			script.setPos = true;
			script.setScrollSpeed = true;
			script.setTrackXOrY = true;
			script.setMinX = true;
			script.setMaxX = true;
			script.setMinY = true;
			script.setMaxY = true;
		}

		//apply stats for firestick
		if (this.type == FrEdLibrary.Type.Firestick) {
			Rotate2Script script = go.GetComponentInChildren<Rotate2Script>();
			script.speed.z = this.speed;
		}

		//apply stats for laser
		if (this.type == FrEdLibrary.Type.Laser) {
			Rotate2Script script = go.GetComponent<Rotate2Script>();
			script.speed.z = this.speed;
		}

		//apply stats for quadlaser
		if (this.type == FrEdLibrary.Type.QuadLaser) {
			Rotate2Script script = go.GetComponent<Rotate2Script>();
			script.speed.z = this.speed;
		}

		//apply stats for frog
		if (this.type == FrEdLibrary.Type.Frog) {
			FrEdFrogScript frogScript = go.GetComponent<FrEdFrogScript>();
			frogScript.ammoId = this.ammoId;
			frogScript.delay = this.firingDelay;
		}

		//set speed & lifespan for ninja stars
		if (this.type == FrEdLibrary.Type.NinjaStar) {
			//Debug.Log("APPLYING NINJA STAR STATS");
			FreshBulletScript freshBulletScript = go.GetComponent<FreshBulletScript>();
			freshBulletScript.speed = this.speed;
			freshBulletScript.lifespan = this.lifespan;
		}

		//set speed & lifespan for missiles
		if (this.type == FrEdLibrary.Type.HomingMissile ||
		    this.type == FrEdLibrary.Type.SpikyHomingMissile)
		{
			FreshHomingMissileScript homingMissileScript = go.GetComponent<FreshHomingMissileScript>();
			homingMissileScript.slowMissile = false;
			homingMissileScript.speed = this.speed;
			homingMissileScript.lifespanInSeconds = this.lifespan;
			homingMissileScript.turnSpeed = this.turningSpeed;
			homingMissileScript.lockToThisZ = this.zPos;
		}

		//set tilt
		if (this.type != FrEdLibrary.Type.Block) {
			go.transform.SetAngZ(this.tilt);
		}

		//set scale
		go.transform.SetScaleX(this.scale.x);
		go.transform.SetScaleY(this.scale.y);

		//Destination level
		if (this.type == FrEdLibrary.Type.Portal) {
			PortalScript portalScript = go.GetComponent<PortalScript>();
			if (portalScript != null) {
				bool isButtLevel = !string.IsNullOrEmpty(this.destinationButt);
				string level = (isButtLevel)? this.destinationButt : this.destinationLevel;
				portalScript.Initialize(level, isButtLevel, this.noSound);
			}
		}

		//set generic info stats
		FrEdInfo info = go.GetComponent<FrEdInfo>();
		if (info != null) {
			info.int1 = this.int1;
			info.set = true;
		}
	}

	private FFrame GetFFrame(string value)
	{
		FFrame f = new FFrame(-1, 0, 0, 0, 0);

		string[] parts = value.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length >= 5)
		{
			int.TryParse(parts[0], out f.order);
			int.TryParse(parts[1], out f.x);
			int.TryParse(parts[2], out f.y);
			int.TryParse(parts[3], out f.size);
			float.TryParse(parts[3], out f.time);
		}
		Debug.Log("FRAME:" + parts);
		for(int i = 0;i < parts.Length;i++)
		{
			Debug.Log(parts[i]);
		}

		return f;
	}

	private Color GetColor(string value)
	{
		Color32 c = Color.black;

		string[] parts = value.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length >= 3) {
			byte.TryParse(parts[0], out c.r);
			byte.TryParse(parts[1], out c.g);
			byte.TryParse(parts[2], out c.b);
			if (parts.Length >= 4) {
				byte.TryParse(parts[3], out c.a);
			}
		}

		return c;
	}

	private Vector3 GetVec3(string value)
	{
		Vector3 vec = Vector3.zero;
		string[] parts = value.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length >= 3) {
			float.TryParse(parts[0], out vec.x);
			float.TryParse(parts[1], out vec.y);
			float.TryParse(parts[2], out vec.z);
		}
		return vec;
	}

	private Vector2 GetVec2(string value)
	{
		Vector2 vec = Vector2.zero;
		string[] parts = value.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length >= 2) {
			float.TryParse(parts[0], out vec.x);
			float.TryParse(parts[1], out vec.y);
		}
		return vec;
	}

	public string Serialize()
	{
		List<string> output = new List<string>();

		if (this.prefabId > 0) {
			output.Add(Utils.SerializeButtEntityText("prefab", this.prefabId));
		}
		if (this.behaviourId > 0) {
			output.Add(Utils.SerializeButtEntityText("behaviour", this.behaviourId));
		}
		output.Add(Utils.SerializeButtEntityText("type", this.type));
		output.Add(Utils.SerializeButtEntityText("pos", this.pos));

		if (this.zPos != -1.0f) {
			output.Add(Utils.SerializeButtEntityText("zPos", this.zPos));
		}

		if (this.int1 != 0) {
			output.Add(Utils.SerializeButtEntityText("int1", this.int1));
		}

		if (this.type == FrEdLibrary.Type.Buffalo && !this.checkFloor) { // only serialize if it's not default.
			output.Add(Utils.SerializeButtEntityText("checkFloor", this.checkFloor));
		}

		if (this.createLate > 0) {
			output.Add(Utils.SerializeButtEntityText("createLate", this.createLate));
		}
		if (this.scale != Vector2.one) {
			output.Add(Utils.SerializeButtEntityText("scale", this.scale));
		}
		if (this.tilt != 0.0f) {
			output.Add(Utils.SerializeButtEntityText("tilt", this.tilt));
		}

		if (this.type == FrEdLibrary.Type.Portal) {
			if (!string.IsNullOrEmpty(this.destinationLevel)) {
				output.Add(Utils.SerializeButtEntityText("destinationLevel", this.destinationLevel));
			}
			if (!string.IsNullOrEmpty(this.destinationButt)) {
				output.Add(Utils.SerializeButtEntityText("destinationButt", this.destinationButt));
			}
			if (this.noSound) {
				output.Add(Utils.SerializeButtEntityText("noSound", this.noSound));
			}
		}

		if (this.easing > 0) {
			output.Add(Utils.SerializeButtEntityText("easing", this.easing));
		}
		if (!float.IsNegativeInfinity(this.speed)) {
			output.Add(Utils.SerializeButtEntityText("speed", this.speed));
		}
		if (!float.IsNegativeInfinity(this.lifespan)) {
			output.Add(Utils.SerializeButtEntityText("lifespan", this.lifespan));
		}
		if (this.ammoId > 0) {
			output.Add(Utils.SerializeButtEntityText("ammo", this.ammoId));
		}
		if (this.firingDelay > 0) {
			output.Add(Utils.SerializeButtEntityText("firingDelay", this.firingDelay));
		}
		if (!float.IsNegativeInfinity(this.turningSpeed)) {
			output.Add(Utils.SerializeButtEntityText("turningSpeed", this.turningSpeed));
		}
		if (this.cameraState != StateBasedCamera.stateTypes.None) {
			output.Add(Utils.SerializeButtEntityText("cameraState", this.cameraState));
		}
		if (!float.IsNegativeInfinity(this.nodePos.x) && !float.IsNegativeInfinity(this.nodePos.y)) {
			output.Add(Utils.SerializeButtEntityText("nodePos", this.nodePos));
		}
		if (this.min.x > float.MinValue || this.min.y > float.MinValue) {
			output.Add(Utils.SerializeButtEntityText("min", this.min));
		}
		if (this.max.x < float.MaxValue || this.max.y < float.MaxValue) {
			output.Add(Utils.SerializeButtEntityText("min", this.min));
		}
		if (this.track != 0.0f) {
			output.Add(Utils.SerializeButtEntityText("track", this.track));
		}
		//if (this.frames > 0) {
		//}
		//if (this.aniId > 0) {
		//}

		if (this.type == FrEdLibrary.Type.Particles) {
			if (this.mesh != FrEdLibrary.MeshType.None) {
				output.Add(Utils.SerializeButtEntityText("mesh", this.mesh));
			}
			if (this.emissionPerSecond != 10.0f) {
				output.Add(Utils.SerializeButtEntityText("emissionPerSecond", this.emissionPerSecond));
			}
			if (this.startSpeedMin != 5.0f) {
				output.Add(Utils.SerializeButtEntityText("startSpeedMin", this.startSpeedMin));
			}
			if (this.startSpeedMax != 5.0f) {
				output.Add(Utils.SerializeButtEntityText("startSpeedMax", this.startSpeedMax));
			}
			if (this.startSizeMin != 5.0f) {
				output.Add(Utils.SerializeButtEntityText("startSizeMin", this.startSizeMin));
			}
			if (this.startSizeMax != 5.0f) {
				output.Add(Utils.SerializeButtEntityText("startSizeMax", this.startSizeMax));
			}
			if (this.startLifespanMin != 5.0f) {
				output.Add(Utils.SerializeButtEntityText("startLifespanMin", this.startLifespanMin));
			}
			if (this.startLifespanMax != 5.0f) {
				output.Add(Utils.SerializeButtEntityText("startLifespanMax", this.startLifespanMax));
			}
			if (this.shape != ParticleSystemShapeType.Box) {
				output.Add(Utils.SerializeButtEntityText("shape", this.shape));
			}
			if (this.shapeScale != Vector3.one) {
				output.Add(Utils.SerializeButtEntityText("shapeScale", this.shapeScale));
			}
		}

		if (this.type == FrEdLibrary.Type.SetSky && this.skyBands > 0) {
			output.Add(Utils.SerializeButtEntityText("skyBands", this.skyBands));
		}
		if (this.color1 != Color.black) {
			output.Add(Utils.SerializeButtEntityText("color1", this.color1));
		}
		if (this.color2 != Color.black) {
			output.Add(Utils.SerializeButtEntityText("color2", this.color2));
		}
		if (this.type == FrEdLibrary.Type.Particles) {
			if (this.alpha1 >= 0.0f) {
				output.Add(Utils.SerializeButtEntityText("alpha1", this.alpha1));
			}
			if (this.alpha2 >= 0.0f) {
				output.Add(Utils.SerializeButtEntityText("alpha2", this.alpha2));
			}
			if (this.alpha3 >= 0.0f) {
				output.Add(Utils.SerializeButtEntityText("alpha3", this.alpha3));
			}
			if (this.alpha4 >= 0.0f) {
				output.Add(Utils.SerializeButtEntityText("alpha4", this.alpha4));
			}
			if (this.alpha5 >= 0.0f) {
				output.Add(Utils.SerializeButtEntityText("alpha5", this.alpha5));
			}
			if (this.rotationMin > 0.0f) {
				output.Add(Utils.SerializeButtEntityText("rotationMin", this.rotationMin));
			}
			if (this.rotationMax > 0.0f) {
				output.Add(Utils.SerializeButtEntityText("rotationMax", this.rotationMax));
			}
		}

		return string.Join(",", output.ToArray()) + ";";
	}
}
