using UnityEngine;
using System.Collections;

public class CountdownObject : MonoBehaviour
{
    void Start()
    {
        if (!xa.countdownObject)
        {
            DontDestroyOnLoad(this.gameObject);
            xa.countdownObject = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Update()
    {
		if (xa.showCountdown && !fa.isMenuLevel)
		{
			transform.LocalSetX(0);
		}
		else
		{
			transform.LocalSetX(-300);
		}
    }
}
