using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemVisualizerButton : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public Button itemButton;
    public Image lockImage;
    public TextMeshProUGUI nbItem;

    public Item itemToDisplay;

    //Selector
    private ItemActionSelector _itemActionSelector;

    // Double-click detection
    private float _lastClickTime;
    private const float DoubleClickThreshold = 0.5f;
    
    private void Awake()
    {
        itemButton.onClick.AddListener(OnItemClick);
        _itemActionSelector = FindObjectsByType<ItemActionSelector>(FindObjectsInactive.Include, FindObjectsSortMode.None)[0];
    }

    private void OnItemClick()
    {
        if (Time.time - _lastClickTime < DoubleClickThreshold)
        {
            OnItemDoubleClick();
            _itemActionSelector.HideItemActions();
            _itemActionSelector.enabled = false;
        }
        else
        {
            ShowItemActions();
        }
        _lastClickTime = Time.time;
    }
    private void OnItemDoubleClick()
    {
        if (itemToDisplay)
        {
            if (itemToDisplay.usable)
            {
                itemToDisplay.UseItem(PlayerInventory.instance);
            }
            else if (itemToDisplay.equipable && !itemToDisplay.isEquipped)
            {
                itemToDisplay.EquipItem(PlayerInventory.instance.GetComponent<PlayerEquipment>());
            }
            else if (itemToDisplay.equipable && itemToDisplay.isEquipped)
            {
                itemToDisplay.UnequipItem(PlayerInventory.instance.GetComponent<PlayerEquipment>());
            }
        }
    }

    private void ShowItemActions()
    {
        _itemActionSelector.enabled = true;
        if (itemToDisplay)
        {
            _itemActionSelector.DisplayItemActions(
                this.transform.position, itemToDisplay.usable, itemToDisplay.equipable,itemToDisplay.isEquipped,itemToDisplay);
        }
    }

    public void SetItem(Craft craft)
    {
        itemName.text = craft.craftName;
        itemButton.image.sprite = craft.craftIcon;
        lockImage.gameObject.SetActive(craft.isLocked);
        lockImage.sprite = craft.lockedIcon;
        nbItem.text = "";
    }

    public void SetItem(ItemStack item)
    {
        itemToDisplay = item.item;
        itemName.text = itemToDisplay.itemName;
        itemButton.image.sprite = itemToDisplay.itemSprite;
        lockImage.gameObject.SetActive(false);
        nbItem.text = item.currentStack.ToString();
    }

    public void SetItemToBuy(ItemStack item,float currentPlayerMoney)
    {
        itemName.text = item.item.itemName;
        itemButton.image.sprite = item.item.itemSprite;
        lockImage.gameObject.SetActive(item.currentStack <= 0 || currentPlayerMoney < item.item.itemValue );
        nbItem.text = "";
    }

    public void SetItemToSold(ItemStack item,float currentMoney,PlayerInventory playerInventory)
    {
        itemName.text = item.item.itemName;
        itemButton.image.sprite = item.item.itemSprite;
        lockImage.gameObject.SetActive(currentMoney < item.item.itemValue || playerInventory.GetItemCount(item)<=0);
        nbItem.text = "";
    }

    public void CheckItem(Craft craft)
    {
        lockImage.gameObject.SetActive(craft.isLocked);
    }
}
