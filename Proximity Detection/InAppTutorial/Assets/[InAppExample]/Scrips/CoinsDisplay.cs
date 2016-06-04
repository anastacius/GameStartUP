using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField]
    private Main main;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = string.Format("Coins: {0}", main.PlayerData.Coins);
    }

}
