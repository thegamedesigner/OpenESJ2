using UnityEngine;
using System.Collections;

public class FlowerSoundScript : MonoBehaviour
{
	public GameObject flowerSound;
	// Use this for initialization
	void Start ()
	{
		if(flowerSound)
		{
			xa.tempobj = (GameObject)(Instantiate(flowerSound, transform.position, xa.null_quat));
			xa.tempobj.transform.parent = xa.createdObjects.transform;
		}
	}
}
