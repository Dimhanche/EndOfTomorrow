using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Forge : MonoBehaviour, IInteract
{
    public UIWindow forgeCanvas;
    public Craft[] crafts;
    [SerializeField] private ItemStack _itemInput;
    private float _cooldown;
    private bool isCooking;
    [SerializeField] private Transform _craftParent;
    [SerializeField] private GameObject _craftPrefab;
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = PlayerInventory.instance;
    }

    public void Interact(ref float cooldown)
    {
        if (!forgeCanvas.CheckOpened())
        {
            forgeCanvas.Show();
        }
        CraftDisplayer();
        cooldown = 0;
    }

    public IEnumerator Cook(Craft currentCraft)
    {
       if(!isCooking)
       {
           IInteract.RemoveFromInventory(currentCraft.itemInputs);
           isCooking = true;
           _itemInput = currentCraft.itemInputs[0];
           _cooldown = currentCraft.craftTime;
           while (_cooldown > 0)
           {
               yield return new WaitForSecondsRealtime(1);
               _cooldown--;
           }

           isCooking = false;
           ItemStack[] copiedItems = new ItemStack[currentCraft.itemOutputs.Length];
           for (int i = 0; i < currentCraft.itemOutputs.Length; i++)
           {
               copiedItems[i] = new ItemStack(currentCraft.itemOutputs[i].item,currentCraft.itemOutputs[i].currentStack);
           }
           IInteract.AddInInventory( copiedItems );
           _itemInput = null;
       }
    }

    public void CraftDisplayer()
    {
        foreach (Transform child in _craftParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < crafts.Length; i++)
        {
            GameObject craftVisualizer = Instantiate(_craftPrefab, _craftParent);
            crafts[i].isLocked = !_playerInventory.CanCraft(crafts[i]);
            craftVisualizer.GetComponent<CraftVisualizer>().SetCraft(crafts[i]);
            int index = i;
            craftVisualizer.GetComponent<CraftVisualizer>().craftButton.onClick.AddListener(() =>
            {
                StartCoroutine(Cook(crafts[index]));
                crafts[index].isLocked = !_playerInventory.CanCraft(crafts[index]);
                craftVisualizer.GetComponent<CraftVisualizer>().CheckCraft(crafts[index]);
            });

        }
    }
}