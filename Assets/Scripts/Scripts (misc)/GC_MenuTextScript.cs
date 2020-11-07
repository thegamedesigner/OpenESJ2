using UnityEngine;
using System.Collections;

public class GC_MenuTextScript : MonoBehaviour
{
    public static int state = 0;//0 is english, 1 is French
    
    public static string[] jump = new string[10];
    public static string[] moveLeft = new string[10];
    public static string[] moveRight = new string[10];
    public static string[] use = new string[10];
    public static string[] respawn = new string[10];


    void Start()
    {
        //English
        jump[0] = "Jump: ";
        moveLeft[0] = "Move Left: ";
        moveRight[0] = "Move Right: ";
        use[0] = "Use: ";
        respawn[0] = "Respawn: ";


        //French
        jump[1] = "Saut: ";
        moveLeft[1] = "Déplacer vers la Gauche:";
        moveRight[1] = "Déplacer vers la Droite: ";
        use[1] = "Utilisez: ";
        respawn[1] = "Recommencer: ";




    }

}
