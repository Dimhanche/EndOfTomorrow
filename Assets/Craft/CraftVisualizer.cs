using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftVisualizer : MonoBehaviour
{
    public TextMeshProUGUI craftName;
    public Button craftButton;
    public Image lockImage;
    public void SetCraft(Craft craft)
    {
        craftName.text = craft.craftName;
        craftButton.image.sprite = craft.craftIcon;
        lockImage.gameObject.SetActive(craft.isLocked);
        lockImage.sprite = craft.lockedIcon;
    }

    public void CheckCraft(Craft craft)
    {
        lockImage.gameObject.SetActive(craft.isLocked);
    }
}
