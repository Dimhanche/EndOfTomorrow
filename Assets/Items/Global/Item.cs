using System;
using UnityEngine;

public enum EItemLabel{
    Equipment,
    Consumable,
    QuestItem,
    Other
}


[Serializable]
public class Item : ScriptableObject
{
    [Header("Item Stats")]
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemValue;
    public bool questItem;
    public bool consumable;
    public EItemLabel itemLabel = EItemLabel.Other;
}

[Serializable]
public class ItemStack
{
    public Item item;
    public int currentStack;

    public ItemStack(Item item, int currentStack =1)
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
