using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab_group : MonoBehaviour
{
    public List<Tab_button> tab_button_list = new List<Tab_button>();
    public List<GameObject> object_to_swap_list;
    public Sprite           tab_idle;
    public Sprite           tab_hover;
    public Sprite           tab_active;
    public Tab_button       selected_tab;


    public void Subscribe(Tab_button button)
    {
        tab_button_list.Add(button);
    }

    public void On_tab_enter(Tab_button button)
    {
        Reset_tabs();

        if (selected_tab == null || 
            button != selected_tab) 
            button.background.sprite = tab_hover;
    }

    public void On_tab_exit(Tab_button button)
    {
        Reset_tabs();
    }

    public void On_tab_selected(Tab_button button)
    {
        Audio_manager.instance.Play_touch_sound();
        selected_tab = button;
        Reset_tabs();
        button.background.sprite = tab_active;
        int index = button.transform.GetSiblingIndex();

        for(int i = 0; i < object_to_swap_list.Count; i++)
        {
            if (i == index) 
                object_to_swap_list[i].SetActive(true);

            else 
                object_to_swap_list[i].SetActive(false);
        }
        Shop_alert_manager.instance.Stop_all_coroutines();
    }

    public void Reset_tabs()
    {
        foreach(Tab_button button in tab_button_list)
        {
            if (selected_tab == null && 
                button == selected_tab) 
                continue;

            button.background.sprite = tab_idle;
        }
    }
}