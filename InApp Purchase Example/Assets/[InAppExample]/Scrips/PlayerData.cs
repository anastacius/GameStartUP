using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public static string VERSION = "0.0.0.4";

    [SerializeField]
    private int coins;

    [SerializeField]
    private string version;

    [SerializeField]
    private string[] productsId;

    [SerializeField]
    private bool[] productsStatus;

    public PlayerData()
    {
        productsId = new string[1] {StoreController.SWORD_01};
        productsStatus = new bool[1] {false};
        version = VERSION;
        coins = 0;
    }

    public int Coins
    {
        get { return coins; }
    }

    public string Version
    {
        get { return version; }
    }

    public string[] ProductsId
    {
        get { return productsId; }
    }

    public bool[] ProductsStatus
    {
        get { return productsStatus; }
    }

    public void SetStatus(string productId, bool status)
    {
        int index = Array.IndexOf(productsId, productId);
        productsStatus[index] = status;
    }

    public bool GetStatus(string productId)
    {
        int index = Array.IndexOf(productsId, productId);
        return productsStatus[index];
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }
}
