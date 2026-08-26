using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class WindowManager
{
    public static int nbWindowsOpen => _windowStack.Count;
    private static List<UIWindow> _windowStack = new List<UIWindow>();

    private static readonly string[] WindowToggleActions = { "Inventory", "Equipment", "SkillTree", "QuestBook" };

    // Rest of the "Player" map: real gameplay, only relevant with no window open.
    private static readonly string[] GameplayActions = { "Move", "Look", "Jump", "Sprint", "Crouch", "Attack", "Interract", "PauseGame" };

    // "UI" map: only relevant while a window is open (menu navigation/clicks).
    private static readonly string[] UIActions = { "Navigate", "Submit", "Cancel", "Point", "Click", "ScrollWheel", "MiddleClick", "RightClick", "TrackedDevicePosition", "TrackedDeviceOrientation" };

    private static bool _dirty = true;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        // Statics survive play sessions when domain reload is off, so reset.
        _windowStack.Clear();
        _dirty = true;

        var go = new GameObject("[WindowManager]") { hideFlags = HideFlags.HideAndDontSave };
        go.AddComponent<WindowManagerRunner>();
    }

    public static void OpenWindow(UIWindow w)
    {
        Debug.Log($"[WindowManager] OpenWindow called: {(w != null ? w.gameObject.name : "NULL")}");
        if (w == null || _windowStack.Contains(w)) return;
        _windowStack.Add(w);
        Debug.Log($"[WindowManager] Stack count: {_windowStack.Count}");
        // Immediate, unlike the rest: interaction is polled in Update, which
        // runs before the state below is applied.
        PlayerEntity.Instance.canInterract = false;
        _dirty = true;
    }

    public static void CloseWindow(UIWindow w)
    {
        Debug.Log($"[WindowManager] CloseWindow called: {(w != null ? w.gameObject.name : "NULL")}");
        if (!w) return;

        // If top, just pop. Otherwise remove the window wherever it is.
        int last = _windowStack.Count - 1;
        if (last >= 0 && _windowStack[last] == w)
            _windowStack.RemoveAt(last);
        else
            _windowStack.Remove(w);

        _dirty = true;
    }

    public static void CloseTopWindow()
    {
        Debug.Log($"[WindowManager] CloseTopWindow - Stack count before: {_windowStack.Count}");
        if (_windowStack.Count == 0)
        {
            _dirty = true;
            return;
        }

        int last = _windowStack.Count - 1;
        var top = _windowStack[last];
        Debug.Log($"[WindowManager] Closing: {top.gameObject.name}");
        _windowStack.RemoveAt(last);
        _dirty = true;
        top.CloseWithoutManager();
        Debug.Log($"[WindowManager] Stack count after: {_windowStack.Count}");
    }

    public static void CloseAllWindow()
    {
        Debug.Log($"[WindowManager] CloseAllWindow - Stack count before: {_windowStack.Count}");
        if (_windowStack.Count == 0)
        {
            _dirty = true;
            return;
        }

        var toClose = _windowStack.ToArray();
        _windowStack.Clear();
        _dirty = true;

        // Top-down, so windows opened on top of others close first.
        for (int i = toClose.Length - 1; i >= 0; i--)
        {
            if (toClose[i]) toClose[i].CloseWithoutManager();
        }
    }

    public static bool CheckAllWindowClose()
    {
        return _windowStack.Count == 0;
    }

    internal static void ApplyPendingState()
    {
        if (!_dirty) return;

        var player = PlayerEntity.Instance;
        if (player == null) return; // not spawned yet - stay dirty, retry next frame

        var playerInput = player.GetComponent<PlayerInput>();
        bool anyOpen = _windowStack.Count > 0;

        player.canInterract = !anyOpen;
        Cursor.lockState = anyOpen ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = anyOpen;

        if (playerInput != null)
        {
            foreach (var n in WindowToggleActions) playerInput.actions.FindAction(n)?.Enable();

            foreach (var n in GameplayActions)
            {
                var a = playerInput.actions.FindAction(n);
                if (a == null) continue;
                if (anyOpen) a.Disable(); else a.Enable();
            }

            foreach (var n in UIActions)
            {
                var a = playerInput.actions.FindAction(n);
                if (a == null) continue;
                if (anyOpen) a.Enable(); else a.Disable();
            }
        }

        _dirty = false;
    }
}