using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using static WindowManager;

public class UIWindow : MonoBehaviour
{
    private Canvas _canvas;
    public UnityEvent onClosed = new UnityEvent();

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        if (_canvas != null) _canvas.enabled = false;
    }

    public void Show()
    {
        Debug.Log($"[UIWindow] Show => {gameObject.name}");
        if (_canvas != null) _canvas.enabled = true;
        WindowManager.OpenWindow(this);
    }

    public bool CheckOpened()
    {
        return _canvas && _canvas.enabled;
    }

    // normal close (called by window itself)
    public void Close()
    {
        Debug.Log($"[UIWindow] Close => {gameObject.name}");
        if (_canvas) _canvas.enabled = false;
        WindowManager.CloseWindow(this);
        onClosed?.Invoke();

        if (ItemActionSelector.Instance && ItemActionSelector.Instance.CheckOpenedWindow())
        {
            ItemActionSelector.Instance.HideItemActions();
        }
    }

    // called by WindowManager when popping the top window to avoid re-registering
    public void CloseWithoutManager()
    {
        Debug.Log($"[UIWindow] CloseWithoutManager => {gameObject.name}");
        if (_canvas != null) _canvas.enabled = false;
        onClosed?.Invoke();

        if (ItemActionSelector.Instance && ItemActionSelector.Instance.CheckOpenedWindow())
        {
            ItemActionSelector.Instance.HideItemActions();
        }
    }

    public void Toggle()
    {
        if (!_canvas.enabled)
            Show();
        else
            Close();
    }
    public void OnClosePerformed()
    {
        WindowManager.CloseAllWindow();
    }

}
