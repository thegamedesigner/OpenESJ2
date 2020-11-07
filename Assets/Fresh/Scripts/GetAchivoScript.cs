using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAchivoScript : MonoBehaviour
{
	public AchivoFuncs.Achivos achivo;

	void Start()
	{

	}

	void Update()
	{
		if (achivo == AchivoFuncs.Achivos.Achivo_AllasKlar)
		{
			AchivoFuncs.GetAchivo(achivo);
		}
		if (achivo == AchivoFuncs.Achivos.Achivo_Cheater)
		{
			if (xa.playerHasDoubleJump)
			{
				AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_Cheater);
			}
		}
		if (achivo == AchivoFuncs.Achivos.Achivo_Reverso)
		{
			if (xa.playerScript.hasSword)
			{
				AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_Reverso);
			}
		}
		this.enabled = false;
	}
}
