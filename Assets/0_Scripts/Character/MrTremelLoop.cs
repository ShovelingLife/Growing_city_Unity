using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MrTremelLoop : MonoBehaviour
{
    public NPC_manager npc_manager;
    static Sequence mr_tremel_sequence;
    static Transform obj_transform;
    static Animator mr_tremel_move_animation;
    SpriteRenderer mr_tremel_sprite;
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
        mr_tremel_sequence = DOTween.Sequence();
        obj_transform = GetComponent<Transform>();
        mr_tremel_move_animation = GetComponent<Animator>();
        mr_tremel_sprite = GetComponent<SpriteRenderer>();
        list_size = npc_manager.mr_tremel_pos_list.Count;
        pos_arr = new Vector3[list_size + 3];

        for (int i = 0; i < list_size; i++) // 0~4 기본 방향
        {
            pos_arr[i] = npc_manager.mr_tremel_pos_list[i];
        }
    }

    // 캐릭터 움직임
    void Character_move()
    {
        Vector3 current_pos = transform.position;
        Color mr_tremel_color = mr_tremel_sprite.color;

        if (current_pos == pos_arr[0]) // 대기 후 왼쪽 이동
        {
            time += Time.deltaTime;
            Reset_animation_tree_parameter();
            if (time > timer)
            {
                time = 0f;
                character_move_action(e_character_move_type.LEFT, "posX", pos_arr[1]);
            }
        }
        if (current_pos == pos_arr[1]) character_move_action(e_character_move_type.UP, "posY", pos_arr[2]); // 위쪽 이동
        if (current_pos == pos_arr[2]) character_move_action(e_character_move_type.RIGHT, "posX", pos_arr[3]); // 왼쪽 이동
        if (current_pos == pos_arr[3]) transform.position = pos_arr[0]; // 도착 지점 이동 후 시작 지점으로
    }

    // 애니메이션 트리 초기화
    void Reset_animation_tree_parameter()
    {
        mr_tremel_move_animation.SetFloat("posX", 0f);
        mr_tremel_move_animation.SetFloat("posY", 0f);
    }

    // 캐릭터 움직이는 종류 Action 대리자
    static void Character_move_direction(e_character_move_type _character_move_type, string _key, Vector3 _pos)
    {
        float xmas_girl_move_type = (float)_character_move_type;
        float move_speed = 10f;

        // 초기화
        mr_tremel_move_animation.SetFloat("posX", 0f);
        mr_tremel_move_animation.SetFloat("posY", 0f);
        // 실행
        mr_tremel_move_animation.SetFloat(_key, xmas_girl_move_type);
        mr_tremel_sequence.Append(obj_transform.DOMove(_pos, move_speed, false));
    }
}
