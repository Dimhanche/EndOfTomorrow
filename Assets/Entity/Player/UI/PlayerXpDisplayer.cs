using UnityEngine;
using UnityEngine.UI;

public class PlayerXpDisplayer : MonoBehaviour
{
    [SerializeField]private  Image _xpBar;

    public void UpdateXpBar(float currentXp, float maxXp)
    {
        _xpBar.fillAmount = currentXp / maxXp;
    }
}
