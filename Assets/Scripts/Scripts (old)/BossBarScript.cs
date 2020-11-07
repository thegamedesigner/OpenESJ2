using UnityEngine;
using System.Collections;

public class BossBarScript : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		xa.glx = transform.localScale;
		if (xa.bossHealthMax > 0) { xa.glx.x = (xa.bossHealth / xa.bossHealthMax) * 1.98f; }
		transform.localScale = xa.glx;
	}
}
