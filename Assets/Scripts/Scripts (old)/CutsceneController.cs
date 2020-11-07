using UnityEngine;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
	public string[] strings1;
	public string[] strings2;
	public GameObject NPC1;
	public GameObject NPC2;
	public GameObject txtObject;
	public GameObject changeLevelAfterDelayObject;
	TextMesh textMesh1 = null;
	TextMesh textMesh2 = null;
	int stringsIndex = 0;
	int talk = 1;
	float delay1 = 0;
	float delay2 = 0;
	TextMesh textMesh;
	bool spawnSecondString = false;
	bool killedPreviousTexts = false;

	void Start()
	{

	}

	void Update()
	{
		if (xa.runes > 0  && talk == 0) { xa.runes--; talk++;}

		if (xa.runeFinalText == 1)
		{
			xa.runeFinalText = 2;
			Instantiate(changeLevelAfterDelayObject);
			talk++;
		}

		if (talk > 0)
		{
			if (!killedPreviousTexts)
			{
				killedPreviousTexts = true;
				if (textMesh1) { iTweenEvent.GetEvent(textMesh1.gameObject, "fadeOutVeryFast").Play(); }
				if (textMesh2) { iTweenEvent.GetEvent(textMesh2.gameObject, "fadeOutVeryFast").Play(); }
			}
			if (delay1 > 10 && !spawnSecondString)
			{
				delay1 = 0;
				talkFunc();
			}
			delay1 += 10 * fa.deltaTime;
		}

		if (spawnSecondString)
		{
			if (delay2 > 20)
			{
				delay2 = 0;
				talkFunc2();
			}
			delay2 += 10 * fa.deltaTime;
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

	void talkFunc2()
	{
		xa.glx = NPC2.transform.position;
		xa.tempobj = (GameObject)(Instantiate(txtObject, xa.glx, xa.null_quat));
		textMesh = xa.tempobj.GetComponentInChildren<TextMesh>();
		textMesh2 = textMesh;
		textMesh.text = strings2[stringsIndex];
		spawnSecondString = false;
		stringsIndex++;
		talk--;
		killedPreviousTexts = false;

	}


}
