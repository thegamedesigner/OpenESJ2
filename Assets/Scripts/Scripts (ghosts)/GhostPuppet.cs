using UnityEngine;
using System.Collections;

public class GhostPuppet : MonoBehaviour {
	public GameObject puppet;
	public TextMesh nameTag;

	public void Awake() {
		MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
        Color color = puppet.GetComponent<Renderer>().material.color;
        color.a = 0.5f;
        materialProperty.SetColor("_Color", color);
        puppet.GetComponent<Renderer>().SetPropertyBlock(materialProperty);
	}

    public void SetAnimationFrame(int frameIndex)
    {
        //Debug.Log(nameTag.text + " - frame Index: " + frameIndex);
		Vector2 animationFrame = Setup.IntToAniFrame( frameIndex );
        //Debug.Log(nameTag.text + " - x: " + (int)animationFrame.x + ", y: " + (int)animationFrame.y);
		AnimationScript_Generic.SetTextureGhosts( (int)animationFrame.x, (int)animationFrame.y, puppet);
	}

	public void SetNameTag(string name) {
		nameTag.text = name;
	}

	public void SetPosition(Vector3 position) {
		float scale = (transform.position.x - position.x) < 0 ? 2f : -2f;
		transform.position = position;
		puppet.transform.localScale = new Vector3(scale,-2,0.1f);
	}

	public void Hide() {
		puppet.GetComponent<Renderer>().enabled = false;
		nameTag.GetComponent<Renderer>().enabled = false;
	}

	public void Show() {
		puppet.GetComponent<Renderer>().enabled = true;
		nameTag.GetComponent<Renderer>().enabled = true;
	}
}
