using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_local<T> : MonoBehaviour where T:MonoBehaviour
{
    static T _instance;

    public static T instance
    {
        get
        {
            GameObject singleton_obj = GameObject.FindObjectOfType<T>().gameObject;
            
            if (!singleton_obj) 
                _instance = singleton_obj.AddComponent<T>();

            else 
                _instance = singleton_obj.GetComponent<T>();

            return _instance;
        }
    }
}