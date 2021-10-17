using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_message_manager : Singleton_local<Upgrade_message_manager>
{
    GameObject cant_upgrade_object;
    Text txt_message;


    private void Awake()
    {
        cant_upgrade_object = transform.GetChild(0).gameObject;
        txt_message = cant_upgrade_object.transform.GetChild(0).GetComponent<Text>();
        cant_upgrade_object.SetActive(false);
    }

    //  구매 할 수 없다고 표시해주는 코루틴을 실행 시킴
    public void Start_coroutine_insufficient_money()
    {
        txt_message.text = Csv_loader_manager.instance[10].ToString();
        StartCoroutine("IE_Cant_upgrade_coroutine");
    }

    //  업그레이드 할 수 없다고 표시해주는 코루틴을 실행 시킴
    public void Start_coroutine_max_level()
    {
        txt_message.text = Csv_loader_manager.instance[13].ToString();
        StartCoroutine("Ie_Cant_upgrade_coroutine");
    }


    // 업그레이드 할 수 없다고 표시해주는 코루틴
    IEnumerator IE_Cant_upgrade_coroutine()
    {
        cant_upgrade_object.SetActive(true);
        yield return new WaitForSeconds(1f);
        cant_upgrade_object.SetActive(false);
    }
}
