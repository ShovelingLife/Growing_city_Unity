using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Green_car : Car_core
{
    protected override void Start()
    {
        Inactivate_car_objects();
        Init_values();
        //tween_arr[0] = this.transform.DOMove(NPC_manager.instance.green_car_pos_list[1], car_speed, false);
    }

    private void Update()
    {
        Car_loop();
    }

    // 시퀀스 초기화
    protected override void Init_values()
    {
        m_first_car_timer      = 0f;
        m_second_car_timer     = 0f;
        m_first_car_wait_time  = 5f;
        m_second_car_wait_time = 7.5f;
        m_car_speed            = 4f;

        for (int i = 0; i < Global.car_waypoint_count; i++)
        {
            m_pos_arr[i] = npc_manager.green_car_pos_list[i];
        }
    }

    // 자동차 무한반복 코루틴
    void Car_loop()
    {
        Vector3 car_pos;
        // 첫번째 자동차
        if (this.gameObject.name == "Green_Car_One") // 0 , 1 , 2
        {
            car_pos = this.gameObject.transform.position;
            if (car_pos == m_pos_arr[0]) // 아래
            {
                Change_car_obj(e_move_type.DOWN);
                m_first_car_sequence.Append(transform.DOMove(m_pos_arr[1], m_car_speed, false));
            }
            if (car_pos == m_pos_arr[1]) // 왼쪽
            {
                float tmp_timer = 1f;
                Change_car_obj(e_move_type.LEFT);
                m_first_car_timer += Time.deltaTime;

                if (m_first_car_timer > tmp_timer)
                {
                    m_first_car_timer = 0f;
                    m_first_car_sequence.Append(transform.DOMove(m_pos_arr[2], m_car_speed, false));
                }
            }
            if (car_pos == m_pos_arr[2])  // 대기
            {
                m_first_car_timer += Time.deltaTime;

                if (m_first_car_timer > m_first_car_wait_time)
                {
                    m_first_car_timer  = 0f;
                    transform.position = m_pos_arr[0];
                }
            }
        }
        // 두번째 자동차
        if (this.gameObject.name == "Green_Car_Two") // 3 , 4 , 5 , 6
        {
            car_pos = this.gameObject.transform.position;
            if (car_pos == m_pos_arr[3]) // 오른쪽
            {
                Change_car_obj(e_move_type.RIGHT);
                m_second_car_sequence.Append(transform.DOMove(m_pos_arr[4], m_car_speed, false));
            }
            if (car_pos == m_pos_arr[4]) // 아래
            {
                Change_car_obj(e_move_type.DOWN);
                m_second_car_sequence.Append(transform.DOMove(m_pos_arr[5], m_car_speed, false));
            }
            if (car_pos == m_pos_arr[5]) // 오른쪽
            {
                Change_car_obj(e_move_type.RIGHT);
                m_second_car_sequence.Append(transform.DOMove(m_pos_arr[6], m_car_speed, false));
            }
            if (car_pos == m_pos_arr[6]) // 대기
            {
                m_second_car_timer += Time.deltaTime;

                if (m_second_car_timer > m_second_car_wait_time)
                {
                    m_second_car_timer = 0f;
                    transform.position = m_pos_arr[3];
                }
            }
        }
    }

    // 첫번째 차를 움직여주는 함수
    void Change_car_obj(e_move_type _car_move_type)
    {
        // LEFT = 0
        // RIGHT = 1
        // UP = 2
        // DOWN = 3
        Inactivate_car_objects();
        transform.GetChild((int)(_car_move_type)).gameObject.SetActive(true);
    }
}