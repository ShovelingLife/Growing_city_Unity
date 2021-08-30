using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 우편함 기능
public class Message_box_manager : Singleton_global<Message_box_manager> 
{
    //우편함 싱글톤
    public GameObject messageBox;
    public GameObject messageBoxPanel;


    private void Start() // 초깃값 우편함 닫힘
    {
        messageBox.SetActive(false);
        messageBoxPanel.SetActive(false);
    }
    public void openMessageBox() // 우편함 열림
    {
        messageBox.SetActive(true);
        messageBoxPanel.SetActive(true);
        //cropMenuBackground.SetActive(false);
        //cityMenuBackground.SetActive(false);
        //residentMenuBackground.SetActive(false);
        //automationMenuBackground.SetActive(false);
    }
    public void closeMessageBox() //우편함 닫힘
    {
        messageBox.SetActive(false);
        messageBoxPanel.SetActive(false);
        //cropMenuBackground.SetActive(true);
        //cityMenuBackground.SetActive(true);
        //residentMenuBackground.SetActive(true);
        //automationMenuBackground.SetActive(true);

    }
}
