using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car_core : MonoBehaviour
{
    public NPC_manager npc_manager;
    // 시퀀스 관련
    protected Sequence m_first_car_sequence;
    protected Sequence m_second_car_sequence;
    // 자동차 관련
    [SerializeField]
    protected Vector3[] m_pos_arr = null;
    protected float m_first_car_timer;
    protected float m_second_car_timer;
    protected float m_first_car_wait_time;
    protected float m_second_car_wait_time;
    protected float m_car_speed;


    protected virtual void Start()
    {
        m_first_car_sequence  = DOTween.Sequence();
        m_second_car_sequence = DOTween.Sequence();
        m_pos_arr             = new Vector3[Global.car_waypoint_count];
    }

    // Initializing values
    protected virtual void Init_values()
    {

    }

    // 모든 오브젝트들을 비활성화
    protected void Inactivate_car_objects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
