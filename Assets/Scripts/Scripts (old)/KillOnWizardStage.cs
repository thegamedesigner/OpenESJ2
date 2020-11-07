using UnityEngine;
using System.Collections;

public class KillOnWizardStage : MonoBehaviour
{
	public int stage = 0;

	void Update()
	{
		if (xa.wizardCutsceneScript)
		{
			if (xa.wizardCutsceneScript.stage >= stage)
			{
				HealthScript script;
				script = this.gameObject.GetComponent<HealthScript>();
				if (script)
				{
					script.health = 0;
				}
				this.enabled = false;
			}
		}
	}
}
