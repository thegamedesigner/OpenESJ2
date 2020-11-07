using System.Collections.Generic;
using UnityEngine;

public class GenericMonsterScript : MonoBehaviour
{
	public bool invincible = false;
	public bool spiky = false;//Means it can't be killed
	public static List<GenericMonsterScript> monsters;
	public float deathFromDoubleJumpExploDist = 0.3f;
	public bool dead = false;
	public GameObject deathExplo = null;
	public bool matchDeathExploScale = false;
	public GameObject[] destroyTheseGOs = new GameObject[0];
	public HealthScript hpScript;
	HurtBoss bossHpScript;
	public int damageToBoss;
	public bool near = false;
	[HideInInspector]
	public GameObject explosiveLinkGO;
	public GameObject explosiveLinkGO2;

	void Awake()
	{
		//monsters = null;
	}

	void Start()
	{
		if (damageToBoss > 0)
		{
			bossHpScript = GameObject.FindGameObjectWithTag("Boss").GetComponent<HurtBoss>();
		}

		if (monsters == null) { monsters = new List<GenericMonsterScript>(); }
		monsters.Add(this);

		hpScript = this.gameObject.GetComponent<HealthScript>();
	}

	void Update()
	{
		if (near && explosiveLinkGO != null)
		{
			float theZ = 33;
			Vector3 plPos = xa.player.transform.position;
			Vector3 myPos = transform.position;
			plPos.z = theZ;
			myPos.z = theZ;
			explosiveLinkGO.transform.position = myPos;
			explosiveLinkGO2.transform.position = plPos;
			explosiveLinkGO.transform.LookAt(plPos, new Vector3(0, 0, 1));
			explosiveLinkGO2.transform.LookAt(myPos, new Vector3(0, 0, 1));
			explosiveLinkGO.transform.AddAngZ(180);
			explosiveLinkGO2.transform.AddAngZ(180);
		}
		if (!dead && !invincible)
		{
			if (hpScript.health <= 0)
			{
				dead = true;
				if (bossHpScript != null)
				{
					bossHpScript.HurtBossFunc(damageToBoss);
				}
			}
		}


		if (dead)
		{
			if (explosiveLinkGO != null)
			{ Destroy(explosiveLinkGO); }
			if (explosiveLinkGO2 != null)
			{ Destroy(explosiveLinkGO2); }
			if (deathExplo)
			{
				GameObject go = Instantiate<GameObject>(deathExplo);
				if(matchDeathExploScale)
				{
					go.transform.SetScaleX(transform.localScale.x);
				}
				go.transform.position = transform.position;
				go.transform.SetZ(xa.GetLayer(xa.layers.Explo1));
			}

			for (int i = 0; i < destroyTheseGOs.Length; i++)
			{
				if (destroyTheseGOs[i] != null)
				{
					Destroy(destroyTheseGOs[i]);
				}
			}
		}
	}

}
