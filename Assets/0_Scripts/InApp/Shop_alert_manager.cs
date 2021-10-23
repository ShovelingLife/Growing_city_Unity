using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_alert_manager : Singleton_local<Shop_alert_manager>
{
    // 구매하기 위한 보석 부족
    GameObject m_not_enough_cash_obj;
    Text       m_txt_not_enough_cash;

    // 보석 구매 완료
    GameObject m_purchase_complete_obj;
    Text       m_txt_purchase_complete;


    private void Start()
    {
        m_not_enough_cash_obj = transform.GetChild(0).gameObject;
        m_txt_not_enough_cash = m_not_enough_cash_obj.transform.GetChild(1).GetComponent<Text>();

        m_purchase_complete_obj = transform.GetChild(1).gameObject;
        m_txt_purchase_complete = m_purchase_complete_obj.transform.GetChild(1).GetComponent<Text>();

        m_not_enough_cash_obj.SetActive(false);
        m_purchase_complete_obj.SetActive(false);
    }

    // 다 꺼둠
    public void Turn_off()
    {
        m_not_enough_cash_obj.SetActive(false);
        m_purchase_complete_obj.SetActive(false);
    }

    // 돈이 부족하다고 표시해주는 코루틴 실행
    public void Run_not_enough_cash_alert()
    {
        m_txt_not_enough_cash.text = Csv_loader_manager.instance[Csv_loader_manager.instance.Get_hash_code_by_str("INSUFFICIENT_MONEY")].ToString();
        StartCoroutine(IE_not_enough_cash());
    }

    // 구매 완료했다고 표시해주는 코루틴 실행
    public void Run_thanks_for_buying_alert()
    {
        m_txt_not_enough_cash.text = Csv_loader_manager.instance[Csv_loader_manager.instance.Get_hash_code_by_str("THANKS_FOR_PURCHASE")].ToString();
        StartCoroutine(IE_purchase_complete());
    }

    // 돈이 부족해서 경고하는 코루틴
    IEnumerator IE_not_enough_cash()
    {
        m_not_enough_cash_obj.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        m_not_enough_cash_obj.SetActive(false);
    }

    // 구매 완료 코루틴
    IEnumerator IE_purchase_complete()
    {
        m_purchase_complete_obj.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_purchase_complete_obj.SetActive(false);
    }
}