using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafsScript : MonoBehaviour
{
	public ParticleSystem ps;
	ParticleSystem.Particle[] particles;

	void Start()
	{

	}

	void Update()
	{
		float dist = Vector2.Distance(transform.position, xa.playerPos);
		if (dist < ps.shape.radius + 3)
		{
			//ok, the player is near enough, let's check each particle
			particles = new ParticleSystem.Particle[ps.main.maxParticles];
			int numOfParticles = ps.GetParticles(particles);

			for (int i = 0; i < particles.Length; i++)
			{
				if (Vector2.Distance(particles[i].position, xa.playerPos) < 2.3f)
				{
					//xa.emptyObj.transform.position = particles[i].position;
					//Vector3 vOld = xa.emptyObj.transform.position;
					//xa.emptyObj.transform.LookAt(xa.playerPos);
					//xa.emptyObj.transform.AddAngX(180);
					//xa.emptyObj.transform.Translate(5 * Time.deltaTime,0,0);
					//Debug.DrawLine(xa.emptyObj.transform.position,vOld, Color.green);
					//particles[i].position = xa.emptyObj.transform.position;

					particles[i].remainingLifetime = 0;
				}
			}
			ps.SetParticles(particles, ps.main.maxParticles);
		}
	}
}
