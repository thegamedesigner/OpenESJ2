using UnityEngine;
using System.Collections;

public class GunControlsAI : MonoBehaviour
{

	public bool useAlwaysFire = false;

	public GameObject[] controlledGuns;

	Gun[] gunScripts;

	void Start()
	{
		//set the correct array length
		gunScripts = new Gun[controlledGuns.Length];

		//load all scripts into this array
		int index = 0;
		while (index < gunScripts.Length)
		{
			if (controlledGuns[index])
			{
				gunScripts[index] = controlledGuns[index].GetComponent<Gun>();
				index++;
			}
		}
		if (useAlwaysFire)
		{
			setFiringGuns(true);
		}
	}

	void Update()
	{
	}



	void setFiringGuns(bool firingState)
	{
		int index = 0;
		while (index < gunScripts.Length)
		{
			if (gunScripts[index])
			{
				if (firingState) { gunScripts[index].startFiringGun(); }
				else { gunScripts[index].stopFiringGun(); }
			}
			index++;
		}
	}

}
