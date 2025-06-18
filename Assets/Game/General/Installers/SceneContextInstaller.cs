using System;
using Game.PlayerAndCards.PlayerScripts;
using Game.Table.Scripts.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private LevelPlacement _levelPlacement;
#if UNITY_EDITOR
    [SerializeField] private QueuesEditorVisual _queueVisual;
#endif


    [SerializeField] private Field _field;
    [SerializeField] private Player _player;

    [SerializeField] private Transform _spawnPoint;

    private CommandFactory _commandFactory;

    [SerializeField] private Button _switchButton;
    [SerializeField] private TextMeshProUGUI _turnWarningText;

    [SerializeField] private HandManager _handManager;
    [SerializeField] private WindowActivator _windowActivator;
    [SerializeField] private PlayerCardsContainer _cardsContainer;
    
    [SerializeField] private CoinsCounter _coinsCounter;
    
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
        BindPlayerCardsContainer();
        BindEnemyService();
        BindEntitySpawnSystem();

        BindHandManager();
        BindTurnSystems();
        BindWindowActivator();

        BindLevelPlacementStackController();
        BindLevelInitializator();

        BindCoinsCounter();
    }
    
    private void BindEnemyService()
    {
        Container.Bind<EnemyService>().FromNew().AsSingle();
    }

    private void BindCoinsCounter()
    {
        BindService(_coinsCounter);
    }

    private void BindPlayerCardsContainer()
    {
        BindService(_cardsContainer);
        _cardsContainer.Init();
    }

    private void BindWindowActivator()
    {
        BindService(_windowActivator);
        _windowActivator.Init();
    }

    private void BindLevelPlacementStackController()
    {
#if UNITY_EDITOR
        var levelPlacementStack = new LevelPlacementStack(_levelPlacement, _queueVisual);
#else
        var levelPlacementStack = new LevelPlacementStack(_levelPlacement);
#endif
        Container.Bind<LevelPlacementStackController>().FromNew().AsSingle().WithArguments(new object[] { levelPlacementStack, _spawnPoint });
    }

    private void BindField()
    {
        BindService(_field);
        _commandFactory.SetParams(_field, Container);
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

    private void BindTurnSystems()
    {
        Container.Bind<TurnManager>().FromNew().AsSingle().WithArguments(_switchButton, _turnWarningText);
        Container.Instantiate<EnemiesTurnController>();
        Container.Instantiate<PlayerTurnController>();
    }

    private void BindLevelInitializator()
    {
        Container.Bind<LevelInitializator>().FromNew().AsSingle();
    }

    private void BindHandManager()
    {
        BindService(_handManager);
    }
    
    private void BindService<T>(T service)
    {
        Container.Bind<T>().FromInstance(service).AsSingle();
    }
}
