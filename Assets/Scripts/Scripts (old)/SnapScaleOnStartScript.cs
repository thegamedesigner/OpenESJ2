using UnityEngine;
using System.Collections;

public class SnapScaleOnStartScript : MonoBehaviour
{

	public Vector3 scale = Vector3.zero;
	void Start()
	{
		transform.localScale = scale;
	}

}
