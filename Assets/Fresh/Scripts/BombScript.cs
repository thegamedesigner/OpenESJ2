using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public enum Type
    {
        None,
        BasicGift,//Explodes into some ninja stars after a short time.
        WobbleGift,
        JustBullet,
        SlimeBullet,
        SubSlimeBullet,
        End
    }
    public HealthScript healthScript;
    public Type type = Type.None;
    public GameObject puppet;
    public GameObject subBullet;
    public GameObject deathExplo;
    public float lifespan = 0;
    public float numOfSubBullets = 3;
    float lifespanTimeSet;
    public float spinSpd = 155;
    public float speed = 5;
    public bool useMinX = false;
    public float minX = 0;//die after crossing this X threshhold
    float bounceTimeSet;
    float bounceTime;

    void Start()
    {
        lifespanTimeSet = fa.time;
        //iTween.ScaleBy(puppet, iTween.Hash("x", 1.2, "y", 1.2, "time", 0.6f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

        if (type == Type.WobbleGift)
        {
            iTween.RotateBy(this.gameObject, iTween.Hash("z", 0.2f, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

        }
    }

    void Update()
    {
        if(useMinX) {if(transform.position.x < minX) {healthScript.health = 0; } }
        if (type != Type.SlimeBullet && type != Type.SubSlimeBullet)
        {
            if (fa.time >= (bounceTimeSet + bounceTime))
            {
                if (lifespan - (fa.time - lifespanTimeSet) < 1.2f)
                {
                    bounceTime = 0.21f;//(lifespan - (fa.time - lifespanTimeSet)) * 0.2f;
                    bounceTimeSet = fa.time;
                    //iTween.RotateBy(puppet, iTween.Hash("z", 1, "time", bounceTime, "easetype", iTween.EaseType.linear));
                    iTween.ScaleTo(puppet, iTween.Hash("x", 3, "y", 3, "time", 0.1f, "easetype", iTween.EaseType.easeInSine));
                    iTween.ScaleTo(puppet, iTween.Hash("delay", 0.11f, "x", 2, "y", 2, "time", 0.1f, "easetype", iTween.EaseType.easeOutSine));
                }
                else
                {
                    bounceTime = 0.61f;//(lifespan - (fa.time - lifespanTimeSet)) * 0.2f;
                    bounceTimeSet = fa.time;
                    //iTween.RotateBy(puppet, iTween.Hash("z", 1, "time", bounceTime, "easetype", iTween.EaseType.linear));
                    iTween.ScaleTo(puppet, iTween.Hash("x", 2.4f, "y", 2.4f, "time", 0.3f, "easetype", iTween.EaseType.easeInSine));
                    iTween.ScaleTo(puppet, iTween.Hash("delay", 0.31f, "x", 2, "y", 2, "time", 0.3f, "easetype", iTween.EaseType.easeOutSine));
                }
            }
        }





        if (type == Type.BasicGift)
        {
            puppet.transform.AddAngZ(spinSpd * Time.deltaTime * fa.pausedFloat);
        }


        transform.Translate(new Vector3(speed * Time.deltaTime * fa.pausedFloat, 0, 0));


        if (fa.time >= (lifespanTimeSet + lifespan))
        {
            //explode
            if (numOfSubBullets > 0)
            {
                float angleAdd = 360 / numOfSubBullets;
                float angle = transform.localEulerAngles.z;
                int index = (int)numOfSubBullets;
                while (index > 0)
                {
                    Instantiate(subBullet, transform.position, Quaternion.Euler(0, 0, angle));
                    angle += angleAdd;
                    index--;
                }
            }
            healthScript.health = 0;
        }

        if (healthScript.health <= 0)
        {
            if (deathExplo)
            {
                Instantiate(deathExplo, transform.position, Quaternion.Euler(0, 0, 0));
            }

            Destroy(this.gameObject);
        }
    }
}
