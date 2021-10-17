using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_property_resident : Upgrade_property_core
{
    protected override void Awake()
    {
        base.Awake();
        base.Init_buttons(e_upgrade_menu_type.RESIDENT);
        dic_current_upgrades.Add(e_upgrade_menu_type.RESIDENT, this);
    }
}
