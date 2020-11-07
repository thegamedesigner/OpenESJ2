using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
	

	public enum C
	{
		Visible,
		NotVisible,
		Locked,
		Unlocked,
		DontSet
	}
	public static void SetCursor(C vis)
	{ SetCursor(vis, C.DontSet); }
	public static void SetCursor(C vis, C lockstate)
	{
		if (vis == C.Visible)
		{
			Cursor.visible = true;
		}
		if (vis == C.NotVisible)
		{
			if (fa.mouseGrab || FreshLevels.IsFPSLevel(SceneManager.GetActiveScene().name))//Only make invisible if mousegrab is on, or it's a FPS level
			{
				Cursor.visible = false;
			}
		}


		if (lockstate == C.Unlocked)
		{
			Cursor.lockState = CursorLockMode.None;
		}
		if (lockstate == C.Locked)
		{
			if (fa.mouseGrab || FreshLevels.IsFPSLevel(SceneManager.GetActiveScene().name))//Only lock if mousegrab is on, or it's a FPS level
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}






	//First vec is yours, the second is what youre pointing at.
	public static float ReturnAngleTowardsVec(Vector3 vec1, Vector3 vec2)
	{
		return ((Mathf.Atan2((vec2.y - vec1.y), (vec2.x - vec1.x)) * Mathf.Rad2Deg));
	}

	//give it a game object and a xyz offset. It adds them in relate space (ie: pushVector) and returns it
	public Vector3 SetAnOffsetPos(GameObject go, float x1, float y1, float z1)
	{
		xa.emptyObj.transform.position = go.transform.position;
		xa.emptyObj.transform.localEulerAngles = transform.localEulerAngles;
		xa.emptyObj.transform.Translate(x1, y1, z1);
		return (xa.emptyObj.transform.position);
	}

	/*vec1 is the pos your in, ang is your angles, dist is how far out, dir is vector3.up or whatever*/
	public static Vector3 projectVec(Vector3 vec1, Vector3 ang, float dist, Vector3 dir)
	{
		if (!xa.de || !xa.emptyObj)
		{
			return vec1;
		}

		xa.emptyObj.transform.position = vec1;
		xa.emptyObj.transform.localEulerAngles = ang;
		xa.emptyObj.transform.Translate(dir * dist);
		return xa.emptyObj.transform.position;

	}

	public static float Distance(float v1, float v2)
	{
		return Vector2.Distance(new Vector2(v1, 0), new Vector2(v2, 0));
	}

	public static void SetAlpha(GameObject go, float a)
	{
		Color tColour;

		tColour = go.transform.GetComponent<Renderer>().material.color;
		tColour.a = a;
		go.transform.GetComponent<Renderer>().material.color = tColour;
	}

	public static void AddToAlpha(GameObject go, float a)
	{
		Color tColour;

		tColour = go.transform.GetComponent<Renderer>().material.color;
		tColour.a += a;
		go.transform.GetComponent<Renderer>().material.color = tColour;
	}

	public static float distBetweenAngles(Vector3 ang1, Vector3 ang2, Vector3 pos)
	{
		float dist = 0;
		Vector3 pos2;
		Vector3 pos3;

		xa.emptyObj.transform.position = pos;
		xa.emptyObj.transform.localEulerAngles = ang1;
		xa.emptyObj.transform.Translate(0, 0, 10);
		pos2 = xa.emptyObj.transform.position;

		xa.emptyObj.transform.position = pos;
		xa.emptyObj.transform.localEulerAngles = ang2;
		xa.emptyObj.transform.Translate(0, 0, 10);
		pos3 = xa.emptyObj.transform.position;


		dist = Vector3.Distance(pos2, pos3);

		//Debug.DrawLine(pos, pos2, Color.green, 3);
		//Debug.DrawLine(pos, pos3, Color.green, 3);
		//Debug.DrawLine(pos2, pos3, Color.green,3);
		//Setup.GC_DebugLog("FUCKINGCALLEDIT: " + dist);

		return (dist);
	}

	public static int rollLeftOrRight(Vector3 pos, Vector3 ang, Vector3 ang3)
	{
		Vector3 pos2;
		Vector3 pos3;
		Vector3 pos4;
		Vector3 ang2;

		xa.emptyObj.transform.position = pos;
		xa.emptyObj.transform.localEulerAngles = ang;
		xa.emptyObj.transform.Translate(50, 0, 0);
		pos2 = xa.emptyObj.transform.position;

		ang2 = ang3;
		xa.emptyObj.transform.position = pos;
		ang2.z -= 15;
		xa.emptyObj.transform.localEulerAngles = ang2;
		xa.emptyObj.transform.Translate(50, 0, 0);
		pos3 = xa.emptyObj.transform.position;

		ang2 = ang3;
		xa.emptyObj.transform.position = pos;
		ang2.z += 15;
		xa.emptyObj.transform.localEulerAngles = ang2;
		xa.emptyObj.transform.Translate(50, 0, 0);
		pos4 = xa.emptyObj.transform.position;

		//Debug.DrawLine(pos, pos3, Color.green);
		//Debug.DrawLine(pos, pos4, Color.green);
		//Debug.DrawLine(pos, pos2, Color.blue);
		//Debug.DrawLine(pos2, pos4, Color.yellow);

		if (Vector3.Distance(pos2, pos3) <= Vector3.Distance(pos2, pos4))
		{
			return (-1);
		}
		else
		{
			return (1);
		}

	}

	public static void adjustEmptyObjY(float i)
	{
		xa.glx = xa.emptyObj.transform.localEulerAngles;
		xa.glx.y += i;
		xa.emptyObj.transform.localEulerAngles = xa.glx;
	}

	public static void callFadeOutFunc(string lvl, bool fast, string lastLvl)
	{
		if (xa.faderOutScript)
		{
			xa.faderOutScript.fadeOutFunc(lvl, fast, lastLvl);
		}
	}

	public static bool checkForDontDeleteZone(Vector3 pos)
	{
		if (xa.levelDontDeleteZones == null) return false;
		foreach (GameObject go in xa.levelDontDeleteZones)
		{
			if (go == null) continue;
			if ((go.transform.position.x + (go.transform.localScale.x * 0.5f)) > pos.x &&
				(go.transform.position.x - (go.transform.localScale.x * 0.5f)) < pos.x &&
				(go.transform.position.y + (go.transform.localScale.y * 0.5f)) > pos.y &&
				(go.transform.position.y - (go.transform.localScale.y * 0.5f)) < pos.y)
			{
				return true;
			}
		}
		return false;
	}

	public static bool checkVecOnScreen(Vector3 vec1, bool strictCollider)
	{
		if (xa.onScreenCollider && xa.strictOnScreenCollider)
		{
			//Game is broken (//gg tagged on changes)
			//Use a onCollider to make this function,
			Bounds b = (strictCollider) ? xa.strictOnScreenCollider.GetComponent<Collider>().bounds : xa.onScreenCollider.GetComponent<Collider>().bounds;
			return (vec1.x < b.max.x &&
					vec1.y < b.max.y &&
					vec1.x > b.min.x &&
					vec1.y > b.min.y);
		}
		return true;
	}

	public enum snds { None, TeleporterLeftToRight, TeleporterRightToLeft, Missile, Key, MissileLong, Stomp }

	public static void playSound(snds type)
	{
		//THIS IS OLD. USE GC_SOUNDSCRIPT.CS
		if (xa.de)
		{
			switch (type)
			{
				case snds.TeleporterLeftToRight:
					if (!xa.hasPlayedThisFrame[0])
					{
						Instantiate(xa.de.snd_TeleporterRightToLeft);
						xa.hasPlayedThisFrame[0] = true;
					}
					break;

				case snds.TeleporterRightToLeft:
					if (!xa.hasPlayedThisFrame[1])
					{
						Instantiate(xa.de.snd_TeleporterLeftToRight);
						xa.hasPlayedThisFrame[1] = true;
					}
					break;

				case snds.Missile:
					if (!xa.hasPlayedThisFrame[2])
					{
						Instantiate(xa.de.snd_MissileSnds);
						xa.hasPlayedThisFrame[2] = true;
					}
					break;

				case snds.Key:
					if (!xa.hasPlayedThisFrame[3])
					{
						Instantiate(xa.de.snd_KeyAndDoor);
						xa.hasPlayedThisFrame[3] = true;
					}
					break;

				case snds.MissileLong:
					if (!xa.hasPlayedThisFrame[4])
					{
						Instantiate(xa.de.snd_MissileLongSnds);
						xa.hasPlayedThisFrame[4] = true;
					}
					break;
			}
		}
	}


	public static void setTexture(int v1, int v2, float multi, GameObject rendererGO, bool isBlockOrDetailBlock)
	{

		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		if (isBlockOrDetailBlock)
		{
			//set up batchable stats
			if (xa.de)
			{
				rendererGO.GetComponent<Renderer>().material = xa.de.batchable_genericBlockAndDetailMat;
			}
			xa.glx = rendererGO.transform.localScale;
			xa.glx.x = -xa.glx.x;
			xa.glx.y = -xa.glx.y;
			rendererGO.transform.localScale = xa.glx;

		}




		x1 = 0.125f * multi;
		y1 = 0.125f * multi;
		x2 = (0.125f * multi) * v1;
		y2 = 1 - (((0.125f * multi) * v2) + (0.125f * multi));

		if (rendererGO)
		{
			//rendererGO.renderer.material.mainTextureScale = new Vector2(x1, y1);
			//rendererGO.renderer.material.mainTextureOffset = new Vector2(x2, y2);

			Vector2 uvOffset = new Vector2(x2, y2);

			MeshFilter filter = rendererGO.GetComponent<MeshFilter>();
			if (filter != null)
			{
				Mesh mesh = filter.mesh;
				Vector2[] newUVs = new Vector2[mesh.uv.Length];
				int i = 0;

				foreach (Vector2 coordinate in mesh.uv)
				{
					newUVs[i] = new Vector2(coordinate.x * x1 + x2, coordinate.y * y1 + y2);
					i++;
				}

				mesh.uv = newUVs;
			}
		}
	}

	public static void downRezScreenSize()
	{
		//Debug.Log("MOTHERFUCKING BUG SHIT STUFF HERE 1 ");
		//Setup.playSound(Setup.snds.Missile);
		if (Screen.width <= 768)
		{
			Screen.SetResolution((int)(Screen.width * 0.5f), (int)(Screen.height * 0.5f), true);
		}
		else
		{
			Screen.SetResolution(768, 480, true);
		}
		// Screen.SetResolution(Screen.resolutions[0].width,Screen.resolutions[0].height,true);

		//Screen.SetResolution(10, 10, true);
	}

	public static int AniFrameToInt(int x1, float y1)
	{
		return x1 + (int)(y1 * 32);
	}
	public static Vector2 IntToAniFrame(int var)
	{
		Vector2 result = Vector2.zero;

		while (var >= 32)
		{
			var -= 32;
			result.y += 1;
		}
		result.x = var;
		return result;
		// return x1 + (int)(y1 * 32);
	}

}
