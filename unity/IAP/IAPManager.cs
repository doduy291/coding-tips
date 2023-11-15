using System;
using UnityEngine;
using UnityEngine.Purchasing;
using Unity.Services.Core;
using UnityEngine.Events;
using UnityEngine.Purchasing.Extension;

namespace IAP
{
    public class IAPManager : MonoBehaviour, IStoreListener
    {
        public static IAPManager Instance;
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
        private static UnityEngine.Purchasing.Product purchasedProduct = null;

        IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;

        public static string GEM_100 = "gem_100";
        public static string GEM_250 = "gem_250";
        public static string GEM_600 = "gem_600";
        public static string GEM_1000 = "gem_1000";
        public static string GEM_2600 = "gem_2600";
        public static string GEM_5000 = "gem_5000";
        public static string NO_ADS = "no_ads";

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

            // Gem
            builder.AddProduct(GEM_100, ProductType.Consumable);
            builder.AddProduct(GEM_250, ProductType.Consumable);
            builder.AddProduct(GEM_600, ProductType.Consumable);
            builder.AddProduct(GEM_1000, ProductType.Consumable);
            builder.AddProduct(GEM_2600, ProductType.Consumable);
            builder.AddProduct(GEM_5000, ProductType.Consumable);
            // No Ads
            builder.AddProduct(NO_ADS, ProductType.NonConsumable);

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
            purchasedProduct = args.purchasedProduct;

            if (m_GooglePlayStoreExtensions.IsPurchasedProductDeferred(purchasedProduct))
            {
                //The purchase is Deferred.
                //Therefore, we do not unlock the content or complete the transaction.
                //ProcessPurchase will be called again once the purchase is Purchased.
                Debug.Log(string.Format("ProcessPurchase: Pending. Product:" + args.purchasedProduct.definition.id + " - " + purchasedProduct.transactionID.ToString()));
                return PurchaseProcessingResult.Pending;
            }

            if (purchasedProduct.definition.id == GEM_100)
            {
                Debug.Log("Purchase: GEM 100");
                IAPEvents?.Invoke();
            }

            if (purchasedProduct.definition.id == GEM_250)
            {
                Debug.Log("Purchase: GEM 250");
                IAPEvents?.Invoke();
            }

            if (purchasedProduct.definition.id == GEM_600)
            {
                Debug.Log("Purchase: GEM 600");
                IAPEvents?.Invoke();
            }

            if (purchasedProduct.definition.id == GEM_1000)
            {
                Debug.Log("Purchase: GEM 1000");
                IAPEvents?.Invoke();
            }

            if (purchasedProduct.definition.id == GEM_2600)
            {
                Debug.Log("Purchase: GEM 2600");
                IAPEvents?.Invoke();
            }

            if (purchasedProduct.definition.id == GEM_5000)
            {
                Debug.Log("Purchase: GEM 5000");
                IAPEvents?.Invoke();
            }

            if (purchasedProduct.definition.id == NO_ADS)
            {

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
