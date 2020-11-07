using UnityEngine;
using System.Collections;

public class AwardPopupScript : MonoBehaviour
{
    /*
     * 
     * Destroy self after X seconds
     */
    public GameObject itweenThis = null;
    public GameObject destroyThis = null;
    public GameObject animateThis = null;
    public int awardID = -1;

    float timeSet = 0;
    int stage = 0;
    void Start()
    {
        timeSet = fa.time;

        //transform.setPos(18, -300 + za.relativeYForAwards, 2, true);

    }

    void Update()
    {
        if (fa.time > timeSet + 1 && stage == 0)
        {
            stage = 1;
            animateThis.SendMessage("playAni" + awardID);
            iTween.MoveBy(itweenThis, iTween.Hash("x", -18, "time", 1, "easetype", iTween.EaseType.easeInOutSine));
        }
        if (fa.time > timeSet + 5 && stage == 1)
        {
            stage = 2;
            iTween.MoveBy(itweenThis, iTween.Hash("x", 18, "time", 1, "easetype", iTween.EaseType.easeInOutSine));
        }
        if (fa.time > timeSet + 11 && stage == 2)
        {
            stage = 3;
            za.relativeYForAwards += 3.6f;
            Destroy(destroyThis);
        }

    }
}
