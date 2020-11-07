using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GenericTriggerZone : MonoBehaviour
{
	public Behaviour[] scriptsToActivate;
	public bool triggerOnlyOnce = false;//makes it trigger only once ever. defaults to triggering once per entry to zone

	bool active = true;
	float x;
	float y;
	float px = -999;
	float py = -999;
	Vector3 halfScale;
	MeshRenderer meshRenderer = null;

	private void Awake()
	{
		this.meshRenderer = this.GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = EditorController.IsEditorActive();
		}

		if (xa.player && !xa.playerDead)
		{
			halfScale = transform.localScale * 0.5f;
			x = transform.position.x;
			y = transform.position.y;
			px = xa.player.transform.position.x;
			py = xa.player.transform.position.y;
			if ((x + halfScale.x) > (px - (xa.playerBoxWidth * 0.5f)) &&
				(x - halfScale.x) < (px + (xa.playerBoxWidth * 0.5f)) &&
				(y + halfScale.y) > (py - (xa.playerBoxHeight * 0.5f)) &&
				(y - halfScale.y) < (py + (xa.playerBoxHeight * 0.5f)))
			{
				if (active)
				{
					foreach (Behaviour co in scriptsToActivate)
					{
						co.enabled = true;
					}
					active = false;
				}
			}
			else
			{
				if (!triggerOnlyOnce) { active = true; }
			}
		}
	}
}
