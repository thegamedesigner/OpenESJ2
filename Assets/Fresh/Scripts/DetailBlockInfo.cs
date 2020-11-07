using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

public class DetailBlockInfo : MonoBehaviour
{
	public static Info[] info;

	public enum DetailType
	{
		PinkMechanical = 0,
		Minimal = 1,
		Slime = 2,
		MegaHell = 3,
		Jungle = 4,
		BlueJungle = 5,
		End
	}

	public class Info
	{
		public Frame[] invisible;
		public Frame[] north;//something above you
		public Frame[] south;
		public Frame[] west;
		public Frame[] east;
		public Frame[] NW;
		public Frame[] NE;
		public Frame[] SW;
		public Frame[] SE;

		public Frame[] NS;
		public Frame[] WE;
		public Frame[] NSW;
		public Frame[] NSE;
		public Frame[] NWE;
		public Frame[] SWE;
		public Frame[] NSWE;
	}

	public static Frame GetFrameForType(DetailType detailType, FreshDetailBlocks.Type dir)
	{
		Frame result = new Frame(0, 3, -1, 16);//default to invisible
		switch (dir)
		{
			case FreshDetailBlocks.Type.Invisible: result = info[(int)detailType].invisible[Random.Range(0, info[(int)detailType].invisible.Length)]; break;
			case FreshDetailBlocks.Type.North: result = info[(int)detailType].north[Random.Range(0, info[(int)detailType].north.Length)]; break;
			case FreshDetailBlocks.Type.South:
				//	Debug.Log("" + (int)detailType);
				int f = info[(int)detailType].south.Length;
				int r = Random.Range(0, f);
				result = info[(int)detailType].south[r];
				break;
			case FreshDetailBlocks.Type.West: result = info[(int)detailType].west[Random.Range(0, info[(int)detailType].west.Length)]; break;
			case FreshDetailBlocks.Type.East: result = info[(int)detailType].east[Random.Range(0, info[(int)detailType].east.Length)]; break;
			case FreshDetailBlocks.Type.NW: result = info[(int)detailType].NW[Random.Range(0, info[(int)detailType].NW.Length)]; break;
			case FreshDetailBlocks.Type.NE: result = info[(int)detailType].NE[Random.Range(0, info[(int)detailType].NE.Length)]; break;
			case FreshDetailBlocks.Type.SW: result = info[(int)detailType].SW[Random.Range(0, info[(int)detailType].SW.Length)]; break;
			case FreshDetailBlocks.Type.SE: result = info[(int)detailType].SE[Random.Range(0, info[(int)detailType].SE.Length)]; break;

			case FreshDetailBlocks.Type.NS: result = info[(int)detailType].NS[Random.Range(0, info[(int)detailType].NS.Length)]; break;
			case FreshDetailBlocks.Type.WE: result = info[(int)detailType].WE[Random.Range(0, info[(int)detailType].WE.Length)]; break;
			case FreshDetailBlocks.Type.NSW: result = info[(int)detailType].NSW[Random.Range(0, info[(int)detailType].NSW.Length)]; break;
			case FreshDetailBlocks.Type.NSE: result = info[(int)detailType].NSE[Random.Range(0, info[(int)detailType].NSE.Length)]; break;
			case FreshDetailBlocks.Type.NWE: result = info[(int)detailType].NWE[Random.Range(0, info[(int)detailType].NWE.Length)]; break;
			case FreshDetailBlocks.Type.SWE: result = info[(int)detailType].SWE[Random.Range(0, info[(int)detailType].SWE.Length)]; break;
			case FreshDetailBlocks.Type.NSWE: result = info[(int)detailType].NSWE[Random.Range(0, info[(int)detailType].NSWE.Length)]; break;
		}
		return result;
	}

