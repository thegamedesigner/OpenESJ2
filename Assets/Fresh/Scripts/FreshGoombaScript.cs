using UnityEngine;

public class FreshGoombaScript : MonoBehaviour
{
	public float speed               = 0;
	public GameObject fallOffPoint   = null;
	public Type type                 = Type.None;
	public EdgeHandling edgeHandling = EdgeHandling.None;

	public enum Type
	{
		None,
		Bunny,
		End
	}

	public enum EdgeHandling
	{
		None,
		FallOff,
		TurnAround,
		End
	}

	void Start()
	{

	}

	void Update()
	{
		transform.Translate(speed * fa.deltaTime, 0, 0);
	}
}
