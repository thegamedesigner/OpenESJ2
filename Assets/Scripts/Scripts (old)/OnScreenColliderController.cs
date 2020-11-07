using UnityEngine;
using System.Collections;

public class OnScreenColliderController : MonoBehaviour
{
	public GameObject prefab;

	void Start()
	{
		if (xa.onScreenColliderController)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
			xa.onScreenColliderController = this.gameObject;
		}
	}

	void OnLevelWasLoaded()
	{
		spawnOnScreenCollider();
	}

	void spawnOnScreenCollider()
	{
		xa.tempobj = (GameObject)(Instantiate(prefab));
		xa.tempobj.transform.parent = Camera.main.GetComponent<Camera>().transform;
		xa.glx = Vector3.zero;
		xa.glx.z = 10;
		xa.tempobj.transform.localPosition = xa.glx;

		xa.onScreenCollider = xa.tempobj;

		xa.tempobj = (GameObject)(Instantiate(prefab));
		xa.tempobj.transform.parent = Camera.main.GetComponent<Camera>().transform;
		xa.glx = Vector3.zero;
		xa.glx.z = 10;
		xa.tempobj.transform.localPosition = xa.glx;
		xa.strictOnScreenCollider = xa.tempobj;
	}
}
