using PlayerAndCards.Player;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private Field _field;
    [SerializeField] private Player _player;

    public override void InstallBindings()
    {
        Bind();
    }

    private void Bind()
    {
        BindField();
        BindPlayer();
    }

    private void BindField()
    {
        BindService(_field);
    }

    private void BindPlayer()
    {
        BindService(_player);
    }

    private void BindService<T>(T service)
    {
        Container.Bind<T>().FromInstance(service).AsSingle();
    }
}
