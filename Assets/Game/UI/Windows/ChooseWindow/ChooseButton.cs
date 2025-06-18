using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ChooseButton : CustomButton
{
    [SerializeField] private TextMeshProUGUI _textPlacement;

    private WindowActivator _windowActivator;
    
    [Inject]
    private void Construct(WindowActivator windowActivator)
    {
        _windowActivator = windowActivator;
    }
    
    public void Init(string message)
    {
        _textPlacement.text = message;
    }

    protected override void OnClick()
    {
        _windowActivator.DeactivateWindow(WindowType.Choose);
    }
}
