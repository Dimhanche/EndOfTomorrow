using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class SOITem : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemValue;
    public bool stackable;
    public int maxStack;
    public bool questItem;
    public bool consumable;
}
