using System;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour, IInteract
{
    public UIWindow merchantCanvas;
    private InfoDisplayer _infoDisplayer;

    public EntityInfo entityInfo;
    private EntityInfo _playerEntityInfo;
    private PlayerInventory _playerInventory;

    public ItemStack[] itemToSell;
    public ItemStack[] itemToBuy;

    public GameObject itemPrefab;

    private void Start()
    {
        _infoDisplayer = merchantCanvas.GetComponent<InfoDisplayer>();
        _playerEntityInfo = PlayerInventory.instance.GetComponent<EntityInfo>();
        _playerInventory = PlayerInventory.instance;
    }

    public void Interact(ref float cooldown)
    {
        Debug.Log("Interacting with merchant");
        if (!merchantCanvas.CheckOpened())
        {
            merchantCanvas.Show();
            DisplayMerchantInfo();
            DisplayAllItems();
        }

        cooldown = 0;
    }

    public void DisplayMerchantInfo()
    {
        _infoDisplayer.nameText.text = entityInfo.entity.entityName;
        _infoDisplayer.moneyText.text = entityInfo.entity.money.ToString();
        _infoDisplayer.ShowBuyMenu();
        _infoDisplayer.buyButton.onClick.AddListener(() => DisplayAllItems());
        _infoDisplayer.sellButton.onClick.AddListener(() => DisplayAllItems(true));

    }

    private void MerchantInfoUpdater()
    {
        _infoDisplayer.moneyText.text = entityInfo.entity.money.ToString();
    }
    public void DisplayAllItems(bool merchantSelling = false)
    {
        DestroyAllItems();
        if (merchantSelling)
        {
            // Merchant is selling
            foreach (ItemStack item in itemToBuy)
            {
                GameObject itemGO = Instantiate(itemPrefab, _infoDisplayer.sellMenu.GetChild(0));
                itemGO.GetComponent<ItemVisualizerButton>().SetItemToSold(item, entityInfo.entity.money,_playerInventory);
                itemGO.GetComponent<ItemVisualizerButton>().itemButton.onClick.AddListener(() => BuyItem(item));
            }
        }
        else
        {
            // Merchant is buying
            foreach (ItemStack item in itemToSell)
            {
                GameObject itemGO = Instantiate(itemPrefab, _infoDisplayer.buyMenu.GetChild(0));
                itemGO.GetComponent<ItemVisualizerButton>().SetItemToBuy(item, _playerEntityInfo.entity.money);
                itemGO.GetComponent<ItemVisualizerButton>().itemButton.onClick.AddListener(() => SellItem(item));
            }
        }
    }

    public void DestroyAllItems()
    {
        foreach (Transform child in _infoDisplayer.buyMenu.GetComponentInChildren<GridLayoutGroup>().transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in _infoDisplayer.sellMenu.GetComponentInChildren<GridLayoutGroup>().transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Merchant Buy Item
    /// </summary>
    /// <param name="item">item buy</param>
    private void BuyItem(ItemStack item)
    {
        item.currentStack++;
        entityInfo.entity.money -= item.item.itemValue;
        _playerEntityInfo.entity.money += item.item.itemValue;
        _playerInventory.RemoveItem(item);
        MerchantInfoUpdater();
        DisplayAllItems(true);
    }
/// <summary>
/// Merchant Sell Item
/// </summary>
/// <param name="item"> item sell</param>
    private void SellItem(ItemStack item)
    {
        item.currentStack--;
        entityInfo.entity.money += item.item.itemValue;
        _playerEntityInfo.entity.money -= item.item.itemValue;
        _playerInventory.AddItem(new ItemStack(item.item));
        MerchantInfoUpdater();
        DisplayAllItems();
    }
}