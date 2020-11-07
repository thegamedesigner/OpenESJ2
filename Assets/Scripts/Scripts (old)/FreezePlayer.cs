using UnityEngine;
using System.Collections;

public class FreezePlayer : MonoBehaviour
{
	public bool unFreeze = false;
	public int forceDirection = 0;
	// Use this for initialization
	void Start()
	{
		if (unFreeze)
		{
			xa.freezePlayerForCutscene = false;
			xa.forcePlayerDirection = 0;
		}
		else
		{
			xa.freezePlayerForCutscene = true;
			xa.forcePlayerDirection = forceDirection;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
