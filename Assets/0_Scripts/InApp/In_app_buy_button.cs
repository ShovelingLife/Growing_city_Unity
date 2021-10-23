using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class In_app_buy_button : MonoBehaviour
{
           In_app_manager     m_in_app_inst;
    public e_item_type        item_type;
    public Text               txt_price;
           string             txt_default;

    private void Start()
    {
        txt_default = txt_price.text;
        StartCoroutine(IE_load_price_routine());
        m_in_app_inst = In_app_manager.instance;
    }

    public void Purchase_cash()
    {
        Audio_manager.instance.Play_touch_sound();

        switch (item_type)
        {
            case e_item_type.DIAMONDS_50:  m_in_app_inst.Buy_50_diamonds();  break;
            case e_item_type.DIAMONDS_125: m_in_app_inst.Buy_125_diamonds(); break;
            case e_item_type.DIAMONDS_250: m_in_app_inst.Buy_250_diamonds(); break;
            case e_item_type.DIAMONDS_500: m_in_app_inst.Buy_500_diamonds(); break;
            case e_item_type.NO_ADS:       m_in_app_inst.Buy_no_ads();       break;
        }
    }

    IEnumerator IE_load_price_routine()
    {
        while (!m_in_app_inst.IsInitialized())
                yield return null;

        string loaded_price = "";

        switch (item_type)
        {
            case e_item_type.DIAMONDS_50:  loaded_price = m_in_app_inst.Get_produce_price_from_store(m_in_app_inst.DIAMONDS_50);  break;
            case e_item_type.DIAMONDS_125: loaded_price = m_in_app_inst.Get_produce_price_from_store(m_in_app_inst.DIAMONDS_125); break;
            case e_item_type.DIAMONDS_250: loaded_price = m_in_app_inst.Get_produce_price_from_store(m_in_app_inst.DIAMONDS_250); break;
            case e_item_type.DIAMONDS_500: loaded_price = m_in_app_inst.Get_produce_price_from_store(m_in_app_inst.DIAMONDS_500); break;
            case e_item_type.NO_ADS:       loaded_price = m_in_app_inst.Get_produce_price_from_store(m_in_app_inst.NO_ADS);       break;
        }
        txt_price.text = txt_default + " " + loaded_price;
    }
}