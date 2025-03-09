using System;
using UnityEngine;
using static WindowManager;

public class UIWindow : MonoBehaviour
{
    private Canvas _canvas;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _playerMovement = PlayerInventory.instance.GetComponent<PlayerMovement>();
        Close(false);
    }

    public void Show(bool closeWindow = true)
    {
        _canvas.enabled =true;
        if(closeWindow)
            _playerMovement.canMove = OpenWindow();
    }

    public bool CheckOpened()
    {
        return _canvas.enabled;
    }

    public void Close(bool closeWindow = true)
    {
        _canvas.enabled = false;

        if(closeWindow)
            _playerMovement.canMove = CloseWindow();
    }

    public void Toggle()
    {
        if (!_canvas.enabled)
        {
            Show();
        }
        else
        {
            Close();
        }
    }
}
