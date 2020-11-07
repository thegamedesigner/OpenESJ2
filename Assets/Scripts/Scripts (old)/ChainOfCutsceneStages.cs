using UnityEngine;
using System.Collections;

public class ChainOfCutsceneStages : MonoBehaviour
{
	public CutsceneCharacterScript cutsceneCharacterScript = null;
	public int startOfChain = 0;
	public int endOfChain = 0;
	public float delayInSeconds = 0;
	float timeSave = 0;

	bool triggered = false;
	void Start()
	{
	
	}

	void Update()
	{
		if (!triggered)
		{
			if (cutsceneCharacterScript.stage == startOfChain)
			{
				triggered = true;
				timeSave = fa.time;
			}
		}
		else
		{
			if ((timeSave + delayInSeconds) <= fa.time)
			{
				timeSave = fa.time;
				cutsceneCharacterScript.stage++;
				if (cutsceneCharacterScript.stage == endOfChain) { triggered = false; }
			}
		}
	}
}
