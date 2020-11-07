using UnityEngine;
using System.Collections;

public class ScoreObject : MonoBehaviour
{
    void Start()
    {
        if (!xa.scoreObject)
        {
            DontDestroyOnLoad(this.gameObject);
            xa.scoreObject = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Update()
    {
		if (xa.showScore && !fa.isMenuLevel)
		{
			transform.LocalSetX(0);
		}
		else
		{
			transform.LocalSetX(-300);
		}
    }
}
