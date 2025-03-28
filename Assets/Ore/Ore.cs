using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public enum EOreType
{
    Copper,
    Iron,
    Gold,
    Diamond
}

public class Ore :  MonoBehaviour,IInteract
{
    public EOreType eOreType;
    [SerializeField] private ItemStack _itemsToDrop;
    [SerializeField] [Range(1, 10)] private int _oreMaxAmountDroppable = 3;
    private int _oreLife = 5;
    private int _cooldownOreRecolt = 2;


    public void Interact(ref float coolDown)
    {
        _oreLife--;
        if (_oreLife <= 0)
        {
            DropOre();
            Destroy(gameObject);
        }

        coolDown = _cooldownOreRecolt;
    }

    public void AddInInventory(ItemStack[] soItemToAdd)
    {
        IInteract.AddInInventory(soItemToAdd);
    }

    private void DropOre()
    {
        int oreAmount = UnityEngine.Random.Range(1, _oreMaxAmountDroppable + 1);
        AddInInventory(new [] { new ItemStack(_itemsToDrop.item, oreAmount) });
    }
}