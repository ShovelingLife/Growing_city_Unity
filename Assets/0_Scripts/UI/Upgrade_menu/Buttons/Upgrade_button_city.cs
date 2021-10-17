using System;
using UnityEngine;

public class Upgrade_button_city : Upgrade_button_core
{
    protected override void Start()
    {
        base.Start();
        base.Init();
    }

    // 업그레이드 레벨이 250을 초과하면 텍스트 변경 및 버튼 잠금
    protected override void Update()
    {
        base.Update();
    }

    // 업그레이드 비용을 증가해주는 메소드
    protected override void Update_price()
    {
        double tmp_upgrade_price_city                     = (Math.Log(upgrade_price) * 0.25f) / 2;
        Data_controller.instance.gold                    -= upgrade_price;
        Data_controller.instance.gold_per_click_city += (tmp_upgrade_price_city * m_upgrade_multiplier);
        Data_controller.instance.arr_all_per_sec[1]      += upgrade_price;
        current_level                                    += m_upgrade_multiplier;
    }
}