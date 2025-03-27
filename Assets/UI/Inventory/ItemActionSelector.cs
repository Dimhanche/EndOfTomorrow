using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemActionSelector : MonoBehaviour
{
    public GameObject itemActionObject;
    [SerializeField]private Button[] _itemActionButton;
    public Vector3 offest;

    private PlayerEquipment _playerEquipment;
    private Item _item;
    private DescriptionItem _descriptionItem;

    private void Awake()
    {
        _itemActionButton = GetComponentsInChildren<Button>();
        _itemActionButton[1].onClick.AddListener(DescriptionItem);
        _itemActionButton[2].onClick.AddListener(DestroyItem);
        HideItemActions();
        _playerEquipment = GetComponentInParent<PlayerEquipment>();
        _descriptionItem = GetComponentInParent<DescriptionItem>();
    }

    public void DisplayItemActions(Vector3 itemPos ,bool usable = false, bool equipable = false,bool isEquipped= false,Item item = null)
    {
        // Display the item actions
        itemActionObject.SetActive(true);
        itemActionObject.transform.position = itemPos+offest;
        _itemActionButton[0].interactable = true;
        _item = item;
        if(usable)
        {
            _itemActionButton[0].GetComponentInChildren<TextMeshProUGUI>().text = "Use";
            _itemActionButton[0].onClick.AddListener(UseItem);
        }
        else if(equipable && !isEquipped)
        {
            _itemActionButton[0].GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
            _itemActionButton[0].onClick.AddListener(EquipItem);
        }
        else if(equipable && isEquipped)
        {
            _itemActionButton[0].GetComponentInChildren<TextMeshProUGUI>().text = "Unequip";
            _itemActionButton[0].onClick.AddListener(UnequipItem);
        }
        else
        {
            _itemActionButton[0].GetComponentInChildren<TextMeshProUGUI>().text = "Can't use";
            _itemActionButton[0].interactable = false;
        }
    }

    public void HideItemActions()
    {
        itemActionObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HideItemActions();
            }
        }
    }

    public void UseItem()
    {
        HideItemActions();
    }

    public void EquipItem()
    {
        _playerEquipment.EquipItem(_item);
        HideItemActions();
    }

    public void UnequipItem()
    {
        _playerEquipment.UnequipItem(_item);
        HideItemActions();
    }


    public void DescriptionItem()
    {
        _descriptionItem.window.Show();
        _descriptionItem.DisplayItemDescription(_item);
        HideItemActions();
    }

    public void DestroyItem()
    {
        PlayerEntity.Instance.GetComponent<PlayerInventory>().RemoveItem(new ItemStack(_item));
        HideItemActions();
    }
}
