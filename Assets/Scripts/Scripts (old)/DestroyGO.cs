using UnityEngine;
using System.Collections;

public class DestroyGO : MonoBehaviour
{
	public GameObject go;
	public void destroyGO()
	{
		Destroy(go);
	}
}
