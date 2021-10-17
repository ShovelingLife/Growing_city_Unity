using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_per_sec_property_city : Upgrade_property_core
{
    Upgrade_button_city[] m_arr_upgrade_city = null;

    // Start is called before the first frame update
    void Start()
    {
        m_arr_upgrade_city = GameObject.FindObjectsOfType<Upgrade_button_city>();
        dic_current_upgrades.Add(e_upgrade_menu_type.CITY, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
