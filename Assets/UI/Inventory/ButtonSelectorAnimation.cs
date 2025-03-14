using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectorAnimation : MonoBehaviour
{
    public Button selectedButton;
    public Button[] selectorButtons;

    private void Start()
    {
        selectedButton = selectorButtons[0];
    }

    public void SelectButton(int index)
    {
        selectedButton = selectorButtons[index];

    }

    private void Update()
    {
        selectedButton.Select();
    }
}
