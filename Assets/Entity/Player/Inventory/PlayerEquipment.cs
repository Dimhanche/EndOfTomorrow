using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour
{
    public UIWindow equipmentCanvas;
    private EquipmentDisplayer _equipmentDisplayer;
    public WeaponsItem weapon;
    public ArmorsItem[] armor;

    private void Start()
    {
        _equipmentDisplayer = equipmentCanvas.GetComponentInChildren<EquipmentDisplayer>();
    }

    public void PlayerOpenEquipment(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            equipmentCanvas.Toggle();
        }
    }

    /// <summary>
    /// Equip Weapon to the player
    /// </summary>
    /// <param name="newWeapon">weapon To Equip</param>
    public void EquipWeapon(WeaponsItem newWeapon)
    {
        weapon = newWeapon;
        _equipmentDisplayer.DisplayEquipment(newWeapon);
    }

    /// <summary>
    /// Equip Armor to the player
    /// </summary>
    /// <param name="newArmor">armor To Equip</param>
    public void EquipArmor(ArmorsItem newArmor)
    {
        Debug.Log("Equipping armor " + newArmor.itemName);
        for (int i = 0; i < armor.Length; i++)
        {
            if (armor[i] == null || (armor[i].armorType == newArmor.armorType))
            {
                armor[i] = newArmor;
                _equipmentDisplayer.DisplayEquipment(newArmor);
                return;
            }
        }
    }


    /// <summary>
    /// Equip an item to the player Armor or Weapon
    /// </summary>
    /// <param name="item">item To Equip</param>
    public void EquipItem(Item item)
    {
        Debug.Log("Equipping item " + item.itemName);
        item.isEquipped = true;
        PlayerInventory.instance.RemoveItem(new ItemStack(item, 1));
        if(item is WeaponsItem)
        {
            EquipWeapon(item as WeaponsItem);
        }
        else if(item is ArmorsItem)
        {
            EquipArmor(item as ArmorsItem);
        }
        else
        {
            Debug.LogError("Item is not equippable");
        }
    }


    /// <summary>
    /// Unequip an item from the player Armor or Weapon
    /// </summary>
    /// <param name="item">item to Unequip</param>
    public void UnequipItem(Item item)
    {
        Debug.Log("Unequipping item " + item.itemName);
        item.isEquipped = false;
        PlayerInventory.instance.AddItem(new ItemStack(item, 1));
        if(item is WeaponsItem)
        {
            UnequipWeapon(item as WeaponsItem);
        }
        else if(item is ArmorsItem)
        {
            UnequipArmor(item as ArmorsItem);
        }
        else
        {
            Debug.LogError("Item is not equipped");
        }
    }


    /// <summary>
    /// Unequip Armor to the player
    /// </summary>
    /// <param name="newArmor">armor To Unequip</param>
    private void UnequipArmor(ArmorsItem item)
    {
        Debug.Log("Unequipping armor " + item.itemName);
        for (int i = 0; i < armor.Length; i++)
        {
            if (armor[i] == item)
            {
                armor[i] = null;
                _equipmentDisplayer.DisplayEquipment(item,true);
                return;
            }
        }
    }

    /// <summary>
    /// Unequip Weapon to the player
    /// </summary>
    /// <param name="newArmor">weapon To Unequip</param>
    private void UnequipWeapon(WeaponsItem item)
    {
        Debug.Log("Unequipping weapon " + item.itemName);
        weapon = null;
        _equipmentDisplayer.DisplayEquipment(item,true);
    }
}
