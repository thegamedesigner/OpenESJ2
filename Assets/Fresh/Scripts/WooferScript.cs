using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferScript : MonoBehaviour
{
	public GameObject prefab;
	public GameObject muzzlepoint;
	float delay = 1.5f;
	float timeset;
	public bool DontRandomize = false;
	void Start()
	{
		
			muzzlepoint.transform.AddAngZ(Random.Range(-95, 95));
	}

	void Update()
	{
		if (fa.time > (timeset + delay))
		{
			timeset = fa.time;
			delay = Random.Range(0.3f, 3f);
			muzzlepoint.transform.AddAngZ(Random.Range(-15, 15));
			GameObject go = Instantiate(prefab, muzzlepoint.transform.position, muzzlepoint.transform.rotation);
			if (Random.Range(0, 10) < 4 && !DontRandomize)
			{
				int r = Random.Range(0, 3);
				if(r == 0){go.GetComponentInChildren<TextMesh>().text = "Bark"; }
				if(r == 1){go.GetComponentInChildren<TextMesh>().text = "Bark"; }
				if(r == 2){go.GetComponentInChildren<TextMesh>().text = "Arf"; }
				if(r == 3){go.GetComponentInChildren<TextMesh>().text = "Ruff"; }
				
			}
		}
	}
}
