using UnityEngine;
using System.Collections;

public class CameraControllerFuncs : MonoBehaviour
{
	public void catchUpToPlayerXFunc()
	{
		if (xa.player)
		{
			iTween.MoveTo(this.gameObject, iTween.Hash("x", transform.position.x, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "islocal", false));
		}
	}

	public void catchUpToPlayerYFunc()
	{
		if (xa.player)
		{
			iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x, xa.player.transform.position.y, transform.position.z), "time", 3, "easetype", iTween.EaseType.easeInOutSine, "islocal", false));
		}
	}
}
