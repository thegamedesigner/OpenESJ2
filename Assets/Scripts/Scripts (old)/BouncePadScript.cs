using UnityEngine;
using System.Collections;

public class BouncePadScript : MonoBehaviour
{
	public GameObject impactExplo = null;
	public float speed = 0;
	[HideInInspector]
	public int triggered = 0;
	float counter = 0;
	public bool swapFaceAni = false;
	public GameObject faceAniObj;
	public int face_AniToPlayOnImpact = -1;

	void Update()
	{
		if (speed != 0)
		{
			counter += 10 * fa.deltaTime;
			if (counter > speed)
			{
				counter = 0;
				xa.glx = transform.localEulerAngles;
				xa.glx.z += 45;
				transform.localEulerAngles = xa.glx;
			}
		}

		if (triggered == 1)
		{
			triggered = 2;


			xa.glx = transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.emptyObj.transform.localEulerAngles = new Vector3(0, 0, 0);
			xa.tempobj = (GameObject)(Instantiate(impactExplo, xa.glx, xa.emptyObj.transform.localRotation));
			xa.tempobj.transform.parent = xa.createdObjects.transform;

			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.BouncePad);

			if (swapFaceAni)
			{
				//Debug.Log("HI!");
				FreshAni aniScript = faceAniObj.GetComponent<FreshAni>();
				if (aniScript != null)
				{
					aniScript.PlayAnimation(face_AniToPlayOnImpact);
				}
			}



		}
	}
}
