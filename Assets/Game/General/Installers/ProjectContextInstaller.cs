using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [SerializeField] private SubscribeService _subscribeService;
    [SerializeField] private float _commandExecuteDelay;
    [SerializeField] private LevelDistributor _levelDistributor;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        BindInstantiator();
        BindSubscribeService();

        BindCommandInvoker();
        BindCommandFactory();

        BindLevelDistributor();
    }

    private void BindLevelDistributor()
    {
        Container.Bind<LevelDistributor>().FromComponentInNewPrefab(_levelDistributor).AsSingle();
    }

    private void BindInstantiator()
    {
        Container.Bind<IInstantiator>().FromInstance(Container).AsSingle();
    }

    private void BindSubscribeService()
    {
        Container.Bind<SubscribeService>().FromComponentInNewPrefab(_subscribeService).AsSingle();
    }

    private void BindCommandInvoker()
    {
        Container.Bind<CommandInvoker>().FromNew().AsSingle();
    }

    private void BindCommandFactory()
    {
        Container.Bind<CommandFactory>().FromNew().AsSingle().WithArguments(_commandExecuteDelay);
    }
}