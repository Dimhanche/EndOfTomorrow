using System;
using UnityEngine;

[Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemValue;
    public bool questItem;
    public bool consumable;
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
