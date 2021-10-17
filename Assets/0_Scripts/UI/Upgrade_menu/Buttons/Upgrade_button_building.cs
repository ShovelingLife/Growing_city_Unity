using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_button_building : Upgrade_button_core
{
    protected override void Start()
    {
        m_max_level = Global.max_upgrade_level / 2;
        base.Init();
    }

    // 업그레이드 레벨이 125을 초과하면 텍스트 변경 및 버튼 잠금
    protected override void Update()
    {
        base.Update();
    }

    // 업그레이드 비용을 증가해주는 메소드
    protected override void Update_price()
    {
        Data_controller inst          = Data_controller.instance;
        double tmp_upgrade_price_building = (Math.Log(upgrade_price) * 0.25f) / 2;
        inst.gold                     -= upgrade_price;
        inst.gold_per_sec_building    += (tmp_upgrade_price_building * m_upgrade_multiplier);
        current_level                 += m_upgrade_multiplier;
    }
}
