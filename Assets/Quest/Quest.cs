using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest :ScriptableObject
{
    public Sprite questSprite;
    public string questTitle;
    public string questDescription;
    public ItemStack requiredItem;


    //Reward
    public ItemStack[] rewardItem;
    public int xpReward;

    public bool isCompleted;

    public Quest(Sprite questSprite, string questTitle, string questDescription)
    {
        this.questSprite = questSprite;
        this.questTitle = questTitle;
        this.questDescription = questDescription;
    }

    public void CheckQuest (PlayerEntity playerEntity)
    {
        PlayerInventory playerInventory = playerEntity.GetComponent<PlayerInventory>();
        if (playerInventory.HasItem(requiredItem.item))
        {
            playerInventory.RemoveItem(requiredItem);
            if(rewardItem.Length > 0)
            {
                foreach (ItemStack itemStack in rewardItem)
                {
                    playerInventory.AddItem(itemStack);
                }
            }
            playerEntity.GetComponent<PlayerLeveling>().AddExperience(xpReward);
            isCompleted = true;
        }
    }
}
