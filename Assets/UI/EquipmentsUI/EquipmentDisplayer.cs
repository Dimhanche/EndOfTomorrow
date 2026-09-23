using UnityEngine;

public class EquipmentDisplayer : MonoBehaviour
{
    public ItemVisualizerButton helmetPlacement;
    public ItemVisualizerButton chestplatePlacement;
    public ItemVisualizerButton leggingsPlacement;
    public ItemVisualizerButton beltPlacement;
    public ItemVisualizerButton gauntletPlacement;
    public ItemVisualizerButton weaponPlacement;

    public void DisplayEquipment(ItemStack stack, bool unequip = false)
    {
        ItemVisualizerButton currentItemVisualizer = GetPlacement(stack.item);
        if (currentItemVisualizer == null)
            return;

        if (unequip)
        {
            currentItemVisualizer.Clear();
            return;
        }

        currentItemVisualizer.itemStack = stack;
        currentItemVisualizer.itemToDisplay = stack.item;
        currentItemVisualizer.itemName.text = stack.item.itemName;
        currentItemVisualizer.itemButton.image.sprite = stack.item.itemSprite;
        currentItemVisualizer.lockImage.gameObject.SetActive(false);
        currentItemVisualizer.nbItem.text = "";
    }

    private ItemVisualizerButton GetPlacement(Item item)
    {
        if (item is ArmorsItem armorItem)
        {
            switch (armorItem.eArmorType)
            {
                case EArmorType.Helmet:
                    return helmetPlacement;
                case EArmorType.Chestplate:
                    return chestplatePlacement;
                case EArmorType.Leggings:
                    return leggingsPlacement;
                case EArmorType.Belt:
                    return beltPlacement;
                case EArmorType.Gauntlet:
                    return gauntletPlacement;
                default:
                    Debug.LogError("Armor Type not found");
                    return null;
            }
        }

        return weaponPlacement;
    }
}
