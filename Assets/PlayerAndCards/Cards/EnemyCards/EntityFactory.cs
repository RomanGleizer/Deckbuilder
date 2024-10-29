using System.IO;
using UnityEngine;
using Zenject;

public abstract class EntityFactory : IEntityFactory<EnemyCard>
{
    protected EnemyCard _enemyCard;

    private IInstantiator _instantiator;

    public EntityFactory()
    {
        LoadResources();
    }

    protected abstract void LoadResources();

    protected void LoadResources(string path)
    {
        _enemyCard = Resources.Load<EnemyCard>(path);
    }

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public virtual EnemyCard Create()
    {
        return _instantiator.InstantiatePrefabForComponent<EnemyCard>(_enemyCard);
    }
}

public class ArmsmanFactory : EntityFactory
{
    private const string PATH = "Armsman";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class CavalrymanFactory : EntityFactory
{
    private const string PATH = "Cavalryman";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class CommanderFactory : EntityFactory
{
    private const string PATH = "Commander";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class DrummerFactory : EntityFactory
{
    private const string PATH = "Drummer";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class GarbageManFactory : EntityFactory
{
    private const string PATH = "GarbageMan";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class PioneerFactory : EntityFactory
{
    private const string PATH = "Pioneer";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class ShooterFactory : EntityFactory
{
    private const string PATH = "Shooter";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class SnareFactory : EntityFactory
{
    private const string PATH = "Snare";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class SniperFactory : EntityFactory
{
    private const string PATH = "Sniper";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class SpyFactory : EntityFactory
{
    private const string PATH = "Spy";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class SwordsmanFactory : EntityFactory
{
    private const string PATH = "Swordsman";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}