using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_button_core : MonoBehaviour
{
    [HideInInspector]
    public Button            upgrade_button;
    public Text              price_txt;
    public Text              level_txt;
    public Text              coin_per_sec_txt;
    public Text              upgrade_txt;
    protected string         m_translation_per_sec;
    protected string         m_level_text_translation;
    protected string         m_upgrade_text_translation;
    public double            current_per_second_quantity;
    public double            upgrade_price;
    protected double         m_current_gold_quantity;
    protected readonly float m_cost_pow = 1.075f;
    [HideInInspector]
    public int               current_level = 1;
    protected int            m_upgrade_multiplier = 1;


    protected virtual void Start()
    {
        Init_price_values();
        Set_translation_values();
    }

    protected virtual void Update()
    {
        Show_all_text();
        Set_translation_values();
    }

    // 업그레이드 구매 레벨 250 이하
    public void Purchase_upgrade_city()
    {
        if ((current_level + m_upgrade_multiplier) <= Global.max_upgrade_level)
        {
            if (Data_controller.instance.gold >= upgrade_price)
            {
                Update_price();
                price_txt.text = Large_number.ToString(upgrade_price).ToString();
            }
            else
                UpgradeMessageManager.instance.Start_coroutine_cant_purchase();
        }
        else
            UpgradeMessageManager.instance.Start_coroutine_cant_upgrade();
    }

    // Values initialization
    protected void Init_price_values()
    {
        upgrade_price  = current_per_second_quantity;
        price_txt.text = Large_number.ToString(upgrade_price).ToString();

        if (File.Exists(Application.persistentDataPath + "/building_menu_data.json"))
            Save_manager.instance.Load_building_menu_data();
    }

    // 번역해주는 변수들을 초기화
    protected void Set_translation_values()
    {
        m_translation_per_sec      = UI_translation.Translate_perSec_text();
        m_upgrade_text_translation = UI_translation.Translate_upgrade_text();

        if (current_level < Global.max_upgrade_level)
            m_level_text_translation = UI_translation.Translate_level_text(current_level) + " " + current_level + Global.max_upgrade_level.ToString();

        else
        {
            m_level_text_translation = UI_translation.Translate_level_text(current_level);
            upgrade_button.enabled = false;
        }
    }

    // A function that returns all upgradable objects
    public virtual Upgrade_button_core[] Get_all_objects()
    {
        return null;
    }

    // 텍스트를 표시해주는 함수
    protected void Show_all_text()
    {
        coin_per_sec_txt.text = m_translation_per_sec + " " + Large_number.ToString(m_current_gold_quantity).ToString();
        level_txt.text        = m_level_text_translation;
        upgrade_txt.text      = m_upgrade_text_translation;
        price_txt.text        = Large_number.ToString(upgrade_price).ToString();
    }

    // Updating price
    protected virtual void Update_price()
    {

    }

    // 업그레이드 비용 곱해주는 함수 레벨 250 찍을 시 버튼 비활성화
    public void Upgrade_multiplier(e_multiplier_quantity _quantity)
    {
        double sum_price = 0;
        int    value = 0;

        switch (_quantity)
        {
            case e_multiplier_quantity.multiplier_per_one:   value = 1;  break;

            case e_multiplier_quantity.multiplier_per_ten:   value = 10; break;

            case e_multiplier_quantity.multiplier_per_fifty: value = 50; break;
        }
        m_upgrade_multiplier = value;

        for (int i = 0; i < value; i++)
             sum_price += current_per_second_quantity * Mathf.Pow(m_cost_pow, current_level + i);

        upgrade_price  = sum_price;
        price_txt.text = Large_number.ToString(upgrade_price * value).ToString();

        if (current_level == Global.max_upgrade_level)
            Destroy(price_txt);
    }
}