using UnityEngine;
using System.Collections;

public class DisableOnStart : MonoBehaviour
{
	void Start()
	{
		this.gameObject.SetActive(false);
	}

}
