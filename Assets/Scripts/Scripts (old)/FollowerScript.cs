using UnityEngine;
using System.Collections;

public class FollowerScript : MonoBehaviour
{
    public GameObject node = null;
    public float speed = 5;
    public iTween.EaseType easeType = iTween.EaseType.linear;
    enum states { starting, moving, arrived, teleporting }
    states state = states.starting;
    void Start()
    {

    }

    public void arrived()
    {
        state = states.arrived;
    }

    void Update()
    {
        if (state == states.teleporting)
        {
            state = states.arrived;
            xa.glx = transform.position;
            xa.glx.x = node.transform.position.x;
            xa.glx.y = node.transform.position.y;
            transform.position = xa.glx;
        
        }

        if (state == states.starting)
        {
            state = states.moving;
            iTween.MoveTo(this.gameObject, iTween.Hash("x", node.transform.position.x, "y", node.transform.position.y, "speed", speed, "oncomplete", "arrived", "oncompletetarget", this.gameObject, "easetype", easeType));
        }

        if(state == states.arrived)
        {
            //get next node
            FollowerNodeScript script = node.GetComponent<FollowerNodeScript>();
            node = script.nextNode;
            if (script.teleportToNextNode)
            {
                state = states.teleporting;
            }
            else
            {
                state = states.starting;
            }
        }
    }
}
