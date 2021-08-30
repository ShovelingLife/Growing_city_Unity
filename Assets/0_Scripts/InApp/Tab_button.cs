using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class Tab_button : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    public Tab_group tab_group;
    public Image     background;


    void Start()
    {
        background = GetComponent<Image>();
        tab_group.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tab_group.On_tab_selected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tab_group.On_tab_enter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tab_group.On_tab_exit(this);
    }
}