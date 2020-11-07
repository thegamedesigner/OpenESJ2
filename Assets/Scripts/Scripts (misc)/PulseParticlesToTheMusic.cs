using UnityEngine;

public class PulseParticlesToTheMusic : MonoBehaviour
{

    ParticleSystem.Particle[] emittedParticles;
    void Start()
    {
    }

    void Update()
    {
        float goal = 0.5f + (1 * xa.beat_Freq);
        emittedParticles = new ParticleSystem.Particle[this.GetComponent<ParticleSystem>().particleCount];

        this.GetComponent<ParticleSystem>().GetParticles(emittedParticles);

        int index = 0;
        while (index < emittedParticles.Length)
        {
            if (emittedParticles[index].startSize < goal)
            {
                emittedParticles[index].startSize = goal;
            }
            else
            {
                emittedParticles[index].startSize -= 1 * fa.deltaTime;
            }
            index++;
        }


        this.GetComponent<ParticleSystem>().SetParticles(emittedParticles, emittedParticles.Length);
    }
}
