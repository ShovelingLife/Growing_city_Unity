using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building_upgrade : Upgrade_button_core
{
    // 업데이트 아이템 가격 및 초당 생성하는 돈
    protected override void Update_price()
    {
        //double tmp_upgrade_price_building = price / 10;
        Data_controller.instance.gold                  -= upgrade_price;
        Data_controller.instance.gold_per_sec_building += upgrade_price;
        current_level++;
        upgrade_price *= 1.5f;
    }
}