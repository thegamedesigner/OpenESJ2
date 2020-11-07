using UnityEngine;
using System.Collections;

public class AwardPopupSpawner : MonoBehaviour
{

    void Start()
    {
        if (xa.awardPopupSpawner)
        {
            Destroy(this.gameObject);
        }
        else
        {
            xa.awardPopupSpawner = this.gameObject;
            xa.awardPopupSpawnerScript = this;
        }
    }

    public void spawnAwardPopup()
    {
        za.relativeYForAwards -= 3.6f;
        xa.tempobj = (GameObject)Instantiate(xa.de.awardPopupPrefab, new Vector3(18, -300 + za.relativeYForAwards + 3.6f, 2), xa.de.awardPopupPrefab.transform.rotation);


        AwardPopupLocalLinks linkScript = xa.tempobj.GetComponent<AwardPopupLocalLinks>();
        if (linkScript)
        {
            linkScript.awardName.text = "need text here!";
            AwardPopupScript script = linkScript.awardScriptObj.GetComponent<AwardPopupScript>();
            //script.awardID = (int)award;
            // linkScript.animationObj.SendMessage("playAni" + (int)(award));
            //linkScript.animationObj.renderer.
        }
    }
}
