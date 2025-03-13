using UnityEngine;

public interface IInteract
{

    void Interact(ref float cooldoawnMax );

    static void AddInInventory(ItemStack[] soItemToAdd)
    {
        for (int i = 0; i < soItemToAdd.Length; i++)
        {
            PlayerInventory.instance.AddItem(soItemToAdd[i]);
        }
    }

    static void RemoveFromInventory(ItemStack[] soItemToRemove)
    {
        for (int i = 0; i < soItemToRemove.Length; i++)
        {
            PlayerInventory.instance.RemoveItem(soItemToRemove[i]);
        }
    }
}
