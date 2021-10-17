using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
#if UNITY_IOS
    public static readonly string game_ID = "3502466"; 
#elif UNITY_ANDROID
    public static readonly string game_ID = "3502467";

#elif UNITY_EDITOR
    public static readonly string game_ID = "";
#endif

    public static readonly int car_waypoint_count = 4;

    public static readonly int max_upgrade_level = 250;

    public static int Get_id(string _obj_name)
    {
        int value = 0;

        if (Int32.TryParse(_obj_name.Split('_')[0], out value))
            return value;

        return value;
    }
}
