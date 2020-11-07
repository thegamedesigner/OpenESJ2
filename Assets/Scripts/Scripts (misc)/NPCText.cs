using UnityEngine;
using System.Collections;

public class NPCText : MonoBehaviour
{
    public GameObject textPrefab = null;
    public GameObject spawnPoint = null;
    public string thingToSay;
    float dist = 8;
    TextMesh textMesh;

    void Update()
    {
        if (thingToSay == null) { this.enabled = false; }
        if (thingToSay == "") { this.enabled = false; }
        if (thingToSay == " ") { this.enabled = false; }

        if (xa.player)
        {
            if (Vector2.Distance(xa.player.transform.position, transform.position) < dist)
            {
                SaySomething();
                this.enabled = false;
            }
        }
    }

    void SaySomething()
    {
        xa.glx = spawnPoint.transform.position;
        xa.tempobj = (GameObject)(Instantiate(textPrefab, xa.glx, textPrefab.transform.rotation));
        textMesh = xa.tempobj.GetComponent<TextMesh>();
        textMesh.text = thingToSay;
    }
}
