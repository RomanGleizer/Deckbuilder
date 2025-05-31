using UnityEngine;
using Zenject;

public class CloseWindowButtonByType : CustomButton
{
    [SerializeField] private WindowType _windowType;
    private WindowActivator _windowActivator;
    
    [Inject]
    private void Construct(WindowActivator windowActivator)
    {
        _windowActivator = windowActivator;
    }
    
    protected override void OnClick()
    {
        _windowActivator.DeactivateWindow(_windowType);
    }
}