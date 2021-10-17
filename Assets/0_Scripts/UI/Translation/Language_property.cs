using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 현재 언어를 관리하는 클래스
public class Language_property : MonoBehaviour
{
    public string language;
    Button        m_current_button;


    void Start()
    {
        m_current_button = GetComponent<Button>();
        m_current_button.onClick.AddListener(Update_language);
    }

    // 언어를 바꿈
    void Update_language()
    {
        Csv_loader_manager.instance.current_language_prop = language;
    }
}
