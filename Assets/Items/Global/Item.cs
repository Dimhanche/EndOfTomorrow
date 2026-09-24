using System;
using UnityEngine;

public enum EItemLabel
{
    Equipment,
    Consumable,
    QuestItem,
    Other,
    None
}


public class Item : ScriptableObject
{
    [Header("Item Stats")] public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemValue;
    public bool questItem;
    public bool consumable;
    public bool usable;
    public bool equipable;
    public EItemLabel itemLabel = EItemLabel.Other;
    public bool isStackable = true;


    public void DescriptionItem(DescriptionItem descriptionItem)
    {
        descriptionItem.window.Show();
        descriptionItem.DisplayItemDescription(this);
    }

}

[Serializable]
public class ItemStack
{
    public Item item;
    public int currentStack = 1;

    public bool isEquipped;
    public int currentAmmo;

    public ItemStack(Item item, int currentStack = 1)
    {
        this.item = item;
        this.currentStack = currentStack;
        if (item is RangeWeaponItem w)
            currentAmmo = w.ammoCapacity;
    }

    public void Use(PlayerInventory inventory)
    {
        //TODO
    }

    public void Equip(PlayerEquipment equipment)
    {
        if (item.equipable && !isEquipped)
            equipment.EquipItem(this);
    }

    public void Unequip(PlayerEquipment equipment)
    {
        if (item.equipable && isEquipped)
            equipment.UnequipItem(this);
    }

    public void Destroy()
    {
        PlayerEntity.Instance.GetComponent<PlayerInventory>().RemoveStack(this);
    }

    public void Reload()
    {
        if (item is RangeWeaponItem rangeWeapon)
        {
            // TODO: once bullet ItemStacks exist, consume them from PlayerInventory here instead of
            // refilling for free, and cap currentAmmo by how many matching bullets are available.
            currentAmmo = rangeWeapon.ammoCapacity;
        }
    }
}



[Serializable]
public class ItemLootable
{
    public ItemStack item;
    public float percentageDrop;
}