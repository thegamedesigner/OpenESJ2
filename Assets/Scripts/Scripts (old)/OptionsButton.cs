using UnityEngine;
using System.Collections;

public class OptionsButton : MonoBehaviour
{
	public enum optionsType { None, Timer, Sound, Music, PG }
	public optionsType type   = optionsType.None;
	public int x              = 0;
	public int y              = 0;
	public bool use16x16Sheet = false;
	public bool use4x4Sheet   = true;
	float multi               = 0;

	void Start()
	{
		multi = 1;
		if (use16x16Sheet) { multi = 0.5f; }
		if (use4x4Sheet) { multi = 2f; }
		setTexture(x, y);
	}

	void Update()
	{
		switch (type)
		{
			case optionsType.Timer:
				if (xa.showTimer)
				{
					setTexture(x, y);
				}
				else
				{
					setTexture(x + 1, y);
				}
			break;
			case optionsType.Music:
				if (xa.muteMusic == 1)
				{
					setTexture(x, y);
				}
				else if(xa.muteMusic == 0)
				{
					setTexture(x + 1, y);
				}
			break;
			case optionsType.Sound:
				if (xa.muteSound == 1)
				{
					setTexture(x, y);
				}
				else if (xa.muteSound == 0)
				{
					setTexture(x + 1, y);
				}
			break;
			case optionsType.PG:
				if (xa.pgMode)
				{
					setTexture(x, y);
				}
				else
				{
					setTexture(x + 1, y);
				}
			break;
			case optionsType.None:
				setTexture(x, y);
			break;
		}
	}

	void setTexture(int v1, int v2)
	{
		Vector2 scale  = new Vector2(0.125f * multi, 0.125f * multi);
		Vector2 offset = new Vector2(0.125f * multi * v1, 1 - ((0.125f * multi * v2) + (0.125f * multi)));

		this.gameObject.GetComponent<Renderer>().material.mainTextureScale  = scale;
		this.gameObject.GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
