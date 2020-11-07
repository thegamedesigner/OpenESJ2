using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FreshDetailBlocks : MonoBehaviour
{
	public bool setBlocks = false;
	public bool done = false;

	public enum Type
	{
		Invisible = 1,//Not touching anything, so become invisible.
		North = 2, //Touching something to the north.
		South = 3,
		West = 4,
		East = 5,

		NW = 6, //Has something to the North & to the West (a corner)
		NE = 7,
		SW = 8,
		SE = 9,

		NS = 10,//Has something above & below
		WE = 11,//Has something left & right
		
		NSW = 12,//
		NSE = 13,//
		NWE = 14,//
		SWE = 15,//

		NSWE = 16,//All four sides


		End
	}

	void Update()
	{
		if (done) { return; }
		if (setBlocks) { SetFreshDetailBlocks(); }
	}

	void SetFreshDetailBlocks()
	{
		//Set all hitboxes to the correct raycast layer
		GameObject[] allGos = GameObject.FindObjectsOfType<GameObject>();
		List<GameObject> allHitboxes = new List<GameObject>();
		for (int i = 0; i < allGos.Length; i++) { if (allGos[i].layer == 19) { allHitboxes.Add(allGos[i]); } }
		for (int i = 0; i < allHitboxes.Count; i++)
		{
			allHitboxes[i].transform.SetZ(xa.GetLayer(xa.layers.RaycastLayer));
		}


		GameObject[] gos = GameObject.FindGameObjectsWithTag("detailBlock");
		for (int i = 0; i < gos.Length; i++)
		{
			Type type = GetType(gos[i]);
			Debug.Log("type is " + type);

			InEditorDetailBlocks script = gos[i].GetComponent<InEditorDetailBlocks>();
			if (script != null)
			{
				script.type = type;
			}

		}
		done = true;
	}

	Type GetType(GameObject go)
	{
		bool n = false;
		bool s = false;
		bool w = false;
		bool e = false;

		LayerMask blockMask = 1 << 19;//Only hits hitboxes on the NovaBlock layer
		Ray ray = new Ray();
		ray.origin = new Vector3(go.transform.position.x, go.transform.position.y, xa.GetLayer(xa.layers.RaycastLayer));
		ray.direction = new Vector3(0, 1, 0); if (Physics.Raycast(ray, 1, blockMask)) { n = true; }
		ray.direction = new Vector3(0, -1, 0); if (Physics.Raycast(ray, 1, blockMask)) { s = true; }
		ray.direction = new Vector3(-1, 0, 0); if (Physics.Raycast(ray, 1, blockMask)) { w = true; }
		ray.direction = new Vector3(1, 0, 0); if (Physics.Raycast(ray, 1, blockMask)) { e = true; }

		if (n && !s && !w && !e) { return Type.North; }
		if (!n && s && !w && !e) { return Type.South; }
		if (!n && !s && w && !e) { return Type.West; }
		if (!n && !s && !w && e) { return Type.East; }

		if (!n && s && w && !e) { return Type.SW; }
		if (!n && s && !w && e) { return Type.SE; }
		if (n && !s && w && !e) { return Type.NW; }
		if (n && !s && !w && e) { return Type.NE; }
		
		if (n && s && !w && !e) { return Type.NS; }
		if (!n && !s && w && e) { return Type.WE; }
		if (n && s && w && !e) { return Type.NSW; }
		if (n && s && !w && e) { return Type.NSE; }
		if (n && !s && w && e) { return Type.NWE; }
		if (!n && s && w && e) { return Type.SWE; }
		if (n && s && w && e) { return Type.NSWE; }

		return Type.Invisible;
	}

}
