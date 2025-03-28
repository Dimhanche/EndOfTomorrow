using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemStack> items;
    [SerializeField] private UIWindow _inventoryCanvas;
    [SerializeField] private GameObject _itemVisualizerPrefab;


    //InventorySection
    [SerializeField] private Button[] _inventorySections;
    private int currentIndex = -1;

    //EntityInfo
    private int _currentMoney => GetComponent<PlayerEntity>().entity.money;
    [SerializeField]  private TextMeshProUGUI _moneyText;
    [SerializeField]  private TextMeshProUGUI _lvlText;


    public void AddItem(ItemStack item)
    {
        for (int j = 0; j < items.Count; j++)
        {
            if (items[j].item == item.item)
            {
                items[j].currentStack += item.currentStack;
                DisplayInventory(currentIndex);
                return;
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].item)
            {
                items[i] = item;
                DisplayInventory(currentIndex);
                return;
            }
        }

        UpdateCurrentMoney();
    }

    public void RemoveItem(ItemStack item)
    {
        for (int i = 0; i < items.Count; i++)
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
        UpdateCurrentMoney();
    }

    public void RemoveItem(ItemStack[] itemsToRemove)
    {
        for (int i = 0; i < items.Count; i++)
        {
            foreach (ItemStack item in itemsToRemove)
            {
                RemoveItem(item);
            }
        }
        UpdateCurrentMoney();
    }


    public int GetItemCount(ItemStack item)
    {
        for (int i = 0; i < items.Count; i++)
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
            OpenInventory();
        }
    }

    public bool HasItem(ItemStack item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item.item && items[i].currentStack >= item.currentStack)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItem(ItemStack[] itemsChecked)
    {
        foreach (ItemStack item in itemsChecked)
        {
            if (!HasItem(item))
            {
                return false;
            }
        }
        return true;
    }

    public void OpenInventory()
    {
        UpdateCurrentMoney();
        UpdatePlayerLevel();
        _inventoryCanvas.Toggle();
        _inventorySections[0].Select();
        DisplayInventory(currentIndex = -1);
    }

    private void UpdatePlayerLevel()
    {
        _lvlText.text = $"LVL {GetComponent<PlayerLeveling>().currentLevel.ToString()}";
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
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item && (items[i].item.itemLabel == itemLabel || displayAll))
            {
                GameObject itemVisualizer = Instantiate(_itemVisualizerPrefab,
                    GetComponentInChildren<GridLayoutGroup>().transform);
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
        for (int j = 0; j < items.Count; j++)
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

    private void UpdateCurrentMoney()
    {
        _moneyText.text = _currentMoney.ToString();
    }

    public void SetIndexSection(int index)
    {
        currentIndex = index;
        DisplayInventory(currentIndex);
    }
}