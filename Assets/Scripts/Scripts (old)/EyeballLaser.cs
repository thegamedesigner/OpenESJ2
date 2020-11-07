using UnityEngine;

public class EyeballLaser : MonoBehaviour
{
	public GameObject prefab = null;
	GameObject ptr = null;
	HealthScript hpScript = null;
	int killedLaser = 0;
	bool stop = false;
	
	public void destroyAndStop()
	{
		stop = true;
		if (ptr)
		{
			hpScript = null;
			hpScript = ptr.GetComponent<HealthScript>();
			if (hpScript)
			{
				ptr = null;
				hpScript.health = 0;
			}
		}
	}

	void Start()
	{

	}

	void Update()
	{
		if (xa.player && !stop)
		{
			if (!ptr)
			{

			   // Setup.GC_DebugLog("no ptr");
			   // Setup.GC_DebugLog("" + (xa.player.transform.position.x - Camera.main.transform.position.x));
				if ((xa.player.transform.position.x - Camera.main.transform.position.x) < -2 && !xa.playerDead)
				{
					//Setup.GC_DebugLog("too close!");
					killedLaser = 0;
					//Setup.GC_DebugLog("CREATED EYEBALL LASER");
					xa.glx = transform.position;
					xa.glx.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
					ptr = (GameObject)(Instantiate(prefab, xa.glx, xa.null_quat));
					ptr.transform.parent = transform;
					xa.glx = Vector3.zero;
					xa.glx.z = -2;
					ptr.transform.localPosition = xa.glx;
				}
			}
			if(ptr && killedLaser <= 0)
			{
				if ((xa.player.transform.position.x - Camera.main.transform.position.x) > 1)
				{
					hpScript = null;
					hpScript = ptr.GetComponent<HealthScript>();
					if(hpScript)
					{
						ptr = null;
						hpScript.health = 0;
						killedLaser = 10;
						//Setup.GC_DebugLog("Killed laser");
					}
				}
			}
		}
	}
}
