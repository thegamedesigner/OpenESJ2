using UnityEngine;
using System.Collections;

public class SpawnBasedOnDistFromPlayer : MonoBehaviour
{
	public string[] longStrings;
	public GameObject go                                                = null;
	public GameObject forceUseThisGOInsteadOfPlayer                     = null;
	public GameObject forceSpawnAsChildOfThisGO                         = null;
	public Transform overrideSpawnPoint                                 = null;
	public string line1                                                 = "";
	public string line2                                                 = "";
	public string line3                                                 = "";
	public string b_line1                                               = "";
	public string b_line2                                               = "";
	public string b_line3                                               = "";
	public string c_line1                                               = "";
	public string c_line2                                               = "";
	public string c_line3                                               = "";
	public float dist                                                   = 0.0f;
	public float delay                                                  = 0.0f;
	public float startDelay                                             = 0.0f;
	public float forceYOffset                                           = 0.0f;
	public SetRendererBasedOnControlsType.controlTypes spawnControlType = SetRendererBasedOnControlsType.controlTypes.Keyboard;
	public int ranAmount                                                = 0;
	public bool useLongStrings                                          = false;
	public bool loop                                                    = false;
	public bool fastFadeTextOut                                         = false;
	public bool slowFadeTextOut                                         = false;
	public bool useRandomText                                           = false;
	public bool spawnOnSpecificControls                                 = false;
	public bool useForceLocal                                           = false;
	public bool useOnlyX                                                = false;
	public bool isText                                                  = true;
	public bool triggerBasedOnDistance                                  = true;
	public bool startTriggered                                          = false;
	TextMesh textMesh                                                   = null;
	float startDelayTime                                                = 0.0f;
	float counter                                                       = 0.0f;
	float result                                                        = 0.0f;
	int longStringIndex                                                 = 0;
	bool triggered                                                      = false;
	bool forceTrigger                                                   = false;
	bool hit                                                            = false;

	void Start()
	{
		startDelayTime = fa.time;
		if (startTriggered)
		{
			forceTrigger = true;
		}
		
		// Check strings for profanity
		if(xa.pgMode && isText)
		{
			if(xa.ma.checkStringForProfanity(line1))
				line1 = "CENSORED";
			if(xa.ma.checkStringForProfanity(line2))
				line2 = "CENSORED";
			if(xa.ma.checkStringForProfanity(line3))
				line3 = "CENSORED";
			if(xa.ma.checkStringForProfanity(b_line1))
				b_line1 = "CENSORED";
			if(xa.ma.checkStringForProfanity(b_line2))
				b_line2 = "CENSORED";
			if(xa.ma.checkStringForProfanity(b_line3))
				b_line3 = "CENSORED";
			if(xa.ma.checkStringForProfanity(c_line1))
				c_line1 = "CENSORED";
			if(xa.ma.checkStringForProfanity(c_line2))
				c_line2 = "CENSORED";
			if(xa.ma.checkStringForProfanity(c_line3))
				c_line3 = "CENSORED";
			if(useLongStrings)
			{
				for(int i = 0; i < longStrings.Length; i++)
				{
					if(xa.ma.checkStringForProfanity(longStrings[i]))
						longStrings[i] = "CENSORED";
				}
			}
		}
	}

	void Update()
	{
		handleStuffFunc();
	}

	void handleStuffFunc()
	{
		if (loop && triggered)
		{
			counter += 10 * fa.deltaTime;
			if (counter > delay)
			{
				counter = 0;
				forceTrigger = true;
			}
		}
		if (!triggered || forceTrigger)
		{
			hit = false;
			if (forceTrigger)
			{
				hit = true;
				forceTrigger = false;
				triggered = true;
			}
			if (triggerBasedOnDistance && ( xa.player || forceUseThisGOInsteadOfPlayer))
			{
				if(!spawnOnSpecificControls || (spawnOnSpecificControls && spawnControlType == SetRendererBasedOnControlsType.controlType))
				{
					xa.glx = transform.position;
					if (forceUseThisGOInsteadOfPlayer)
					{
						xa.glx.z = forceUseThisGOInsteadOfPlayer.transform.position.z;
						if (useOnlyX) { xa.glx.y = xa.player.transform.position.y; }
						if (Vector3.Distance(xa.glx, forceUseThisGOInsteadOfPlayer.transform.position) < dist)
						{
							hit = true;
							triggered = true;
						}
					}
					else
					{
						xa.glx.z = xa.player.transform.position.z;
						if (useOnlyX) { xa.glx.y = xa.player.transform.position.y; }
						if (Vector3.Distance(xa.glx, xa.player.transform.position) < dist)
						{
							hit = true;
							triggered = true;
						}
					}
				}
			}
			if (hit)
			{
				if ((startDelay + startDelayTime) <= fa.time)
				{
					createObject();
				}
			}
		}
	}

	public void createObject()
	{
		if (go)
		{
			if (overrideSpawnPoint)
			{
				xa.glx = overrideSpawnPoint.position;
			}
			else
			{
				xa.glx = transform.position;
			}
			
			xa.glx.y -= 0.3f;
			xa.glx.y += forceYOffset;
			result = 0;
			if (useRandomText)
			{
				result = Random.Range(0, (int)(ranAmount));
				if (result == ranAmount) { result = 0; }
			}
			if (isText)
			{
				if (result == 0) { if (line2 != "") { xa.glx.y += 0.7f; } if (line3 != "") { xa.glx.y += 0.7f; } }
				if (result == 1) { if (b_line2 != "") { xa.glx.y += 0.7f; } if (b_line3 != "") { xa.glx.y += 0.7f; } }
				if (result == 2) { if (c_line2 != "") { xa.glx.y += 0.7f; } if (c_line3 != "") { xa.glx.y += 0.7f; } }
			}
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.tempobj = (GameObject)(Instantiate(go, xa.glx, xa.null_quat));
			if (isText)
			{
				textMesh = xa.tempobj.GetComponentInChildren<TextMesh>();
				if (useLongStrings)
				{
					if (longStringIndex < longStrings.Length)
					{
						textMesh.text = longStrings[longStringIndex];
						longStringIndex++;
					}
					else
					{
						textMesh.text = longStrings[longStrings.Length - 1];
					}
				}
				else
				{
					textMesh.text = line1 + "\n" + line2 + "\n" + line3;
					if (useRandomText)
					{
						if (result == 0) { textMesh.text = line1 + "\n" + line2 + "\n" + line3; }
						if (result == 1) { textMesh.text = b_line1 + "\n" + b_line2 + "\n" + b_line3; }
						if (result == 2) { textMesh.text = c_line1 + "\n" + c_line2 + "\n" + c_line3; }
					}
				}
				if (fastFadeTextOut)
				{
					iTweenEvent.GetEvent(textMesh.gameObject, "fadeOutFast").Play();
					// iTweenEvent.GetEvent(xa.tempobj, "trigger2").Play();
				}
				if (slowFadeTextOut)
				{
					iTweenEvent.GetEvent(textMesh.gameObject, "fadeOutSlow").Play();
					// iTweenEvent.GetEvent(xa.tempobj, "trigger2").Play();
				}
			}
			else // !isText
			{
				xa.onScreenObjectsDirty = true;
			}
			
			if (xa.createdObjects && !useForceLocal)
			{
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}
			
			if (useForceLocal)
			{
				xa.tempobj.transform.parent = forceSpawnAsChildOfThisGO.transform;
			}
		}
	}
}
