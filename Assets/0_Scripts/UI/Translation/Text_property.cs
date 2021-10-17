using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_property : MonoBehaviour
{
    Text          m_current_txt;
    public string hash_table_key;
    int           m_hash_table_int_key = 0;


    void Start()
    {
        m_current_txt        = GetComponent<Text>();
    }

    public void Try_parsing(string _key)
    {
        Int32.TryParse(_key, out m_hash_table_int_key);
    }

    void Update()
    {
        Csv_loader_manager inst = Csv_loader_manager.instance;

        if (m_hash_table_int_key > 0) // 해쉬코드가 숫자면
            m_current_txt.text = inst[m_hash_table_int_key].ToString();

        else
        {
            if (hash_table_key != "")
                m_current_txt.text = inst[inst.Get_hash_code_by_str(hash_table_key.ToUpper())].ToString();
        }
    }
}