using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemVisualizer : MonoBehaviour
{
    public ItemStack currentItem;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemStack;
    public Image itemImage;

    public void SetItem(ItemStack itemToDisplay)
    {
        currentItem =itemToDisplay;
        itemStack.text = itemToDisplay.currentStack.ToString();
        itemImage.sprite = itemToDisplay.item.itemSprite;
    }
}
