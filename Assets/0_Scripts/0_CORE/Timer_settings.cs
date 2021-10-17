using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Timer_settings
{
    // 타이머 관련
    public float wait_time;
    float        m_current_time = 0f;
    bool         m_is_finished = false;

    void Update()
    {
        Set_timer();
    }

    public void Set_timer()
    {
        m_current_time += Time.deltaTime;

        if (m_current_time > wait_time)
        {
            m_is_finished = true;
            m_current_time = 0f;
        }
    }

    public bool Is_timer_finished()
    {
        bool tmp_variable = m_is_finished;
        m_is_finished     = false;

        return tmp_variable;
    }
}