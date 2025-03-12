using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplayer : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI moneyText;
    public Button buyButton,sellButton;

    public RectTransform buyMenu,sellMenu;

    private void Start()
    {
        buyMenu.gameObject.SetActive(false);
        sellMenu.gameObject.SetActive(false);

        buyButton.onClick.AddListener(ShowBuyMenu);
        sellButton.onClick.AddListener(ShowSellMenu);
    }

    public void ShowBuyMenu()
    {
        buyMenu.gameObject.SetActive(true);
        sellMenu.gameObject.SetActive(false);
    }

    public void ShowSellMenu()
    {
        buyMenu.gameObject.SetActive(false);
        sellMenu.gameObject.SetActive(true);
    }
}
