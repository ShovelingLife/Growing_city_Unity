using System;
using UnityEngine;

public class Upgrade_button_city : Upgrade_button_core
{
    protected override void Update()
    {
        m_current_gold_quantity = Data_controller.instance.gold_per_click_resident;        
    }

    // 업그레이드 비용을 증가해주는 메소드
    protected override void Update_price()
    {
        double cost_pow_goldPerClick                    = (Math.Log(upgrade_price) * 0.25f) / 2;
        Data_controller.instance.gold                    -= upgrade_price;
        Data_controller.instance.gold_per_click_resident += (cost_pow_goldPerClick*m_upgrade_multiplier);
        current_level                                   += m_upgrade_multiplier;
    }
}