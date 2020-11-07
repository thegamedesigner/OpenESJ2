using UnityEngine;
using System.Collections;

public class BossHeartScript : MonoBehaviour
{
	public float respawnTime = 0;
	public float moveEachHit_Y = 3;
	public GameObject wizardObj;
	public GameObject iTweenObj;
	public GameObject iTweenObj2;
	public GameObject hitExplo;

	int hitsLeft = 3;

	float goal_Y;

	float counter = 0;
	bool deadForNow = false;
	HealthScript hpScript;

	void Start()
	{
		hpScript = this.gameObject.GetComponent<HealthScript>();

		goal_Y = transform.position.y;
	}

	void Update()
	{
		if (hpScript.health <= 0 && !deadForNow)
		{
			//wizScript.saySomething = 1;
			counter = respawnTime;
			deadForNow = true;
			xa.wizardCutsceneScript.stage++;
			hitsLeft--;
			iTweenEvent.GetEvent(iTweenObj, "trigger1").Play();
			iTweenEvent.GetEvent(iTweenObj2, "trigger1").Play();

			if (hitsLeft <= 0)
			{
				goal_Y += 1000; // go far far away
			  //  xa.wizardCutsceneScript.stage++;
			}
			else
			{
				goal_Y += 3;
			}

			xa.glx = transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.tempobj = (GameObject)(Instantiate(hitExplo, xa.glx, xa.null_quat));
			xa.tempobj.transform.parent = transform;
		}

		if (counter > 0 && hitsLeft > 0)
		{
			counter -= 10 * fa.deltaTime;

			if (counter <= 0)
			{
				hpScript.health = 100;
				deadForNow = false;
				iTweenEvent.GetEvent(iTweenObj, "trigger2").Play();
				iTweenEvent.GetEvent(iTweenObj2, "trigger2").Play();
			}

		}
		if (goal_Y < -2.88) { goal_Y = -2.88f; }
		if (transform.position.y <= -2)
		{
			xa.glx = transform.position;
			xa.glx.y += 3 * fa.deltaTime;
			transform.position = xa.glx;
		}
		if (transform.position.y < goal_Y)
		{
			xa.glx = transform.position;
			xa.glx.y += 1 * fa.deltaTime * (hitsLeft>0?1:3);
			if (xa.glx.y >= goal_Y)
			{
				xa.glx.y = goal_Y;
			}
			transform.position = xa.glx;
		}
	}
}
