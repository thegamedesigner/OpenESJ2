using UnityEngine;
using System.Collections;

public class PopeShieldsDestroySelf : MonoBehaviour
{
	public GameObject go1;
	public GameObject go2;
	public float amount = 0;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (xa.popeHealth <= amount)
		{
			xa.tempobj = (GameObject)(Instantiate(go2, go1.transform.position, xa.null_quat));
			xa.tempobj.transform.parent = go1.transform.parent;

			Destroy(go1);
			this.enabled = false;
		}
	}
}
