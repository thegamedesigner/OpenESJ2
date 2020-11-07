using UnityEngine;

public class BeatJumpWaveScript : MonoBehaviour
{
	void Start()
	{
		foreach (Transform child in transform) {
			xa.glx = child.position;
			float off = Mathf.Abs(xa.glx.x - transform.position.x);
			xa.glx.y -= (off*off)/100f;
			child.position = xa.glx;
		}
	}
	void Update()
	{
		xa.glx = transform.position;
		if (xa.glx.y < -8)
			{ xa.glx.y += 6 * fa.deltaTime; }
		else
			{ xa.glx.y += 3 * fa.deltaTime; }
		transform.position = xa.glx;
	
		if (xa.glx.y > 15)
		{
			Destroy(this.gameObject);
		}
	}
}
