using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Craft", menuName = "Craft")]
[Serializable]
public class Craft :ScriptableObject
{
    public string craftName;

    public float craftTime;
    public bool isLocked;

    public Sprite lockedIcon;
    public Sprite craftIcon;

    public ItemStack[] itemInputs;
    public ItemStack[] itemOutputs;

}
