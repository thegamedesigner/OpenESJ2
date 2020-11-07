using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
	public enum Type
	{
		ShotgunAmmo,
		RocketAmmo,
		HealthPack,
		End
	}

	public Type type;

	void Start()
	{
		iTween.MoveBy(this.gameObject,iTween.Hash("y", 0.2f, "time", 2, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.PunchScale(this.gameObject,iTween.Hash("x", 1.5f, "y", 1.5f, "z", 1.5f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void Update()
	{
		if (Vector3.Distance(FPSMainScript.playerPos, transform.position) < 1.5f)
		{
			//Get ammo
			if(type == Type.ShotgunAmmo) {FPSMainScript.FPSPlayerScript.shotgunAmmo += 8;
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.GetShotgunAmmo); }
			if(type == Type.RocketAmmo) {FPSMainScript.FPSPlayerScript.rocketAmmo += 3;
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.GetShotgunAmmo); }
			if(type == Type.HealthPack) {FPSMainScript.FPSPlayerScript.healthScript.health = 100;
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Checkpoint,1.5f);
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.GetShotgunAmmo);}

			Destroy(this.gameObject);
		}
	}
}
