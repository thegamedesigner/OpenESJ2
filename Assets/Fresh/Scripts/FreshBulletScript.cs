using UnityEngine;

public class FreshBulletScript : MonoBehaviour
{
	public float speed = 5;
	public float lifespan = 2;
	float scaleInSpd = 5;//2;
	bool scaleOut = false;
	bool dead = false;
	public xa.layers myLayer = xa.layers.None;
	float lifespanTimeSet = 0;
	public GameObject deathExplo = null;
	public HealthScript healthScript;
	public bool dontScaleIn;

	void Awake()
	{
		SnapToLayerFunc();
		lifespanTimeSet = fa.time;
	}

	void Start()
	{
		if (!dontScaleIn)
		{
			transform.SetScaleX(0);
			transform.SetScaleY(0);
		}
	}

	void Update()
	{
		if (healthScript.health == 0) { dead = true; }
		if (fa.time > (lifespanTimeSet + lifespan)) {scaleOut = true; }

		transform.Translate(new Vector3(speed * fa.deltaTime, 0, 0));

		if (!scaleOut && !dead)
		{
			if (!dontScaleIn)
			{
				if (transform.localScale.x < 1)
				{
					transform.AddScaleX(scaleInSpd * fa.deltaTime);
					if (transform.localScale.x > 1) { transform.SetScaleX(1); }
				}
				if (transform.localScale.y < 1)
				{
					transform.AddScaleY(scaleInSpd * fa.deltaTime);
					if (transform.localScale.y > 1) { transform.SetScaleY(1); }
				}
			}
		}
		else if (dead)
		{
			GameObject go = Instantiate(deathExplo);
			go.transform.position = transform.position;
			
			Destroy(this.gameObject);
		}
		else if (scaleOut)
		{
			if (transform.localScale.x > 0) { transform.AddScaleX(-14 * fa.deltaTime); }
			if (transform.localScale.y > 0) { transform.AddScaleY(-14 * fa.deltaTime); }

			if (transform.localScale.x <= 0 || transform.localScale.y <= 0)
			{
	
				Destroy(this.gameObject);
			}
		}
	}

	void SnapToLayerFunc()
	{
		xa.glx = transform.position;
		xa.glx.z = xa.GetLayer(myLayer);
		transform.position = xa.glx;
	}
}
