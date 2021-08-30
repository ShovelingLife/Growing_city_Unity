using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class XmasGirlLoop : MonoBehaviour
{
    public NPC_manager npc_manager;
    static Sequence xmas_girl_sequence;
    static Transform obj_transform;
    static Animator girl_move_animation;
    SpriteRenderer xmas_girl_sprite;
    Action<e_character_move_type, string, Vector3> character_move_action = new Action<e_character_move_type, string, Vector3>(Character_move_direction);
    [SerializeField]
    Vector3[] pos_arr = null;
    float time = 0f;
    public float timer = 4f;
    int index = 4;
    [SerializeField]
    int list_size = 0;

    void Start()
    {
        Init();
    }
    void Update()
    {
        Character_move();
    }

    // 초기화
    void Init()
    {
        // 리스트 0~4번 index 5개
        // 위치 배열 0~4 가는길, 5~7 돌아오는 길
        xmas_girl_sequence = DOTween.Sequence();
        obj_transform = GetComponent<Transform>();
        girl_move_animation = GetComponent<Animator>();
        xmas_girl_sprite = GetComponent<SpriteRenderer>();
        list_size = npc_manager.xmas_girl_pos_list.Count;
        pos_arr = new Vector3[list_size+3];

        for (int i = 0; i < list_size; i++) // 0~4 기본 방향
        {
            pos_arr[i] = npc_manager.xmas_girl_pos_list[i]; 
        }
        // 5~7 반대 방향
        pos_arr[5] = pos_arr[4];
        pos_arr[6] = pos_arr[2];
        pos_arr[7] = pos_arr[1];
        pos_arr[5].y *= 2;
        pos_arr[6].x += 0.01f;
        pos_arr[6].y = pos_arr[5].y;
        pos_arr[7].x += 0.01f;
    }
    
    // 캐릭터 움직임
    void Character_move()
    {
        Vector3 current_pos = transform.position;
        Color xmas_girl_color = xmas_girl_sprite.color;

        if (current_pos == pos_arr[0]) // 대기 후 오른쪽 이동
        {
            time += Time.deltaTime;
            Reset_animation_tree_parameter();
            if (time > timer)
            {
                time = 0f;
                character_move_action(e_character_move_type.RIGHT, "posX", pos_arr[1]);
            }
        }
        if (current_pos == pos_arr[1]) character_move_action(e_character_move_type.DOWN, "posY", pos_arr[2]); // 아래쪽 이동
        if (current_pos == pos_arr[2]) character_move_action(e_character_move_type.LEFT, "posX", pos_arr[3]); // 왼쪽 이동
        if (current_pos == pos_arr[3]) character_move_action(e_character_move_type.UP, "posY", pos_arr[4]); // 윗쪽 이동
        if (current_pos == pos_arr[4]) // 종착지점 도착 후 임의의 위치로 이동 후 대기
        {
            xmas_girl_color.a = 0;
            xmas_girl_sprite.color = xmas_girl_color;
            Reset_animation_tree_parameter();
            time += Time.deltaTime;
            if (time > timer)
            {
                time = 0f;
                xmas_girl_color.a = 255;
                xmas_girl_sprite.color = xmas_girl_color;
                character_move_action(e_character_move_type.DOWN, "posY", pos_arr[5]);
            }
        }
        if (current_pos == pos_arr[5]) character_move_action(e_character_move_type.RIGHT, "posX", pos_arr[6]); // 오른쪽로 이동
        if (current_pos == pos_arr[6]) character_move_action(e_character_move_type.UP, "posY", pos_arr[7]); // 위쪽으로 이동
        if (current_pos == pos_arr[7]) character_move_action(e_character_move_type.LEFT, "posX", pos_arr[0]); // 왼쪽으로 이동
    }

    // 애니메이션 트리 초기화
    void Reset_animation_tree_parameter()
    {
        girl_move_animation.SetFloat("posX", 0f);
        girl_move_animation.SetFloat("posY", 0f);
    }

    // 캐릭터 움직이는 종류 Action 대리자
    static void Character_move_direction(e_character_move_type _character_move_type, string _key, Vector3 _pos)
    {
        float xmas_girl_move_type = (float)_character_move_type;
        float move_speed = 10f;

        // 초기화
        girl_move_animation.SetFloat("posX", 0f);
        girl_move_animation.SetFloat("posY", 0f);
        // 실행
        girl_move_animation.SetFloat(_key, xmas_girl_move_type);
        xmas_girl_sequence.Append(obj_transform.DOMove(_pos, move_speed, false));
    }
}