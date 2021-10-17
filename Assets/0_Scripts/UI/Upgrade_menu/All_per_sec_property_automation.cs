using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_per_sec_property_automation : Upgrade_property_core
{
    Upgrade_button_automation[] m_arr_upgrade_automation = null;

    // Start is called before the first frame update
    void Start()
    {
        m_arr_upgrade_automation = GameObject.FindObjectsOfType<Upgrade_button_automation>();
        dic_current_upgrades.Add(e_upgrade_menu_type.AUTOMATION, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
