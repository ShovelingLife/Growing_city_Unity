﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class In_app_manager : Singleton_local<In_app_manager> , IStoreListener
{
    private static IStoreController   m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public Button ad_buy_button;

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
    public string DIAMONDS_50  = "50_DIAMOND";
    public string DIAMONDS_125 = "125_DIAMOND";
    public string DIAMONDS_250 = "250_DIAMOND";
    public string DIAMONDS_500 = "500_DIAMOND";
    public string NO_ADS       = "NO_AD";


    void Start()
    {
      // If we haven't set up the Unity Purchasing reference
      if (m_StoreController == null)
      // Begin to configure our connection to Purchasing
      InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())            
            return; // ... we are done here.

    // Create a builder, first passing in a suite of Unity provided stores.
    var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

    // Add a product to sell / restore by way of its identifier, associating the general identifier
    // with its store-specific identifiers.
    builder.AddProduct(DIAMONDS_50, ProductType.Consumable);
    builder.AddProduct(DIAMONDS_125, ProductType.Consumable);
    builder.AddProduct(DIAMONDS_250, ProductType.Consumable);
    builder.AddProduct(DIAMONDS_500, ProductType.Consumable);
    // Continue adding the non-consumable product.
    builder.AddProduct(NO_ADS, ProductType.NonConsumable);

    // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
    // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
    UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void Buy_50_diamonds()
    {
        Buy_product_ID(DIAMONDS_50);
    }

    public void Buy_125_diamonds()
    {
        Buy_product_ID(DIAMONDS_125);
    }

    public void Buy_250_diamonds()
    {
        Buy_product_ID(DIAMONDS_250);
    }

    public void Buy_500_diamonds()
    {
        Buy_product_ID(DIAMONDS_500);
    }

    public void Buy_no_ads()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        Buy_product_ID(NO_ADS);
    }

    public string Get_produce_price_from_store(string id)
    {
        if (m_StoreController != null && 
            m_StoreController.products != null)
            return m_StoreController.products.WithID(id).metadata.localizedPriceString;

        else 
            return "";
    }

    void Buy_product_ID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void Restore_purchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    // AD 배너광고 변경 해주는 함수
    void Disable_ad_buy_button()
    {
        ad_buy_button.interactable = false;
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, DIAMONDS_50, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Shop_manager.instance.Add_cash(50);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, DIAMONDS_125, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Shop_manager.instance.Add_cash(125);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, DIAMONDS_250, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Shop_manager.instance.Add_cash(250);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, DIAMONDS_500, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Shop_manager.instance.Add_cash(500);
        }
        // Or ... a subscription product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, NO_ADS, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Disable_ad_buy_button();
            PlayerPrefs.SetInt("ad_bought", 1);
            PlayerPrefs.Save();
        }
        else
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}