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


[Serializable]
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
    public bool isEquipped;
    public EItemLabel itemLabel = EItemLabel.Other;
    public void UseItem(PlayerInventory playerInventory)
    {
        //TODO: Use the item
    }

    public void EquipItem(PlayerEquipment playerEquipment)
    {
        if (equipable && !isEquipped)
        {
            playerEquipment.EquipItem(this);
        }
    }

    public void UnequipItem(PlayerEquipment playerEquipment)
    {
        if (equipable && isEquipped)
        {
            playerEquipment.UnequipItem(this);
        }
    }


    public void DescriptionItem(DescriptionItem descriptionItem,Item item)
    {
        descriptionItem.window.Show();
        descriptionItem.DisplayItemDescription(item);
    }

    public void DestroyItem()
    {
        PlayerEntity.Instance.GetComponent<PlayerInventory>().RemoveItem(new ItemStack(this));
    }
}

[Serializable]
public class ItemStack
{
    public Item item;
    public int currentStack;

    public ItemStack(Item item, int currentStack = 1)
    {
        this.item = item;
        this.currentStack = currentStack;
    }
}

[Serializable]
public class ItemLootable
{
    public ItemStack item;
    public float percentageDrop;
}