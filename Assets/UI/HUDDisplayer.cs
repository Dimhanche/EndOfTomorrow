using System;
using TMPro;
using UnityEngine;

public class HUDDisplayer : MonoBehaviour
{
    public TextMeshProUGUI weaponAmmoInfoText;
    private void OnEnable()
    {
        PlayerEquipment.OnEquipmentChanged.AddListener(DisplayRangedWeaponInfo);
        PlayerAttack.OnAttackEvent.AddListener(DisplayRangedWeaponInfo);
    }

    private void OnDisable()
    {
        PlayerEquipment.OnEquipmentChanged.RemoveListener(DisplayRangedWeaponInfo);
        PlayerAttack.OnAttackEvent.RemoveListener(DisplayRangedWeaponInfo);
    }

    private void DisplayRangedWeaponInfo(ItemStack weaponStack)
    {
        if (weaponStack.item is RangeWeaponItem weapon)
        {
            weaponAmmoInfoText.text = $"Ammo: {weaponStack.currentAmmo}/{weapon.ammoCapacity}";
        }
    }
}
