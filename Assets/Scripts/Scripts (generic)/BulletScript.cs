using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
	public float speed = 0;
	[HideInInspector]
	public bool moving = true;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (moving)
		{
			transform.Translate(speed * fa.deltaTime, 0, 0);
		}

	}
}
