using UnityEngine;
using System.Collections;

public class MusicLineScript : MonoBehaviour
{
	public int num = 0;
	MusicLineManager mgr = null;

	void Start()
	{
		if (xa.musicLineManager == null)
		{
			mgr = this.gameObject.AddComponent<MusicLineManager>();
			xa.musicLineManager = mgr;
		}
		xa.musicLineManager.AddLine(gameObject, num);
	}

	void OnDestroy()
	{
		if (mgr)
		{
			Destroy(mgr);
			xa.musicLineManager = null;
		}
	}
	// Update is called once per frame
	/*void Update()
	{
		float goal = MusicLineManager.GetGoal(num);
		xa.glx = transform.localScale;
		if (xa.glx.y < goal)
		{
			xa.glx.y += spd * fa.deltaTime;
		}
		else if (xa.glx.y > goal)
		{
			xa.glx.y -= spd * fa.deltaTime;
		}
		transform.localScale = xa.glx;
	}*/
}
