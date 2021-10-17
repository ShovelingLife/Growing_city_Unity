using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scroll_menu : MonoBehaviour
{
    public GameObject scroll_menu_obj;
    float                    m_original_pos_y = -1200f;
    float                    m_new_pos_y      = -525f;
    float                    m_time           = 1f;
    public bool              is_closed        = true;
    public int               index;
    public int               hash_code;


    public void Init_obj()
    {
        scroll_menu_obj     = transform.GetChild(1).gameObject;
    }

    // 스크롤을 올림
    public void Scrolling_menu_up()
    {
        transform.DOLocalMoveY(m_new_pos_y, m_time);
        is_closed = false;
    }

    // 스크롤을 내림
    public void Scrolling_menu_down()
    {
        transform.DOLocalMoveY(m_original_pos_y, m_time);
        Upgrade_menu_manager.instance.Set_scroll_at_original_pos(index);
        is_closed = true;
    }

    // 내릴지 올릴지 결정
    public void Check_scrolling_direction()
    {
        if (is_closed)
            Scrolling_menu_up();

        else
            Scrolling_menu_down();
    }
}
