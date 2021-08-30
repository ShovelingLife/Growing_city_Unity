using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_global<T> : MonoBehaviour where T : MonoBehaviour
{
    static T          m_instance;
    static GameObject singleton_obj;

    public static T instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();

                if (m_instance == null)
                {
                    singleton_obj = new GameObject();
                    singleton_obj.name = "(Singleton) " + typeof(T).ToString();
                    m_instance = singleton_obj.AddComponent<T>();
                    //Singleton_global<T> tt = _instance as Singleton_global<T>;
                    //tt._InitSetting();
                }
                else singleton_obj = m_instance.gameObject;

                DontDestroyOnLoad(singleton_obj);
            }

            return m_instance;
        }
    }
    //protected virtual void _InitSetting()
    //{

    //}
}