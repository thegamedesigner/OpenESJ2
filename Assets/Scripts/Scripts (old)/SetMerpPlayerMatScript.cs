using UnityEngine;
using System.Collections;

public class SetMerpPlayerMatScript : MonoBehaviour
{
	public Material world1_mat;
	public Material world2_mat;

	public Material spacenaut_w1_mat;
	void Update()
	{
		if (za.merpsLocalNode)
		{
			if (za.merpsLocalNode.useSpacenautAsPlayer)
			{
				if (za.merpsLocalNode.world == za.merpsWorlds.World1) { GetComponent<Renderer>().material = spacenaut_w1_mat; }
			}
			else
			{
				if (za.merpsLocalNode.world == za.merpsWorlds.World1) { GetComponent<Renderer>().material = world1_mat; }
				if (za.merpsLocalNode.world == za.merpsWorlds.World2) { GetComponent<Renderer>().material = world2_mat; }
			}
			this.enabled = false;
		}
	}
}
