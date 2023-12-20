using System;
using UnityEngine;
using UnityEngine.Purchasing;
using Unity.Services.Core;
using UnityEngine.Events;
using UnityEngine.Purchasing.Extension;
using System.Collections.Generic;

namespace IAP
{
    public class IAPManager : MonoBehaviour, IStoreListener
    {
        public static IAPManager Instance;
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
        private static UnityEngine.Purchasing.Product purchasedProduct = null;

        IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;

        /* 
            In general, if you are initializing the dictionary with a small number of predefined key/value pairs, 
            the initializer syntax can be more concise. 
            If you need to dynamically add key/value pairs based on certain conditions or during runtime, 
            explicit method calls like Add might be more suitable. (https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.add?view=net-8.0)
        */
        // ====> This is intializer syntax <====
        public static Dictionary<string, InAppProduct> inAppProducts = new Dictionary<string, InAppProduct>() {
            // Gold
            {"gold_1500", new InAppProduct("gold_1500", ProductType.Consumable)},
            {"gold_4000", new InAppProduct("gold_4000", ProductType.Consumable)},
            {"gold_17500", new InAppProduct("gold_17500", ProductType.Consumable)},
            // Gem
            {"gem_100", new InAppProduct("gem_100", ProductType.Consumable)},
            {"gem_250", new InAppProduct("gem_250", ProductType.Consumable)},
            {"gem_600", new InAppProduct("gem_600", ProductType.Consumable)},
            {"gem_1000", new InAppProduct("gem_1000", ProductType.Consumable)},
            {"gem_2600", new InAppProduct("gem_2600", ProductType.Consumable)},
            {"gem_5000", new InAppProduct("gem_5000", ProductType.Consumable)},
            // Ads
            //{"NO_ADS", new InAppProductValue("no_ads", ProductType.Subscription)},
            // Packages
            {"p1", new InAppProduct("p1", ProductType.Consumable)},
            {"p2", new InAppProduct("p2", ProductType.Consumable)},
            {"p3", new InAppProduct("p3", ProductType.Consumable)},
            {"p4", new InAppProduct("p4", ProductType.Consumable)},
            {"p5", new InAppProduct("p5", ProductType.Consumable)},
            {"p6", new InAppProduct("p6", ProductType.Consumable)},
        };


        public UnityEvent IAPEvents;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        async void Start()
        {
            await UnityServices.InitializeAsync();
            InitializePurchasing();
        }

        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (string key in inAppProducts.Keys)
            {
                InAppProduct IAPValue = inAppProducts[key];
                builder.AddProduct(IAPValue.productId, IAPValue.type);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        public void BuySubscription(string subscriptionType)
        {
            BuyProductID(subscriptionType);
        }

        public void BuyConsumable(string consumableType)
        {
            BuyProductID(consumableType);
        }

        public void BuyNonConsumable(string nonConsumableType)
        {
            BuyProductID(nonConsumableType);
        }

        void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                UnityEngine.Purchasing.Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product:" + product.definition.id.ToString()));
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");

            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
            m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();

        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            throw new NotImplementedException();
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            InAppProduct result;
            purchasedProduct = args.purchasedProduct;

            if (m_GooglePlayStoreExtensions.IsPurchasedProductDeferred(purchasedProduct))
            {
                //The purchase is Deferred.
                //Therefore, we do not unlock the content or complete the transaction.
                //ProcessPurchase will be called again once the purchase is Purchased.
                Debug.Log(string.Format("ProcessPurchase: Pending. Product:" + args.purchasedProduct.definition.id + " - " + purchasedProduct.transactionID.ToString()));
                return PurchaseProcessingResult.Pending;
            }

            if (inAppProducts.TryGetValue(purchasedProduct.definition.id, out result))
            {
                if (result.productId == purchasedProduct.definition.id)
                {
                    IAPEvents?.Invoke();
                    Debug.Log("Purchase: " + purchasedProduct.definition.id);
                }
            }

            IAPEvents.RemoveAllListeners();
            Debug.Log(string.Format("ProcessPurchase: Complete. Product:" + args.purchasedProduct.definition.id + " - " + purchasedProduct.transactionID.ToString()));
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureDescription));
        }
    }
}

public class InAppProduct
{
    public string productId;
    public ProductType type;

    public InAppProduct(string productId, ProductType type)
    {
        this.productId = productId;
        this.type = type;
    }
}