	public static void InitDetailBlockInfo()//28
	{
		info = new Info[6];

		//Pink world mechanical
		info[0] = new Info();
		info[0].invisible = new Frame[1];
		info[0].invisible[0] = new Frame(0, 6, -1, 8);

		info[0].north = new Frame[8];
		info[0].north[0] = new Frame(24, 33, -1, 8);
		info[0].north[1] = new Frame(25, 33, -1, 8);
		info[0].north[2] = new Frame(26, 33, -1, 8);
		info[0].north[3] = new Frame(27, 33, -1, 8);
		info[0].north[4] = new Frame(28, 33, -1, 8);
		info[0].north[5] = new Frame(29, 33, -1, 8);
		info[0].north[6] = new Frame(30, 33, -1, 8);
		info[0].north[7] = new Frame(31, 33, -1, 8);

		info[0].south = new Frame[8];
		info[0].south[0] = new Frame(24, 32, -1, 8);
		info[0].south[1] = new Frame(25, 32, -1, 8);
		info[0].south[2] = new Frame(26, 32, -1, 8);
		info[0].south[3] = new Frame(27, 32, -1, 8);
		info[0].south[4] = new Frame(28, 32, -1, 8);
		info[0].south[5] = new Frame(29, 32, -1, 8);
		info[0].south[6] = new Frame(30, 32, -1, 8);
		info[0].south[7] = new Frame(31, 32, -1, 8);

		info[0].NW = new Frame[8];
		info[0].NW[0] = new Frame(24, 34, -1, 8);
		info[0].NW[1] = new Frame(25, 34, -1, 8);
		info[0].NW[2] = new Frame(26, 34, -1, 8);
		info[0].NW[3] = new Frame(27, 34, -1, 8);
		info[0].NW[4] = new Frame(28, 34, -1, 8);
		info[0].NW[5] = new Frame(29, 34, -1, 8);
		info[0].NW[6] = new Frame(30, 34, -1, 8);
		info[0].NW[7] = new Frame(31, 34, -1, 8);

		info[0].NE = new Frame[8];
		info[0].NE[0] = new Frame(24, 35, -1, 8);
		info[0].NE[1] = new Frame(25, 35, -1, 8);
		info[0].NE[2] = new Frame(26, 35, -1, 8);
		info[0].NE[3] = new Frame(27, 35, -1, 8);
		info[0].NE[4] = new Frame(28, 35, -1, 8);
		info[0].NE[5] = new Frame(29, 35, -1, 8);
		info[0].NE[6] = new Frame(30, 35, -1, 8);
		info[0].NE[7] = new Frame(31, 35, -1, 8);

		info[0].SW = new Frame[4];
		info[0].SW[0] = new Frame(24, 36, -1, 8);
		info[0].SW[1] = new Frame(25, 36, -1, 8);
		info[0].SW[2] = new Frame(26, 36, -1, 8);
		info[0].SW[3] = new Frame(27, 36, -1, 8);

		info[0].SE = new Frame[4];
		info[0].SE[0] = new Frame(24, 37, -1, 8);
		info[0].SE[1] = new Frame(25, 37, -1, 8);
		info[0].SE[2] = new Frame(26, 37, -1, 8);
		info[0].SE[3] = new Frame(27, 37, -1, 8);

		info[0].west = new Frame[8];
		info[0].west[0] = new Frame(24, 38, -1, 8);
		info[0].west[1] = new Frame(25, 38, -1, 8);
		info[0].west[2] = new Frame(26, 38, -1, 8);
		info[0].west[3] = new Frame(27, 38, -1, 8);
		info[0].west[4] = new Frame(28, 38, -1, 8);
		info[0].west[5] = new Frame(29, 38, -1, 8);
		info[0].west[6] = new Frame(30, 38, -1, 8);
		info[0].west[7] = new Frame(31, 38, -1, 8);

		info[0].east = new Frame[8];
		info[0].east[0] = new Frame(24, 39, -1, 8);
		info[0].east[1] = new Frame(25, 39, -1, 8);
		info[0].east[2] = new Frame(26, 39, -1, 8);
		info[0].east[3] = new Frame(27, 39, -1, 8);
		info[0].east[4] = new Frame(28, 39, -1, 8);
		info[0].east[5] = new Frame(29, 39, -1, 8);
		info[0].east[6] = new Frame(30, 39, -1, 8);
		info[0].east[7] = new Frame(31, 39, -1, 8);

		info[0].NS = new Frame[8]; //Didn't want to go back and draw these frames for this style, so made them the invisible frame instead
		info[0].NS[0] = new Frame(0, 6, -1, 8);

		info[0].WE = new Frame[8];
		info[0].WE[0] = new Frame(0, 6, -1, 8);
		info[0].WE[1] = new Frame(0, 6, -1, 8);

		info[0].NSW = new Frame[8];
		info[0].NSW[0] = new Frame(0, 6, -1, 8);

		info[0].NSE = new Frame[8];
		info[0].NSE[0] = new Frame(0, 6, -1, 8);

		info[0].NWE = new Frame[8];
		info[0].NWE[0] = new Frame(0, 6, -1, 8);

		info[0].SWE = new Frame[8];
		info[0].SWE[0] = new Frame(0, 6, -1, 8);

		info[0].NSWE = new Frame[8];
		info[0].NSWE[0] = new Frame(0, 6, -1, 8);



		//minimal
		info[1] = new Info();
		info[1].invisible = new Frame[1];
		info[1].invisible[0] = new Frame(0, 6, -1, 8);

		info[1].north = new Frame[8];
		info[1].north[0] = new Frame(36, 33, -1, 8);
		info[1].north[1] = new Frame(37, 33, -1, 8);
		info[1].north[2] = new Frame(38, 33, -1, 8);
		info[1].north[3] = new Frame(39, 33, -1, 8);
		info[1].north[4] = new Frame(40, 33, -1, 8);
		info[1].north[5] = new Frame(41, 33, -1, 8);
		info[1].north[6] = new Frame(42, 33, -1, 8);
		info[1].north[7] = new Frame(43, 33, -1, 8);

		info[1].south = new Frame[8];
		info[1].south[0] = new Frame(36, 32, -1, 8);
		info[1].south[1] = new Frame(37, 32, -1, 8);
		info[1].south[2] = new Frame(38, 32, -1, 8);
		info[1].south[3] = new Frame(39, 32, -1, 8);
		info[1].south[4] = new Frame(40, 32, -1, 8);
		info[1].south[5] = new Frame(41, 32, -1, 8);
		info[1].south[6] = new Frame(42, 32, -1, 8);
		info[1].south[7] = new Frame(43, 32, -1, 8);

		info[1].NW = new Frame[8];
		info[1].NW[0] = new Frame(36, 34, -1, 8);
		info[1].NW[1] = new Frame(37, 34, -1, 8);
		info[1].NW[2] = new Frame(38, 34, -1, 8);
		info[1].NW[3] = new Frame(39, 34, -1, 8);
		info[1].NW[4] = new Frame(40, 34, -1, 8);
		info[1].NW[5] = new Frame(41, 34, -1, 8);
		info[1].NW[6] = new Frame(42, 34, -1, 8);
		info[1].NW[7] = new Frame(43, 34, -1, 8);

		info[1].NE = new Frame[8];
		info[1].NE[0] = new Frame(36, 35, -1, 8);
		info[1].NE[1] = new Frame(37, 35, -1, 8);
		info[1].NE[2] = new Frame(38, 35, -1, 8);
		info[1].NE[3] = new Frame(39, 35, -1, 8);
		info[1].NE[4] = new Frame(40, 35, -1, 8);
		info[1].NE[5] = new Frame(41, 35, -1, 8);
		info[1].NE[6] = new Frame(42, 35, -1, 8);
		info[1].NE[7] = new Frame(43, 35, -1, 8);

		info[1].SW = new Frame[4];
		info[1].SW[0] = new Frame(36, 36, -1, 8);
		info[1].SW[1] = new Frame(37, 36, -1, 8);
		info[1].SW[2] = new Frame(38, 36, -1, 8);
		info[1].SW[3] = new Frame(39, 36, -1, 8);

		info[1].SE = new Frame[4];
		info[1].SE[0] = new Frame(36, 37, -1, 8);
		info[1].SE[1] = new Frame(37, 37, -1, 8);
		info[1].SE[2] = new Frame(38, 37, -1, 8);
		info[1].SE[3] = new Frame(39, 37, -1, 8);

		info[1].west = new Frame[8];
		info[1].west[0] = new Frame(36, 38, -1, 8);
		info[1].west[1] = new Frame(37, 38, -1, 8);
		info[1].west[2] = new Frame(38, 38, -1, 8);
		info[1].west[3] = new Frame(39, 38, -1, 8);
		info[1].west[4] = new Frame(40, 38, -1, 8);
		info[1].west[5] = new Frame(41, 38, -1, 8);
		info[1].west[6] = new Frame(42, 38, -1, 8);
		info[1].west[7] = new Frame(43, 38, -1, 8);

		info[1].east = new Frame[8];
		info[1].east[0] = new Frame(36, 39, -1, 8);
		info[1].east[1] = new Frame(37, 39, -1, 8);
		info[1].east[2] = new Frame(38, 39, -1, 8);
		info[1].east[3] = new Frame(39, 39, -1, 8);
		info[1].east[4] = new Frame(40, 39, -1, 8);
		info[1].east[5] = new Frame(41, 39, -1, 8);
		info[1].east[6] = new Frame(42, 39, -1, 8);
		info[1].east[7] = new Frame(43, 39, -1, 8);

		info[1].NS = new Frame[1];
		info[1].NS[0] = new Frame(43, 37, -1, 8);

		info[1].WE = new Frame[2];
		info[1].WE[0] = new Frame(40, 37, -1, 8);
		info[1].WE[1] = new Frame(41, 37, -1, 8);

		info[1].NSW = new Frame[1];
		info[1].NSW[0] = new Frame(40, 36, -1, 8);

		info[1].NSE = new Frame[1];
		info[1].NSE[0] = new Frame(41, 36, -1, 8);

		info[1].NWE = new Frame[1];
		info[1].NWE[0] = new Frame(43, 36, -1, 8);

		info[1].SWE = new Frame[1];
		info[1].SWE[0] = new Frame(42, 36, -1, 8);

		info[1].NSWE = new Frame[1];
		info[1].NSWE[0] = new Frame(42, 37, -1, 8);



		//slime
		info[2] = new Info();
		info[2].invisible = new Frame[1];
		info[2].invisible[0] = new Frame(0, 6, -1, 8);
		
		info[2].south = new Frame[8];
		info[2].south[0] = new Frame(14, 34, -1, 16);
		info[2].south[1] = new Frame(15, 34, -1, 16);
		info[2].south[2] = new Frame(16, 34, -1, 16);
		info[2].south[3] = new Frame(17, 34, -1, 16);
		info[2].south[4] = new Frame(18, 34, -1, 16);
		info[2].south[5] = new Frame(19, 34, -1, 16);
		info[2].south[6] = new Frame(20, 34, -1, 16);
		info[2].south[7] = new Frame(21, 34, -1, 16);

		info[2].north = new Frame[8];
		info[2].north[0] = new Frame(14, 35, -1, 16);
		info[2].north[1] = new Frame(15, 35, -1, 16);
		info[2].north[2] = new Frame(16, 35, -1, 16);
		info[2].north[3] = new Frame(17, 35, -1, 16);
		info[2].north[4] = new Frame(18, 35, -1, 16);
		info[2].north[5] = new Frame(19, 35, -1, 16);
		info[2].north[6] = new Frame(20, 35, -1, 16);
		info[2].north[7] = new Frame(21, 35, -1, 16);

		info[2].NW = new Frame[8];
		info[2].NW[0] = new Frame(14, 36, -1, 16);
		info[2].NW[1] = new Frame(15, 36, -1, 16);
		info[2].NW[2] = new Frame(16, 36, -1, 16);
		info[2].NW[3] = new Frame(17, 36, -1, 16);
		info[2].NW[4] = new Frame(18, 36, -1, 16);
		info[2].NW[5] = new Frame(19, 36, -1, 16);
		info[2].NW[6] = new Frame(20, 36, -1, 16);
		info[2].NW[7] = new Frame(21, 36, -1, 16);

		info[2].NE = new Frame[8];
		info[2].NE[0] = new Frame(14, 37, -1, 16);
		info[2].NE[1] = new Frame(15, 37, -1, 16);
		info[2].NE[2] = new Frame(16, 37, -1, 16);
		info[2].NE[3] = new Frame(17, 37, -1, 16);
		info[2].NE[4] = new Frame(18, 37, -1, 16);
		info[2].NE[5] = new Frame(19, 37, -1, 16);
		info[2].NE[6] = new Frame(20, 37, -1, 16);
		info[2].NE[7] = new Frame(21, 37, -1, 16);

		info[2].SW = new Frame[4];
		info[2].SW[0] = new Frame(14, 38, -1, 16);
		info[2].SW[1] = new Frame(15, 38, -1, 16);
		info[2].SW[2] = new Frame(16, 38, -1, 16);
		info[2].SW[3] = new Frame(17, 38, -1, 16);

		info[2].SE = new Frame[4];
		info[2].SE[0] = new Frame(14, 39, -1, 16);
		info[2].SE[1] = new Frame(15, 39, -1, 16);
		info[2].SE[2] = new Frame(16, 39, -1, 16);
		info[2].SE[3] = new Frame(17, 39, -1, 16);

		info[2].west = new Frame[8];
		info[2].west[0] = new Frame(14, 40, -1, 16);
		info[2].west[1] = new Frame(15, 40, -1, 16);
		info[2].west[2] = new Frame(16, 40, -1, 16);
		info[2].west[3] = new Frame(17, 40, -1, 16);
		info[2].west[4] = new Frame(18, 40, -1, 16);
		info[2].west[5] = new Frame(19, 40, -1, 16);
		info[2].west[6] = new Frame(20, 40, -1, 16);
		info[2].west[7] = new Frame(21, 40, -1, 16);

		info[2].east = new Frame[8];
		info[2].east[0] = new Frame(14, 41, -1, 16);
		info[2].east[1] = new Frame(15, 41, -1, 16);
		info[2].east[2] = new Frame(16, 41, -1, 16);
		info[2].east[3] = new Frame(17, 41, -1, 16);
		info[2].east[4] = new Frame(18, 41, -1, 16);
		info[2].east[5] = new Frame(19, 41, -1, 16);
		info[2].east[6] = new Frame(20, 41, -1, 16);
		info[2].east[7] = new Frame(21, 41, -1, 16);


		info[2].NS = new Frame[1];
		info[2].NS[0] = new Frame(21, 39, -1, 16);

		info[2].WE = new Frame[2];
		info[2].WE[0] = new Frame(18, 39, -1, 16);
		info[2].WE[1] = new Frame(19, 39, -1, 16);

		info[2].NSW = new Frame[1];
		info[2].NSW[0] = new Frame(18, 38, -1, 16);

		info[2].NSE = new Frame[1];
		info[2].NSE[0] = new Frame(19, 38, -1, 16);

		info[2].NWE = new Frame[1];
		info[2].NWE[0] = new Frame(21, 38, -1, 16);

		info[2].SWE = new Frame[1];
		info[2].SWE[0] = new Frame(20, 38, -1, 16);

		info[2].NSWE = new Frame[1];
		info[2].NSWE[0] = new Frame(20, 39, -1, 16);
		
		//megahell
		info[3] = new Info();
		info[3].invisible = new Frame[1];
		info[3].invisible[0] = new Frame(0, 6, -1, 8);
		
		info[3].south = new Frame[8];
		info[3].south[0] = new Frame(22, 34, -1, 16);
		info[3].south[1] = new Frame(23, 34, -1, 16);
		info[3].south[2] = new Frame(24, 34, -1, 16);
		info[3].south[3] = new Frame(25, 34, -1, 16);
		info[3].south[4] = new Frame(26, 34, -1, 16);
		info[3].south[5] = new Frame(27, 34, -1, 16);
		info[3].south[6] = new Frame(28, 34, -1, 16);
		info[3].south[7] = new Frame(29, 34, -1, 16);

		info[3].north = new Frame[8];
		info[3].north[0] = new Frame(22, 35, -1, 16);
		info[3].north[1] = new Frame(23, 35, -1, 16);
		info[3].north[2] = new Frame(24, 35, -1, 16);
		info[3].north[3] = new Frame(25, 35, -1, 16);
		info[3].north[4] = new Frame(26, 35, -1, 16);
		info[3].north[5] = new Frame(27, 35, -1, 16);
		info[3].north[6] = new Frame(28, 35, -1, 16);
		info[3].north[7] = new Frame(29, 35, -1, 16);

		info[3].NW = new Frame[8];
		info[3].NW[0] = new Frame(22, 36, -1, 16);
		info[3].NW[1] = new Frame(23, 36, -1, 16);
		info[3].NW[2] = new Frame(24, 36, -1, 16);
		info[3].NW[3] = new Frame(25, 36, -1, 16);
		info[3].NW[4] = new Frame(26, 36, -1, 16);
		info[3].NW[5] = new Frame(27, 36, -1, 16);
		info[3].NW[6] = new Frame(28, 36, -1, 16);
		info[3].NW[7] = new Frame(29, 36, -1, 16);

		info[3].NE = new Frame[8];
		info[3].NE[0] = new Frame(22, 37, -1, 16);
		info[3].NE[1] = new Frame(23, 37, -1, 16);
		info[3].NE[2] = new Frame(24, 37, -1, 16);
		info[3].NE[3] = new Frame(25, 37, -1, 16);
		info[3].NE[4] = new Frame(26, 37, -1, 16);
		info[3].NE[5] = new Frame(27, 37, -1, 16);
		info[3].NE[6] = new Frame(28, 37, -1, 16);
		info[3].NE[7] = new Frame(29, 37, -1, 16);

		info[3].SW = new Frame[4];
		info[3].SW[0] = new Frame(22, 38, -1, 16);
		info[3].SW[1] = new Frame(23, 38, -1, 16);
		info[3].SW[2] = new Frame(24, 38, -1, 16);
		info[3].SW[3] = new Frame(25, 38, -1, 16);

		info[3].SE = new Frame[4];
		info[3].SE[0] = new Frame(22, 39, -1, 16);
		info[3].SE[1] = new Frame(23, 39, -1, 16);
		info[3].SE[2] = new Frame(24, 39, -1, 16);
		info[3].SE[3] = new Frame(25, 39, -1, 16);

		info[3].west = new Frame[8];
		info[3].west[0] = new Frame(22, 40, -1, 16);
		info[3].west[1] = new Frame(23, 40, -1, 16);
		info[3].west[2] = new Frame(24, 40, -1, 16);
		info[3].west[3] = new Frame(25, 40, -1, 16);
		info[3].west[4] = new Frame(26, 40, -1, 16);
		info[3].west[5] = new Frame(27, 40, -1, 16);
		info[3].west[6] = new Frame(28, 40, -1, 16);
		info[3].west[7] = new Frame(29, 40, -1, 16);

		info[3].east = new Frame[8];
		info[3].east[0] = new Frame(22, 41, -1, 16);
		info[3].east[1] = new Frame(23, 41, -1, 16);
		info[3].east[2] = new Frame(24, 41, -1, 16);
		info[3].east[3] = new Frame(25, 41, -1, 16);
		info[3].east[4] = new Frame(26, 41, -1, 16);
		info[3].east[5] = new Frame(27, 41, -1, 16);
		info[3].east[6] = new Frame(28, 41, -1, 16);
		info[3].east[7] = new Frame(29, 41, -1, 16);


		info[3].NS = new Frame[1];
		info[3].NS[0] = new Frame(29, 39, -1, 16);

		info[3].WE = new Frame[3];
		info[3].WE[0] = new Frame(26, 39, -1, 16);
		info[3].WE[1] = new Frame(27, 39, -1, 16);

		info[3].NSW = new Frame[1];
		info[3].NSW[0] = new Frame(26, 38, -1, 16);

		info[3].NSE = new Frame[1];
		info[3].NSE[0] = new Frame(27, 38, -1, 16);

		info[3].NWE = new Frame[1];
		info[3].NWE[0] = new Frame(29, 38, -1, 16);

		info[3].SWE = new Frame[1];
		info[3].SWE[0] = new Frame(28, 38, -1, 16);

		info[3].NSWE = new Frame[1];
		info[3].NSWE[0] = new Frame(28, 39, -1, 16);


		

		//Jungle
		info[4] = new Info();
		info[4].invisible = new Frame[1];
		info[4].invisible[0] = new Frame(0, 6, -1, 8);
		
		info[4].south = new Frame[8];
		info[4].south[0] = new Frame(30, 34, -1, 16);
		info[4].south[1] = new Frame(31, 34, -1, 16);
		info[4].south[2] = new Frame(32, 34, -1, 16);
		info[4].south[3] = new Frame(33, 34, -1, 16);
		info[4].south[4] = new Frame(34, 34, -1, 16);
		info[4].south[5] = new Frame(35, 34, -1, 16);
		info[4].south[6] = new Frame(36, 34, -1, 16);
		info[4].south[7] = new Frame(37, 34, -1, 16);

		info[4].north = new Frame[8];
		info[4].north[0] = new Frame(30, 35, -1, 16);
		info[4].north[1] = new Frame(31, 35, -1, 16);
		info[4].north[2] = new Frame(32, 35, -1, 16);
		info[4].north[3] = new Frame(33, 35, -1, 16);
		info[4].north[4] = new Frame(34, 35, -1, 16);
		info[4].north[5] = new Frame(35, 35, -1, 16);
		info[4].north[6] = new Frame(36, 35, -1, 16);
		info[4].north[7] = new Frame(37, 35, -1, 16);

		info[4].NW = new Frame[8];
		info[4].NW[0] = new Frame(30, 36, -1, 16);
		info[4].NW[1] = new Frame(31, 36, -1, 16);
		info[4].NW[2] = new Frame(32, 36, -1, 16);
		info[4].NW[3] = new Frame(33, 36, -1, 16);
		info[4].NW[4] = new Frame(34, 36, -1, 16);
		info[4].NW[5] = new Frame(35, 36, -1, 16);
		info[4].NW[6] = new Frame(36, 36, -1, 16);
		info[4].NW[7] = new Frame(37, 36, -1, 16);

		info[4].NE = new Frame[8];
		info[4].NE[0] = new Frame(30, 37, -1, 16);
		info[4].NE[1] = new Frame(31, 37, -1, 16);
		info[4].NE[2] = new Frame(32, 37, -1, 16);
		info[4].NE[3] = new Frame(33, 37, -1, 16);
		info[4].NE[4] = new Frame(34, 37, -1, 16);
		info[4].NE[5] = new Frame(35, 37, -1, 16);
		info[4].NE[6] = new Frame(36, 37, -1, 16);
		info[4].NE[7] = new Frame(37, 37, -1, 16);

		info[4].SW = new Frame[4];
		info[4].SW[0] = new Frame(30, 38, -1, 16);
		info[4].SW[1] = new Frame(31, 38, -1, 16);
		info[4].SW[2] = new Frame(32, 38, -1, 16);
		info[4].SW[3] = new Frame(33, 38, -1, 16);

		info[4].SE = new Frame[4];
		info[4].SE[0] = new Frame(30, 39, -1, 16);
		info[4].SE[1] = new Frame(31, 39, -1, 16);
		info[4].SE[2] = new Frame(32, 39, -1, 16);
		info[4].SE[3] = new Frame(33, 39, -1, 16);

		info[4].west = new Frame[8];
		info[4].west[0] = new Frame(30, 40, -1, 16);
		info[4].west[1] = new Frame(31, 40, -1, 16);
		info[4].west[2] = new Frame(32, 40, -1, 16);
		info[4].west[3] = new Frame(33, 40, -1, 16);
		info[4].west[4] = new Frame(34, 40, -1, 16);
		info[4].west[5] = new Frame(35, 40, -1, 16);
		info[4].west[6] = new Frame(36, 40, -1, 16);
		info[4].west[7] = new Frame(37, 40, -1, 16);

		info[4].east = new Frame[8];
		info[4].east[0] = new Frame(30, 41, -1, 16);
		info[4].east[1] = new Frame(31, 41, -1, 16);
		info[4].east[2] = new Frame(32, 41, -1, 16);
		info[4].east[3] = new Frame(33, 41, -1, 16);
		info[4].east[4] = new Frame(34, 41, -1, 16);
		info[4].east[5] = new Frame(35, 41, -1, 16);
		info[4].east[6] = new Frame(36, 41, -1, 16);
		info[4].east[7] = new Frame(37, 41, -1, 16);


		info[4].NS = new Frame[1];
		info[4].NS[0] = new Frame(37, 39, -1, 16);

		info[4].WE = new Frame[3];
		info[4].WE[0] = new Frame(34, 39, -1, 16);
		info[4].WE[1] = new Frame(35, 39, -1, 16);

		info[4].NSW = new Frame[1];
		info[4].NSW[0] = new Frame(34, 38, -1, 16);

		info[4].NSE = new Frame[1];
		info[4].NSE[0] = new Frame(35, 38, -1, 16);

		info[4].NWE = new Frame[1];
		info[4].NWE[0] = new Frame(37, 38, -1, 16);

		info[4].SWE = new Frame[1];
		info[4].SWE[0] = new Frame(36, 38, -1, 16);

		info[4].NSWE = new Frame[1];
		info[4].NSWE[0] = new Frame(36, 39, -1, 16);

		

		//Blue Jungle
		info[5] = new Info();
		info[5].invisible = new Frame[1];
		info[5].invisible[0] = new Frame(0, 6, -1, 8);
		
		info[5].south = new Frame[8];
		info[5].south[0] = new Frame(38, 34, -1, 16);//30, 38
		info[5].south[1] = new Frame(39, 34, -1, 16);//31, 39
		info[5].south[2] = new Frame(40, 34, -1, 16);//32, 40
		info[5].south[3] = new Frame(41, 34, -1, 16);//33, 41
		info[5].south[4] = new Frame(42, 34, -1, 16);//34, 42
		info[5].south[5] = new Frame(43, 34, -1, 16);//35, 43
		info[5].south[6] = new Frame(44, 34, -1, 16);//36, 44
		info[5].south[7] = new Frame(45, 34, -1, 16);//37, 45

		info[5].north = new Frame[8];
		info[5].north[0] = new Frame(38, 35, -1, 16);
		info[5].north[1] = new Frame(39, 35, -1, 16);
		info[5].north[2] = new Frame(40, 35, -1, 16);
		info[5].north[3] = new Frame(41, 35, -1, 16);
		info[5].north[4] = new Frame(42, 35, -1, 16);
		info[5].north[5] = new Frame(43, 35, -1, 16);
		info[5].north[6] = new Frame(44, 35, -1, 16);
		info[5].north[7] = new Frame(45, 35, -1, 16);

		info[5].NW = new Frame[8];
		info[5].NW[0] = new Frame(38, 36, -1, 16);
		info[5].NW[1] = new Frame(39, 36, -1, 16);
		info[5].NW[2] = new Frame(40, 36, -1, 16);
		info[5].NW[3] = new Frame(41, 36, -1, 16);
		info[5].NW[4] = new Frame(42, 36, -1, 16);
		info[5].NW[5] = new Frame(43, 36, -1, 16);
		info[5].NW[6] = new Frame(44, 36, -1, 16);
		info[5].NW[7] = new Frame(45, 36, -1, 16);

		info[5].NE = new Frame[8];
		info[5].NE[0] = new Frame(38, 37, -1, 16);
		info[5].NE[1] = new Frame(39, 37, -1, 16);
		info[5].NE[2] = new Frame(40, 37, -1, 16);
		info[5].NE[3] = new Frame(41, 37, -1, 16);
		info[5].NE[4] = new Frame(42, 37, -1, 16);
		info[5].NE[5] = new Frame(43, 37, -1, 16);
		info[5].NE[6] = new Frame(44, 37, -1, 16);
		info[5].NE[7] = new Frame(45, 37, -1, 16);

		info[5].SW = new Frame[4];
		info[5].SW[0] = new Frame(38, 38, -1, 16);
		info[5].SW[1] = new Frame(39, 38, -1, 16);
		info[5].SW[2] = new Frame(40, 38, -1, 16);
		info[5].SW[3] = new Frame(41, 38, -1, 16);

		info[5].SE = new Frame[4];
		info[5].SE[0] = new Frame(38, 39, -1, 16);
		info[5].SE[1] = new Frame(39, 39, -1, 16);
		info[5].SE[2] = new Frame(40, 39, -1, 16);
		info[5].SE[3] = new Frame(41, 39, -1, 16);

		info[5].west = new Frame[8];
		info[5].west[0] = new Frame(38, 40, -1, 16);
		info[5].west[1] = new Frame(39, 40, -1, 16);
		info[5].west[2] = new Frame(40, 40, -1, 16);
		info[5].west[3] = new Frame(41, 40, -1, 16);
		info[5].west[4] = new Frame(42, 40, -1, 16);
		info[5].west[5] = new Frame(43, 40, -1, 16);
		info[5].west[6] = new Frame(44, 40, -1, 16);
		info[5].west[7] = new Frame(45, 40, -1, 16);

		info[5].east = new Frame[8];
		info[5].east[0] = new Frame(38, 41, -1, 16);
		info[5].east[1] = new Frame(39, 41, -1, 16);
		info[5].east[2] = new Frame(40, 41, -1, 16);
		info[5].east[3] = new Frame(41, 41, -1, 16);
		info[5].east[4] = new Frame(42, 41, -1, 16);
		info[5].east[5] = new Frame(43, 41, -1, 16);
		info[5].east[6] = new Frame(44, 41, -1, 16);
		info[5].east[7] = new Frame(45, 41, -1, 16);


		info[5].NS = new Frame[1];
		info[5].NS[0] = new Frame(45, 39, -1, 16);

		info[5].WE = new Frame[3];
		info[5].WE[0] = new Frame(42, 39, -1, 16);
		info[5].WE[1] = new Frame(43, 39, -1, 16);

		info[5].NSW = new Frame[1];
		info[5].NSW[0] = new Frame(42, 38, -1, 16);

		info[5].NSE = new Frame[1];
		info[5].NSE[0] = new Frame(43, 38, -1, 16);

		info[5].NWE = new Frame[1];
		info[5].NWE[0] = new Frame(45, 38, -1, 16);

		info[5].SWE = new Frame[1];
		info[5].SWE[0] = new Frame(44, 38, -1, 16);

		info[5].NSWE = new Frame[1];
		info[5].NSWE[0] = new Frame(44, 39, -1, 16);

		


	}

}
