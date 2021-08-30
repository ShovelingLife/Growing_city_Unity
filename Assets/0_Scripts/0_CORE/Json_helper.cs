using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Json_helper
{
    [Serializable]
    private class Wrapper<T>
    {
        public T[] items_arr;
    }

    public static T[] FromJson<T>(string _json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(_json);
        return wrapper.items_arr;
    }

    public static string ToJson<T>(T[] _array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items_arr = _array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] _array, bool _prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items_arr = _array;
        return JsonUtility.ToJson(wrapper, _prettyPrint);
    }
}