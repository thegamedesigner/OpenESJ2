using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrushableScript : MonoBehaviour
{
    public static List<CrushableScript> list = new List<CrushableScript>();

    public bool killMe = false;
    public bool collectMe = false;//Trigger the item script
    public GameObject deathEffect;

    //Put this on any gameobject, makes it crushable (stompable, with the new system)
    /*
     Since stomp is instant, I should just check a box once, on the keypress, for having this script on them. (use a static array held in this script)
     
     Everything with this script uses a generic death corpse, for now.
     */
    void Awake()
    {
        list.Add(this);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void Crushed()
    {

    }

    //This function is called when the player stomps.
    public static void CrushStuff(float topY, float bottomY)
    {

    }
}
