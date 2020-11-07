using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
	public Vector3 spawnPosOffset      = Vector3.zero;
	public GameObject obj;
	public float ranXMin               = 0;
	public float ranXMax               = 0;
	public float ranYMin               = 0;
	public float ranYMax               = 0;
	public float spawnSpeed            = 0;
	public float ranSpawnSpeed         = 0;
	public float stopAtThisMusicalTime = 0;
	public bool useAngle               = false;
	float counter                      = 0;
	float ranNum                       = 0;

	void Start()
	{
		ranNum = Random.Range(0, ranSpawnSpeed);
	}

	void Update()
	{
		if (stopAtThisMusicalTime > xa.music_Time || stopAtThisMusicalTime == 0)
		{
			counter += 10 * fa.deltaTime;
			if (counter >= (spawnSpeed + ranNum))
			{
				ranNum = Random.Range(0,ranSpawnSpeed);
				counter = 0;

				if (obj)
				{
					xa.glx = transform.position;
					xa.glx += spawnPosOffset;
					xa.glx.x += Random.Range(ranXMin, ranXMax);
					xa.glx.y += Random.Range(ranYMin, ranYMax);
					//xa.glx.z = xa.explo1Layer;
					xa.tempobj = (GameObject)(Instantiate(obj, xa.glx, xa.null_quat));
					if (useAngle) {
						xa.tempobj.transform.localEulerAngles = transform.localEulerAngles;
					}
					xa.tempobj.transform.parent = xa.createdObjects.transform;
				}

			}
		}
	}
}
