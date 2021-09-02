using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
#if UNITY_IOS
    public static readonly string game_ID = "3502466"; 
#elif UNITY_ANDROID
    public static readonly string game_ID = "3502467";
#endif

    public static readonly int car_waypoint_count = 4;

    public static readonly int max_upgrade_level = 250;
}
