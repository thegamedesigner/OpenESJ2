using UnityEngine;
using System.Collections.Generic;

namespace Structs
{
	[System.Serializable]
	public struct Frame
	{
		public int x;
		public int y;
		public float time;
		public int size;

		public Frame(int xi, int yi, float ti, int si)
		{
			x = xi;
			y = yi;
			time = ti;
			size = si;
		}
	}

	[System.Serializable]
	public struct FFrame
	{
		public int order;
		public int x;
		public int y;
		public float time;
		public int size;

		public FFrame(int ord,int xi, int yi, float ti, int si)
		{
			order = ord;
			x = xi;
			y = yi;
			time = ti;
			size = si;
		}
	}

	[System.Serializable]
	public struct Int3
	{
		public int x;
		public int y;
		public int z;

		public Int3(int xi, int yi, int zi)
		{
			x = xi;
			y = yi;
			z = zi;
		}
	}

	[System.Serializable]
	public struct Int2
	{
		public int x;
		public int y;

		public Int2(int xi, int yi)
		{
			x = xi;
			y = yi;
		}
	}


}
