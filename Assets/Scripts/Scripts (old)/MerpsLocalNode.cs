using UnityEngine;
using System.Collections;

public class MerpsLocalNode : MonoBehaviour
{
	public za.merpsWorlds world = za.merpsWorlds.World1;
	public GameObject faderIn = null;
	public bool useSpacenautAsPlayer = false;
	public int liquidGridWidth = 0;//centered on zero, so 16 is from -8 to 8
	public int liquidGridHeight = 0;
	public int waterGridWidth = 0;//centered on zero, so 16 is from -8 to 8
	public int waterGridHeight = 0;


	MerpsFaderIn fadeInScript = null;
	//bool haveSetMerpsRenderer = false;
	bool haveUnpaused = false;

	void Start()
	{
		za.merpsLocalNode = this;
		LiquidScript.liquidGrid = new int[liquidGridWidth, liquidGridHeight];
		LiquidScript.liquidUsable = new bool[liquidGridWidth, liquidGridHeight];
		LiquidScript.liquidScripts = new LiquidScript[liquidGridWidth, liquidGridHeight];
		LiquidScript.liquidNew = new int[liquidGridWidth, liquidGridHeight];
		LiquidScript.liquidBlur = new int[liquidGridWidth, liquidGridHeight];
		WaterScript.waterGrid = new int[waterGridWidth, waterGridHeight];
		WaterScript.waterUsable = new bool[waterGridWidth, waterGridHeight];
		WaterScript.waterScripts = new WaterScript[waterGridWidth, waterGridHeight];
		WaterScript.waterNew = new int[waterGridWidth, waterGridHeight];
		WaterScript.waterGroup = new int[waterGridWidth, waterGridHeight];
		WaterScript.waterList = new Vector2[waterGridWidth * waterGridHeight];
		fadeInScript = faderIn.GetComponent<MerpsFaderIn>();
		if (xa.beenToLevel0)
		{
			Time.timeScale = 0;
		}
	}

	void Update()
	{
	   // Camera.main.camera.orthographicSize = (Screen.height/1) * 0.01f;
		if (!haveUnpaused && Input.anyKeyDown)
		{
			Time.timeScale = 1;
			haveUnpaused = true;
		}
		if (faderIn)
		{
			fadeInScript.fadeIn();
		}
	}

	int index;
	int index2;
	Vector3 resultVec;
	/*
	void OnGUI()
	{
		GUI.Label(new Rect(index, index2, 10, 10), "XD");
		//draw numbers over each liquid grid space
		index = 0;
		while (index < LiquidScript.liquidGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < LiquidScript.liquidGrid.GetLength(1))
			{
				resultVec = Camera.main.WorldToScreenPoint(new Vector3(index, index2, 30));
				GUI.Label(new Rect(resultVec.x,Screen.height - resultVec.y,30,30),"" + LiquidScript.liquidNew[index,index2]);
				index2++;
			}
			index++;
		}
	}*/
}
