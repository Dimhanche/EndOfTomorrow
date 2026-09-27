using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerEquipment : EntityEquipment
{
    public UIWindow equipmentCanvas;
    private EquipmentDisplayer _equipmentDisplayer;
    private PlayerInventory _playerInventory;
    public ItemStack weaponStack;

    public static UnityEvent<ItemStack> OnEquipmentChanged = new UnityEvent<ItemStack>();

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
    /// <param name="newWeapon">Weapon To Equip</param>
    /// <param name="stack">The exemplar being equipped</param>
    public void EquipWeapon(WeaponItem newWeapon, ItemStack stack)
    {
        weapon = newWeapon;
        weaponStack = stack;
        OnEquipmentChanged?.Invoke(stack);
        _equipmentDisplayer.DisplayEquipment(stack);
    }

    /// <summary>
    /// Equip Armor
    /// </summary>
    /// <param name="newArmor">Armor to equip</param>
    /// <param name="stack">The exemplar being equipped</param>
    public void EquipArmor(ArmorsItem newArmor, ItemStack stack)
    {
        OnEquipmentChanged?.Invoke(stack);
        for (int i = 0; i < armor.Length; i++)
        {
            if (armor[i] == null || (armor[i].eArmorType == newArmor.eArmorType))
            {
                armor[i] = newArmor;
                _equipmentDisplayer.DisplayEquipment(stack);
                return;
            }
        }
    }


    /// <summary>
    /// Equip an item exemplar, Armor or Weapon
    /// </summary>
    /// <param name="stack">exemplar To Equip</param>
    public void EquipItem(ItemStack stack)
    {
        stack.isEquipped = true;
        _playerInventory.RemoveStack(stack);
        if(stack.item is WeaponItem weaponItem)
        {
            if (weapon != null)
            {
                UnequipItem(weaponStack);
            }

            EquipWeapon(weaponItem, stack);
        }
        else if(stack.item is ArmorsItem armorItem)
        {
            EquipArmor(armorItem, stack);
        }
        else
        {
            Debug.LogError("Item is not equippable");
        }
    }


    /// <summary>
    /// Unequip an item exemplar, Armor or Weapon
    /// </summary>
    /// <param name="stack">exemplar to Unequip</param>
    public void UnequipItem(ItemStack stack)
    {
        stack.isEquipped = false;
        _playerInventory.AddItem(stack);
        if(stack.item is WeaponItem)
        {
            UnequipWeapon(stack);
        }
        else if(stack.item is ArmorsItem)
        {
            UnequipArmor(stack);
        }
        else
        {
            Debug.LogError("Item is not equipped");
        }
    }


    /// <summary>
    /// Unequip Armor
    /// </summary>
    /// <param name="stack">exemplar To Unequip</param>
    private void UnequipArmor(ItemStack stack)
    {
        OnEquipmentChanged?.Invoke(stack);
        ArmorsItem item = stack.item as ArmorsItem;
        for (int i = 0; i < armor.Length; i++)
        {
            if (armor[i] == item)
            {
                armor[i] = null;
                _equipmentDisplayer.DisplayEquipment(stack, true);
                return;
            }
        }
    }

    /// <summary>
    /// Unequip Weapon
    /// </summary>
    /// <param name="stack">exemplar To Unequip</param>
    private void UnequipWeapon(ItemStack item)
    {
        Debug.Log("Unequipping Weapon: " + item.item.name);
        //_playerInventory.AddItem(item);
        OnEquipmentChanged?.Invoke(item);
        weapon = null;
        weaponStack = null;
        _equipmentDisplayer.DisplayEquipment(item, true);
    }

    private void UnequipWeapon(Item item)
    {
        ItemStack stack = new ItemStack(item)
        {
            currentStack = 1
        };
        UnequipWeapon(stack);
    }
}
