using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour
{
	public GameObject missile;
	public GameObject muzzlePoint;
	public HealthScript hpscript;
	public GameObject[] nodes = new GameObject[0];
	public GameObject[] portals = new GameObject[0];
	public GameObject puppetHead;
	public GameObject[] bodyChunks = new GameObject[0];
	int spacing = 12;//15;
	float turnSpeed = 75;
	int index = 0;
	List<Vector3> crumbs = new List<Vector3>();
	List<Vector3> crumbsAng = new List<Vector3>();
	float missileDelay = 4.8f;//5 is possible
	float missileTimeset = 0;
	float ammoDelay = 0.4f;
	float ammoTimeset = 0;
	int ammoAmount = 1;
	int firingAmmo = 0;
	bool goToFirstNode = false;

	void Start()
	{
	}

	void Update()
	{
		if (xa.hasCheckpointed)
		{
			if (fa.time > (missileTimeset + missileDelay))
			{
				missileTimeset = fa.time;
				firingAmmo = ammoAmount;
			}

			if (firingAmmo > 0)
			{
				if (fa.time > (ammoTimeset + ammoDelay))
				{
					firingAmmo --;
					ammoTimeset = fa.time;
					GameObject go = Instantiate(missile, muzzlePoint.transform.position,muzzlePoint.transform.rotation);
				}
			}


			if (!goToFirstNode) { goToFirstNode = true; GotoNode(0); }
			if (hpscript.health <= 0)
			{
				for (int i = 0; i < portals.Length; i++)
				{
					if (portals[i].transform.position.x > xa.player.transform.position.x)
					{
						portals[i].SetActive(true);
						break;
					}
				}

				for (int i = 0; i < bodyChunks.Length; i++)
				{
					iTween.MoveBy(bodyChunks[i], iTween.Hash("x", Random.Range(-5, 5), "y", Random.Range(-5, 5), "easetype", iTween.EaseType.easeInOutSine, "time", 4));

				}
			}
			else
			{
				crumbs.Add(transform.position);
				crumbsAng.Add(transform.localEulerAngles);
				if (crumbs.Count > 500)
				{
					crumbs.RemoveAt(0);
					crumbsAng.RemoveAt(0);
				}

				int gap = crumbs.Count;
				gap -= 1;
				for (int i = 0; i < bodyChunks.Length; i++)
				{
					if (gap > 0)
					{
						bodyChunks[i].transform.position = crumbs[gap];
						bodyChunks[i].transform.localEulerAngles = crumbsAng[gap];
					}
					gap -= spacing;
				}

				if (!xa.hasCheckpointed) { return; }
				Vector3 v = nodes[index].transform.position;
				v.z = transform.position.z;
				if (Vector3.Distance(v, transform.position) < 1)
				{
					index++;
					if (index >= nodes.Length) { index = 0; }

					GotoNode(index);
				}


				PointAtGoal();
			}
		}

	}

	void GotoNode(int i)
	{
		float speed = 5;
		spacing = 15; 
		turnSpeed = 75;
		if(xa.playerPos.x < transform.position.x) {speed = 3; spacing = 15; }
		if(xa.playerPos.x > transform.position.x) {speed = 9; spacing = 13; turnSpeed = 95; }
		if(xa.playerPos.x > (transform.position.x + 15)) {speed = 13f; spacing = 8;  turnSpeed = 120; }
		if(xa.playerPos.x > (transform.position.x + 26)) {speed = 16f; spacing = 5; turnSpeed = 155; }
		//iTween.MoveTo(this.gameObject, iTween.Hash("delay", 0.1f, "time", speed, "easetype", iTween.EaseType.easeInOutSine, "x", nodes[i].transform.position.x, "y", nodes[i].transform.position.y));
		iTween.MoveTo(this.gameObject, iTween.Hash("delay", 0.1f, "speed", speed, "easetype", iTween.EaseType.easeInOutSine, "x", nodes[i].transform.position.x, "y", nodes[i].transform.position.y));
	}

	void PointAtGoal()
	{
		Vector3 target = nodes[index].transform.position;
		target.z = transform.position.z;
		//float speed = 55;
		//transform.Translate(speed * fa.deltaTime, 0.0f, 0.0f);

		Vector3 straight = transform.localEulerAngles;
		Vector3 right = straight;
		Vector3 left = straight;
		right.z += 4;
		left.z -= 4;

		Vector3 projStraight = Setup.projectVec(transform.position, straight, 5, Vector3.right);
		Vector3 projRight = Setup.projectVec(transform.position, right, 5, Vector3.right);
		Vector3 projLeft = Setup.projectVec(transform.position, left, 5, Vector3.right);

		float targetDistStraight = Vector3.Distance(target, projStraight);
		float targetDistRight = Vector3.Distance(target, projRight);
		float targetDistLeft = Vector3.Distance(target, projLeft);

		float targetDistance = Vector3.Distance(target, transform.position);


		Vector3 currentHeading = transform.localEulerAngles;
		if (targetDistStraight > targetDistRight || targetDistStraight > targetDistLeft)
		{
			if (targetDistRight < targetDistLeft)
			{
				currentHeading.z += turnSpeed * fa.deltaTime;
			}
			else
			{
				currentHeading.z -= turnSpeed * fa.deltaTime;
			}
		}
		transform.localEulerAngles = currentHeading;

	}
}
