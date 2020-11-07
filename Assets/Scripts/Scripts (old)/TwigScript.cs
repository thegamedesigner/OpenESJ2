using UnityEngine;
using System.Collections;

public class TwigScript : MonoBehaviour
{
	public bool startGrowing = false;
	public bool ignoreScreen = false;
	public float growSpdX = 0.5f;
	public float growSpdY = 0.5f;
	public float growSpdZ = 0.5f;

	public bool moving = false;
	public Vector3 rotationSpd = Vector3.zero;
	public Vector3 swaySpd = Vector3.zero;
	public Vector3 swayMax = Vector3.zero;
	public float addSpd = 0;
	Vector3 currSwaySpd = Vector3.zero;
	public bool reverseSway = false;

	public GameObject flowerPrefab = null;
	public bool amFlower = false;
	GameObject myFlower = null;

	public bool snapToParentsSnapPoint = false;
	public bool takeXAngFromParent = false;
	public bool takeYAngFromParent = false;
	public bool takeZAngFromParent = false;
	public bool keepPreexistingAnglesOffset = false;

	public GameObject obj1;
	public GameObject snapPointObj1;
	public bool triggerObj1 = false;
	public bool snapObj1ToSnapPoint = false;
	public bool useYStartingScale1 = false;
	public Vector3 triggerPercentageObj1 = Vector3.zero;

	public GameObject obj2;
	public GameObject snapPointObj2;
	public bool triggerObj2 = false;
	public bool snapObj2ToSnapPoint = false;
	public bool useYStartingScale2 = false;
	public Vector3 triggerPercentageObj2 = Vector3.zero;

	public GameObject obj3;
	public GameObject snapPointObj3;
	public bool triggerObj3 = false;
	public bool snapObj3ToSnapPoint = false;
	public bool useYStartingScale3 = false;
	public Vector3 triggerPercentageObj3 = Vector3.zero;

	public GameObject obj4;
	public GameObject snapPointObj4;
	public bool triggerObj4 = false;
	public bool snapObj4ToSnapPoint = false;
	public bool useYStartingScale4 = false;
	public Vector3 triggerPercentageObj4 = Vector3.zero;

	public bool useRandomScale = false;
	public bool useXForY = false;
	public Vector3 minRandomScale = Vector3.zero;
	public Vector3 maxRandomScale = Vector3.zero;

	public bool useRandomAngleX = false;
	public bool useRandomAngleY = false;
	public bool useRandomAngleZ = false;
	public Vector3 randomAngleMin = Vector3.zero;
	public Vector3 randomAngleMax = Vector3.zero;

	Vector3 startingScale;
	bool growing = false;
	bool doneGrowing = false;
	bool fullScale = false;
	GameObject parent1;
	TwigScript parent1Script;
	GameObject parentSnapPoint;
	TwigScript obj1Script = null;
	TwigScript obj2Script = null;
	TwigScript obj3Script = null;
	TwigScript obj4Script = null;
	Vector3 ranAngOffset = Vector3.zero;
	Vector3 currentSway = Vector3.zero;
	Vector3 currentSwayDir = Vector3.zero;
	Vector3 preexistingOffset = Vector3.zero;
	Vector3 currentTotalScale = Vector3.zero;
	
	public GameObject playSoundOnTrigger;

	[HideInInspector]
	public bool off = true;

	bool gotParent = false;
	float slowDownSpd = 2f;

	int add300Y = 0;

