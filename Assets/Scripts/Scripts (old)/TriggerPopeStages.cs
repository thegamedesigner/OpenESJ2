using UnityEngine;
using System.Collections;

public class TriggerPopeStages : MonoBehaviour
{
	public Behaviour[] stageScripts;
	float localStage = 0;
	void Update()
	{
		if (xa.popeStage != localStage)
		{
			localStage = xa.popeStage;
			stageScripts[(int)(xa.popeStage)].enabled = true;

		}
	}
}
