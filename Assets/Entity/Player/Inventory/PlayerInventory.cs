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
                return;
            }
        }
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log($"{item} is added to Inventory with {item.currentStack} stack");
            if (!items[i].item)
            {
                items[i] = item;
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
                return;
            }
        }
    }

    public void OpenInventory(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _inventoryCanvas.Toggle();
            DisplayInvenotry();
        }
    }

    private void DisplayInvenotry()
    {
        DestroyAllItemVisualizer();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item != null)
            {
                GameObject itemVisualizer = Instantiate(_itemVisualizerPrefab, GetComponentInChildren<GridLayoutGroup>().transform);
                itemVisualizer.GetComponent<ItemVisualizer>().SetItem(items[i]);
            }
        }
    }

    private void DestroyAllItemVisualizer()
    {
        foreach (Transform child in GetComponentInChildren<GridLayoutGroup>().transform)
        {
            Destroy(child.gameObject);
        }
    }

    public bool CanCraft(Craft craft)
    {
        int ItemNeeded = 0;
        for(int j = 0; j < items.Length; j++)
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
}