	void Start()
	{

		if (addSpd != 0) { slowDownSpd = addSpd; }
		currSwaySpd = swaySpd;

		currentTotalScale = transform.localScale;
		if (useRandomScale)
		{
			currentTotalScale.x = Random.Range(minRandomScale.x, maxRandomScale.x);
			currentTotalScale.y = Random.Range(minRandomScale.y, maxRandomScale.y);
			currentTotalScale.z = Random.Range(minRandomScale.z, maxRandomScale.z);

			if (useXForY) { currentTotalScale.y = currentTotalScale.x; }
		}

		//store starting stats
		startingScale = currentTotalScale;

		if (keepPreexistingAnglesOffset)
		{
			preexistingOffset = transform.localEulerAngles;
		}

		if (useYStartingScale1) { triggerPercentageObj1.y = startingScale.y; }
		if (useYStartingScale2) { triggerPercentageObj2.y = startingScale.y; }
		if (useYStartingScale3) { triggerPercentageObj3.y = startingScale.y; }
		if (useYStartingScale4) { triggerPercentageObj4.y = startingScale.y; }

		//snap to almost no scale
		xa.glx.y = 0.001f; xa.glx.x = 0.001f; xa.glx.z = 0.001f;
		transform.localScale = xa.glx;

		//get scripts
		if (obj1) { obj1Script = obj1.GetComponent<TwigScript>(); }
		if (obj2) { obj2Script = obj2.GetComponent<TwigScript>(); }
		if (obj3) { obj3Script = obj3.GetComponent<TwigScript>(); }
		if (obj4) { obj4Script = obj4.GetComponent<TwigScript>(); }

		//set parent1s
		if (obj1Script) { obj1Script.parent1 = this.gameObject; }
		if (obj2Script) { obj2Script.parent1 = this.gameObject; }
		if (obj3Script) { obj3Script.parent1 = this.gameObject; }
		if (obj4Script) { obj4Script.parent1 = this.gameObject; }

		if (obj1Script && snapPointObj1) { obj1Script.parentSnapPoint = snapPointObj1; }
		if (obj2Script && snapPointObj2) { obj2Script.parentSnapPoint = snapPointObj2; }
		if (obj3Script && snapPointObj3) { obj3Script.parentSnapPoint = snapPointObj3; }
		if (obj4Script && snapPointObj4) { obj4Script.parentSnapPoint = snapPointObj4; }

		//if random angles
		xa.glx = transform.localEulerAngles;
		if (useRandomAngleX) { ranAngOffset.x += Random.Range(randomAngleMin.x, randomAngleMax.x); }
		if (useRandomAngleY) { ranAngOffset.y += Random.Range(randomAngleMin.y, randomAngleMax.y); }
		if (useRandomAngleZ) { ranAngOffset.z += Random.Range(randomAngleMin.z, randomAngleMax.z); }
		xa.glx += ranAngOffset;
		transform.localEulerAngles = xa.glx;

		//set to parent angles
		takeParentAnglesOnce();

		//disable me
		if (!startGrowing) { this.enabled = false; }
	}

	void handleTurnOn()
	{
		//check if this plant is, in fact, on screen, etc.
		if (ignoreScreen)
		{
			growing = true;
			off = false;
		}
		else
		{
			if (!growing)
			{
				if (transform.position.x < xa.frontEdgeOfScreen)
				{
					growing = true;
					off = false;
				}
			}
		}
		if(growing)
		{
			if(playSoundOnTrigger)
			{
				//Debug.Log ("Playing Sound!!!!!!!!!!!!!!!!!!!");
				xa.tempobj = (GameObject)(Instantiate(playSoundOnTrigger, xa.player.transform.position, xa.null_quat));
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}
		}


	}

	void handleTurnOff()
	{
		//this should use xa.edgeOfScreen stuff
		//if (!Camera.main.camera.pixelRect.Contains(Camera.main.camera.WorldToScreenPoint(this.gameObject.transform.position)))
		//{
		//	off = true;
		//	this.enabled = false;
		//}
	}

	void handleChildTurnOff()
	{
		if (parent1Script)
		{
			if (parent1Script.off)
			{
				off = true;
				this.enabled = false;
			}
		}
	}

