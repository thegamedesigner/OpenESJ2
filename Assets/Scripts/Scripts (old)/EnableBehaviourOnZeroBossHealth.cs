using UnityEngine;
using System.Collections;

public class EnableBehaviourOnZeroBossHealth : MonoBehaviour
{
	public Behaviour behaviour = null;
	bool hasBeenNonZero = false;
	public float forcedAmount = 0;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (xa.genericBossHealth <= forcedAmount && hasBeenNonZero)
		{
			behaviour.enabled = true;
			this.enabled = false;
		}
		if (xa.genericBossHealth != forcedAmount)
		{
			hasBeenNonZero = true;
		}

	}
}
