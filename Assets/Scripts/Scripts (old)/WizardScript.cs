using UnityEngine;
using System.Collections;

public class WizardScript : MonoBehaviour
{
	public GameObject puppet;
	public GameObject gun;
	public GameObject textObj;
	WizardScript2 secondScript;
	//public GameObject heart;

	GunScript gunScript;
   // BossHeartScript heartScript;

	string stance = "idle";

	public int x1 = 0;
	public int y1 = 0;
	public int z1 = 0;
	public int numOfFrames1 = 0;
	public float speed1 = 0;

	[HideInInspector]
	public int saySomething = 0;
	float sayIdleTime = 12;

	float counter = 0;
	float sayCounter = 0;
	string currAni = "";
	string oldAni = "";
	int oldx1 = 0;
	int oldy1 = 0;
	TextMesh textMesh;
	float startCounter = 12;
	int hurtTextIndex = 0;

	bool dead = false;

	void OnGUI()
	{
		/*if (!xa.noDebug)
		{
		   // GUI.Label(new Rect(10, 300, 1000, 1000), currAni);

			if (gunScript)
			{
			   // GUI.Label(new Rect(10, 200, 1000, 1000), ""+gunScript.delayBeforeFiringCounter);

			}
		}
		*/
	}

	void Start()
	{
		x1 = 0;
		y1 = 0;
		setPuppetsTexture(x1, y1);

		gunScript = gun.GetComponent<GunScript>();
		secondScript = this.gameObject.GetComponent<WizardScript2>();
	 //   heartScript = heart.GetComponent<BossHeartScript>();
	}

	void setHurtText()
	{
		xa.wizardCutsceneScript.stage++;
		//Setup.GC_DebugLog("GOT HERE");
		hurtTextIndex++;
	}

