using UnityEngine;

public class MerpsBlockPuppetScript : MonoBehaviour
{
	public bool use32x32Sheet = false;
	public bool use16x16Sheet = true;
	public bool use8x8Sheet = false;
	public bool use4x4Sheet = false;

	public bool dontAffectMyTexture = false;
	public bool affectMeNotMyParent = false;

	public Vector2 frame = Vector2.zero;

	float multi = 1.5f;

	void Start()
	{
		multi = 1;
		if (use32x32Sheet) { multi = 0.25f; }
		if (use16x16Sheet) { multi = 0.5f; }
		if (use8x8Sheet) { multi = 1; }
		if (use4x4Sheet) { multi = 2f; }

		//snap to layer
		if (affectMeNotMyParent) { xa.glx = transform.position; }
		else { xa.glx = transform.parent.transform.position; }
		xa.glx.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
		if (affectMeNotMyParent) { transform.position = xa.glx; }
		else { transform.parent.transform.position = xa.glx; }

		if (!dontAffectMyTexture)
		{
			MerpsSetup.setTexture((int)(frame.x), (int)(frame.y), multi, this.gameObject);
		}
	}
}
