using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapContextInstaller : MonoInstaller
{
    [SerializeField] private WindowActivator _windowActivator;
    [SerializeField] private PlayerCardsContainer _playerCardsContainer;
    [SerializeField] private CoinsCounter _coinsCounter;
    
    public override void InstallBindings()
    {
        BindWindowActivator();
        BindPlayerCardContainer();
        BindCoinsCounter();
    }

    private void BindCoinsCounter()
    {
        BindService(_coinsCounter);
    }

    private void BindWindowActivator()
    {
        BindService(_windowActivator);
        _windowActivator.Init();
    }

    private void BindPlayerCardContainer()
    {
        BindService(_playerCardsContainer);
        _playerCardsContainer.Init();
    }
    
    private void BindService<T>(T service)
    {
        Container.Bind<T>().FromInstance(service).AsSingle();
    }
}
