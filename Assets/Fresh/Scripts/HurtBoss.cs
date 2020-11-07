using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoss : MonoBehaviour
{
	public static HurtBoss self;
	public GameObject redHealthBar;
	public HealthScript healthScript;//The boss's health script
	public float maxHealth = 100;

	void Awake()
	{
		self = this;
	}

	public void HurtBossFunc(int dam)
	{
		healthScript.health -= dam;
		if(healthScript.health <= 0) {healthScript.health = 0; }
		
		redHealthBar.transform.SetScaleX(healthScript.health * 0.1f);
		float offset = (18 - (healthScript.health * 0.1f)) * 0.5f;
		redHealthBar.transform.LocalSetX(offset);
					
	}

}
