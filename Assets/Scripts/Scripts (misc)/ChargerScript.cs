using UnityEngine;

public class ChargerScript : GenericMonsterInheritance
{
	public LayerMask layerMask;//this system of layerMasks is terrible
	public LayerMask blockedByLayersMask;

	public GameObject animatingObject = null;

	RaycastHit hit;
	Ray ray = new Ray();
	float dist = 100;
	int playerIs = 0;
	public float speed = 5;

	bool moving = false;
	bool calledMoving = false;
	void Start()
	{

	}

	void Update()
	{

		if (moving)
		{
			xa.glx = transform.localScale;
			xa.glx.x = -playerIs;
			transform.localScale = xa.glx;
			if (!calledMoving) { moveForward(); }
			calledMoving = true;

		}
		else
		{
			//Debug.Log("Looking: " + Time.time);
			lookLeftAndRight();
		}
		//Debug.Log(playerIs);
	}

	void lookLeftAndRight()
	{
		float PlayerAndBlocks = xa.GetLayer(xa.layers.PlayerAndBlocks);
		playerIs = 0;
		Vector3 pos = transform.position;
		pos.z = PlayerAndBlocks;
		ray.origin = pos;
		ray.direction = transform.right;
		if (Physics.Raycast(ray, out hit, dist, layerMask))
		{
			//Debug.DrawLine(ray.origin, ray.GetPoint(hit.distance), Color.cyan);
			if (hit.collider.gameObject.tag == "playerHitBox")
			{
				playerIs = 1;
				moving = true;
			}
		}
		else
		{
			//Debug.DrawLine(ray.origin, ray.GetPoint(dist), Color.red);
		}

		pos = transform.position;
		pos.z = PlayerAndBlocks;
		ray.origin = pos;
		ray.direction = -transform.right;
		if (Physics.Raycast(ray, out hit, dist, layerMask))
		{
			//Debug.DrawLine(ray.origin, ray.GetPoint(hit.distance), Color.cyan);
			if (hit.collider.gameObject.tag == "playerHitBox")
			{
				playerIs = -1;
				moving = true;
			}
		}
		else
		{
			//Debug.DrawLine(ray.origin, ray.GetPoint(dist), Color.red);
		}
	}

	void moveForward()
	{
		// transform.Translate(speed * playerIs * fa.deltaTime, 0, 0);

		xa.glx = transform.position;
		xa.glx.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
		ray.origin = xa.glx;
		ray.direction = transform.right * playerIs;
		if (Physics.Raycast(ray, out hit, 200, blockedByLayersMask))
		{
			//Debug.DrawLine(ray.origin, ray.GetPoint(hit.distance), Color.blue, 3);


			iTween.MoveTo(this.gameObject, iTween.Hash("x", ray.GetPoint(hit.distance - 0.25f).x, "easetype", iTween.EaseType.linear, "speed", speed, "oncompletetarget", this.gameObject, "oncomplete", "finishedMoving"));
			//iTween.MoveTo(this.gameObject, iTween.Hash("x", ray.GetPoint(hit.distance - 0.25f).x, "easetype", iTween.EaseType.linear, "speed", speed, "oncompletetarget", this.gameObject, "oncomplete", "finishedMoving"));

			animatingObject.SendMessage("playAni2");
		}

	}

	public void finishedMoving()
	{
		moving = false;
		calledMoving = false;
		animatingObject.SendMessage("playAni0");
	}
}
