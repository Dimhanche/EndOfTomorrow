using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemVisualizerButton : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public Button itemButton;
    public Image lockImage;

    public void SetItem(Craft craft)
    {
        itemName.text = craft.craftName;
        itemButton.image.sprite = craft.craftIcon;
        lockImage.gameObject.SetActive(craft.isLocked);
        lockImage.sprite = craft.lockedIcon;
    }

    public void SetItemToBuy(ItemStack item,float currentPlayerMoney)
    {
        itemName.text = item.item.itemName;
        itemButton.image.sprite = item.item.itemSprite;
        lockImage.gameObject.SetActive(item.currentStack <= 0 || currentPlayerMoney < item.item.itemValue );
    }

    public void SetItemToSold(ItemStack item,float currentMoney,PlayerInventory playerInventory)
    {
        itemName.text = item.item.itemName;
        itemButton.image.sprite = item.item.itemSprite;
        lockImage.gameObject.SetActive(currentMoney < item.item.itemValue || playerInventory.GetItemCount(item)<=0);
    }

    public void CheckItem(Craft craft)
    {
        lockImage.gameObject.SetActive(craft.isLocked);
    }
}
