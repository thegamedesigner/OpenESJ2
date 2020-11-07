using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDeeCameraScript : MonoBehaviour
{
	public enum Type
	{
		None,
		Mod1,//Spins the camera around on a flat axis
		Mod2,//slowly spin the camera. Also moves objs along sine curve
		Mod3,//Spin in two directions. Barely playable, if the level is simple. 
        Sewer1,
		End
	}

	public Type type = Type.None;

	void Start()
	{
		
		switch (type)
		{
			case Type.Mod1:
				//iTween.RotateBy(this.gameObject,iTween.Hash("y", 1, "time", 10, "easetype", iTween.EaseType.linear));
				//iTween.RotateBy(this.gameObject,iTween.Hash("delay", 10, "y", 1, "time", 12, "easetype", iTween.EaseType.linear));
				//iTween.RotateBy(this.gameObject,iTween.Hash("delay", 20, "y", 1, "time", 12, "easetype", iTween.EaseType.linear));
				//iTween.RotateBy(this.gameObject,iTween.Hash("delay", 30, "y", 1, "time", 12, "easetype", iTween.EaseType.linear));
				
				break;
			case Type.Mod2:
			iTween.RotateTo(this.gameObject,iTween.Hash("delay", 2, "x", 35, "y", 35, "time", 22, "easetype", iTween.EaseType.easeInOutSine));
				break;
			case Type.Sewer1:
			iTween.RotateTo(this.gameObject,iTween.Hash("delay", 2, "x", 180, "y", 180, "time", 5, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
				break;
		}
	}

	void Update()
	{
		switch (type)
		{
			case Type.Mod1:
				
				transform.AddAngY(75 * fa.deltaTime);

				break;
			case Type.Mod2:
				

				break;
			case Type.Mod3:
				
				transform.AddAngY(75 * fa.deltaTime);
				transform.AddAngZ(15 * fa.deltaTime);

				break;
			case Type.Sewer1:
				

				break;
		}
	}
}
