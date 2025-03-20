using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static WindowManager;

public class UIWindow : MonoBehaviour
{
    private Canvas _canvas;
    private PlayerEntity _playerEntity;
    private PlayerInput _playerInput;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _playerEntity = PlayerInventory.instance.GetComponent<PlayerEntity>();
        _playerInput = PlayerInventory.instance.GetComponent<PlayerInput>();
        Close();
        CloseAllWindow();
    }

    public void Show()
    {
        _canvas.enabled = true;

        _playerEntity.canMove = OpenWindow();
    }

    public bool CheckOpened()
    {
        return _canvas.enabled;
    }

    public void Close()
    {
        _canvas.enabled = false;
        _playerEntity.canMove = CloseWindow();
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