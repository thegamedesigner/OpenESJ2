using UnityEngine;
using System.Collections;

public class LiquidController : MonoBehaviour
{
	float liquidUpdateSpeedInSeconds = 0.1f;

	float timeSet;
	int index;
	int index2;

	void Start()
	{
		timeSet = fa.time;
	 
	}

	void Update()
	{
		if (fa.time >= (timeSet + liquidUpdateSpeedInSeconds))
		{
			timeSet = fa.time;
			updateLiquid();
		}
	}

	void updateLiquid()
	{
		//lower blur delay
		index = 0;
		while (index < LiquidScript.liquidGrid.GetLength(0))
		{
			index2 = 0;
			while (index2 < LiquidScript.liquidGrid.GetLength(1))
			{
				if (LiquidScript.liquidBlur[index, index2] > 0)
				{
					LiquidScript.liquidBlur[index, index2]--;
				}
				index2++;
			}
			index++;
		}

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
		}
	}
}
