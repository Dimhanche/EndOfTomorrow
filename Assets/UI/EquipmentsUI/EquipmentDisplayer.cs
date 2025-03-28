using UnityEngine;

public class EquipmentDisplayer : MonoBehaviour
{
    public ItemVisualizerButton helmetPlacement;
    public ItemVisualizerButton chestplatePlacement;
    public ItemVisualizerButton leggingsPlacement;
    public ItemVisualizerButton beltPlacement;
    public ItemVisualizerButton gauntletPlacement;
    public ItemVisualizerButton weaponPlacement;

    private ItemVisualizerButton _currentItemVisualizer;

    public void DisplayEquipment(ArmorsItem armorToDisplay,bool unequip = false)
    {
        switch (armorToDisplay.eArmorType)
        {
            case EArmorType.Helmet:
                _currentItemVisualizer = helmetPlacement;
                break;
            case EArmorType.Chestplate:
                _currentItemVisualizer = chestplatePlacement;
                break;
            case EArmorType.Leggings:
                _currentItemVisualizer = leggingsPlacement;
                break;
            case EArmorType.Belt:
                _currentItemVisualizer = beltPlacement;
                break;
            case EArmorType.Gauntlet:
                _currentItemVisualizer = gauntletPlacement;
                break;
            default:
                Debug.LogError("Armor Type not found");
                break;
        }

        if (unequip)
        {
            _currentItemVisualizer.itemToDisplay = null;
            _currentItemVisualizer.itemName.text = "";
            _currentItemVisualizer.itemButton.image.sprite = null;
            _currentItemVisualizer.lockImage.gameObject.SetActive(false);
            _currentItemVisualizer.nbItem.text = "";
            return;
        }
        _currentItemVisualizer.itemToDisplay = armorToDisplay;
        _currentItemVisualizer.itemName.text = armorToDisplay.itemName;
        _currentItemVisualizer.itemButton.image.sprite = armorToDisplay.itemSprite;
        _currentItemVisualizer.lockImage.gameObject.SetActive(false);
        _currentItemVisualizer.nbItem.text = "";
    }

    public void DisplayEquipment(WeaponsItem weaponToDisplay,bool unequip = false)
    {
        _currentItemVisualizer = weaponPlacement;
        if (unequip)
        {
            _currentItemVisualizer.itemToDisplay = null;
            _currentItemVisualizer.itemName.text = "";
            _currentItemVisualizer.itemButton.image.sprite = null;
            _currentItemVisualizer.lockImage.gameObject.SetActive(false);
            _currentItemVisualizer.nbItem.text = "";
            return;
        }
        _currentItemVisualizer.itemToDisplay = weaponToDisplay;
        _currentItemVisualizer.itemName.text = weaponToDisplay.itemName;
        _currentItemVisualizer.itemButton.image.sprite = weaponToDisplay.itemSprite;
        _currentItemVisualizer.lockImage.gameObject.SetActive(false);
        _currentItemVisualizer.nbItem.text = "";
    }
}
