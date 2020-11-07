using UnityEngine;
using System.Collections;

public class MonsterHurtThePlayerScript : MonoBehaviour
{
	public float dist = 0;

	public bool dieOnImpact = false;


	void Start()
	{
		dist += 0.81f;//player bounding
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, dist);
	}

	void Update()
	{
		if (xa.player)
		{
			xa.glx = transform.position;
			xa.glx.z = xa.player.transform.position.z;
			if (Vector3.Distance(xa.glx, xa.player.transform.position) < dist)
			{
				HealthScript script;
				script = xa.player.GetComponent<HealthScript>();
				if (script.invincibleTimer <= 0) { script.health = 0; }

				if (dieOnImpact)
				{
					script = this.gameObject.GetComponent<HealthScript>();
					script.health = 0;
				}
			}
		}
	}
}
