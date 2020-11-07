using UnityEngine;
using System.Collections;

public class BobTheseGOUpAndDown : MonoBehaviour
{
    [Multiline]
    public string label = "";
    public GameObject[] GOs = new GameObject[0];
    public float dist = 2;
    public float scattering = 2;
    public float friction = 1;
    public float speedCap = 1;//never move faster then this
    float[] speeds;
    float[] currentSpeeds;
    bool[] dir;

    void Start()
    {
        speeds = new float[GOs.Length];
        currentSpeeds = new float[GOs.Length];
        dir = new bool[GOs.Length];

        int index = 0;
        while (index < speeds.Length)
        {
            speeds[index] = Random.Range(0, dist);
            GOs[index].transform.AddToPos(0, -Random.Range(0, scattering), 0);
            currentSpeeds[index] = speeds[index];
            if (Random.Range(0, 10) <= 5)
            {
                currentSpeeds[index] = -speeds[index];
                dir[index] = true;
            }
            index++;
        }
    }

    void Update()
    {
        int index = 0;

        while (index < GOs.Length)
        {
            if (currentSpeeds[index] > speedCap)
            {
                GOs[index].transform.AddToPos(0, speedCap * fa.deltaTime, 0);
            }
            else if (currentSpeeds[index] < -speedCap)
            {
                GOs[index].transform.AddToPos(0, -speedCap * fa.deltaTime, 0);
            }
            else
            {
                GOs[index].transform.AddToPos(0, currentSpeeds[index] * fa.deltaTime, 0);
            }

            if (dir[index])
            {
                //going down
                currentSpeeds[index] -= friction * fa.deltaTime;
                if (currentSpeeds[index] < -speeds[index])
                {
                    dir[index] = false;
                }

            }
            else
            {
                //going up
                currentSpeeds[index] += friction * fa.deltaTime;
                if (currentSpeeds[index] > speeds[index])
                {
                    dir[index] = true;
                }
            }

            index++;
        }
    }
}
