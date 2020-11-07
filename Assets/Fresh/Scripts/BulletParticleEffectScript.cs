using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticleEffectScript : MonoBehaviour
{
    public GameObject bullet;
    public ParticleSystem ps;

    bool dead = false;
    float timeSet = 0;
    float delay = 0;

    void Start()
    {
        //  transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (fa.time > (timeSet + 7))
        {
            Destroy(this.gameObject);
        }

        if (bullet != null)
        {
            transform.position = bullet.transform.position;
            //     ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }
}
