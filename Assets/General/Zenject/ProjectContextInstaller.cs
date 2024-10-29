using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [SerializeField] private SubscribeService _subscribeService;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        BindInstantiator();
        BindSubscribeService();

        BindCommandFactory();
    }

    private void BindInstantiator()
    {
        Container.Bind<IInstantiator>().FromInstance(Container).AsSingle();
    }

    private void BindSubscribeService()
    {
        Container.Bind<SubscribeService>().FromComponentInNewPrefab(_subscribeService).AsSingle();
    }

    private void BindCommandFactory()
    {
        Container.Bind<CommandFactory>().FromNew().AsSingle();
    }
}
