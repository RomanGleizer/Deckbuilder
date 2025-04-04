using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeckOpenButton : CustomButton
{
    private WindowActivator _windowActivator;

    [Inject]
    private void Construct(WindowActivator windowActivator)
    {
        _windowActivator = windowActivator;
    }
    
    protected override void OnClick()
    {
        _windowActivator.ActivateWindow(WindowType.Deck);
    }
}
