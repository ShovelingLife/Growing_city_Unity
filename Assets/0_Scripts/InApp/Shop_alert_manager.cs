using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_alert_manager : Singleton_local<Shop_alert_manager>
{
    public GameObject not_enough_cash_txt_obj;
    public GameObject purchase_complete_txt_obj;
    public Text[]     alert_message_txt_arr = new Text[2];


    private void Awake()
    {
        not_enough_cash_txt_obj.SetActive(false);
        purchase_complete_txt_obj.SetActive(false);
    }

    // 돈이 부족하다고 표시해주는 코루틴 실행
    public void Run_not_enough_money_alert()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                alert_message_txt_arr[1].text = "Not enough cash.";
                break;

            case "Spanish":
                alert_message_txt_arr[1].text = "Diamante insuficiente.";
                break;

            case "Korean":
                alert_message_txt_arr[1].text = "보석이 부족합니다.";
                break;
        }
        StartCoroutine("IE_Not_enough_cash");
    }

    // 구매 완료했다고 표시해주는 코루틴 실행
    public void Run_thanks_for_buying_alert()
    {
        switch (Lean.Localization.LeanLocalization.CurrentLanguage)
        {
            case "English":
                alert_message_txt_arr[0].text = "Thanks for buying!";
                break;

            case "Spanish":
                alert_message_txt_arr[0].text = "Gracias por comprar!";
                break;

            case "Korean":
                alert_message_txt_arr[0].text = "구매해주셔서 감사합니다!";
                break;
        }
        StartCoroutine("IE_Purchase_complete");
    }

    // 모든 코루틴을 정지
    public void Stop_all_coroutines()
    {
        not_enough_cash_txt_obj.SetActive(false);
        purchase_complete_txt_obj.SetActive(false);
        StopAllCoroutines();
    }

    // 돈이 부족해서 경고하는 코루틴
    IEnumerator IE_not_enough_cash()
    {
        not_enough_cash_txt_obj.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        not_enough_cash_txt_obj.SetActive(false);
    }

    // 구매 완료 코루틴
    IEnumerator IE_purchase_complete()
    {
        purchase_complete_txt_obj.SetActive(true);
        yield return new WaitForSeconds(1f);
        purchase_complete_txt_obj.SetActive(false);
    }
}