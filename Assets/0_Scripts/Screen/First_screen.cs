using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class First_screen : MonoBehaviour
{
    private void Start()
    {
        Audio_manager.instance.Play_city_sound();
    }

    public void NextScene()
    {
        SceneManager.LoadScene("MainGame");
        Audio_manager.instance.Play_touch_sound();
    }
}