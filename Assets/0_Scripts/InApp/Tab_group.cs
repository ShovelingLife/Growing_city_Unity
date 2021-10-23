using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab_group : MonoBehaviour
{
           List<Tab_button> m_lst_tab_button = new List<Tab_button>();
    public List<GameObject> lst_object_to_swap;
    public Sprite           tab_idle;
    public Sprite           tab_hover;
    public Sprite           tab_active;
    public Tab_button       selected_tab;


    public void Subscribe(Tab_button button)
    {
        m_lst_tab_button.Add(button);
    }

    public void On_tab_enter(Tab_button button)
    {
        Reset_tabs();

        if (selected_tab == null || 
            button != selected_tab) 
            button.img_background.sprite = tab_hover;
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
        button.img_background.sprite = tab_active;
        int index = button.transform.GetSiblingIndex();

        for(int i = 0; i < lst_object_to_swap.Count; i++)
        {
            if (i == index) 
                lst_object_to_swap[i].SetActive(true);

            else 
                lst_object_to_swap[i].SetActive(false);
        }
    }

    public void Reset_tabs()
    {
        foreach(Tab_button button in m_lst_tab_button)
        {
            if (selected_tab == null && 
                button == selected_tab) 
                continue;

            button.img_background.sprite = tab_idle;
        }
    }
}