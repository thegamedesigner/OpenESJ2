using UnityEngine;

public class BeatJumpThrowerScript : MonoBehaviour
{
	public float start = 0;
	public float stop = 0;
	public float beat = 0;
	public float[] hits = new float[10];
	
	public GameObject beatJumpWave;
	
	float counter = 0;

	void Start()
	{
		counter = xa.music_Time;
	}
	void Update()
	{
		float now = xa.music_Time;
	
		int index = 0;
		while (index < hits.Length)
		{
			if (hits[index] == 0) break;
			else if (counter < hits[index] && now >= hits[index])
			{
				Beat();
			}
			index += 1;
		}
	
		counter = now;
	}
	void Beat()
	{
		if (beatJumpWave!=null)
		{
	
			xa.glx = transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.tempobj = (GameObject)(Instantiate(beatJumpWave, xa.glx, xa.null_quat));
			xa.tempobj.transform.parent = Camera.main.GetComponent<Camera>().transform;
	
		}
	}
/*
	void createBullet(float angle)
	{
		if (xa.emptyObj)
		{
			xa.emptyObj.transform.localEulerAngles = this.gameObject.transform.localEulerAngles;
			xa.glx = xa.emptyObj.transform.localEulerAngles;
			xa.glx.z = angle;
			xa.emptyObj.transform.localEulerAngles = xa.glx;
			firingRotation = xa.emptyObj.transform.rotation;
		}

		if (bullet != null)
		{
			xa.glx = muzzlePoint.transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.tempobj = (GameObject)(Instantiate(bullet, xa.glx, firingRotation));
			xa.tempobj.transform.parent = xa.createdObjects.transform;
		}
	}*/
}
