using UnityEngine;
using System.Collections;

public class SpawnText : MonoBehaviour
{
	public GameObject textPrefab = null;
	public string[] thingsToSay = new string[0];
	public GameObject forceTextToSpawnHere = null;
	int stage = 0;
	GameObject lastText = null;
	TextMesh textMesh = null;
	public bool loop = false;
	public bool stickOnLastText = false;
	public bool loopLastX = false;
	public int loopLastXAmount = 0;
	public bool loopFromX = false;
	public int loopFromXAmount = 0;
	public bool rememberStageAfterRespawns = false;
	public int staticStageSlot0To9 = -1;
    public GameObject zoomToThisGameObject = null;

	public void saySomething(int index)
	{
		if (index >= thingsToSay.Length && !loop) { return; }
		if (forceTextToSpawnHere) { xa.glx = forceTextToSpawnHere.transform.position; }
		else
		{
			xa.glx = transform.position;
		}
		xa.tempobj = (GameObject)(Instantiate(textPrefab, xa.glx, textPrefab.transform.rotation));
		textMesh = xa.tempobj.GetComponent<TextMesh>();
		textMesh.text = thingsToSay[index];
		lastText = xa.tempobj;
        if (zoomToThisGameObject)
        {
            iTween.MoveTo(xa.tempobj, iTween.Hash("x", zoomToThisGameObject.transform.position.x, "y", zoomToThisGameObject.transform.position.y, "easetype", iTween.EaseType.easeInOutSine, "time", 1f));
        }

	}

	public void sayNextThing()
	{
		saySomething(stage);
		stage++;
		if (stage >= thingsToSay.Length && loop) { stage = 0; }
		if (stage >= thingsToSay.Length && stickOnLastText) { stage = thingsToSay.Length - 1; }
		if (stage >= thingsToSay.Length && loopLastX) { stage = thingsToSay.Length - loopLastXAmount; }
		if (stage >= thingsToSay.Length && loopFromX) { stage = loopFromXAmount; }

		if (rememberStageAfterRespawns)
		{
			za.chatObjectStaticStages[staticStageSlot0To9] = stage;
		}
	}

	public void killLastAndSayNext()
	{
		if (lastText) { lastText.SendMessage("triggerItween"); lastText.SendMessage("triggerItween2"); }
		sayNextThing();

	}
	public void killLast()
	{
		if (lastText) { lastText.SendMessage("triggerItween"); lastText.SendMessage("triggerItween2"); }
	}

	public void killLastAndSaySomething(string index)
	{
		if (lastText) { lastText.SendMessage("triggerItween"); lastText.SendMessage("triggerItween2"); }

		saySomething(int.Parse(index));

	}

	void Start()
	{
		if (za.chatObjectStaticStagesLevel != UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex)
		{
			za.chatObjectStaticStagesLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			cleanAllStaticStageSlots();//clean all slots
		}

		if (rememberStageAfterRespawns)
		{
			stage = za.chatObjectStaticStages[staticStageSlot0To9];
		}
	}

	void cleanAllStaticStageSlots()
	{
		int index = 0;
		while (index < za.chatObjectStaticStages.Length)
		{
			za.chatObjectStaticStages[index] = 0;
			index++;
		}
	}
}
