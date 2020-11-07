using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
	public static int lvlNum = 10;//(10 is the starting number);//Highest level number ever reached. Changed by loading/cloud-loading.

	public static NodeController self = null;
	public static int[] lvlNums;
	public static FreshLevels.Type[] lvlTypes;


	[UnityEngine.Serialization.FormerlySerializedAs("camera")]
	public GameObject nodeCamera;
	public GameObject cursor;
	public NodeScript currentNode;
	public Text currentLevelName;
	public Sprite whiteSkull;
	public Sprite whiteClock;
	public Sprite goldSkull;
	public Sprite goldClock;
	public Text timeText;
	public Text deathText;
	public Image clock;
	public Image skull;

	GameObject[] nodesGOs;
	NodeScript[] nodes;

	public GameObject[] allSkies;

	bool checkedForSavedMapPos = false;
	public static bool oncePerGameStart = false;

	void Start()
	{
		self = this;
		InitNodes();
	}

	void Update()
	{
		bool changed = false;

		if (!oncePerGameStart)
		{
			Debug.Log("ONCE");
			oncePerGameStart = true;
			changed = true;

			for (int i = 0; i < nodes.Length; i++)
			{
				if (nodes[i].levelType == FreshLevels.Type.IntroStory)
				{
					currentNode = nodes[i];
					break;
				}
			}
			currentLevelName.text = currentNode.levelName;
			LoadLevelStats();

		}

		if (!checkedForSavedMapPos)
		{
			checkedForSavedMapPos = true;
			if (fa.lastLevelPlayed != FreshLevels.Type.None)
			{
				for (int i = 0; i < nodes.Length; i++)
				{
					if (nodes[i].levelType == fa.lastLevelPlayed)
					{
						currentNode = nodes[i];
						changed = true;
					}
				}
			}
		}

		if (Controls.GetInputDown(Controls.Type.MenuUp, 0))
		{
			if (currentNode.north != null)
			{
				currentNode = currentNode.north;//
				fa.lastLevelPlayed = currentNode.levelType;
				changed = true;
			}
		}
		if (Controls.GetInputDown(Controls.Type.MenuDown, 0))
		{
			if (currentNode.south != null)
			{
				currentNode = currentNode.south;
				fa.lastLevelPlayed = currentNode.levelType;
				changed = true;
			}
		}
		if (Controls.GetInputDown(Controls.Type.MenuLeft, 0))
		{
			if (currentNode.west != null)
			{
				currentNode = currentNode.west;
				fa.lastLevelPlayed = currentNode.levelType;
				changed = true;
			}
		}
		if (Controls.GetInputDown(Controls.Type.MenuRight, 0))
		{
			if (currentNode.east != null)
			{
				currentNode = currentNode.east;
				fa.lastLevelPlayed = currentNode.levelType;
				changed = true;
			}
		}

		if (Controls.GetInputDown(Controls.Type.MenuSelect, 0))
		{
			if (currentNode.locked)
			{
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart);
				iTween.PunchRotation(currentNode.gameObject, iTween.Hash("z", 17.5f, "time", 0.3f));
			}
			else
			{
				string lvlName = FreshLevels.GetStrNameForType(currentNode.levelType);
				//Debug.Log("Tried to go to level: " + lvlName);
				if (lvlName != "")
				{
					xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, lvlName);
				}
			}
		}

		if (changed)
		{
			for (int i = 0; i < allSkies.Length; i++)
			{
				allSkies[i].SetActive(false);
			}

			iTween.MoveTo(nodeCamera, iTween.Hash("x", currentNode.transform.position.x, "y", currentNode.transform.position.y, "easetype", iTween.EaseType.easeInOutSine, "time", 0.2f));

			if (currentNode.setCamPos)
			{
				//iTween.MoveTo(camera, iTween.Hash("x", currentNode.camPos.x, "y", currentNode.camPos.y, "easetype", iTween.EaseType.easeInOutSine, "time", 0.5f));
				//camera.transform.SetX(currentNode.camPos.x);
				//camera.transform.SetY(currentNode.camPos.y);
			}

			iTween.RotateTo(nodeCamera, iTween.Hash("z", currentNode.cameraAngle2, "easetype", iTween.EaseType.easeInOutSine, "time", 0.2f));


			for (int i = 0; i < currentNode.objsOn.Length; i++)
			{
				currentNode.objsOn[i].SetActive(true);
			}


			currentLevelName.text = currentNode.levelName;

			LoadLevelStats();
		}

		cursor.transform.SetX(currentNode.gameObject.transform.position.x);
		cursor.transform.SetY(currentNode.gameObject.transform.position.y);
	}

	public static void Cheat_UnlockALevel()
	{
		lvlNum += 10;
		Fresh_Saving.SaveLvlNum();
		self.InitNodes();
	}

	public static void Cheat_LockALevel()
	{
		lvlNum -= 10;
		Fresh_Saving.SaveLvlNum();
		self.InitNodes();
	}

	public static void Cheat_LockAllLevels()
	{
		lvlNum = 10;
		Fresh_Saving.SaveLvlNum();
		self.InitNodes();
	}

	public static void WonLevel(FreshLevels.Type lvlType)
	{
		//turn type into a int, then increase lvlNum to that amount, then save it.
		//Debug.Log("HERE1, won level " + lvlType + ", lvlTypes length: " + lvlTypes.Length);
		if (lvlTypes == null) { return; }

		for (int i = 0; i < lvlTypes.Length; i++)
		{
			if (lvlTypes[i] == lvlType || (lvlTypes[i] == FreshLevels.Type.IntroStory && lvlType == FreshLevels.Type.Tut_JumpAndAirsword))
			{
				//Debug.Log("HERE2");
				//find the next highest
				for (int a = 0; a < lvlNums.Length; a++)
				{
					if (lvlNums[a] == lvlNums[i] + 10)
					{
						//Debug.Log("HERE3 " + lvlNum + ", " + lvlNums[a]);
						if (lvlNums[a] > lvlNum)
						{
							//Debug.Log("HERE4");
							lvlNum = lvlNums[a];
						}
						//Debug.Log("HERE5");
						//Fresh_Saving.SaveLvlNum();
						return;
					}
				}
			}
		}
	}

	void LoadLevelStats()
	{
		int index = -1;
		index = FreshLevels.GetIndexForType(currentNode.levelType);
		Debug.Log("Loading for: " + currentNode.levelType + ", Time: " + FreshLevels.levels[index].lowestTime);
		if (index != -1)
		{
			if (FreshLevels.levels[index].lowestDeaths == -1)
			{
				skull.sprite = whiteSkull;
				//saved deaths not found (or level type not found), so probably hasn't beaten this level yet. 
				deathText.text = "--";
			}
			else
			{
				if (FreshLevels.levels[index].lowestDeaths == 0)
				{
					skull.sprite = goldSkull;
				}
				else
				{
					skull.sprite = whiteSkull;
				}
				deathText.text = "" + FreshLevels.levels[index].lowestDeaths;
			}
			if (FreshLevels.levels[index].lowestTime == -1)
			{
				clock.sprite = whiteClock;
				//saved deaths not found (or level type not found), so probably hasn't beaten this level yet. 
				timeText.text = "--- / " + FreshLevels.GetGoldTimeForLevel(FreshLevels.levels[index].type);
			}
			else
			{
				if (FreshLevels.levels[index].lowestTime < FreshLevels.GetGoldTimeForLevel(FreshLevels.levels[index].type))
				{
					clock.sprite = goldClock;
				}
				else
				{
					clock.sprite = whiteClock;
				}
				//Debug.Log("Getting spdrun time, " + FreshLevels.levels[index] + ": " + FreshLevels.levels[index].lowestTime);
				timeText.text = RawFuncs.GetSpeedRunTimeStr(FreshLevels.levels[index].lowestTime) + " / " + +FreshLevels.GetGoldTimeForLevel(FreshLevels.levels[index].type);
			}
		}
		else
		{
			//level type not found, possible error. 
			deathText.text = "xx";
			timeText.text = "xx:xx:xxx";
		}

	}

	void InitNodes()
	{
		//Load lvlNum (for now, fake it)
		//lvlNum = 10;//(10 is the starting number);
		lvlNums = new int[100];//Max 100 levels then
		lvlTypes = new FreshLevels.Type[100];//Max 100 levels then
		int lvlNumIndex = 0;

		nodesGOs = GameObject.FindGameObjectsWithTag("metaMapNode");
		nodes = new NodeScript[nodesGOs.Length];
		for (int i = 0; i < nodesGOs.Length; i++)
		{
			nodes[i] = nodesGOs[i].GetComponent<NodeScript>();
			lvlTypes[lvlNumIndex] = nodes[i].levelType;



			//unlock with the new system
			bool alwaysUnlocked = false;
			if (nodes[i].levelType == FreshLevels.Type.IntroStory) { alwaysUnlocked = true; }

			//check if you own these DLC
			if (nodes[i].levelType == FreshLevels.Type.AlpDLC_IntroStory && (xa.hasAlpDLC || fa.forceUnlockOfAlpDLC)) { alwaysUnlocked = true; }
			if (nodes[i].levelType == FreshLevels.Type.IsThisADaggerISeeBeforeMe && xa.hasBonusDLC) { alwaysUnlocked = true; }

			if (alwaysUnlocked)
			{
				nodes[i].locked = false;
				if (nodes[i].lockObj != null) { nodes[i].lockObj.SetActive(nodes[i].locked); }
			}
			else
			{
				string strictName = FreshLevels.GetStrictLabelForType(nodes[i].levelType);
				if (PlayerPrefs.HasKey("nu_" + strictName))
				{
					nodes[i].locked = false;
					if (nodes[i].lockObj != null) { nodes[i].lockObj.SetActive(nodes[i].locked); }
				}
				else
				{
					nodes[i].locked = true;
					nodes[i].lockObj.SetActive(nodes[i].locked);
				}
			}


			/*
			lvlNums[lvlNumIndex] = nodes[i].lvlNum;
			lvlNumIndex++;

			if (nodes[i].lvlNum != -1 && lvlNum < nodes[i].lvlNum)
			{
				nodes[i].locked = true;
				nodes[i].lockObj.SetActive(nodes[i].locked);
			}
			else if (!xa.hasBonusDLC && nodes[i].BonusDLC)
			{
				nodes[i].locked = true;
				nodes[i].lockObj.SetActive(nodes[i].locked);
			}
			else
			{
				nodes[i].locked = false;
				if (nodes[i].lockObj != null) { nodes[i].lockObj.SetActive(nodes[i].locked); }
			}*/

		}
	}


}
