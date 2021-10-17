using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_property_crop : Upgrade_property_core
{
    protected override void Awake()
    {
        base.Awake();
        base.Init_buttons(e_upgrade_menu_type.CROP);
        dic_current_upgrades.Add(e_upgrade_menu_type.CROP, this);
    }
}
