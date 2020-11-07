using UnityEngine;
using System.Collections;

public class PartyTimeInfo : MonoBehaviour
{
    public static bool beat = false;
    public static float timeUntilNextBeat = 0;
    public static float timeSinceLastBeat = 0;
    public static Vector3[] currentTrackBeats;
    public static float[] currentTrackPartyTimes;

    public static float[] flare_PartyTimes = {
                                              14.872f,
                                              73.958f
                                           };

    public static Vector3[] flare_Beats = {
                                            new Vector3(14.874f,0,0), //Just a single beat
                                            new Vector3(15.337f,0.463f,29.00f),  //The start of a beat-loop. Start, step-length, end
                                            new Vector3(29.643f,0.460f,42.765f),
                                            new Vector3(44.878f,0.460f,58.5f),
                                            new Vector3(59.182f,0.460f,72.87f)
                                           };

    public static float[] rpm_PartyTimes = {
                                              15.071f,
                                              105.295f,
                                              135.058f,
                                              195.070f
                                           };

    public static float[] wobbleWobble_Beats = { 
                                            0.000f,
                                            0.465f,
                                            0.926f,
                                            1.388f,
                                            1.848f,
                                            2.309f,
                                            2.772f,
                                            3.235f,
                                            3.694f,
                                            4.156f,
                                            4.618f,
                                            5.078f,
                                            5.540f,
                                            6.001f,//gap is next
                                            7.382f,
                                            7.849f,
                                            8.310f,
                                            8.771f,
                                            9.233f,
                                            9.695f,
                                            10.156f,
                                            10.618f,
                                            11.078f,
                                            11.537f,
                                            12.002f,
                                            12.463f,
                                            12.925f,
                                            13.386f,
                                            13.847f,//gap is next
                                            15.231f,
                                            15.694f,
                                            16.155f,
                                            16.617f,
                                            17.079f,
                                            17.540f,
                                            18.002f,
                                            18.461f,
                                            18.925f,
                                            19.387f,
                                            19.847f,
                                            20.311f,
                                            20.772f,
                                            21.224f,
                                            21.685f
                                        };
}
