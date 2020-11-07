using UnityEngine;
using System.Collections;

public class LiquidScript : MonoBehaviour
{
	public static int[,] liquidGrid = new int[0, 0];//width, height
	public static int[,] liquidNew = new int[0, 0];//width, height
	public static bool[,] liquidUsable = new bool[0, 0];//width, height
	public static int[,] liquidBlur = new int[0, 0];//width, height
	public static LiquidScript[,] liquidScripts;
	public static int blurDelay = 4;

	public int amount = 0;
	int maxAmount = 3;
	public GameObject puppet;
	public GameObject puppetPtr;

	int index;
	int index2;
	int myx;
	int myy;
	bool left;
	bool right;
	int choice;//1 = left, 2= right, 0= nowhere
	//Ani_Triggered aniScript;
	int boogle = 1;//false = left, true = right;
	//int oldAniState = -1;
	//int aniState = 0;
	bool flowedDown = false;
	void Start()
	{
		myx = (int)(transform.position.x);
		myy = (int)(transform.position.y);
		liquidGrid[myx, myy] = amount;
		liquidUsable[myx, myy] = true;
		liquidScripts[myx, myy] = this;

		//create my puppet
		xa.tempobj = (GameObject)(Instantiate(puppet, transform.position, puppet.transform.rotation));
		xa.tempobj.transform.parent = transform;
		puppetPtr = xa.tempobj;
		//aniScript = puppetPtr.GetComponent<Ani_Triggered>();

		GetComponent<Renderer>().enabled = false;
	}

	void Update()
	{
		//animateMe();
		/*
		 Setup.GC_DebugLog(fa.time + " " + timeSet);
			  if (fa.time >= (timeSet + liquidUpdateSpeedInSeconds))
			  {
				  timeSet = fa.time;
				  handleLiquid();
			  }
	  */
	}

	public void handleNewLiquid()
	{
		myx = (int)(transform.position.x);
		myy = (int)(transform.position.y);
		liquidGrid[myx, myy] += liquidNew[myx, myy];
		liquidNew[myx, myy] = 0;
	}

	public void handleLiquid()
	{
		myx = (int)(transform.position.x);
		myy = (int)(transform.position.y);
		flowedDown = false;

		//Do I have any liquid to care about?
		if (liquidGrid[myx, myy] == 0) { return; }

		//check the sq below me
		if (checkIfCanFlowDown())//flow down
		{
			//the square beneath me is usable & not full
			//not full, so add to it
			liquidGrid[myx, myy]--;
			liquidNew[myx, (myy - 1)]++;
			flowedDown = true;
			//if (Random.Range(0, 10) <= 5) { boogle = 1; }
			// else { boogle = 2; }

		}
		else//flow sideways (if possible)
		{
			//spread sideways.
			choice = 0;
			checkLeftAndRight();
			chooseBetweenLeftAndRight();
			if (choice == 1)
			{
				//flow left
				liquidGrid[myx, myy]--;
				liquidNew[(myx - 1), myy]++;
				boogle = 2;
			}
			if (choice == 2)
			{
				//flow right
				liquidGrid[myx, myy]--;
				liquidNew[(myx + 1), myy]++;
				boogle = 1;
			}
		}

	}

	public void setBlur()
	{
		if (flowedDown) { liquidBlur[myx, myy] = blurDelay; }
		if (liquidGrid[myx, myy] > 0) { liquidBlur[myx, myy] = blurDelay; }
	}

	bool checkIfCanFlowDown()
	{
		if ((myy - 1) >= 0)//isn't off the grid
		{
			if (liquidUsable[myx, (myy - 1)])//is a usable square below me
			{
				if ((liquidGrid[myx, (myy - 1)] + liquidNew[myx, (myy - 1)]) < maxAmount)//isn't full
				{
					return (true);
				}
			}
		}
		return (false);
	}

	void chooseBetweenLeftAndRight()
	{
		choice = 0;
		//if (liquidGrid[myx, myy] > 1)
		//{
		if (left && !right)
		{
			choice = 1;
		}
		else if (!left && right)
		{
			choice = 2;
		}
		else if (left && right)
		{
			//if (liquidGrid[(myx - 1), myy] < liquidGrid[(myx + 1), myy])
			//{
			//    choice = 1;
			//}
			// else if (liquidGrid[(myx - 1), myy] > liquidGrid[(myx + 1), myy])
			//{
			//    choice = 2;
			//}
			//else
			//{
			if (boogle == 1) { choice = 1; }
			if (boogle == 2) { choice = 2; }
			//}
		}
		//}
	}

