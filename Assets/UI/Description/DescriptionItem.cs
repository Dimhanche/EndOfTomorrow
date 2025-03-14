using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionItem : MonoBehaviour
{
    public UIWindow window;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI labelItem;
    public Image itemSprite;

    public void DisplayItemDescription(Item item)
    {
        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;
        itemPrice.text = "Value : "+item.itemValue.ToString();
        itemSprite.sprite = item.itemSprite;
        labelItem.text = item.itemLabel.ToString();
    }
}
