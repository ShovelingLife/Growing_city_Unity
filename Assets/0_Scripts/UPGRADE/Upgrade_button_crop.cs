using System;
using UnityEngine;

//Crop메뉴 업그레이드 클래스
public class Upgrade_button_crop : Upgrade_button_core
{
    // 업그레이드 레벨이 150을 초과하면 텍스트 변경 및 버튼 잠금
    protected override void Update()
    {
        m_current_gold_quantity = Data_controller.instance.gold_per_click_gold;
    }

    // 업그레이드 비용을 증가해주는 메소드
    protected override void Update_price()
    {
        double cost_pow_goldPerClick                = (Math.Log(upgrade_price) * 0.25f)/2;
        Data_controller.instance.gold                -= upgrade_price;
        Data_controller.instance.gold_per_click_gold += (cost_pow_goldPerClick*m_upgrade_multiplier);
        current_level                               += m_upgrade_multiplier;
    }
}