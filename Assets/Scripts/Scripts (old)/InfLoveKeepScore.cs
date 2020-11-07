using UnityEngine;

class InfLoveKeepScore : MonoBehaviour
{
	void Start()
	{
		gameObject.GetComponent<TextMesh>().text = xa.lastPortalStep + " (Best: " + xa.maxPortalStep + ")";
	}
}
