using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour,IInteract
{
    public ItemStack[] guaranteedLootItems;
    public ItemLootable[] possibleLootItems;
    [SerializeField]private bool _isOpened;

    public void Interact(ref float cooldown)
    {
        if (!_isOpened)
        {
            cooldown = 0.25f;
            Loot();
        }
    }

    public void Loot()
    {
        for (int i = 0; i < guaranteedLootItems.Length; i++)
        {
            IInteract.AddInInventory(guaranteedLootItems);
        }

        List<ItemStack> tempItemList = new List<ItemStack>();
        for (int i = 0; i < possibleLootItems.Length; i++)
        {
            if (Random.Range(0, 100) < possibleLootItems[i].percentageDrop)
            {
                tempItemList.Add(new ItemStack(possibleLootItems[i].item.item, possibleLootItems[i].item.currentStack));
            }
        }
        ItemStack[] tempItem = tempItemList.ToArray();
        IInteract.AddInInventory(tempItem);
        _isOpened = true;
    }
}
