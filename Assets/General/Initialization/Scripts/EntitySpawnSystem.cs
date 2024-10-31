using System.Collections.Generic;
using Table.Scripts.Entities;
using UnityEngine;
using Zenject;

public class EntitySpawnSystem
{
    private Dictionary<EntityType, EntityFactory> _factories = new Dictionary<EntityType, EntityFactory>();

    private IInstantiator _instantiator;

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
        InitializeFactories();
    }

    private void InitializeFactories()
    {
        _factories.Add(EntityType.Armsman, _instantiator.Instantiate<ArmsmanFactory>());
        _factories.Add(EntityType.Cavalryman, _instantiator.Instantiate<CavalrymanFactory>());
        _factories.Add(EntityType.Commander, _instantiator.Instantiate<EntityCommanderFactory>());
        _factories.Add(EntityType.Drummer, _instantiator.Instantiate<DrummerFactory>());
        _factories.Add(EntityType.GarbageMan, _instantiator.Instantiate<GarbageManFactory>());
        _factories.Add(EntityType.Pioneer, _instantiator.Instantiate<PioneerFactory>());
        _factories.Add(EntityType.Shooter, _instantiator.Instantiate<ShooterFactory>());
        _factories.Add(EntityType.Snare, _instantiator.Instantiate<SnareFactory>());
        _factories.Add(EntityType.Sniper, _instantiator.Instantiate<SniperFactory>());
        _factories.Add(EntityType.Spy, _instantiator.Instantiate<SpyFactory>());
        _factories.Add(EntityType.Swordsman, _instantiator.Instantiate<SwordsmanFactory>());
    }

    public EnemyCard SpawnEntity(EntityType entityType)
    {
        if (_factories.ContainsKey(entityType))
        {
            var enemy = _factories[entityType].Create();
            enemy.Init();
            return enemy;
        }
        else throw new System.ArgumentNullException("Argument with type " + entityType + " doesn't exist!");
    }

    public EnemyCard SpawnEntity(EntityType entityType, Cell spawnCell)
    {
        var enemyCard = SpawnEntity(entityType);

        if (enemyCard != null)
        {
            enemyCard.transform.position = spawnCell.transform.position;
            enemyCard.SetStartCell(spawnCell);
        }

        return enemyCard;
    }
}