using UnityEngine;
using System.Collections;

public class SnapYToFreq : MonoBehaviour
{
    public enum Type { X, Y, XandY }
    public float slideUpSpeed = 0;
    public float slideDownSpeed = 0;
    public GameObject[] GOs = new GameObject[0];
    public int[] freqs = new int[0];
    public float[] multis = new float[0];
    public Type[] type = new Type[0];
    float[] startingY;
    float[] startingX;
    void Start()
    {
        startingY = new float[GOs.Length];
        startingX = new float[GOs.Length];
        int index = 0;
        while (index < GOs.Length)
        {
            startingX[index] = GOs[index].transform.localPosition.x;
            startingY[index] = GOs[index].transform.localPosition.y;
            index++;
        }
    }

    void Update()
    {
        int index = 0;
        while (index < GOs.Length && index < type.Length)
        {
            if (type[index] == Type.X || type[index] == Type.XandY)
            {
                if (GOs[index].transform.localPosition.x > startingX[index] + (xa.music_Spectrum[freqs[index]] * multis[index]))
                {
                    GOs[index].transform.LocalAddToPos(-slideDownSpeed * fa.deltaTime, 0, 0);
                }
                if (GOs[index].transform.localPosition.x < startingX[index] + (xa.music_Spectrum[freqs[index]] * multis[index]))
                {
                    GOs[index].transform.LocalAddToPos(slideUpSpeed * fa.deltaTime, 0, 0);
                }
            }
            if (type[index] == Type.Y || type[index] == Type.XandY)
            {
                if (GOs[index].transform.localPosition.y > startingY[index] + (xa.music_Spectrum[freqs[index]] * multis[index]))
                {
                    GOs[index].transform.LocalAddToPos(0, -slideDownSpeed * fa.deltaTime, 0);
                    if (GOs[index].transform.localPosition.y < startingY[index] + (xa.music_Spectrum[freqs[index]] * multis[index]))
                    {
                        GOs[index].transform.LocalSetY(startingY[index] + (xa.music_Spectrum[freqs[index]] * multis[index]));
                    }
                }
                if (GOs[index].transform.localPosition.y < startingY[index] + (xa.music_Spectrum[freqs[index]] * multis[index]))
                {
                    GOs[index].transform.LocalAddToPos(0, slideUpSpeed * fa.deltaTime, 0);
                    if (GOs[index].transform.localPosition.y > startingY[index] + (xa.music_Spectrum[freqs[index]] * multis[index]))
                    {
                        GOs[index].transform.LocalSetY(startingY[index] + (xa.music_Spectrum[freqs[index]] * multis[index]));
                    }
                }
            }

            index++;
        }
    }
}
