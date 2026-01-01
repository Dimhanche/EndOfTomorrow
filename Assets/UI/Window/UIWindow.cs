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
        _playerEntity = PlayerEntity.Instance;
        _canvas = GetComponent<Canvas>();
        _playerInput = _playerEntity.GetComponent<PlayerInput>();
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
        if(_canvas != null)
            return _canvas.enabled;
        return false;
    }

    public void Close()
    {
        _canvas.enabled = false;
        _playerEntity.canMove = CloseWindow();
        print("lag cause of " + ItemActionSelector.Instance);
        if (ItemActionSelector.Instance && ItemActionSelector.Instance.CheckOpenedWindow())
        {
            print("Close");
            ItemActionSelector.Instance.HideItemActions();
        }
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