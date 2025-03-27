using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : EntityEquipment
{
    public UIWindow equipmentCanvas;
    private EquipmentDisplayer _equipmentDisplayer;
    private PlayerInventory _playerInventory;



    private void Start()
    {
        _equipmentDisplayer = equipmentCanvas.GetComponentInChildren<EquipmentDisplayer>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    public void PlayerOpenEquipment(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            equipmentCanvas.Toggle();
        }
    }

    /// <summary>
    /// Equip Weapon
    /// </summary>
    /// <param name="newWeapon">weapon To Equip</param>
    public void EquipWeapon(WeaponsItem newWeapon)
    {
        weapon = newWeapon;
        _equipmentDisplayer.DisplayEquipment(newWeapon);
    }

    /// <summary>
    /// Equip Armor
    /// </summary>
    /// <param name="newArmor">armor To Equip</param>
    public void EquipArmor(ArmorsItem newArmor)
    {
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
    /// Equip an item  Armor or Weapon
    /// </summary>
    /// <param name="item">item To Equip</param>
    public void EquipItem(Item item)
    {
        item.isEquipped = true;
        _playerInventory.RemoveItem(new ItemStack(item, 1));
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
    /// Unequip an item  Armor or Weapon
    /// </summary>
    /// <param name="item">item to Unequip</param>
    public void UnequipItem(Item item)
    {
        item.isEquipped = false;
        _playerInventory.AddItem(new ItemStack(item, 1));
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
    /// Unequip Armor
    /// </summary>
    /// <param name="newArmor">armor To Unequip</param>
    private void UnequipArmor(ArmorsItem item)
    {
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
    /// Unequip Weapon
    /// </summary>
    /// <param name="newArmor">weapon To Unequip</param>
    private void UnequipWeapon(WeaponsItem item)
    {
        weapon = null;
        _equipmentDisplayer.DisplayEquipment(item,true);
    }
}
