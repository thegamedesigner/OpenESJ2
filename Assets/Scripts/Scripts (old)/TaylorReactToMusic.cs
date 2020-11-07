using UnityEngine;
using System.Collections;

public class TaylorReactToMusic : MonoBehaviour
{
	public bool SetXScaleToFreq = false;
	public bool SetYScaleToFreq = false;
	public float MinXScale = 0;
	public float MinYScale = 0;
	public float MultiplyXScaleByThis = 0;
	public float MultiplyYScaleByThis = 0;

	public bool slowXScaleDown = false;
	public bool slowYScaleDown = false;
	public float scaleDownSpeed = 0;
	public bool slowXScaleUp = false;
	public bool slowYScaleUp = false;
	public float scaleUpSpeed = 0;


	public int forceReadFromFreq = 0;
	float localFreq = 0;

	public float minFreqAmount = 0;
	public float maxScaleX = 0;
	public float maxScaleY = 0;
	
	public int oncePerXBeats = 0;
	
	//Moving Stuff
	public bool moveOnX = false;
	public bool moveOnY = false;
	public Vector2 movementVector = Vector2.zero;
	public float movementFreqMultiplier = 1;
	public bool capSpeed = true;
	public float movementMaxSpeed = 1;
	public bool divideSpeed = false;
	public float movementSpeedDivisor = 1;
	public bool modMovement = false;
	public int modAmount = 0;
	
	//Create Stuff
	public bool createOnBeat = false;
	public GameObject createOnBeatPrefab = null;

	public bool triggerITweensOnBeat = false;
	public string itweenName1 = "";
	public string itweenName2 = "";
	public string itweenName3 = "";

	public bool rotateConstantly = false;
	public Vector3 spdBelowMin = Vector3.zero;
	public Vector3 spdAboveMin = Vector3.zero;

	[HideInInspector]
	public bool beat = false;
	ReactToMusic beatParentScript;
	public GameObject beatParent;

	public bool useDelayAfterBeat = false;
	public float delayAfterBeatDelay = 0;
	float delayAfterBeatCounter = 0;
	public bool bufferFreq = false;
	float bufferedFreq = 0;

	public bool snapScaleOnBeat = false;
	public bool scaleUpSlowlyToSnap = false;
	public Vector3 snapScale = Vector3.zero;

	public float rotatingAmountOnBeat = 0;

	public bool useStartStopTime = false;
	public float startTime = 0;
	public float stopTime = 0;

	public bool disableAfterDelay = false;
	public float disableDelay = 0;

	public bool addAfterDelay1 = false;
	public float addDelay1 = 0;
	public float addAmount1 = 0;
	public float multiplyAmount1 = 0;
	public bool addAfterDelay2 = false;
	public float addDelay2 = 0;
	public float addAmount2 = 0;
	public float multiplyAmount2 = 0;
	public bool addAfterDelay3 = false;
	public float addDelay3 = 0;
	public float addAmount3 = 0;
	public float multiplyAmount3 = 0;
	public bool addAfterDelay4 = false;
	public float addDelay4 = 0;
	public float addAmount4 = 0;
	public float multiplyAmount4 = 0;
	public bool addAfterDelay5 = false;
	public float addDelay5 = 0;
	public float addAmount5 = 0;
	public float multiplyAmount5 = 0;
	float fakeAddAmount = 0;
	float fakeMultiplyAmount = 0;
	Vector3 startingPos = Vector3.zero;
	Vector3 oldPos;
	bool off = false;
	Vector3 scaleSnapGoal = Vector3.zero;
	int beatCount = -1;

	IEnumerator Start()
	{
		yield return new WaitForSeconds(0.1f);
		startingPos = transform.position;
	
		if (beatParent) { beatParentScript = beatParent.GetComponent<ReactToMusic>(); }

	}

	// void OnGUI()
	//  {
	//	  //EditorGUI.PrefixLabel(Rect(3, 3, position.width - 6, 15),		 0,		 GUIContent("Leave at zero"));
	// }

