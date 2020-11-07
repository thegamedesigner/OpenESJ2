using UnityEngine;

public class HealthScript : MonoBehaviour
{
	public float health                            = 0.0f;
	[HideInInspector] public float invincibleTimer = 0.0f;
	[HideInInspector] public bool setPosWhenKilled = false;
	[HideInInspector] public Vector3 posWhenKilled = Vector3.zero;

	[HideInInspector] public int uid = -1; 

}
