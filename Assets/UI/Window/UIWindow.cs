using System;
using UnityEngine;
using static WindowManager;

public class UIWindow : MonoBehaviour
{
    private Canvas _canvas;
    private EntityInfo _playerEntity;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _playerEntity = PlayerInventory.instance.GetComponent<EntityInfo>();
        Close(false);
        CloseAllWindow();
    }

    public void Show(bool closeWindow = true)
    {
        _canvas.enabled =true;
        if(closeWindow)
            _playerEntity.canMove = OpenWindow();
    }

    public bool CheckOpened()
    {
        return _canvas.enabled;
    }

    public void Close(bool closeWindow = true)
    {
        _canvas.enabled = false;
        if(closeWindow)
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
