using UnityEngine;
using System.Collections;

public class CollideWithBlocks : MonoBehaviour
{
	public bool dieOnImpact = false;
	public GameObject killThisGO = null;

	public bool justDestroyAndCreateExplo = false;
    public GameObject deathExplo = null;
    public GameObject destroyThisGO = null;
    public Behaviour disableThisBehaviour;

    public float forceHitDist = 0;
    public float forceBackHitDist = 0;//This pushes the point, from which it raycasts, backwards

	bool triggered = false;
	RaycastHit hit;
	Ray ray = new Ray();

	float checkDist = 0.5f;
	void Start()
	{
        if (forceHitDist != 0) { checkDist = forceHitDist; }
	}

	void Update()
	{
        LayerMask mask = 1 << 19;
		triggered = false;
        ray.direction = transform.TransformDirection(-Vector3.left);
        ray.origin = new Vector3(transform.position.x, transform.position.y, xa.GetLayer(xa.layers.RaycastLayer));
        ray.origin = ray.GetPoint(-forceBackHitDist);


        Debug.DrawLine(ray.origin, ray.GetPoint(checkDist), Color.blue);
        if (Physics.Raycast(ray, checkDist, mask))
        {
            triggered = true;
        }
        /*
		//Debug.DrawLine(ray.origin, ray.GetPoint(checkDist), Color.blue);
		if (Physics.Raycast(ray, out hit, checkDist, mask) == true)
		{
		}
		ray.direction = new Vector3(-1, 0, 0);
		//Debug.DrawLine(ray.origin, ray.GetPoint(checkDist), Color.blue);
        if (Physics.Raycast(ray, out hit, checkDist, mask) == true)
		{
				triggered = true;
		}*/
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
				if (deathExplo) { Instantiate(deathExplo, transform.position, xa.null_quat); }


                if (!destroyThisGO)
                {
                    Destroy(this.gameObject);
                }
                if (destroyThisGO)
                {
                    Destroy(destroyThisGO);
                }

                if (disableThisBehaviour)
                {
                    disableThisBehaviour.enabled = false;
                }


            }
            this.enabled = false;
		}
	}
}