	void Update()
	{
		if (add300Y > 0)
		{
			add300Y -= 1;
			if (add300Y <= 0)
			{
				xa.glx = transform.position;
				//   xa.glx.y += 300;
				transform.position = xa.glx;
			}
		}
		if (myFlower)
		{
			myFlower.transform.localScale = transform.localScale;
			xa.glx = myFlower.transform.localPosition;
			xa.glx.z = -1;
			myFlower.transform.localPosition = xa.glx;
		}

		//get parent
		if (!gotParent) { getParent(); }

		//check if I should turn on
		if (startGrowing) { handleTurnOn(); }

		if (!off)
		{
			//check if I can turn off
			if (startGrowing) { handleTurnOff(); }//seed
			else { handleChildTurnOff(); }//non-seed

			//handle growing
			if (growing && !doneGrowing) { handleGrowing(); }

			//handle moving
			if (moving) { handleMoving(); }

			//take traits from parent in real-time
			if (takeXAngFromParent || takeYAngFromParent || takeZAngFromParent) { takeParentAngles(); }

			//snap to parent's snap point in real-time
			if (snapToParentsSnapPoint) { transform.position = parentSnapPoint.transform.position; }

			//handle triggering objects 1-4
			if (triggerObj1 || triggerObj2 || triggerObj3 || triggerObj4) { handleTriggeringObjs(); }
		}
	}

	void handleGrowing()
	{
		if (growing && !fullScale)
		{
			//grow
			xa.glx = transform.localScale;
			float devSpd = 1;
			if (transform.localScale.x < currentTotalScale.x) { xa.glx.x += growSpdX * devSpd * fa.deltaTime; }
			if (transform.localScale.y < currentTotalScale.y) { xa.glx.y += growSpdY * devSpd * fa.deltaTime; }
			if (transform.localScale.z < currentTotalScale.z) { xa.glx.z += growSpdZ * devSpd * fa.deltaTime; }

			if (xa.glx.x > currentTotalScale.x) { xa.glx.x = currentTotalScale.x; }
			if (xa.glx.y > currentTotalScale.y) { xa.glx.y = currentTotalScale.y; }
			if (xa.glx.z > currentTotalScale.z) { xa.glx.z = currentTotalScale.z; }

			if (transform.localScale.x >= currentTotalScale.x && transform.localScale.y >= currentTotalScale.y && transform.localScale.z >= currentTotalScale.z)
			{ fullScale = true; }

			transform.localScale = xa.glx;
		}
		else if (fullScale)
		{
			//has reached full size
			doneGrowing = true;
		}
	}

