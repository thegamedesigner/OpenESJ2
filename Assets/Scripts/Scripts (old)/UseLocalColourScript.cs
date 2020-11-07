using UnityEngine;
using UnityEngine.Serialization;

public class UseLocalColourScript : MonoBehaviour
{
	[SerializeField,FormerlySerializedAs("useLtColour")] private bool useLightColour = false;
	[SerializeField,FormerlySerializedAs("useDkColour")] private bool useDarkColour  = false;

	private void Awake()
	{
		if (this.useLightColour) {
			this.gameObject.GetComponent<Renderer>().material.color = xa.localNodeScript.lightColour;
		} else if (this.useDarkColour) {
			this.gameObject.GetComponent<Renderer>().material.color = xa.localNodeScript.darkColour;
		}
	}
}
