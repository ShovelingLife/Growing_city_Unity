using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;

public class Time_manager : Singleton_local<Time_manager>
{
    public Save_manager save_manager;
    public DateTime    new_time;
    public DateTime    old_time;
    public TimeSpan    gold_reward_time;
    public double      sum_of_reward_time;
    float              remaining_booster_minutes;
    float              remaining_booster_seconds;
    bool               m_is_booster_stopped;
    public float       current_boostValue_gameplay;


    private void Awake()
    {
        new_time = DateTime.Now;
        old_time = DateTime.Parse(PlayerPrefs.GetString("timeData"));
        gold_reward_time = new_time - old_time;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("timeData", DateTime.Now.ToString());
    }

    // 부스터 값들을 설정해주는 함수
    public void Set_gameplay_booster_values(bool _is_stopped, float _currentBoostValue)
    {
        m_is_booster_stopped = _is_stopped;
        current_boostValue_gameplay += _currentBoostValue;
    }

    // 경과한 시간을 반환함 로그아웃 후 로그인
    public string Time_passed_login(TimeSpan _time)
    {
        return _time.Hours + ":" + _time.Minutes + ":" + _time.Seconds;
    }

    // 경과한 시간을 보상 double형으로 저장
    public void Get_sum_of_time_reward()
    {
        double time_hour    = Convert.ToDouble(gold_reward_time.Hours);
        double time_minutes = Convert.ToDouble(gold_reward_time.Minutes);
        double time_seconds = Convert.ToDouble(gold_reward_time.Seconds);
        sum_of_reward_time  = (time_hour * 360) + (time_minutes * 60) + time_seconds;
    }

    // 게임 부스터 적용해주는 값
    public string Gameplay_run_booster()
    {
        PlayerPrefs.SetInt("Booster_on", 1);

        if (!m_is_booster_stopped) 
            current_boostValue_gameplay -= Time.deltaTime;

        remaining_booster_minutes = Mathf.Floor(current_boostValue_gameplay / 60);
        remaining_booster_seconds = current_boostValue_gameplay % 60;

        if (remaining_booster_seconds > 59) 
            remaining_booster_seconds = 59;

        if (remaining_booster_minutes < 0)
        {
            m_is_booster_stopped = true;
            remaining_booster_minutes    = 0;
            remaining_booster_seconds    = 0;
            PlayerPrefs.SetInt("Booster_on", 0);
        }
        return string.Format("{0:0}:{1:00}", remaining_booster_minutes, remaining_booster_seconds);
    }
}