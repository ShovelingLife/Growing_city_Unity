using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class In_app_buy_button : MonoBehaviour
{
    public enum e_item_type
    {
        DIAMONDS_50,
        DIAMONDS_125,
        DIAMONDS_250,
        DIAMONDS_500,
        NO_ADS
    }
    public e_item_type itemType;
    public Text        priceText;
    string             defaultText;

    private void Start()
    {
        defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }
    public void Purchase_cash()
    {
        Audio_manager.instance.Play_touch_sound();

        switch (itemType)
        {
            case e_item_type.DIAMONDS_50:
                In_app_manager.instance.Buy50Diamonds();
                break;

            case e_item_type.DIAMONDS_125:
                In_app_manager.instance.Buy125Diamonds();
                break;

            case e_item_type.DIAMONDS_250:
                In_app_manager.instance.Buy250Diamonds();
                break;

            case e_item_type.DIAMONDS_500:
                In_app_manager.instance.Buy500Diamonds();
                break;

            case e_item_type.NO_ADS:
                In_app_manager.instance.BuyNoAds();
                break;
        }
    }
    IEnumerator LoadPriceRoutine()
    {
        while (!In_app_manager.instance.IsInitialized())
            yield return null;

        string loadedPrice = "";

        switch (itemType)
        {
            case e_item_type.DIAMONDS_50:
                loadedPrice = In_app_manager.instance.getProducePriceFromStore(In_app_manager.instance.DIAMONDS_50);
                break;

            case e_item_type.DIAMONDS_125:
                loadedPrice = In_app_manager.instance.getProducePriceFromStore(In_app_manager.instance.DIAMONDS_125);
                break;

            case e_item_type.DIAMONDS_250:
                loadedPrice = In_app_manager.instance.getProducePriceFromStore(In_app_manager.instance.DIAMONDS_250);
                break;

            case e_item_type.DIAMONDS_500:
                loadedPrice = In_app_manager.instance.getProducePriceFromStore(In_app_manager.instance.DIAMONDS_500);
                break;

            case e_item_type.NO_ADS:
                loadedPrice = In_app_manager.instance.getProducePriceFromStore(In_app_manager.instance.NO_ADS);
                break;
        }
        priceText.text = defaultText + " " + loadedPrice;
    }
}