﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_shake : MonoBehaviour
{
    Transform m_target;
    Vector3   m_initial_pos;
    float     m_pending_shake_duration = 0f;
    bool      m_is_shaking             = false;


    void Start()
    {
        m_target = GetComponent<Transform>();
        m_initial_pos = m_target.localPosition;
    }

    private void Update()
    {
        if (m_pending_shake_duration > 0 && !m_is_shaking)
            StartCoroutine(IE_shake());
    }
    
    // 화면을 흔들어주는 코루틴
    IEnumerator IE_shake()
    {
        m_is_shaking  = true;
        var startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < startTime + m_pending_shake_duration)
        {
            var randomPoint        = new Vector3(Random.Range(-0.10f, 0.10f), Random.Range(0.10f, -0.10f), m_initial_pos.z);
            m_target.localPosition = randomPoint;
            yield return null;
        }
        m_pending_shake_duration = 0f;
        m_target.localPosition   = m_initial_pos;
        m_is_shaking             = false;
    }

    // 화면을 흔듬
    public void Shake(float _duration)
    {
        if (_duration > 0)
            m_pending_shake_duration += _duration;
    }
}