using System;
using UnityEngine;
using Zenject;

public class ChooseMapPoint : MapPoint
{
    [Header("Event Data")]
    [SerializeField] private string _eventTittle;
    [SerializeField] private string _description;
    [SerializeField] private string[] _answers;
    [SerializeField] private Sprite _sprite;
    
    private WindowActivator _windowActivator;
    
    [Inject]
    private void Construct(WindowActivator windowActivator)
    {
        _windowActivator = windowActivator;
    }
    
    public override void ActivatePointEvent()
    {
        var chooseWindow = _windowActivator.ActivateWindow(WindowType.Choose) as ChoseWindow;
        chooseWindow?.Init(_eventTittle, _description, _answers, _sprite);
    }
}