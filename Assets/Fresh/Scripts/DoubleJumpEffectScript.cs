using UnityEngine;

public class DoubleJumpEffectScript : MonoBehaviour
{
    public GameObject ring;
    public GameObject blast1;
    public GameObject blast2;
    public GameObject backdrop;
    public GameObject center;

    float timeset;

    void Start()
    {
        //fa.FreezeForX(1);

        //handle ring
        if (ring != null)
        {
            ring.transform.SetScaleX(1);
            ring.transform.SetScaleY(1);
            iTween.ScaleTo(ring, iTween.Hash("x", 0.9, "y", 0.9, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine));
            iTween.FadeTo(ring, iTween.Hash("delay", 0.1f, "alpha", 0, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine));

        }

        //handle 
        if (blast1 != null)
        {
            float ranAng = Random.Range(0f, 360f);
            blast1.transform.SetAngZ(ranAng);
            iTween.ScaleTo(blast1, iTween.Hash("x", 1, "y", 1, "time", 0.1f, "easetype", iTween.EaseType.easeInSine));
            iTween.ScaleTo(blast1, iTween.Hash("delay", 0.1f, "x", 1.5, "y", 1.5, "time", 0.5f, "easetype", iTween.EaseType.easeOutSine));
            iTween.FadeTo(blast1, iTween.Hash("alpha", 0, "time", 0.5f, "easetype", iTween.EaseType.easeInOutSine));
        }

        //handle blast2
        if (blast2 != null)
        {
            float ranAng = Random.Range(0f, 360f);
            blast2.transform.SetAngZ(ranAng);
            iTween.ScaleTo(blast2, iTween.Hash("x", 1, "y", 1, "time", 0.1f, "easetype", iTween.EaseType.easeInSine));
            iTween.ScaleTo(blast2, iTween.Hash("delay", 0.1f, "x", 1.5, "y", 1.5, "time", 0.5f, "easetype", iTween.EaseType.easeOutSine));
            iTween.FadeTo(blast2, iTween.Hash("alpha", 0, "time", 0.4f, "easetype", iTween.EaseType.easeInOutSine));
        }

        //handle backdrop
        if (backdrop != null)
        {
            iTween.ScaleTo(backdrop, iTween.Hash("x", 1, "y", 1, "time", 0.7, "easetype", iTween.EaseType.easeInOutSine));
            iTween.FadeTo(backdrop, iTween.Hash("alpha", 0, "time", 0.7f, "easetype", iTween.EaseType.easeInOutSine));

        }

        //handle center
        if (center != null)
        {
            iTween.ScaleTo(center, iTween.Hash("x", 1, "y", 1, "time", 0.7, "easetype", iTween.EaseType.easeInOutSine));
            iTween.FadeTo(center, iTween.Hash("alpha", 0, "time", 0.7f, "easetype", iTween.EaseType.easeInOutSine));

        }
        timeset = fa.time;
    }

    void Update()
    {
        if (fa.time > (timeset + 1))
        {
            Destroy(this.gameObject);
        }
    }
}
