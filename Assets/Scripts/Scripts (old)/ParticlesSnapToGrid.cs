using UnityEngine;

public class ParticlesSnapToGrid : MonoBehaviour
{
	public ParticleSystem particles;
	public int arraySize = 1500;
	ParticleSystem.Particle[] gos;
	public bool useCustomGrid = false;
	public float customGrid = 4;
	public bool dontSetSize = false;
	float grid = 4;
	// Use this for initialization
	void Start()
	{
		grid = customGrid;
		xa.glx = transform.position;
		transform.position = xa.glx;

		gos = new ParticleSystem.Particle[arraySize];
	}

	float roundFunc(float a)
	{
		a = Mathf.RoundToInt(a * grid) / grid;
		return (a);
	}

	// Update is called once per frame
	void Update()
	{
		int index = 0;
		int total = 0;

		total = particles.GetParticles(gos);
		while (index < total)
		{
			xa.glx = gos[index].position;
			xa.glx.x = Mathf.RoundToInt(xa.glx.x) + roundFunc(xa.glx.x - Mathf.RoundToInt(xa.glx.x));
			xa.glx.y = Mathf.RoundToInt(xa.glx.y) + roundFunc(xa.glx.y - Mathf.RoundToInt(xa.glx.y));
			gos[index].position = xa.glx;

			if (!dontSetSize) {
				gos[index].startSize = 0.25f;
			}


			index++;
		}
		particles.SetParticles(gos, total);
	}
}
