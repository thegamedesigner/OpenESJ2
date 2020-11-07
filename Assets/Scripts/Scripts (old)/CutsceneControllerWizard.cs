using UnityEngine;
using System.Collections;

public class CutsceneControllerWizard : MonoBehaviour
{

	public string[] strings1;
	public GameObject NPC1;
	public GameObject txtObject;
	TextMesh textMesh1 = null;
	int stringsIndex = 0;
	int talk = 1;
	float delay1 = 0;
	TextMesh textMesh;
	bool spawnSecondString = false;
	bool killedPreviousTexts = false;

	void Start()
	{

	}

	void Update()
	{
		//if (xa.wizardCutsceneScript.stage > 0 && talk == 0) { xa.wizardCutsceneStage--; talk++; }


		if (talk > 0)
		{
			if (!killedPreviousTexts)
			{
				killedPreviousTexts = true;
				if (textMesh1) { iTweenEvent.GetEvent(textMesh1.gameObject, "fadeOutVeryFast").Play(); }
			}
			if (delay1 > 10 && !spawnSecondString)
			{
				delay1 = 0;
				talkFunc();
			}
			delay1 += 10 * fa.deltaTime;
		}


	}

	void talkFunc()
	{
		xa.glx = NPC1.transform.position;
		xa.tempobj = (GameObject)(Instantiate(txtObject, xa.glx, xa.null_quat));
		textMesh = xa.tempobj.GetComponentInChildren<TextMesh>();
		textMesh1 = textMesh;
		textMesh.text = strings1[stringsIndex];
		spawnSecondString = true;
		//

	}

}
