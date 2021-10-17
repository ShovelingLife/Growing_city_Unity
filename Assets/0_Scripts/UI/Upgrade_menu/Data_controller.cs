using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_controller : MonoBehaviour
{
    public static Data_controller instance=null;
    public double                 gold;
    public int                    cash;
    public double                 gold_per_click_gold;
    public double                 gold_per_click_city;
    public double                 gold_per_sec_automation;
    public double                 gold_per_sec_resident;
    public double                 gold_per_sec_building;
    public double[]               arr_all_per_sec = new double[4];


    private void Awake()
    {
        if (instance == null) 
            instance = this;

        else Destroy(this);
    }
}