	void handleMoving()
	{
		//move branch
		//rotation
		xa.glx = transform.localEulerAngles;
		xa.glx += rotationSpd * fa.deltaTime;
		transform.localEulerAngles = xa.glx;

		//swaying
		if (swaySpd.x == 0) { currSwaySpd.x = 0; }
		if (swaySpd.y == 0) { currSwaySpd.y = 0; }
		if (swaySpd.z == 0) { currSwaySpd.z = 0; }

		xa.glx = transform.localEulerAngles;
		if (reverseSway)
		{
			if (currentSwayDir.x >= 0) { currentSway.x += currSwaySpd.x * fa.deltaTime; xa.glx.x -= currSwaySpd.x * fa.deltaTime; }
			else { currentSway.x -= currSwaySpd.x * fa.deltaTime; xa.glx.x += currSwaySpd.x * fa.deltaTime; }
			if (currentSwayDir.y >= 0) { currentSway.y += currSwaySpd.y * fa.deltaTime; xa.glx.y -= currSwaySpd.y * fa.deltaTime; }
			else { currentSway.y -= currSwaySpd.y * fa.deltaTime; xa.glx.y += currSwaySpd.y * fa.deltaTime; }
			if (currentSwayDir.z >= 0) { currentSway.z += currSwaySpd.z * fa.deltaTime; xa.glx.z -= currSwaySpd.z * fa.deltaTime; }
			else { currentSway.z -= currSwaySpd.z * fa.deltaTime; xa.glx.z += currSwaySpd.z * fa.deltaTime; }
		}
		else
		{
			if (currentSwayDir.x >= 0) { currentSway.x += currSwaySpd.x * fa.deltaTime; xa.glx.x += currSwaySpd.x * fa.deltaTime; }
			else { currentSway.x -= currSwaySpd.x * fa.deltaTime; xa.glx.x -= currSwaySpd.x * fa.deltaTime; }
			if (currentSwayDir.y >= 0) { currentSway.y += currSwaySpd.y * fa.deltaTime; xa.glx.y += currSwaySpd.y * fa.deltaTime; }
			else { currentSway.y -= currSwaySpd.y * fa.deltaTime; xa.glx.y -= currSwaySpd.y * fa.deltaTime; }
			if (currentSwayDir.z >= 0) { currentSway.z += currSwaySpd.z * fa.deltaTime; xa.glx.z += currSwaySpd.z * fa.deltaTime; }
			else { currentSway.z -= currSwaySpd.z * fa.deltaTime; xa.glx.z -= currSwaySpd.z * fa.deltaTime; }
		}

		if (currentSway.x > swayMax.x && currentSwayDir.x >= 0) { currSwaySpd.x -= slowDownSpd * fa.deltaTime; } else if (currentSwayDir.x >= 0) { currSwaySpd.x += slowDownSpd * fa.deltaTime; }
		if (currentSway.x < -swayMax.x && currentSwayDir.x < 0) { currSwaySpd.x -= slowDownSpd * fa.deltaTime; } else if (currentSwayDir.x < 0) { currSwaySpd.x += slowDownSpd * fa.deltaTime; }
		if (currentSway.y > swayMax.y && currentSwayDir.y >= 0) { currSwaySpd.y -= slowDownSpd * fa.deltaTime; } else if (currentSwayDir.y >= 0) { currSwaySpd.y += slowDownSpd * fa.deltaTime; }
		if (currentSway.y < -swayMax.y && currentSwayDir.y < 0) { currSwaySpd.y -= slowDownSpd * fa.deltaTime; } else if (currentSwayDir.y < 0) { currSwaySpd.y += slowDownSpd * fa.deltaTime; }
		if (currentSway.z > swayMax.z && currentSwayDir.z >= 0) { currSwaySpd.z -= slowDownSpd * fa.deltaTime; } else if (currentSwayDir.z >= 0) { currSwaySpd.z += slowDownSpd * fa.deltaTime; }
		if (currentSway.z < -swayMax.z && currentSwayDir.z < 0) { currSwaySpd.z -= slowDownSpd * fa.deltaTime; } else if (currentSwayDir.z < 0) { currSwaySpd.z += slowDownSpd * fa.deltaTime; }
		if (currSwaySpd.x > swaySpd.x) { currSwaySpd.x = swaySpd.x; }
		if (currSwaySpd.y > swaySpd.y) { currSwaySpd.y = swaySpd.y; }
		if (currSwaySpd.z > swaySpd.z) { currSwaySpd.z = swaySpd.z; }

		if (currentSway.x > swayMax.x && currentSwayDir.x >= 0 && currSwaySpd.x <= 0) { currentSwayDir.x = -1; }
		if (currentSway.x < -swayMax.x && currentSwayDir.x < 0 && currSwaySpd.x <= 0) { currentSwayDir.x = 1; }
		if (currentSway.y > swayMax.y && currentSwayDir.y >= 0 && currSwaySpd.y <= 0) { currentSwayDir.y = -1; }
		if (currentSway.y < -swayMax.y && currentSwayDir.y < 0 && currSwaySpd.y <= 0) { currentSwayDir.y = 1; }
		if (currentSway.z > swayMax.z && currentSwayDir.z >= 0 && currSwaySpd.z <= 0) { currentSwayDir.z = -1; }
		if (currentSway.z < -swayMax.z && currentSwayDir.z < 0 && currSwaySpd.z <= 0) { currentSwayDir.z = 1; }

		// if (swaySpd.z != 0) { Setup.GC_DebugLog(currSwaySpd.z); }

		transform.localEulerAngles = xa.glx;
	}

