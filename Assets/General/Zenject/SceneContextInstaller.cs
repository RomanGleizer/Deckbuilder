using PlayerAndCards.Player;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private LevelPlacement _levelPlacement;

    [SerializeField] private Field _field;
    [SerializeField] private Player _player;

    private CommandFactory _commandFactory;

    public override void InstallBindings()
    {
        Bind();
    }

    [Inject]
    private void Construct(CommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }

    private void Bind()
    {
        BindField();
        BindPlayer();
        BindEntitySpawnSystem();
        BindLevelInitializator();
    }

    private void BindField()
    {
        BindService(_field);
        _commandFactory.SetField(_field);
        _field.Initialize();
    }

    private void BindPlayer()
    {
        BindService(_player);
    }

    private void BindEntitySpawnSystem()
    {
        Container.Bind<EntitySpawnSystem>().FromNew().AsSingle();
    }

    private void BindLevelInitializator()
    {
        Container.Bind<LevelInitializator>().FromNew().AsSingle();
    }

    private void BindService<T>(T service)
    {
        Container.Bind<T>().FromInstance(service).AsSingle();
    }
}
