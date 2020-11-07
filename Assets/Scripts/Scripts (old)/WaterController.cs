using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour
{
	float waterUpdateSpeedInSeconds = 0.1f;
	float timeSet;
	int index;
	int index2;


	void Start()
	{
		timeSet = fa.time;

	}

	void Update()
	{

		if (fa.time >= (timeSet + waterUpdateSpeedInSeconds))
		{
			timeSet = fa.time;
			updateWater();
		}
	}

	void updateWater()
	{
		WaterScript.sortGroups();

		//flow water downward
		index = 0;
		while (index < WaterScript.waterGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < WaterScript.waterGrid.GetLength(1))
			{
				if (WaterScript.waterUsable[index, index2])
				{
					WaterScript.waterScripts[index, index2].fallWater();
				}
				index2++;
			}
			index++;
		}

		//spawn new water
		index = 0;
		while (index < WaterScript.waterGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < WaterScript.waterGrid.GetLength(1))
			{
				if (WaterScript.waterUsable[index, index2])
				{
					WaterScript.waterScripts[index, index2].spawnWater();
				}
				index2++;
			}
			index++;
		}

		//update new water to water grid
		index = 0;
		while (index < WaterScript.waterGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < WaterScript.waterGrid.GetLength(1))
			{
				if (WaterScript.waterUsable[index, index2])
				{
					WaterScript.waterScripts[index, index2].handleNewWater();
				}
				index2++;
			}
			index++;
		}

		//animate loop
		index = 0;
		while (index < WaterScript.waterGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < WaterScript.waterGrid.GetLength(1))
			{
				if (WaterScript.waterUsable[index, index2])
				{
					WaterScript.waterScripts[index, index2].animateMe();
				}
				index2++;
			}
			index++;
		}
		/*
		//main loop, move liquid if able
		index = 0;
		while (index < LiquidScript.liquidGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < LiquidScript.liquidGrid.GetLength(1))
			{
				if (LiquidScript.liquidUsable[index, index2])
				{
					LiquidScript.liquidScripts[index, index2].handleLiquid();
				}
				index2++;
			}
			index++;
		}


		//move new liquid into liquid grid
		index = 0;
		while (index < LiquidScript.liquidGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < LiquidScript.liquidGrid.GetLength(1))
			{
				if (LiquidScript.liquidUsable[index, index2])
				{
					LiquidScript.liquidScripts[index, index2].handleNewLiquid();
				}
				index2++;
			}
			index++;
		}

		//blur check
		index = 0;
		while (index < LiquidScript.liquidGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < LiquidScript.liquidGrid.GetLength(1))
			{
				if (LiquidScript.liquidUsable[index, index2])
				{
					LiquidScript.liquidScripts[index, index2].setBlur();
				}
				index2++;
			}
			index++;
		}

		//animation loop
		index = 0;
		while (index < LiquidScript.liquidGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < LiquidScript.liquidGrid.GetLength(1))
			{
				if (LiquidScript.liquidUsable[index, index2])
				{
					LiquidScript.liquidScripts[index, index2].animateMe();
				}
				index2++;
			}
			index++;
		}*/
	}
}
