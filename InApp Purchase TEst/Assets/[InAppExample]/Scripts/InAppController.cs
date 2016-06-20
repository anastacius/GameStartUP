using UnityEngine;
using UnityEngine.Purchasing;

namespace Assets.Scripts
{
    public class InAppController : MonoBehaviour, IStoreListener
    {
        private IStoreController storeController;
        private IAppleExtensions appleExtension;


        private void Awake()
        {
            StandardPurchasingModule module = StandardPurchasingModule.Instance();
            

            // The FakeStore supports: no-ui (always succeeding), basic ui (purchase pass/fail), and 
            // developer ui (initialization, purchase, failure code setting). These correspond to 
            // the FakeStoreUIMode Enum values passed into StandardPurchasingModule.useFakeStoreUIMode.
            module.useFakeStoreUIMode = FakeStoreUIMode.Default;

            ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
            // This enables the Microsoft IAP simulator for local testing.
            // You would remove this before building your release package.
            builder.Configure<IGooglePlayConfiguration>().SetPublicKey("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2O/9/H7jYjOsLFT/uSy3ZEk5KaNg1xx60RN7yWJaoQZ7qMeLy4hsVB3IpgMXgiYFiKELkBaUEkObiPDlCxcHnWVlhnzJBvTfeCPrYNVOOSJFZrXdotp5L0iS2NVHjnllM+HA1M0W2eSNjdYzdLmZl1bxTpXa4th+dVli9lZu7B7C2ly79i/hGTmvaClzPBNyX+Rtj7Bmo336zh2lYbRdpD5glozUq+10u91PMDPH+jqhx10eyZpiapr8dFqXl5diMiobknw9CgcjxqMTVBQHK6hS0qYKPmUDONquJn280fBs1PTeA6NMG03gb9FLESKFclcuEZtvM8ZwMMRxSLA9GwIDAQAB");

            // Define our products.
            // In this case our products have the same identifier across all the App stores,
            // except on the Mac App store where product IDs cannot be reused across both Mac and
            // iOS stores.
            // So on the Mac App store our products have different identifiers,
            // and we tell Unity IAP this by using the IDs class.


            builder.AddProduct("sword.epicsword_01", ProductType.NonConsumable);
            builder.AddProduct("100.gold.coins", ProductType.Consumable);


            // Now we're ready to initialize Unity IAP.
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("Billing failed to initialize!");
            switch (error)
            {
                case InitializationFailureReason.AppNotKnown:
                    Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
                    break;
                case InitializationFailureReason.PurchasingUnavailable:
                    // Ask the user if billing is disabled in device settings.
                    Debug.Log("Billing disabled!");
                    break;
                case InitializationFailureReason.NoProductsAvailable:
                    // Developer configuration error; check product metadata.
                    Debug.Log("No products available for purchase!");
                    break;
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product item, PurchaseFailureReason failureReason)
        {
            Debug.Log("Purchase failed: " + item.definition.id);
            Debug.Log(failureReason);    }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            appleExtension = extensions.GetExtension<IAppleExtensions>();

            Debug.Log("Available items:");
            foreach (var item in controller.products.all)
            {
                if (item.availableToPurchase)
                {
                    Debug.Log(string.Join(" - ",
                        new[]
                        {
                        item.metadata.localizedTitle,
                        item.metadata.localizedDescription,
                        item.metadata.isoCurrencyCode,
                        item.metadata.localizedPrice.ToString(),
                        item.metadata.localizedPriceString
                        }));
                }
            }

        }
    }
}
