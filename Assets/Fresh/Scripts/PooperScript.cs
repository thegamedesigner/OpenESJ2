using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooperScript : MonoBehaviour
{
	public bool silent = false;
	public bool makeChild = false;
	public bool poopAnywhere = false;
	public GameObject poop;
	public float maxDist = 4;
	Vector3 lastPos;

	void Start()
	{

	}

	void Update()
	{

		if (Vector3.Distance(transform.position, lastPos) > maxDist)
		{
			if (!silent)
			{
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart);
			}
			lastPos = transform.position;

			//Don't poop off-screen
			if (poopAnywhere || (transform.position.x < 14 && transform.position.x > -22 && transform.position.y > -8 && transform.position.y < 8))
			{
				GameObject go = Instantiate(poop, transform.position, transform.rotation);
				if (makeChild)
				{
					go.transform.SetParent(this.transform);
				}
			}
		}
	}
}
