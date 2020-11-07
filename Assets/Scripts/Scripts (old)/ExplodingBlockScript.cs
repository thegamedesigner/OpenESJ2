using UnityEngine;

public class ExplodingBlockScript : MonoBehaviour
{
	public float fuse = 0;
	public float justBeforeFuse = 0;

	public GameObject pulsingExplo = null;
	public GameObject explo = null;
	public Behaviour triggerScript = null;

	public bool triggerITweens = false;
	public GameObject triggerObj = null;

	public GameObject slaveObj1 = null;
	public GameObject slaveObj2 = null;

	public GameObject puppet = null;
	public int triggered_x1 = 0;
	public int triggered_y1 = 0;
	public int justBeforeExplo_x1 = 0;
	public int justBeforeExplo_y1 = 0;


	float counter = 0;
	bool triggered = false;
	bool finished = false;
	GameObject exploPtr;

	void Start()
	{
	}

	void Update()
	{
		if (triggered && !finished && fuse != 0)
		{
			counter += 10 * fa.deltaTime;

			if (counter >= justBeforeFuse)
			{
				if (puppet != null) { setPuppetTexture(justBeforeExplo_x1, justBeforeExplo_y1); }
			}

			if (counter >= fuse)
			{
				finished = true;

				if (triggerScript) { triggerScript.enabled = true; }
				if (explo)
				{
					xa.glx = transform.position;
					xa.glx.z = xa.GetLayer(xa.layers.Explo1);
					xa.tempobj = (GameObject)(Instantiate(explo, xa.glx, xa.null_quat));
					xa.tempobj.transform.parent = xa.createdObjects.transform;
				}

				if (triggerITweens)
				{
					iTweenEvent.GetEvent(triggerObj, "trigger1").Play();

					if (slaveObj1) { iTweenEvent.GetEvent(slaveObj1, "trigger1").Play(); }
					if (slaveObj2) { iTweenEvent.GetEvent(slaveObj2, "trigger1").Play(); }
				}
			}
		}
	}

	public void triggerMe()
	{
		if (triggered) { return; }
		triggered = true;

		if (puppet != null) { setPuppetTexture(triggered_x1, triggered_y1); }

		//Setup.GC_DebugLog("On exploding platform");

		if (pulsingExplo)
		{
			float explo1Layer = xa.GetLayer(xa.layers.Explo1);
			xa.glx = transform.position;
			xa.glx.z = explo1Layer;
			xa.tempobj = (GameObject)(Instantiate(pulsingExplo, xa.glx, xa.null_quat));
			xa.tempobj.transform.parent = transform;


			if (slaveObj1)
			{
				xa.glx = slaveObj1.transform.position;
				xa.glx.z = explo1Layer;
				xa.tempobj = (GameObject)(Instantiate(pulsingExplo, xa.glx, xa.null_quat));
				xa.tempobj.transform.parent = slaveObj1.transform.parent;
			}
			if (slaveObj2)
			{
				xa.glx = slaveObj2.transform.position;
				xa.glx.z = explo1Layer;
				xa.tempobj = (GameObject)(Instantiate(pulsingExplo, xa.glx, xa.null_quat));
				xa.tempobj.transform.parent = slaveObj2.transform.parent;
			}
		}


	}

	public void destroyMe()
	{
		Destroy(this.gameObject);
	}

	void setPuppetTexture(int v1, int v2)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f;
		y1 = 0.125f;
		x2 = 0.125f * v1;
		y2 = 1 - ((0.125f * v2) + 0.125f);

		puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}
}
