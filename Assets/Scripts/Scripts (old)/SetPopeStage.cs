using UnityEngine;
using System.Collections;

public class SetPopeStage : MonoBehaviour
{
	public float stage = 0;

	void Update()
	{
		if (this.enabled)
		{
			xa.popeStage = stage;
			//Setup.GC_DebugLog("pope stage set to " + stage);
			this.enabled = false;
		}
	}
}
