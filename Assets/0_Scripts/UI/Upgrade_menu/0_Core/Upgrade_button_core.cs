using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


// 각 업그레이드 항목을 담당하는 클래스
public class Upgrade_button_core : MonoBehaviour
{
    [SerializeField]
    protected Button m_upgrade_button; // 업그레이드 버튼
    [SerializeField]
    protected Text   m_txt_price;      // 현재 가격
    [SerializeField]
    protected Text   m_txt_level;      // 현재 레벨

    // 텍스트 번역
    protected string m_per_sec_translation;
    protected string m_level_text_translation;
    protected string m_upgrade_text_translation;

    // 업그레이드 비용 
    public    double upgrade_price;
    public    float  cost_pow = 0f;
    [HideInInspector]
    protected int    m_upgrade_multiplier = 1;
    public    int    current_level        = 1;
    protected int    m_max_level          = 0;
    public    int    id                   = 0;

    public int upgrade_multiplier_prop { set { m_upgrade_multiplier = value; } }


    protected virtual void Start()
    {
        m_max_level = Global.max_upgrade_level;
    }

    protected virtual void Update()
    {
        Set_translation_values();
        Show_all_text();
    }

    // 텍스트 변수 초기화
    protected void Init()
    {
        // 값 초기화
        m_upgrade_button = transform.GetChild(1).GetComponent<Button>();
        m_txt_price = transform.GetChild(2).GetComponent<Text>();
        m_txt_level = transform.GetChild(3).GetComponent<Text>();

        if (!m_txt_price) // 가격 텍스트가 널일 시
            m_txt_price = transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();

        if (!m_txt_level) // 레벨 텍스트가 널일 시
            m_txt_level = transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();


        // 업그레이드 버튼 설정
        m_upgrade_button.onClick.AddListener(Purchase_upgrade);
        m_upgrade_button.transform.GetChild(0).GetComponent<Text_property>().Try_parsing("21");

        Set_translation_values(); 
    }

    // 업그레이드 구매 레벨 250 이하
    public void Purchase_upgrade()
    {
        double before_value = upgrade_price;

        if ((current_level + m_upgrade_multiplier) <= m_max_level)
        {
            upgrade_price = Get_upgrade_value();

            if (Data_controller.instance.gold >= upgrade_price)
            {
                Update_price();
                m_txt_price.text = Large_number.ToString(upgrade_price).ToString();
            }
            else
            {
                Upgrade_message_manager.instance.Start_coroutine_insufficient_money();
                upgrade_price = before_value;
            }
        }
        else
            Upgrade_message_manager.instance.Start_coroutine_max_level();
    }

    // 번역해주는 변수들을 초기화
    protected void Set_translation_values()
    {
        Csv_loader_manager inst = Csv_loader_manager.instance;
        m_per_sec_translation   = inst[inst.Get_hash_code_by_str("PER_SECOND")].ToString();

        if (current_level < m_max_level)
            m_level_text_translation = inst[inst.Get_hash_code_by_str("LEVEL")].ToString() + " " + current_level + "/" + m_max_level.ToString();

        else
        {
            m_level_text_translation = inst[inst.Get_hash_code_by_str("MAX_LEVEL")].ToString();
            m_upgrade_button.enabled = false;
        }
    }

    // 텍스트를 표시해주는 함수
    void Show_all_text()
    {
        m_txt_level.text = m_level_text_translation;
        m_txt_price.text = Large_number.ToString(Get_upgrade_value()).ToString();
    }

    // Updating price
    protected virtual void Update_price()
    {

    }

    // 곱해준 값에서 되돌려줌
    double Get_upgrade_value()
    {
        if (current_level + m_upgrade_multiplier >= m_max_level)
        {
            m_upgrade_multiplier = 1;
            return (upgrade_price * m_upgrade_multiplier) * Mathf.Pow(cost_pow, current_level + m_upgrade_multiplier);
        }
        return (upgrade_price * m_upgrade_multiplier) * Mathf.Pow(cost_pow, current_level + m_upgrade_multiplier);
    }

}