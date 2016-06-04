using System;
using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public const string SAVE_DATA_KEY = "PlayerData";


    [SerializeField]
    private Button swordButton;
    [SerializeField]
    private Button coinsButton;
    [SerializeField]
    private StoreController storeController;

    private PlayerData playerData;

    public PlayerData PlayerData
    {
        get { return playerData; }
    }

    private Text swordButtonLabel;

    private void Awake()
    {
        swordButtonLabel = swordButton.GetComponentInChildren<Text>();
        storeController.OnPurchaseSuccessEvent += OnPurchaseSuccess;
        swordButton.onClick.AddListener(OnSwordClick);
        coinsButton.onClick.AddListener(OnCoinsClick);
        LoadPlayerData();
    }

   

    private void OnDestroy()
    {
        storeController.OnPurchaseSuccessEvent -= OnPurchaseSuccess;

        swordButton.onClick.RemoveAllListeners();
        coinsButton.onClick.RemoveAllListeners();
    }

    private void OnPurchaseSuccess(Product product)
    {
        if (product.definition.type == ProductType.NonConsumable)
        {
            playerData.SetStatus(product.definition.id, true);
        }
        else if (product.definition.type == ProductType.Consumable)
        {
            if (product.definition.id == StoreController.COINS_01)
            {
                playerData.AddCoins(100);
            }
        }

        UpdateSwordButton();
    }

    private void OnSwordClick()
    {
        if(playerData.GetStatus(StoreController.SWORD_01))
            return;

        storeController.Purchase(StoreController.SWORD_01);
    }

    private void OnCoinsClick()
    {
        storeController.Purchase(StoreController.COINS_01);
    }



    private void UpdateSwordButton()
    {
        if (playerData.GetStatus(StoreController.SWORD_01))
            swordButtonLabel.text = "Voce ja tem esta espada";
        else
        {
            swordButtonLabel.text = string.Format("Comprar Espada - {0}",
                storeController.GetProduct(StoreController.SWORD_01).metadata.localizedPriceString);
        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetString(SAVE_DATA_KEY, JsonUtility.ToJson(playerData));
    }

    private void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
        {
            playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(SAVE_DATA_KEY));
            if (playerData == null || 
                string.IsNullOrEmpty(playerData.Version) || 
                !string.Equals(playerData.Version, PlayerData.VERSION))
                playerData = new PlayerData();
        }
        else
        {
            playerData = new PlayerData();
        }
        UpdateSwordButton();
    }
}