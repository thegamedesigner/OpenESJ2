using UnityEngine;
using System.Collections;

public class TriggerOtherScriptScript : MonoBehaviour
{
	public Behaviour[] scriptsToActivate;
	public bool triggerWhenPlayerIsInBox = false;
	public bool triggerWhenPlayerIsInScaleBox = false;
	public bool triggerWhenClickedOnColliderTaggedAsClickable = false;
	public bool triggerWhenTappedOnColliderTaggedAsClickable = false;
	public bool triggerOnDistFromGO = false;
	public bool triggerOnDistFromPlayer = false;
	public bool triggerOnStart = false;
	public bool triggerOnHaltGlorg = false;
	public bool triggerOnCutsceneStage = false;
	public CutsceneCharacterScript cutsceneScript = null;
	public int stageToTriggerAt = 0;
	public GameObject distGO = null;
	public float distTrigger = 0;
	public bool disableBehaviours = false;

	public float boxWidthLeft = 0;
	public float boxWidthRight = 0;
	public float boxHeightUp = 0;
	public float boxHeightDown = 0;

	public float delayInSeconds = 0;

	public bool useSendMsg = false;
	public string[] msgToSend;
	public GameObject[] GOToSendTo;
	public bool showDebugMsg = false;
	public string debugMsg = "Triggered!";
	public bool checkForPopeHealth = false;
	public float popeHealthLessThan = 0;
	public bool checkForPopeHits = false;
	public bool lessThanXPopeHits = false;
	public int requiredPopeHits = 0;
	public bool disableOnTriggering = false;

	int triggered = 0;
	bool permaTriggered = false;
	float counter = 0;

	void Start()
	{
		if (triggerOnStart) { triggeredFunc(); }
	}

	void Update()
	{
		if (triggerOnCutsceneStage)
		{
			if(cutsceneScript.stage == stageToTriggerAt)
			{
				if (!permaTriggered)
				{
					triggeredFunc();
				}
			}
		}
		if (triggered > 0)
		{
			checkTriggered();
		}
		else
		{
			if(triggerOnHaltGlorg)
			{
				if (xa.haltForSecondGlorg)
				{
					if (!permaTriggered) { triggeredFunc(); }
				}
			}
	
				if (triggerOnDistFromPlayer)
				{
					if (xa.player)
					{
						xa.glx = transform.position;
						xa.glx2 = xa.player.transform.position;
						xa.glx.z = xa.glx2.z;
						if (Vector3.Distance(xa.glx, xa.glx2) < distTrigger)
						{
							if (triggered == 0) { triggeredFunc(); }
						}
					}

				}
			if (triggerOnDistFromGO)
			{
				xa.glx = transform.position;
				xa.glx2 = distGO.transform.position;
				xa.glx.z = xa.glx2.z;
				if (Vector3.Distance(xa.glx, xa.glx2) < distTrigger)
				{
					if (triggered == 0) { triggeredFunc(); }
				}
			}
			if (triggerWhenPlayerIsInBox)
			{
				if (xa.player)
				{
					if (xa.player.transform.position.x < (transform.position.x + boxWidthRight) && xa.player.transform.position.x > (transform.position.x - boxWidthLeft) && xa.player.transform.position.y < (transform.position.y + boxHeightUp) && xa.player.transform.position.y > (transform.position.y - boxHeightDown))
					{
						//  Setup.GC_DebugLog("GOT HERE");
						if (triggered == 0) { triggeredFunc(); }
					}
				}
			}
			if (triggerWhenPlayerIsInScaleBox)
			{
				if (xa.player)
				{
					if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x) &&
						(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x) &&
						(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y) &&
						(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y))
					{
						//  Setup.GC_DebugLog("GOT HERE");
						if (triggered == 0) { triggeredFunc(); }
					}
				}
			}

			if (triggerWhenClickedOnColliderTaggedAsClickable)
			{
				if (Input.GetMouseButtonDown(0))
				{
					checkButton(Input.mousePosition);
					//checkButton(AspectUtility.mousePosition);
				}
			}
		}
	}


	public void triggeredFunc()
	{
		counter = fa.time;
		triggered = 1;
		permaTriggered = true;
	}

	void checkTriggered()
	{
		if (triggered < 2)
		{
			if ((counter + delayInSeconds) < fa.time)
			{




				if (checkForPopeHits)
				{
					triggered = 0;
					if (lessThanXPopeHits)
					{
						if (xa.popeHits < requiredPopeHits)
						{
							enableStuff();
						}
					}
					else
					{
						if (xa.popeHits >= requiredPopeHits)
						{
							enableStuff();
						}
					}
				}
				else if (checkForPopeHealth)
				{
					if (xa.popeHealth <= popeHealthLessThan)
					{
						triggered = 0;
						enableStuff();
					}
				}
				else
				{
					triggered = 0;
					enableStuff();
				}
			}
		}
	}

	void enableStuff()
	{
		////Debug.LogWarning (name + " enabled! " + Time.time.ToString());
	
		if (showDebugMsg)
		{
			//Setup.GC_DebugLog(debugMsg);
		}
	
		////Debug.LogWarning("Activating Scripts " + Time.time.ToString());
		foreach (Behaviour co in scriptsToActivate)
		{
	
			if (disableBehaviours) { co.enabled = false; /*//Debug.LogWarning(co.name + " disabled! " + Time.time.ToString());*/ }
			else { co.enabled = true; /*//Debug.LogWarning(co.name + " enabled! " + Time.time.ToString());*/ }
		}
		if (useSendMsg)
		{
			////Debug.LogWarning("Seding messages" + Time.time.ToString());
			int index = 0;
			while (index < GOToSendTo.Length)
			{
				if (GOToSendTo[index])
				{
					////Debug.LogWarning("Sending " + msgToSend[index] + " to " + GOToSendTo[index].name + " " + Time.time.ToString());
					GOToSendTo[index].SendMessage(msgToSend[index]);
				}
				index++;
			}
		}
		if (disableOnTriggering) { this.enabled = false; }
	}

	void checkButton(Vector3 inputVec)
	{
		Ray ray = new Ray();
		RaycastHit hit;
		ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(inputVec);
		if (this.gameObject.GetComponent<Collider>().Raycast(ray, out hit, 100) == true)
		{
			if (hit.collider.gameObject.tag == "clickable")
			{
				triggeredFunc();
			}
		}
	}
}
