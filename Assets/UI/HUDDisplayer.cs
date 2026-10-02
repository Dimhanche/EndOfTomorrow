using System;
using TMPro;
using UnityEngine;

public class HUDDisplayer : MonoBehaviour
{
    public TextMeshProUGUI weaponAmmoInfoText;
    private void OnEnable()
    {
        PlayerEquipment.OnWeaponChanged.AddListener(DisplayRangedWeaponInfo);
        PlayerAttack.OnAttackEvent.AddListener(DisplayRangedWeaponInfo);
        PlayerAttack.OnReloadEvent.AddListener(DisplayRangedWeaponInfo);

    }

    private void OnDisable()
    {
        PlayerEquipment.OnWeaponChanged.RemoveListener(DisplayRangedWeaponInfo);
        PlayerAttack.OnAttackEvent.RemoveListener(DisplayRangedWeaponInfo);
        PlayerAttack.OnReloadEvent.RemoveListener(DisplayRangedWeaponInfo);
    }

    private void DisplayRangedWeaponInfo(Weapon currentWeapon)
    {
        if (currentWeapon is RangeWeapon weapon)
        {
            weaponAmmoInfoText.text = $"Ammo: {weapon.ammountAmmo}/{weapon.weaponItem.ammoCapacity}";
        }
    }
}
