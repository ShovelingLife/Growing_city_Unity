using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class Tab_button : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    Tab_group        m_tab_group;
    public Image     img_background;


    void Start()
    {
        m_tab_group = GameObject.FindObjectOfType<Tab_group>();
        img_background  = GetComponent<Image>();
        m_tab_group.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_tab_group.On_tab_selected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_tab_group.On_tab_enter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        m_tab_group.On_tab_exit(this);
    }
}