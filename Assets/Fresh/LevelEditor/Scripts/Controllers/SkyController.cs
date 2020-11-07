using UnityEngine;

/// <summary>
/// Handles GradientShader.shader
/// </summary>
[RequireComponent(typeof(Renderer))]
public class SkyController : MonoBehaviour
{
	Renderer rnd = null;

	public void Awake()
	{
		this.rnd = this.GetComponent<Renderer>();
	}

	/// <summary>
	/// Set the sky colours and banding.
	/// </summary>
	/// <param name="topColour">Colour at the top of the screen</param>
	/// <param name="bottomColour">Colour at the bottom of the screen</param>
	/// <param name="bands">Number of bands across the screen. Smooth gradient if 0 or 1.</param>
	public void ConfigureSky(Color topColour, Color bottomColour, int bands)
	{
		if (bands > 0) bands--;
		this.rnd.material.SetColor("_Colour1", topColour);
		this.rnd.material.SetColor("_Colour2", bottomColour);
		this.rnd.material.SetFloat("_SectionCount", bands);
	}

	/// <summary>
	/// Tween the sky colours to new values
	/// </summary>
	public void TweenSky(Color newTop, Color newBottom, int bands)
	{
		// TODO
	}
}
