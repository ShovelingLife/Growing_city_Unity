using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMessageManager : MonoBehaviour
{
    static UpgradeMessageManager _instance;
    public static UpgradeMessageManager instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject cant_upgrade_object;
    //public GameObject max_upgrade_object;
    public Text txt_message;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;

        else
            Destroy(this.gameObject);

        cant_upgrade_object.SetActive(false);
    }

    //  구매 할 수 없다고 표시해주는 코루틴을 실행 시킴
    public void Start_coroutine_cant_purchase()
    {
        if(Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish") 
            txt_message.text = "No se puede comprar por falta de dinero.";

        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English") 
            txt_message.text = "Can't purchase insufficient gold.";

        if (Lean.Localization.LeanLocalization.CurrentLanguage == "Korean") 
            txt_message.text = "돈이 부족해서 구매를 할 수가 없습니다.";

        StartCoroutine("Ie_Cant_upgrade_coroutine");
    }

    //  업그레이드 할 수 없다고 표시해주는 코루틴을 실행 시킴
    public void Start_coroutine_cant_upgrade()
    {
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish") 
            txt_message.text = "No se puede comprar por limite de nivel.";

        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English") 
            txt_message.text = "Can't purchase due it's limit.";

        if (Lean.Localization.LeanLocalization.CurrentLanguage == "Korean") 
            txt_message.text = "최대 단계를 넘어서서 구매를 할 수가 없습니다.";

        StartCoroutine("Ie_Cant_upgrade_coroutine");
    }


    // 업그레이드 할 수 없다고 표시해주는 코루틴
    IEnumerator Ie_Cant_upgrade_coroutine()
    {
        cant_upgrade_object.SetActive(true);
        yield return new WaitForSeconds(1f);
        cant_upgrade_object.SetActive(false);
    }
}
