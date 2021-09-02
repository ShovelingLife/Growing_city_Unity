using System;
using UnityEngine;

public class Upgrade_button_automation : Upgrade_button_core
{
    protected override void Update() // lv이 250이면 버튼 비활성화 및 텍스트 출력 / 초당 획득하는 돈 출력
    {
        m_current_gold_quantity = Data_controller.instance.gold_per_sec_automation;
    }

    // A function that returns all upgradable objects
    public override Upgrade_button_core[] Get_all_objects()
    {
        return Resources.FindObjectsOfTypeAll<Upgrade_button_automation>();
    }

    // 업데이트 아이템 가격 및 초당 생성하는 돈
    protected override void Update_price()
    {
        double tmp_upgrade_price_automation             = (Math.Log(upgrade_price) * 0.25f) / 2;
        Data_controller.instance.gold                    -= upgrade_price;
        Data_controller.instance.gold_per_sec_automation += (tmp_upgrade_price_automation * m_upgrade_multiplier);
        current_level                                   += m_upgrade_multiplier;
    }
}