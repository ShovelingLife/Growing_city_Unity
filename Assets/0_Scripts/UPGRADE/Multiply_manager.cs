using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Multiply_manager : Singleton_local<Multiply_manager>
{
    // ------- Crop menu variables -------
    Upgrade_button_crop[] crop_upgrade_menu_arr;
    public Button[]       crop_multiplier_button_arr = new Button[3];

    // ------- City menu variables -------
    Upgrade_button_city[] city_upgrade_menu_arr;
    public Button[]       city_multiplier_button_arr = new Button[3];

    // ------- Resident menu variables -------
    Upgrade_button_resident[] resident_upgrade_menu_arr;
    public Button[]           resident_multiplier_button_arr = new Button[3];

    // ------- Automation menu variables -------
    Upgrade_button_automation[] automation_upgrade_menu_arr;
    public Button[]             automation_multiplier_button_arr = new Button[3];

    // ANOTHER VARIABLES
    public e_multiplier_quantity current_multiplier;
    public Sprite                selected_button_orange_sprite;
    public Sprite                unselected_button_orange_sprite;
    public Sprite                selected_button_blue_sprite;
    public Sprite                unselected_button_blue_sprite;


    private void Awake()
    {
        Init_menu_objects_array();
    }
    private void Start()
    {
        Reset_button_when_closed();
    }

    private void Update()
    {
        Select_crop_multiplier_button();
        Select_city_multiplier_button();
        Select_resident_multiplier_button();
        Select_automation_multiplier_button();
    }

    // 게임 오브젝트 배열 초기화
    void Init_menu_objects_array()
    {
        crop_upgrade_menu_arr       = Resources.FindObjectsOfTypeAll<Upgrade_button_crop>();
        city_upgrade_menu_arr       = Resources.FindObjectsOfTypeAll<Upgrade_button_city>();
        resident_upgrade_menu_arr   = Resources.FindObjectsOfTypeAll<Upgrade_button_resident>();
        automation_upgrade_menu_arr = Resources.FindObjectsOfTypeAll<Upgrade_button_automation>();
        crop_upgrade_menu_arr.OrderBy(x => x.name);
        city_upgrade_menu_arr.OrderBy(x => x.name);
        resident_upgrade_menu_arr.OrderBy(x => x.name);
        automation_upgrade_menu_arr.OrderBy(x => x.name);
    }

    // 메뉴 닫을 시 버튼 초기화
    public void Reset_button_when_closed()
    {
        current_multiplier = e_multiplier_quantity.multiplier_per_one;
        Update_crop_price_multiplier();
        Update_city_price_multiplier();
        Update_resident_price_multiplier();
        Update_automation_price_multiplier();
    }

    // Upgrade button for Crop menu
    void Update_crop_price_multiplier()
    {
        for (int i = 0; i < 3; i++)
             crop_multiplier_button_arr[i].image.sprite = unselected_button_orange_sprite;

        switch (current_multiplier)
        {
            case e_multiplier_quantity.multiplier_per_one:   crop_multiplier_button_arr[0].image.sprite = selected_button_orange_sprite; break;

            case e_multiplier_quantity.multiplier_per_ten:   crop_multiplier_button_arr[1].image.sprite = selected_button_orange_sprite; break;

            case e_multiplier_quantity.multiplier_per_fifty: crop_multiplier_button_arr[2].image.sprite = selected_button_orange_sprite; break;
        }
    }

    // Upgrade button for City menu
    void Update_city_price_multiplier()
    {
        for (int i = 0; i < 3; i++)
             city_multiplier_button_arr[i].image.sprite = unselected_button_orange_sprite;

        switch (current_multiplier)
        {
            case e_multiplier_quantity.multiplier_per_one:   city_multiplier_button_arr[0].image.sprite = selected_button_orange_sprite; break;

            case e_multiplier_quantity.multiplier_per_ten:   city_multiplier_button_arr[1].image.sprite = selected_button_orange_sprite; break;

            case e_multiplier_quantity.multiplier_per_fifty: city_multiplier_button_arr[2].image.sprite = selected_button_orange_sprite; break;
        }
    }

    // Upgrade button for Resident menu
    void Update_resident_price_multiplier()
    {
        for (int i = 0; i < 3; i++)
             resident_multiplier_button_arr[i].image.sprite = unselected_button_blue_sprite;

        switch (current_multiplier)
        {
            case e_multiplier_quantity.multiplier_per_one:   resident_multiplier_button_arr[0].image.sprite = selected_button_blue_sprite; break;

            case e_multiplier_quantity.multiplier_per_ten:   resident_multiplier_button_arr[1].image.sprite = selected_button_blue_sprite; break;

            case e_multiplier_quantity.multiplier_per_fifty: resident_multiplier_button_arr[2].image.sprite = selected_button_blue_sprite; break;
        }
    }

    // Upgrade button for Automation menu
    void Update_automation_price_multiplier()
    {
        for (int i = 0; i < 3; i++)
             automation_multiplier_button_arr[i].image.sprite = unselected_button_blue_sprite;

        switch (current_multiplier)
        {
            case e_multiplier_quantity.multiplier_per_one:   automation_multiplier_button_arr[0].image.sprite = selected_button_blue_sprite; break;

            case e_multiplier_quantity.multiplier_per_ten:   automation_multiplier_button_arr[1].image.sprite = selected_button_blue_sprite; break;

            case e_multiplier_quantity.multiplier_per_fifty: automation_multiplier_button_arr[2].image.sprite = selected_button_blue_sprite; break;
        }
    }

    // Crop 메뉴 업그레이드 비용 곱해주는 버튼 종류
    void Select_crop_multiplier_button()
    {
        foreach (var item in crop_upgrade_menu_arr)
                 item.Upgrade_multiplier(current_multiplier);
    }

    // City 메뉴 업그레이드 비용 곱해주는 버튼 종류
    void Select_city_multiplier_button()
    {
        foreach (var item in city_upgrade_menu_arr)
                 item.Upgrade_multiplier(current_multiplier);
    }

    // Resident 메뉴 업그레이드 비용 곱해주는 버튼 종류
    void Select_resident_multiplier_button()
    {
        foreach (var item in resident_upgrade_menu_arr)
                 item.Upgrade_multiplier(current_multiplier);
    }

    // Automation 메뉴 업그레이드 비용 곱해주는 버튼 종류
    void Select_automation_multiplier_button()
    {
        foreach (var item in automation_upgrade_menu_arr)
                 item.Upgrade_multiplier(current_multiplier);
    }
}