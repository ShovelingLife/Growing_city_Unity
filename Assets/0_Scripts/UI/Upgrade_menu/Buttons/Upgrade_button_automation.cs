using System;
using UnityEngine;

public class Upgrade_button_automation : Upgrade_button_core
{
    protected override void Start()
    {
        base.Start();
        base.Init();
    }

    // 업그레이드 레벨이 150을 초과하면 텍스트 변경 및 버튼 잠금
    protected override void Update()
    {
        base.Update();
    }

    // 업데이트 아이템 가격 및 초당 생성하는 돈
    protected override void Update_price()
    {
        double tmp_upgrade_price_automation              = (Math.Log(upgrade_price) * 0.25f) / 2;
        Data_controller.instance.gold                    -= upgrade_price;
        Data_controller.instance.gold_per_sec_automation += (tmp_upgrade_price_automation * m_upgrade_multiplier);
        Data_controller.instance.arr_all_per_sec[3]      += upgrade_price;
        current_level                                    += m_upgrade_multiplier;
    }
}