using UnityEngine;
using System.Collections;

public class DestroyOnStart : MonoBehaviour
{
	public GameObject useThisGO = null;
	void Start ()
	{
		if (useThisGO)
		{
			Destroy(useThisGO);
		}
		else
		{
			Destroy(this.gameObject);
		}
	
	}
	
}