	void Update()
	{
	
		beat = false;

		//start, then stop, reacting to the music
		if (useStartStopTime)
		{
			if (xa.music_Time < startTime) { off = true; }
			if (xa.music_Time >= startTime) { off = false; }
			if (xa.music_Time >= stopTime) { off = true; }
		}

		if (!off)
		{


			if (beatParentScript)
			{
				if (beatParentScript.beat) { beatCount++; }
			}

			localFreq = xa.beat_Freq;
			if (forceReadFromFreq != 0)
			{
				localFreq = xa.music_Spectrum[forceReadFromFreq];
			}
			if (localFreq < minFreqAmount) { localFreq = 0; }

			if (useDelayAfterBeat)
			{
				if (delayAfterBeatCounter <= 0)
				{
					if (localFreq > 0)//because zero means it's already been capped by min beat
					{
						beatCount++;
						delayAfterBeatCounter = delayAfterBeatDelay;
						if (bufferFreq)
						{
							bufferedFreq = localFreq;
						}
					}
				}
				else
				{
					delayAfterBeatCounter -= 10 * fa.deltaTime;
				}
			}

			if (beatCount >= oncePerXBeats) { beatCount = -1; beat = true; }

			if (rotatingAmountOnBeat != 0 && beat)
			{
				xa.glx = transform.localEulerAngles;
				xa.glx.z += rotatingAmountOnBeat;
				transform.localEulerAngles = xa.glx;
			}
			if (snapScaleOnBeat && beat)
			{
				xa.glx = transform.localScale;
				if (snapScale.x != 0) { xa.glx.x = snapScale.x; }
				if (snapScale.y != 0) { xa.glx.y = snapScale.y; }
				if (snapScale.z != 0) { xa.glx.z = snapScale.z; }


				transform.localScale = xa.glx;

			}
			if (scaleUpSlowlyToSnap && beat)
			{
				scaleSnapGoal = snapScale;
			}


			if (rotateConstantly)
			{
				if (bufferedFreq >= minFreqAmount)
				{
					xa.glx = transform.localEulerAngles;
					xa.glx.x += spdAboveMin.x * fa.deltaTime;
					xa.glx.y += spdAboveMin.y * fa.deltaTime;
					xa.glx.z += spdAboveMin.z * fa.deltaTime;
					transform.localEulerAngles = xa.glx;
				}
				else
				{
					xa.glx = transform.localEulerAngles;
					xa.glx.x += spdBelowMin.x * fa.deltaTime;
					xa.glx.y += spdBelowMin.y * fa.deltaTime;
					xa.glx.z += spdBelowMin.z * fa.deltaTime;
					transform.localEulerAngles = xa.glx;
				}
			}

			xa.glx = transform.localScale;
			if (slowXScaleDown && !SetXScaleToFreq && scaleSnapGoal.x == 0) { xa.glx.x -= scaleDownSpeed * fa.deltaTime; if (xa.glx.x < MinXScale) { xa.glx.x = MinXScale; } }
			if (slowYScaleDown && !SetYScaleToFreq && scaleSnapGoal.y == 0) { xa.glx.y -= scaleDownSpeed * fa.deltaTime; if (xa.glx.y < MinYScale) { xa.glx.y = MinYScale; } }

			if (scaleUpSlowlyToSnap)
			{
				if (scaleSnapGoal.x > 0)
				{
					if (xa.glx.x < scaleSnapGoal.x) { xa.glx.x += scaleUpSpeed * fa.deltaTime; }
					if (xa.glx.x >= scaleSnapGoal.x) { xa.glx.x = scaleSnapGoal.x; scaleSnapGoal.x = 0; }
				}
				if (scaleSnapGoal.y > 0)
				{
					if (xa.glx.y < scaleSnapGoal.y) { xa.glx.y += scaleUpSpeed * fa.deltaTime; }
					if (xa.glx.y >= scaleSnapGoal.y) { xa.glx.y = scaleSnapGoal.y; scaleSnapGoal.y = 0; }
				}


			}
			transform.localScale = xa.glx;

			if (disableAfterDelay)
			{
				if (disableDelay <= xa.music_Time) { this.enabled = false; }
			}
			//this breaks after the music has looped! Fix!
			if (addAfterDelay1) { if (addDelay1 <= xa.music_Time) { fakeAddAmount += addAmount1; addAfterDelay1 = false; } }
			if (addAfterDelay2) { if (addDelay2 <= xa.music_Time) { fakeAddAmount += addAmount2; addAfterDelay2 = false; } }
			if (addAfterDelay3) { if (addDelay3 <= xa.music_Time) { fakeAddAmount += addAmount3; addAfterDelay3 = false; } }
			if (addAfterDelay4) { if (addDelay4 <= xa.music_Time) { fakeAddAmount += addAmount4; addAfterDelay4 = false; } }
			if (addAfterDelay5) { if (addDelay5 <= xa.music_Time) { fakeAddAmount += addAmount5; addAfterDelay5 = false; } }

			xa.glx = transform.localScale;
			if (SetXScaleToFreq)
			{
				float var1 = MinXScale + (localFreq * (MultiplyXScaleByThis + fakeMultiplyAmount)) + fakeAddAmount;

				if (var1 < xa.glx.x)
				{
					if (slowXScaleDown) { xa.glx.x -= scaleDownSpeed * fa.deltaTime; if (xa.glx.x < var1) { xa.glx.x = var1; } }
					else { xa.glx.x = var1; }
				}
				else
				{
					if (slowXScaleUp) { xa.glx.x += scaleUpSpeed * fa.deltaTime; if (xa.glx.x > var1) { xa.glx.x = var1; } }
					else { xa.glx.x = var1; }
				}
				if (xa.glx.x > maxScaleX && maxScaleX != 0) { xa.glx.x = maxScaleX; }
			}

			if (SetYScaleToFreq)
			{
				float var1 = MinYScale + (localFreq * (MultiplyYScaleByThis + fakeMultiplyAmount)) + fakeAddAmount;

				if (var1 < xa.glx.y)
				{
					if (slowYScaleDown) { xa.glx.y -= scaleDownSpeed * fa.deltaTime; if (xa.glx.y < var1) { xa.glx.y = var1; } }
					else { xa.glx.y = var1; }
				}
				else
				{
					if (slowYScaleUp) { xa.glx.y += scaleUpSpeed * fa.deltaTime; if (xa.glx.y > var1) { xa.glx.y = var1; } }
					else { xa.glx.y = var1; }
				}
				if (xa.glx.y > maxScaleY && maxScaleY != 0) { xa.glx.y = maxScaleY; }
			}
			transform.localScale = xa.glx;


			//move based on freq
			if((moveOnX || moveOnY) && startingPos != Vector3.zero)
			{
				xa.glx = startingPos;
				if(moveOnX)
					xa.glx.x = startingPos.x + (localFreq * movementFreqMultiplier) * movementVector.x;
	
				if(moveOnY)
					xa.glx.y = startingPos.y + (localFreq * movementFreqMultiplier) * movementVector.y;
	
				transform.position = xa.glx;
	
				//Divise speed
				if(divideSpeed)
					transform.position = oldPos + ((transform.position - oldPos) / movementSpeedDivisor);
	
				//Maximize speed
				if(capSpeed)
					transform.position = oldPos + ((transform.position - oldPos).normalized * movementMaxSpeed);
				//}
			}
	
			//call a custom function on the beat
			if (createOnBeat && beat)
			{
				xa.glx = transform.position;
				xa.glx.z = xa.GetLayer(xa.layers.Explo1);
				xa.tempobj = (GameObject)(Instantiate(createOnBeatPrefab, xa.glx, xa.null_quat));
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}

			//trigger some itweens on the beat
			if (triggerITweensOnBeat && beat)
			{
				if (itweenName1 != "") { iTweenEvent.GetEvent(this.gameObject, itweenName1).Play(); }
				if (itweenName2 != "") { iTweenEvent.GetEvent(this.gameObject, itweenName2).Play(); }
				if (itweenName3 != "") { iTweenEvent.GetEvent(this.gameObject, itweenName3).Play(); }
			}
	
			//Old Position
			oldPos = transform.position;
		}
	}


}
