using UnityEngine;
using System.Collections;

public class SetParent : MonoBehaviour
{
	public GameObject go;
	public bool setToNull = false;

	void Start()
	{
		if (setToNull) { go.transform.parent = null; }

	}

}
