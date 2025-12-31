using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Forge : MonoBehaviour, IInteract
{
    public UIWindow forgeCanvas;
    public Image cookImage;
    public Craft[] crafts;
    [SerializeField] private ItemStack _itemInput;
    private float _cooldown;
    private bool _isCooking;
    [SerializeField] private Transform _craftParent;
    [SerializeField] private GameObject _craftPrefab;
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = PlayerEntity.Instance.GetComponent<PlayerInventory>();
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
       if(!_isCooking)
       {
           IInteract.RemoveFromInventory(currentCraft.itemInputs);
           _isCooking = true;
           _itemInput = currentCraft.itemInputs[0];
           _cooldown = currentCraft.craftTime;
           while (_cooldown > 0)
           {
               yield return new WaitForSecondsRealtime(0.25f);
               cookImage.fillAmount = 1-(_cooldown / currentCraft.craftTime);
               _cooldown-=.25f;
           }
           cookImage.fillAmount = 0;
           _isCooking = false;
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
        cookImage.fillAmount = 0;
        for (int i = 0; i < crafts.Length; i++)
        {
            GameObject craftVisualizer = Instantiate(_craftPrefab, _craftParent);
            crafts[i].isLocked = !_playerInventory.CanCraft(crafts[i]);
            craftVisualizer.GetComponent<ItemVisualizerButton>().SetItem(crafts[i]);
            int index = i;
            craftVisualizer.GetComponent<ItemVisualizerButton>().itemButton.onClick.AddListener(() =>
            {
                StartCoroutine(Cook(crafts[index]));
                crafts[index].isLocked = !_playerInventory.CanCraft(crafts[index]);
                craftVisualizer.GetComponent<ItemVisualizerButton>().CheckItem(crafts[index]);
            });

        }
    }
}