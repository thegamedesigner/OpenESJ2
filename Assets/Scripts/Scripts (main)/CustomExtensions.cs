using UnityEngine;
using System.Collections;

public static class CustomExtensions
{
	public static void AddScaleX(this Transform trans, float x)
	{
		Vector3 vec = trans.localScale;
		vec.x += x;
		trans.localScale = vec;
	}

	public static void AddScaleY(this Transform trans, float y)
	{
		Vector3 vec = trans.localScale;
		vec.y += y;
		trans.localScale = vec;
	}

	public static void AddScaleZ(this Transform trans, float z)
	{
		Vector3 vec = trans.localScale;
		vec.z += z;
		trans.localScale = vec;
	}

	public static void SetX(this Transform trans, float x)
	{
		trans.position = new Vector3(x, trans.position.y, trans.position.z);
	}
	public static void SetY(this Transform trans, float y)
	{
		trans.position = new Vector3(trans.position.x, y, trans.position.z);
	}

	public static void SetZ(this Transform trans, float z)
	{
		trans.position = new Vector3(trans.position.x, trans.position.y, z);
	}
	
	public static void LocalSetX(this Transform trans, float x)
	{
		trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
	}

	public static void LocalAddX(this Transform trans, float x)
	{
		trans.localPosition = new Vector3(trans.localPosition.x + x, trans.localPosition.y, trans.localPosition.z);
	}

	public static void LocalSetY(this Transform trans, float y)
	{
		trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
	}

	public static void SetPos(this Transform trans, float x, float y, float z, bool ignoreZeroes)
	{
		if (ignoreZeroes)
		{
			if (x == 0) { x = trans.position.x; }
			if (y == 0) { y = trans.position.y; }
			if (z == 0) { z = trans.position.z; }
		}
		trans.position = new Vector3(x, y, z);
	}
	public static void SetPos(this Transform trans, float x, float y, float z)
	{
		SetPos(trans, x, y, z, false);
	}

	public static void LocalSetPos(this Transform trans, float x, float y, float z, bool ignoreZeroes)
	{
		if (ignoreZeroes)
		{
			if (x == 0) { x = trans.localPosition.x; }
			if (y == 0) { y = trans.localPosition.y; }
			if (z == 0) { z = trans.localPosition.z; }
		}
		trans.position = new Vector3(x, y, z);
	}
	public static void LocalSetPos(this Transform trans, float x, float y, float z)
	{
		SetPos(trans, x, y, z, false);
	}

	public static void AddToPos(this Transform trans, float x, float y, float z)
	{
		trans.position += new Vector3(x, y, z);
	}

	public static void LocalAddToPos(this Transform trans, float x, float y, float z)
	{
		trans.localPosition += new Vector3(x, y, z);
	}

	public static void SetAng(this Transform trans, float x, float y, float z)
	{
		trans.localEulerAngles = new Vector3(x, y, z);
	}

	public static void AddToAng(this Transform trans, float x, float y, float z)
	{
		trans.localEulerAngles += new Vector3(x, y, z);
	}

	public static void SetBool(this PlayerPrefs playerPref, string name, bool booleanValue)
	{
		PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
	}

	public static bool GetBool(this PlayerPrefs playerPref, string name)
	{
		return PlayerPrefs.GetInt(name) == 1 ? true : false;
	}


	public static void SetAlpha(this Color color, float a)
	{
		color = new Vector4(color.r, color.g, color.b, a);
	}


	public static void AddX(this Transform trans, float x)
	{
		trans.position = new Vector3(trans.position.x + x, trans.position.y, trans.position.z);
	}
	public static void AddY(this Transform trans, float y)
	{
		trans.position = new Vector3(trans.position.x, trans.position.y + y, trans.position.z);
	}

	public static void AddZ(this Transform trans, float z)
	{
		trans.position = new Vector3(trans.position.x, trans.position.y, trans.position.z + z);
	}

	public static void LocalSetZ(this Transform trans, float z)
	{
		trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, z);
	}

	public static void SetAngX(this Transform trans, float x)
	{
		trans.localEulerAngles = new Vector3(x, trans.localEulerAngles.y, trans.localEulerAngles.z);
	}
	public static void SetAngY(this Transform trans, float y)
	{
		trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, y, trans.localEulerAngles.z);
	}
	public static void SetAngZ(this Transform trans, float z)
	{
		trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, z);
	}

	public static void AddAngX(this Transform trans, float x)
	{
		trans.localEulerAngles += new Vector3(x, 0, 0);
	}
	public static void AddAngY(this Transform trans, float y)
	{
		trans.localEulerAngles += new Vector3(0, y, 0);
	}
	public static void AddAngZ(this Transform trans, float z)
	{
		trans.localEulerAngles += new Vector3(0, 0, z);
	}

	public static void AddAng(this Transform trans, Vector3 vec)
	{
		AddAng(trans, vec.x, vec.y, vec.z);
	}
	public static void AddAng(this Transform trans, float x, float y, float z)
	{
		trans.localEulerAngles += new Vector3(x, y, z);
	}

	public static void SetPan(this Transform trans, float z)
	{
		trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, z);
	}

	public static void SetScaleX(this Transform trans, float x)
	{
		trans.localScale = new Vector3(x, trans.localScale.y, trans.localScale.z);
	}

	public static void SetScaleY(this Transform trans, float y)
	{
		trans.localScale = new Vector3(trans.localScale.x, y, trans.localScale.z);
	}

	public static void SetScaleZ(this Transform trans, float z)
	{
		trans.localScale = new Vector3(trans.localScale.x, trans.localScale.y, z);
	}
}
