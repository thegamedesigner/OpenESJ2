using UnityEngine;
using System.Collections;

public class MerpsCollideWithBlocks : MonoBehaviour
{
	public GameObject[] createSpecialDeathExploOn_UpDownLeftRight = new GameObject[4];
	public GameObject killThisGO                                  = null;
	public GameObject deathExplo                                  = null;
	public float widthLeft                                        = 0.5f;
	public float widthRight                                       = 0.5f;
	public float heightUp                                         = 0.5f;
	public float heightDown                                       = 0.5f;
	public bool[] dontRayCast_UpDownLeftRight                     = new bool[4];
	public bool dieOnImpact                                       = false;
	public bool justDestroyAndCreateExplo                         = false;
	Ray ray                                                       = new Ray();
	RaycastHit hit;
	float checkDist                                               = 1.0f;
	int dir                                                       = 0;
	bool triggered                                                = false;
	bool result                                                   = false;

	void Update()
	{
		triggered = false;

		if (!dontRayCast_UpDownLeftRight[0])
		{
			ray.direction = new Vector3(0, checkDist, 0);
			ray.origin = this.gameObject.transform.position;
			Debug.DrawLine(ray.origin, ray.GetPoint(heightUp), Color.red);
			if (Physics.Raycast(ray, out hit, heightUp, 11) == true)
			{
				if (hit.collider.gameObject.tag == "solidThing")
				{
					xa.glx = transform.position;
					xa.glx.x = hit.point.x;
					xa.glx.y = hit.point.y;
					transform.position = xa.glx;
					triggered = true;
					dir = 1;
				}
			}
		}
		if (!dontRayCast_UpDownLeftRight[1] && !triggered)
		{
			ray.direction = new Vector3(0, -checkDist, 0);
			ray.origin = this.gameObject.transform.position;
			Debug.DrawLine(ray.origin, ray.GetPoint(heightDown), Color.blue);
			if (Physics.Raycast(ray, out hit, heightDown, 11) == true)
			{
				if (hit.collider.gameObject.tag == "solidThing")
				{
					xa.glx = transform.position;
					xa.glx.x = hit.point.x;
					xa.glx.y = hit.point.y;
					transform.position = xa.glx;
					triggered = true;
					dir = 2;
				}
			}
		}
		if (!dontRayCast_UpDownLeftRight[2] && !triggered)
		{
			ray.direction = new Vector3(-checkDist, 0, 0);
			ray.origin = this.gameObject.transform.position;
			Debug.DrawLine(ray.origin, ray.GetPoint(widthLeft), Color.blue);
			if (Physics.Raycast(ray, out hit, widthLeft, 11) == true)
			{
				if (hit.collider.gameObject.tag == "solidThing")
				{
					xa.glx = transform.position;
					xa.glx.x = hit.point.x;
					xa.glx.y = hit.point.y;
					transform.position = xa.glx;
					triggered = true;
					dir = 3;
				}
			}
		}
		if (!dontRayCast_UpDownLeftRight[3] && !triggered)
		{
			ray.direction = new Vector3(checkDist, 0, 0);
			ray.origin = this.gameObject.transform.position;
			Debug.DrawLine(ray.origin, ray.GetPoint(widthRight), Color.cyan);
			if (Physics.Raycast(ray, out hit, widthRight, 11) == true)
			{
				if (hit.collider.gameObject.tag == "solidThing")
				{
					xa.glx = transform.position;
					xa.glx.x = hit.point.x;
					xa.glx.y = hit.point.y;
					transform.position = xa.glx;
					triggered = true;
					dir = 4;
				}
			}
		}
		if (triggered)
		{
			//die
			if (dieOnImpact)
			{
				HealthScript script;
				script = killThisGO.GetComponent<HealthScript>();
				script.health = 0;
			}

			if (justDestroyAndCreateExplo)
			{
				result = true;
				if (createSpecialDeathExploOn_UpDownLeftRight[0] && dir == 1) { Instantiate(createSpecialDeathExploOn_UpDownLeftRight[0], transform.position, createSpecialDeathExploOn_UpDownLeftRight[0].transform.rotation); result = false; }
				if (createSpecialDeathExploOn_UpDownLeftRight[1] && dir == 2) { Instantiate(createSpecialDeathExploOn_UpDownLeftRight[1], transform.position, createSpecialDeathExploOn_UpDownLeftRight[1].transform.rotation); result = false; }
				if (createSpecialDeathExploOn_UpDownLeftRight[2] && dir == 3) { Instantiate(createSpecialDeathExploOn_UpDownLeftRight[2], transform.position, createSpecialDeathExploOn_UpDownLeftRight[2].transform.rotation); result = false; }
				if (createSpecialDeathExploOn_UpDownLeftRight[3] && dir == 4) { Instantiate(createSpecialDeathExploOn_UpDownLeftRight[3], transform.position, createSpecialDeathExploOn_UpDownLeftRight[3].transform.rotation); result = false; }
				if (result)
				{
					if (deathExplo)
					{
						Instantiate(deathExplo, transform.position, deathExplo.transform.rotation);
					}
				}
				Destroy(this.gameObject);
			}
		}
	}
}
