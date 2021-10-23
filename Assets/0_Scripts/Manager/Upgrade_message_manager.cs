using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_message_manager : Singleton_local<Upgrade_message_manager>
{
    GameObject m_cant_upgrade_obj;
    Text       m_txt_message;


    private void Awake()
    {
        m_cant_upgrade_obj = transform.GetChild(0).gameObject;
        m_txt_message      = m_cant_upgrade_obj.transform.GetChild(0).GetComponent<Text>();
        m_cant_upgrade_obj.SetActive(false);
    }

    //  구매 할 수 없다고 표시해주는 코루틴을 실행 시킴
    public void Start_coroutine_insufficient_money()
    {
        m_txt_message.text = Csv_loader_manager.instance[10].ToString();
        StartCoroutine("IE_Cant_upgrade_coroutine");
    }

    //  업그레이드 할 수 없다고 표시해주는 코루틴을 실행 시킴
    public void Start_coroutine_max_level()
    {
        m_txt_message.text = Csv_loader_manager.instance[13].ToString();
        StartCoroutine("Ie_Cant_upgrade_coroutine");
    }


    // 업그레이드 할 수 없다고 표시해주는 코루틴
    IEnumerator IE_Cant_upgrade_coroutine()
    {
        m_cant_upgrade_obj.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_cant_upgrade_obj.SetActive(false);
    }
}
