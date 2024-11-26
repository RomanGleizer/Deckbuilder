using System.Collections.Generic;
using Game.Table.Scripts.Entities;
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
        _factories.Add(EntityType.Rock, _instantiator.Instantiate<RockFactory>());
        _factories.Add(EntityType.Chest, _instantiator.Instantiate<ChestFactory>());
    }

    public EntityCard SpawnEntity(EntityType entityType)
    {
        if (_factories.ContainsKey(entityType))
        {
            var entity = _factories[entityType].Create();
            entity.Init();
            return entity;
        }
        else throw new System.ArgumentNullException("Argument with type " + entityType + " doesn't exist!");
    }

    public EntityCard SpawnEntity(EntityType entityType, Cell spawnCell)
    {
        var entityCard = SpawnEntity(entityType);

        if (entityCard != null)
        {
            entityCard.transform.position = spawnCell.transform.position;
            entityCard.SetStartCell(spawnCell);
        }

        return entityCard;
    }
}