	void checkLeftAndRight()
	{
		//can I go left?
		left = true;
		if ((myx - 1) < 0) { left = false; }//off grid
		else
		{
			if (!liquidUsable[(myx - 1), myy]) { left = false; }//not usable
			else
			{
				if ((liquidGrid[(myx - 1), myy] + liquidNew[(myx - 1), myy]) >= maxAmount) { left = false; }//full
			}
		}

		//can I go right?
		right = true;
		if ((myx + 1) >= liquidGrid.GetLength(0)) { right = false; }//off grid
		else
		{
			if (!liquidUsable[(myx + 1), myy]) { right = false; }//not usable
			else
			{
				if ((liquidGrid[(myx + 1), myy] + liquidNew[(myx + 1), myy]) >= maxAmount) { right = false; }//full
			}
		}
	}


	public void animateMe()
	{
		myx = (int)(transform.position.x);
		myy = (int)(transform.position.y);

		// setMyAniState();
		// if (oldAniState != aniState) { oldAniState = aniState; playCorrectAni(); }
		if (liquidBlur[myx, myy] <= 0) { Setup.setTexture(0, 3, 0.25f, puppetPtr, false); }
		if (liquidBlur[myx, myy] > 0)
		{
			setCorrectFrame();
		}
		//if (liquidGrid[myx, myy] <= 0) { Setup.setTexture(0, 3, 0.25f, puppetPtr); }
		//if (liquidGrid[myx, myy] > 0) { Setup.setTexture(3, 13, 0.25f, puppetPtr); }


	}

	bool resultLeft = false;
	bool resultRight = false;
	bool resultUp = false;
	bool resultDown = false;

	void setCorrectFrame()
	{
		resultLeft = false;
		resultRight = false;
		resultUp = false;
		resultDown = false;

		if (checkIndex((myx - 1), true))
		{
			if (liquidBlur[(myx - 1), myy] > 0 || liquidUsable[(myx - 1), myy] == false) { resultLeft = true; }
		}
		else { resultLeft = true; }
		if (checkIndex((myx + 1), true))
		{
			if (liquidBlur[(myx + 1), myy] > 0 || liquidUsable[(myx + 1), myy] == false) { resultRight = true; }
		}
		else { resultRight = true; }
		if (checkIndex((myy - 1), false))
		{
			if (liquidBlur[myx, (myy - 1)] > 0 || liquidUsable[myx, (myy - 1)] == false) { resultDown = true; }
		}
		else { resultDown = true; }
		if (checkIndex((myy + 1), false))
		{
			if (liquidBlur[myx, (myy + 1)] > 0) { resultUp = true; }
		}

		if (resultLeft && !resultRight && resultDown && !resultUp)
		{
            Setup.setTexture(1, 15, 0.25f, puppetPtr, false);
		}
		if (!resultLeft && resultRight && resultDown && !resultUp)
		{
            Setup.setTexture(2, 15, 0.25f, puppetPtr, false);
		}
		if (!resultLeft && !resultRight && resultDown)
		{
            Setup.setTexture(5, 15, 0.25f, puppetPtr, false);
		}
		if (resultUp)
		{
            Setup.setTexture(3, 13, 0.25f, puppetPtr, false);
		}
		if (resultUp && !resultDown)
		{
            Setup.setTexture(3, 15, 0.25f, puppetPtr, false);
		}
		if (resultLeft && resultRight)
		{
			Setup.setTexture(3, 13, 0.25f, puppetPtr, false);
		}
	}

	bool checkIndex(int a, bool width)
	{
		if (width && (a <= 0 || a >= liquidGrid.GetLength(0))) { return (false); }
		if (!width && (a <= 0 || a >= liquidGrid.GetLength(1))) { return (false); }
		return (true);
	}

	/*
	void setMyAniState()
	{
		//invisible - no liquid
		if (liquidBlur[myx, myy] <= 0) { aniState = 0; }

		//normal - Something on down/left/right
		if (liquidBlur[myx, myy] == 1) { aniState = 1; }
		if (liquidBlur[myx, myy] == 2) { aniState = 2; }
		if (liquidBlur[myx, myy] == 3) { aniState = 3; }
		if (liquidBlur[myx, myy] >= 4) { aniState = 4; }


		//falling
	}

	void playCorrectAni()
	{
		//normal, full cube
		if (aniState == 0) { aniScript.playAni0(); }//invisible
		if (aniState == 1) { aniScript.playAni1(); }//1 (thin layer)
		if (aniState == 2) { aniScript.playAni2(); }//2 (about half)
		if (aniState == 3) { aniScript.playAni3(); }//3 (2/3rds)
		if (aniState == 4) { aniScript.playAni4(); }//full cube, (Normal)
		//if (aniState == 1) { aniScript.playAni1(); }//corner between left & down
		// if (aniState == 2) { aniScript.playAni2(); }//corner between right & down
	}
	*/

}
