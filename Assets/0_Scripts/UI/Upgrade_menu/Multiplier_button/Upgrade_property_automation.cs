using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_property_automation : Upgrade_property_core
{
    protected override void Awake()
    {
        base.Awake();
        base.Init_buttons(e_upgrade_menu_type.AUTOMATION);
        dic_current_upgrades.Add(e_upgrade_menu_type.AUTOMATION, this);
    }
}
