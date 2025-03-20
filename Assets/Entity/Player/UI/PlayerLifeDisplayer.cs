using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerLifeDisplayer : MonoBehaviour
{
    [SerializeField]private Image _lifeBar;

    public void UpdateLifeBar(float currentLife, float maxLife)
    {
        _lifeBar.fillAmount = currentLife / maxLife;
    }
}