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
        BindInstaller();
    }

    private void BindInstaller()
    {
        BindService<IInstantiator>(Container);
    }

    private void BindSubscribeService()
    {
        BindService(Container.InstantiatePrefabForComponent<SubscribeService>(_subscribeService));
    }

    private void BindCommandFactory()
    {
        BindService(Container.Instantiate<CommandFactory>());
    }

    private void BindService<T>(T service)
    {
        Container.Bind<T>().FromInstance(service).AsSingle();
    }
}
