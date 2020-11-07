using UnityEngine;
using System.Collections;

public class AddToScoreOnEnabled : MonoBehaviour
{
    public int score = 0;
    void Start()
    {
        xa.displayScore += score;
    }
}
