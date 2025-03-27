using UnityEngine;

public interface IInteract
{

    void Interact(ref float cooldoawnMax );

    static void AddInInventory(ItemStack[] soItemToAdd)
    {
        for (int i = 0; i < soItemToAdd.Length; i++)
        {
            PlayerEntity.Instance.GetComponent<PlayerInventory>().AddItem(soItemToAdd[i]);
        }
    }

    static void RemoveFromInventory(ItemStack[] soItemToRemove)
    {
        for (int i = 0; i < soItemToRemove.Length; i++)
        {
            PlayerEntity.Instance.GetComponent<PlayerInventory>().RemoveItem(soItemToRemove[i]);
        }
    }
}
