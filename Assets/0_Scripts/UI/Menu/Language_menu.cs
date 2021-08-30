using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language_menu : MonoBehaviour
{
    public GameObject language_menu;
    public GameObject mainUI_canvas;


    void Start()
    {
        language_menu.SetActive(false);
    }

    // 언어 변경 창 열기
    public void Open_language_menu()
    {
        Tutorial_manager.step = 2;
        Audio_manager.instance.Play_touch_sound();
        language_menu.SetActive(true);
    }

    // 언어 변경 창 닫기
    public void Close_language_menu()
    {
        Tutorial_manager.step = 3;
        Audio_manager.instance.Play_touch_sound();
        language_menu.SetActive(false);

        if (!PlayerPrefs.HasKey("FirstTime")) 
            Tutorial_manager.instance.transform.SetParent(mainUI_canvas.transform);
    }
}