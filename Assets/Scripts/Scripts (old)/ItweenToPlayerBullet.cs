using UnityEngine;

public class ItweenToPlayerBullet : MonoBehaviour
{
	Vector3 goal = Vector3.zero;
	void Start()
	{

	}

	void Update()
	{
		if (this.enabled)
		{
			if(xa.player)
			{
				goal = xa.player.transform.position;
				goal.z = xa.GetLayer(xa.layers.Monsters);
				iTween.MoveTo(this.gameObject,iTween.Hash("position", goal,"easetype",iTween.EaseType.easeInOutSine,"time",3));
				this.enabled = false;
			}
		}
	}
}
