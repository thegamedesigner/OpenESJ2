using System.Collections.Generic;
using UnityEngine;

public class FreshBoostPlatformScript : MonoBehaviour
{
	public MovingPlat myClass = new MovingPlat();

	public enum State
	{
		None,
		Launching,
		WaitingAtTop,
		Resetting,
		WaitingAtBottom,
		End
	}

	public static List<MovingPlat> movingPlats = new List<MovingPlat>();
	[System.Serializable]
	public class MovingPlat
	{
		public float boostAdd = 0.5f;
		public float delayAtTop = 1;
		public float delayAtBottom = 1;
		public float launchSpeed = 15;
		public float resetSpeed = 5;
		public float timeset = 0;
		public GameObject go;
		public GameObject node;
		public Collider hitbox;
		public Vector3 startPos;
		public Vector3 nodePos;
		public State state = State.None;
		public Vector2 vel = new Vector2(0, 0);

		public void Init()
		{
			//Debug.Log("init this plat");
			state = State.Launching;
			startPos = go.transform.position;
			nodePos = node.transform.position;
			node.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	void Awake()
	{
		movingPlats = new List<MovingPlat>();
	}


	void Start()
	{
		movingPlats.Add(myClass);
		myClass.Init();

	}


	public static void HandleFreshMovingPlatforms()//Called at the end of NovaPlayer update()
	{
		if (xa.player == null) { return; }
		GameObject pl = xa.player;
		//Update platforms
		for (int i = 0; i < movingPlats.Count; i++)
		{
			MovingPlat m = movingPlats[i];
			m.vel = new Vector2(0, 0);
			//Debug.Log(m.state);
			switch (m.state)
			{
				case State.Launching:
					m.vel.y = m.launchSpeed * fa.deltaTime;
					m.go.transform.Translate(new Vector3(0, m.launchSpeed * fa.deltaTime, 0));
					if (m.go.transform.position.y > m.nodePos.y) { m.timeset = fa.time; m.state = State.WaitingAtTop; m.go.transform.SetY(m.nodePos.y); }
					break;
				case State.WaitingAtTop:
					if (fa.time > (m.timeset + m.delayAtTop))
					{
						m.state = State.Resetting;
					}
					break;
				case State.Resetting:
					m.vel.y = -m.resetSpeed * fa.deltaTime;
					m.go.transform.Translate(new Vector3(0, -m.resetSpeed * fa.deltaTime, 0));
					if (m.go.transform.position.y < m.startPos.y) { m.timeset = fa.time; m.state = State.WaitingAtBottom; m.go.transform.SetY(m.startPos.y); }
					break;
				case State.WaitingAtBottom:
					if (fa.time > (m.timeset + m.delayAtBottom))
					{
						m.state = State.Launching;
					}
					break;

			}
		}

	}
	
	public static void CheckIfOnMovingPlat()
	{
		if (xa.player == null) { return; }
		if (xa.playerScript == null) { return; }
		xa.playerScript.onFreshMovingPlat = false;

		GameObject pl = xa.player;
		//Is the player on the ground?
		if (xa.playerOnGround)
		{
			//Am I on top of a fresh moving plat?
			LayerMask blockMask = 1 << 19;//Only hits hitboxes on the NovaBlock layer
			Ray ray = new Ray();
			RaycastHit hit;
			ray.origin = new Vector3(pl.transform.position.x, pl.transform.position.y, xa.GetLayer(xa.layers.RaycastLayer));
			ray.direction = new Vector3(0, -1, 0);
			if (Physics.Raycast(ray, out hit, 1, blockMask))
			{
				Info info = hit.collider.gameObject.GetComponent<Info>();
				if (info != null)
				{
					if (info.movingPlatScript != null)
					{
						if (info.movingPlatScript.myClass.vel.y > 0)
						{
							xa.playerScript.onFreshMovingPlat = true;
							xa.playerScript.movingPlatVelY = info.movingPlatScript.myClass.boostAdd;
							float zDepth = xa.GetLayer(xa.layers.GribblyFront0);
							Debug.DrawLine(new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), zDepth), new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), zDepth), Color.cyan);
							Debug.DrawLine(new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), zDepth), new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), zDepth), Color.cyan);

							Debug.DrawLine(new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), zDepth), new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), zDepth), Color.cyan);
							Debug.DrawLine(new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), zDepth), new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), zDepth), Color.cyan);

						}
						//Hey, I'm standing above a moving platform AND I'm on the ground! I'm probably on it!
						//pl.transform.SetY(pl.transform.position.y + info.movingPlatScript.myClass.vel.y);
					}
				}
			}


		}

	}

	public static void StickMeToMovingPlatform()
	{
		if (xa.player == null) { return; }
		if (xa.playerScript == null) { return; }

		GameObject pl = xa.player;
		//Is the player on the ground?
		if (!xa.playerJumped)
		{
			//Am I on top of a fresh moving plat?
			LayerMask blockMask = 1 << 19;//Only hits hitboxes on the NovaBlock layer
			Ray ray = new Ray();
			RaycastHit hit;
			ray.origin = new Vector3(pl.transform.position.x, pl.transform.position.y, xa.GetLayer(xa.layers.RaycastLayer));
			ray.direction = new Vector3(0, -1, 0);
			if (Physics.Raycast(ray, out hit, 1, blockMask))
			{
				Info info = hit.collider.gameObject.GetComponent<Info>();
				if (info != null)
				{
					if (info.movingPlatScript != null)
					{
						pl.transform.SetY(hit.point.y + (xa.playerScript.plHeight * 0.5f));
					}
				}
			}


		}

	}
}
