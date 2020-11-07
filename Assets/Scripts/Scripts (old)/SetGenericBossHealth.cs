using UnityEngine;
using System.Collections;

public class SetGenericBossHealth : MonoBehaviour
{
	public float bossHealth = 100;

	void Update()
	{
		xa.genericBossHealth = bossHealth;
		this.enabled = false;
	}
}
