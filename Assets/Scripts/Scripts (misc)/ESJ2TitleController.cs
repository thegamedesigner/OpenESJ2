using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class ESJ2TitleController : MonoBehaviour
{
    public GameObject ElectronicObj;
    public GameObject SuperObj;
    public GameObject JoyObj;
    public GameObject TwoObj;
    int stage = 0;

    [HideInInspector]
    public float electronicY = 0;
    TweenParms tweenParms_Electronic;
    bool electronicBool = false;

    [HideInInspector]
    public float superY = 0;
    TweenParms tweenParms_Super;
    bool superBool = false;

    [HideInInspector]
    public float joyY = 0;
    TweenParms tweenParms_Joy;
    bool joyBool = false;

    [HideInInspector]
    public float twoY = 0;
    TweenParms tweenParms_Two;
    bool twoBool = false;


    void Start()
    {
        electronicY = ElectronicObj.transform.position.y;
        superY = SuperObj.transform.position.y;
        joyY = JoyObj.transform.position.y;
        twoY = TwoObj.transform.position.y;
        tweenParms_Electronic = new TweenParms().Prop("electronicY", -2.873726).Ease(EaseType.EaseInOutSine).Loops(1);
        tweenParms_Super = new TweenParms().Prop("superY", -2.873726).Ease(EaseType.EaseInOutSine).Loops(1);
        tweenParms_Joy = new TweenParms().Prop("joyY", -2.873726).Ease(EaseType.EaseInOutSine).Loops(1);
        tweenParms_Two = new TweenParms().Prop("twoY", -2.873726).Ease(EaseType.EaseInOutSine).Loops(1);
    }

    void Update()
    {
        if (fa.time >= 0.017f && !electronicBool)
        {
            TweenThings(1);
            electronicBool = true;
        }
        if (fa.time >= 0.252f && !superBool)
        {
            TweenThings(2);
            superBool = true;
        }
        if (fa.time >= 0.497f && !joyBool)
        {
            TweenThings(3);
            joyBool = true;
        }
        if (fa.time >= 0.700f && !twoBool)
        {
            TweenThings(4);
            twoBool = true;
        }

        ElectronicObj.transform.LocalSetY(electronicY);
        SuperObj.transform.LocalSetY(superY);
        JoyObj.transform.LocalSetY(joyY);
        TwoObj.transform.LocalSetY(twoY);
    }

    void TweenThings(int thing)
    {
        switch (thing)
        {
            case 1:
                HOTween.To(this, 0.2f, tweenParms_Electronic);
                break;
            case 2:
                HOTween.To(this, 0.2f, tweenParms_Super);
                break;
            case 3:
                HOTween.To(this, 0.2f, tweenParms_Joy);
                break;
            case 4:
                HOTween.To(this, 0.2f, tweenParms_Two);
                break;
        }
    }
}
