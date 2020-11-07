using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour
{
	public static int[,] waterGrid = new int[0, 0];//width, height
	public static int[,] waterNew = new int[0, 0];//width, height
	public static bool[,] waterUsable = new bool[0, 0];//width, height
	public static WaterScript[,] waterScripts;
	public static int[,] waterGroup = new int[0, 0];//width, height
	public static Vector2[] waterList;
	public static int groupID = 1;
	public static int listIndex = 0;
	public static int onList = 0;
 
	public bool waterSource = false;
	public GameObject textObj = null;   

	int myx;
	int myy;
	int index;
	int index2;
	int oldAniState = -1;
	int aniState = 0;
	TextMesh textMesh;

	void Start()
	{
		myx = (int)(transform.position.x);
		myy = (int)(transform.position.y);
		waterUsable[myx, myy] = true;
		waterScripts[myx, myy] = this;

		textMesh = textObj.GetComponent<TextMesh>();
		textMesh.text = "x";
	}

	void Update()
	{
		myx = (int)(transform.position.x);
		myy = (int)(transform.position.y);
		textMesh.text = "" + waterGroup[myx, myy];
	}

	public void spawnWater()
	{
		//is the tile of the spawner free?
		if (waterSource)
		{
			if (waterGrid[myx, myy] == 0)
			{
				waterNew[myx, myy]++;
			}
			else
			{
				//find an edge to place it
				findEdge(waterGroup[myx, myy]);
			}
		}
	}

	public void fallWater()
	{
		//check for water with nothing under it, and make it move down
		if (waterGrid[myx, myy] == 1)//have water
		{
			if (checkIfCanFlowDown())
			{
				waterGrid[myx, myy]--;
				waterNew[myx, (myy - 1)]++;
			}
		}
	}

	bool checkIfCanFlowDown()
	{
		if ((myy - 1) >= 0)//isn't off the grid
		{
			if (waterUsable[myx, (myy - 1)])//is a usable square below me
			{
				if ((waterGrid[myx, (myy - 1)] + waterNew[myx, (myy - 1)]) == 0)//isn't full
				{
					return (true);
				}
			}
		}
		return (false);
	}

	public void animateMe()
	{
		if (waterGrid[myx, myy] == 0) { aniState = 0; }
		if (waterGrid[myx, myy] == 1) { aniState = 1; }


		if (oldAniState != aniState)
		{
			oldAniState = aniState;
            if (aniState == 0) { Setup.setTexture(0, 3, 0.25f, this.gameObject, false); }
            if (aniState == 1) { Setup.setTexture(3, 13, 0.25f, this.gameObject, false); }
		}
	}


	public void handleNewWater()
	{
		myx = (int)(transform.position.x);
		myy = (int)(transform.position.y);
		waterGrid[myx, myy] += waterNew[myx, myy];
		waterNew[myx, myy] = 0;
	}

	Vector2 result;

	public static void sortGroups()
	{
		Vector2 result;
		//find a non-grouped tile

		WaterScript.cleanWaterGroups();
		groupID = 0;
		//start a spread from this tile
		result.x = -2;
		while(result.x != -1)
		{
			result = WaterScript.findNonGroupedTile();
			if (result.x != -1)
			{
				WaterScript.spreadGroup(result);
			}
		}
	}

	public static void findEdge(int groupNum)
	{

	}

	public static void spreadGroup(Vector2 vec1)
	{
		//wipe the list clean
		WaterScript.cleanWaterList();

		//put the input tile on the list
		waterList[listIndex] = vec1;
		listIndex++;
		onList++;

		//get a new groupID
		groupID++;

		//set the input tile to the group id
		waterGroup[(int)vec1.x,(int)vec1.y] = groupID;

		//spread from the list
		while (onList > 0)
		{
			spreadFromList();
		}
	}

	public static void spreadFromList()
	{
		int index;
		index = 0;
		while (index < waterList.Length)
		{
			if (waterList[index].x != -1)
			{
				//spread from this tile
				spreadFromWaterTile(waterList[index]);

				//remove from list
				waterList[index] = new Vector2(-1, -1);
				onList--;
			}
			index++;
		}
	}

	public static void spreadFromWaterTile(Vector2 vec)
	{
		//check in four directions for water

		//Left (-x)
		if (vec.x - 1 >= 0)
		{
			WaterScript.checkSingleTile(new Vector2(vec.x - 1, vec.y));
		}

		//Right (x)
		if (vec.x + 1 >= 0)
		{
			WaterScript.checkSingleTile(new Vector2(vec.x + 1, vec.y));
		}

		//Up (y)
		if (vec.y + 1 >= 0)
		{
			WaterScript.checkSingleTile(new Vector2(vec.x, vec.y + 1));
		}

		//Down (-y)
		if (vec.y - 1 >= 0)
		{
			WaterScript.checkSingleTile(new Vector2(vec.x, vec.y - 1));
		}

	}

	public static void checkSingleTile(Vector2 vec)
	{
		if (waterUsable[(int)vec.x, (int)vec.y])
		{
			//is there water here?
			if (waterGrid[(int)vec.x, (int)vec.y] > 0)
			{
				if (waterGroup[(int)vec.x, (int)vec.y] <= 0)
				{
					//set to group ID
					waterGroup[(int)vec.x, (int)vec.y] = groupID;

					//add to list
					waterList[listIndex] = new Vector2(vec.x, vec.y);
					onList++;
					listIndex++;
				}
			}
		}
	}

	public static void cleanWaterList()
	{
		listIndex = 0;
		onList = 0;
		int index;
		index = 0;
		while (index < waterList.Length)
		{
			waterList[index] = new Vector2(-1, -1);
			index++;
		}

	}

	public static void cleanWaterGroups()
	{
		int index;
		int index2;
		index = 0;
		while (index < waterGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < waterGrid.GetLength(1))
			{
				waterGroup[index, index2] = 0;

				index2++;
			}
			index++;
		}

	}

	public static Vector2 findNonGroupedTile()
	{
		int index;
		int index2;
		index = 0;
		while (index < waterGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < waterGrid.GetLength(1))
			{
				if (waterGrid[index, index2] > 0 && waterGroup[index,index2] < 1)//has water & no group ID
				{
					return (new Vector2(index, index2));
				}
				index2++;
			}
			index++;
		}
		return (new Vector2(-1,-1));
	}
}