	void setStartingFrame()
	{
		counter = 0;
		if (stance == "idle") { x1 = 0; y1 = 0; }
		if (stance == "goIntoFiring") { x1 = 4; y1 = 1; }
		if (stance == "goOutOfFiring") { x1 = 7; y1 = 1; }
		if (stance == "fire") { x1 = 6; y1 = 0; }
		if (stance == "doubleArmsUp") { x1 = 0; y1 = 1; }
		if (stance == "doubleArmsIdle") { x1 = 3; y1 = 0; }
		if (stance == "doubleArmsDown") { x1 = 4; y1 = 1; }

	}
	void Update()
	{
		if (!dead)
		{

			if (startCounter > 0)
			{
				startCounter -= 10 * fa.deltaTime;
				if (startCounter <= 0)
				{
					saySomething = 1;
				}
			}

			counter += 10 * fa.deltaTime;
			oldAni = stance;
			oldx1 = x1;
			oldy1 = y1;
			stance = "idle";
			if (gunScript.delayAfterFiringCounter > 0) { stance = "goOutOfFiring"; }
			if (!gunScript.reloading) { stance = "fire"; }
			if (gunScript.delayBeforeFiringCounter > 0) { stance = "goIntoFiring"; }

			if (saySomething == 1)
			{
				sayCounter += 10 * fa.deltaTime;
				if (sayCounter > 555)
				{
					sayCounter = 0;
					saySomething = 2;

				   // xa.wizardCutsceneStage++;
				}
			}
			else if (saySomething == 2)
			{
				sayCounter += 10 * fa.deltaTime;
				if (sayCounter > sayIdleTime)
				{
					sayCounter = 0;
					saySomething = 3;
				}
			}
			else if (saySomething == 3)
			{
				sayCounter += 10 * fa.deltaTime;
				if (sayCounter > 555)
				{
					sayCounter = 0;
					saySomething = 0;
				}
			}

			if (saySomething == 1) { stance = "doubleArmsUp"; }
			if (saySomething == 2) { stance = "doubleArmsIdle"; }
			if (saySomething == 3) { stance = "doubleArmsDown"; }

			if (stance != oldAni) { setStartingFrame(); }

			if (stance == "fire")
			{
				if (counter >= 1.5)
				{
					counter = 0;
					x1++;
					if (x1 > 7)
					{
						x1 = 6;
					}
					setPuppetsTexture(x1, y1);
				}
			}
			if (stance == "goIntoFiring")
			{
				if (counter >= 0.5)
				{
					counter = 0;
					x1++;
					if (x1 > 7)
					{
						x1 = 7;//just stick on last frame
						gunScript.delayBeforeFiringCounter = 0;
					}
					setPuppetsTexture(x1, y1);
				}
			}
			if (stance == "goOutOfFiring")
			{
				if (counter >= 0.5)
				{
					counter = 0;
					x1--;
					if (x1 < 4)
					{
						x1 = 4;//just stick on last frame
						gunScript.delayAfterFiringCounter = 0;
					}
					setPuppetsTexture(x1, y1);
				}
			}
			if (stance == "doubleArmsIdle")
			{
				if (counter >= 1.5)
				{
					counter = 0;
					x1++;
					if (x1 > 4)
					{
						x1 = 3;//loop
					}
					setPuppetsTexture(x1, y1);
				}
			}
			if (stance == "doubleArmsUp")
			{
				if (counter >= 0.5)
				{
					counter = 0;
					x1++;
					if (x1 > 3)
					{
						x1 = 3;//just stick on last frame
						sayCounter = 999;
					}
					setPuppetsTexture(x1, y1);
				}
			}
			if (stance == "doubleArmsDown")
			{
				if (counter >= 0.5)
				{
					counter = 0;
					x1--;
					if (x1 < 0)
					{
						x1 = 0;//just stick on last frame
						sayCounter = 999;
					}
					setPuppetsTexture(x1, y1);
				}
			}



			if (stance == "yell")
			{
				setPuppetsTexture(2, 0);
			}
			if (stance == "idle")
			{
				if (counter >= 1.5)
				{
					counter = 0;
					x1++;
					if (x1 > 1)
					{
						x1 = 0;
					}
					setPuppetsTexture(x1, 0);
				}
			}



			if (x1 != oldx1 || y1 != oldy1)
			{
				currAni += " - ";
				if (x1 == 0 && y1 == 0) { currAni += "idle 1"; }
				if (x1 == 1 && y1 == 0) { currAni += "idle 2"; }
				if (x1 == 4 && y1 == 1) { currAni += "armUp 1"; }
				if (x1 == 5 && y1 == 1) { currAni += "armUp 2"; }
				if (x1 == 6 && y1 == 1) { currAni += "armUp 3"; }
				if (x1 == 7 && y1 == 1) { currAni += "armUp 4"; }
				if (x1 == 6 && y1 == 0) { currAni += "fire 1"; }
				if (x1 == 7 && y1 == 0) { currAni += "fire 2"; }
				if (x1 == 3 && y1 == 0) { currAni += "doubleArmsIdle 1"; }
				if (x1 == 4 && y1 == 0) { currAni += "doubleArmsIdle 2"; }
				if (x1 == 0 && y1 == 1) { currAni += "doubleArmsUp 1"; }
				if (x1 == 1 && y1 == 1) { currAni += "doubleArmsUp 2"; }
				if (x1 == 2 && y1 == 1) { currAni += "doubleArmsUp 3"; }
				if (x1 == 3 && y1 == 1) { currAni += "doubleArmsUp 4"; }

			}
		}
		else
		{

			counter += 10 * fa.deltaTime;
			if (counter >= 1.5)
			{
				counter = 0;
				z1++;
				if (z1 >= 2)
				{
					z1 = 0;
				}
	
				setPuppetsTexture(z1, 0);
			}

		}
	}

	void setPuppetsTexture(int v1, int v2)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f;
		y1 = 0.125f;
		x2 = 0.125f * v1;
		y2 = 1 - ((0.125f * v2) + 0.125f);

		puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}

	public void die()
	{
		//iTweenEvent.GetEvent(this.gameObject, "flyUp").Play();
		dead = true;
		secondScript.triggerMe();//
	 //   this.enabled = false;
		//return;

	}
}
