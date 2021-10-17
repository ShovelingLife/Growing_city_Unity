using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_per_sec_property_resident : Upgrade_property_core
{
    Upgrade_button_resident[] m_arr_upgrade_resident = null;

    // Start is called before the first frame update
    void Start()
    {
        m_arr_upgrade_resident = GameObject.FindObjectsOfType<Upgrade_button_resident>();
        dic_current_upgrades.Add(e_upgrade_menu_type.RESIDENT, this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
