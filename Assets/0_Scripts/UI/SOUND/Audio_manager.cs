using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_manager : Singleton_local<Audio_manager>
{
    public AudioSource game_audio_source;
    public AudioSource effect_audio_source;
    public AudioClip   city_sound;
    public AudioClip   touch_sound;
    public AudioClip   coin_sound;


    private void Start()
    {
        Init_sound();
    }

    // 사운드를 초기화해주는 함수
    void Init_sound()
    {
        if      (PlayerPrefs.GetInt("Sound") == 0) 
                 Set_sound_off();

        else if (PlayerPrefs.GetInt("Sound") == 1) 
                 Set_sound_on();
    }

    // 소리를 켜주는 함수
    public void Set_sound_on()
    {
        game_audio_source.volume   = 1f;
        effect_audio_source.volume = 1f;
    }

    // 소리를 꺼주는 함수
    public void Set_sound_off()
    {
        game_audio_source.volume   = 0f;
        effect_audio_source.volume = 0f;
    }

    // 터치 소리를 켜줌
    public void Play_touch_sound()
    {
        effect_audio_source.PlayOneShot(touch_sound);
    }

    // 동전 소리를 켜줌
    public void Play_coin_sound()
    {
        effect_audio_source.PlayOneShot(coin_sound);
    }

    // 게임 사운드 틀어주는 함수
    public void Play_city_sound()
    {
        game_audio_source.clip = city_sound;
        game_audio_source.Play();
    }
}