	void handleTriggeringObjs()
	{
		//trigger objects
		if (triggerObj1)
		{
			if (transform.localScale.x >= triggerPercentageObj1.x && transform.localScale.y >= triggerPercentageObj1.y && transform.localScale.z >= triggerPercentageObj1.z)
			{
				if (obj1Script && obj1)
				{
					obj1Script.enabled = true;
					obj1Script.growing = true;
					obj1Script.off = false;

					if (snapObj1ToSnapPoint)
					{
						obj1.transform.position = snapPointObj1.transform.position;
						obj1Script.parentSnapPoint = snapPointObj1;
					}
				}
			}
		}
		if (triggerObj2)
		{
			if (transform.localScale.x >= triggerPercentageObj2.x && transform.localScale.y >= triggerPercentageObj2.y && transform.localScale.z >= triggerPercentageObj2.z)
			{
				if (obj2Script && obj2)
				{
					obj2Script.enabled = true;
					obj2Script.growing = true;
					obj2Script.off = false;

					if (snapObj2ToSnapPoint)
					{
						obj2.transform.position = snapPointObj2.transform.position;
						obj2Script.parentSnapPoint = snapPointObj2;
					}
				}
			}
		}
		if (triggerObj3)
		{
			if (transform.localScale.x >= triggerPercentageObj3.x && transform.localScale.y >= triggerPercentageObj3.y && transform.localScale.z >= triggerPercentageObj3.z)
			{
				if (obj3Script && obj3)
				{
					obj3Script.enabled = true;
					obj3Script.growing = true;
					obj3Script.off = false;

					if (snapObj3ToSnapPoint)
					{
						obj3.transform.position = snapPointObj3.transform.position;
						obj3Script.parentSnapPoint = snapPointObj3;
					}
				}
			}
		}
		if (triggerObj4)
		{
			if (transform.localScale.x >= triggerPercentageObj4.x && transform.localScale.y >= triggerPercentageObj4.y && transform.localScale.z >= triggerPercentageObj4.z)
			{
				if (obj4Script && obj4)
				{
					obj4Script.enabled = true;
					obj4Script.growing = true;
					obj4Script.off = false;

					if (snapObj4ToSnapPoint)
					{
						obj4.transform.position = snapPointObj4.transform.position;
					}
				}
			}
		}
	}

	void takeParentAngles()
	{
		//take traits from parent in real-time
		if (parent1)
		{
			xa.glx = transform.localEulerAngles;
			if (takeXAngFromParent)
			{
				xa.glx.x = parent1.transform.localEulerAngles.x;
				xa.glx.x += ranAngOffset.x;
				xa.glx.x += preexistingOffset.x;
			}
			if (takeYAngFromParent) { xa.glx.y = parent1.transform.localEulerAngles.y; xa.glx.y += ranAngOffset.y; xa.glx.y += preexistingOffset.y; }
			if (takeZAngFromParent) { xa.glx.z = parent1.transform.localEulerAngles.z; xa.glx.z += ranAngOffset.z; xa.glx.z += preexistingOffset.z; }
			transform.localEulerAngles = xa.glx;
		}
	}

	void takeParentAnglesOnce()
	{
		//take traits from parent, once, in start
		if (parent1)
		{
			xa.glx = transform.localEulerAngles;
			if (takeXAngFromParent) { xa.glx.x = parent1.transform.localEulerAngles.x; }
			if (takeYAngFromParent) { xa.glx.y = parent1.transform.localEulerAngles.y; }
			if (takeZAngFromParent) { xa.glx.z = parent1.transform.localEulerAngles.z; }
			xa.glx += ranAngOffset;
			transform.localEulerAngles = xa.glx;
		}
	}

	void getParent()
	{
		if (!gotParent)
		{
			if (parent1)
			{
				gotParent = true;
				parent1Script = parent1.GetComponent<TwigScript>();
				if (!flowerPrefab) { flowerPrefab = parent1Script.flowerPrefab; }


				if (amFlower)
				{

					if (flowerPrefab)
					{
						//Setup.GC_DebugLog("Boom " + fa.time);
						//xa.glx = transform.position;
						/*
						xa.glx = myFlower.transform.localEulerAngles;
						xa.glx.z = transform.localEulerAngles.z;
						myFlower.transform.localEulerAngles = xa.glx;*/

						myFlower = (GameObject)(Instantiate(flowerPrefab, transform.position, transform.rotation));
						myFlower.transform.parent = transform;
	
					}
				}

			}
		}
	}
}
