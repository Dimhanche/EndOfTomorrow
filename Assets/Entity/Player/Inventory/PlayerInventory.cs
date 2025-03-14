using System;
using System.Linq;
using NUnit.Framework;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public ItemStack[] items;
    public static PlayerInventory instance;
    [SerializeField] private UIWindow _inventoryCanvas;
    [SerializeField] private GameObject _itemVisualizerPrefab;


    //InventorySection
    [SerializeField] private Button[] _inventorySections;
    private int currentIndex = -1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddItem(ItemStack item)
    {
        for (int j = 0; j < items.Length; j++)
        {
            if (items[j].item == item.item)
            {
                items[j].currentStack += item.currentStack;
                DisplayInventory(currentIndex);
                return;
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (!items[i].item)
            {
                items[i] = item;
                DisplayInventory(currentIndex);
                return;
            }
        }
    }

    public void RemoveItem(ItemStack item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item == item.item)
            {
                items[i].currentStack -= item.currentStack;
                if (items[i].currentStack <= 0)
                {
                    items[i].item = null;
                }

                DisplayInventory(currentIndex);
                return;
            }
        }
    }

    public int GetItemCount(ItemStack item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item == item.item)
            {
                return items[i].currentStack;
            }
        }

        return 0;
    }

    public void PlayerOpenInventoryInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _inventoryCanvas.Toggle();
            _inventorySections[0].Select();
            DisplayInventory(currentIndex = -1);
        }
    }

    private void DisplayInventory(int index)
    {
        DestroyAllItemVisualizer();
        switch (index)
        {
            case 0:
                DisplayItemByLabel(EItemLabel.Equipment);
                break;
            case 1:
                DisplayItemByLabel(EItemLabel.Consumable);
                break;
            case 2:
                DisplayItemByLabel(EItemLabel.QuestItem);
                break;
            case 3:
                DisplayItemByLabel(EItemLabel.Other);
                break;
            default:
                DisplayItemByLabel(EItemLabel.None, true);
                break;
        }
    }

    private void DisplayItemByLabel(EItemLabel itemLabel, bool displayAll = false)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item && (items[i].item.itemLabel == itemLabel || displayAll))
            {
                GameObject itemVisualizer = Instantiate(_itemVisualizerPrefab,
                    GetComponentsInChildren<GridLayoutGroup>()[currentIndex + 1].transform);
                itemVisualizer.GetComponent<ItemVisualizerButton>().SetItem(items[i]);
                items[i].item.isEquipped = false;
            }
        }
    }

    private void DestroyAllItemVisualizer()
    {
        foreach (GridLayoutGroup childGridLayoutGroup in GetComponentsInChildren<GridLayoutGroup>())
        {
            foreach (Transform child in childGridLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public bool CanCraft(Craft craft)
    {
        int ItemNeeded = 0;
        for (int j = 0; j < items.Length; j++)
        {
            if (items[j].item != null)
            {
                for (int i = 0; i < craft.itemInputs.Length; i++)
                {
                    if (items[j].item == craft.itemInputs[i].item && items[j].currentStack >= craft.itemInputs[i].currentStack)
                    {
                        ItemNeeded++;
                    }
                }
            }
        }

        if (ItemNeeded == craft.itemInputs.Length)
        {
            return true;
        }

        return false;
    }

    public void SetIndexSection(int index)
    {
        currentIndex = index;
        DisplayInventory(currentIndex);
    }
}