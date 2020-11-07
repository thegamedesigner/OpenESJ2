using UnityEngine;
using System.Collections;

public class SetInvisibleOnStart : MonoBehaviour
{

	void Awake()
	{
		this.gameObject.GetComponent<Renderer>().enabled = false;
		Destroy(this);
	}

}
