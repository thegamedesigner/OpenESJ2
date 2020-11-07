using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
	public GameObject orSpawnThis  = null;
	public string spawnThis        = "";
	public float customSpawnBuffer = 0;
	public float musicTime         = 0;
	public bool waitForMusicTime   = false;
	
	void Awake()
	{
		GetComponent<Renderer>().enabled = false;
	}

	void Update()
	{
		float x = transform.position.x;
		float y = transform.position.y;

		if (waitForMusicTime || x > (xa.backEdgeOfScreen - 3 - customSpawnBuffer))
		{
			if (waitForMusicTime || x < (xa.frontEdgeOfScreen + 3 + customSpawnBuffer))//buffer zone in front of screen
			{
				if (waitForMusicTime || y < (xa.topEdgeOfScreen + 3 + customSpawnBuffer))
				{
					if(!waitForMusicTime || waitForMusicTime && xa.music_Time >= musicTime)
					{
						if (orSpawnThis)
						{
							//spawn a prefab
							xa.tempobj = (GameObject)(Instantiate(orSpawnThis, transform.position, xa.null_quat));
							if (xa.createdObjects)
							{
								xa.tempobj.transform.parent = xa.createdObjects.transform;
							}
							Destroy(this.gameObject);
						}
						else
						{
							//spawn from library
							if (xa.spawnLibraryScript)
							{
								xa.tempobj = null;
								xa.tempobj = xa.spawnLibraryScript.returnPrefab(spawnThis);
								if (xa.tempobj != null)
								{
									xa.tempobj = (GameObject)(Instantiate(xa.tempobj, transform.position, xa.null_quat));
									if (xa.createdObjects)
									{
										xa.tempobj.transform.parent = xa.createdObjects.transform;
									}
								}
								Destroy(this.gameObject);
							}
						}
						xa.onScreenObjectsDirty = true;
					}
				}
			}
		}
	}
}
