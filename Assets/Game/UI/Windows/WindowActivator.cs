using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowActivator : MonoBehaviour
{
    [SerializeField] private Window[] _windows;
    private Dictionary<WindowType, Window> _windowsDict = new Dictionary<WindowType, Window>();
    
    public void Init()
    {
        foreach (var window in _windows)
        {
            _windowsDict.Add(window.Type, window);
        }
    }
    
    public Window ActivateWindow(WindowType windowType)
    {
        var window = _windowsDict[windowType];
        window.ActivateWindow();
        return window;
    }

    public void DeactivateWindow(WindowType windowType)
    {
        var window = _windowsDict[windowType];
        window.DeactivateWindow();
    }
}