using DG.Tweening.Plugins.Core.PathCore;
using System.IO;
using UnityEngine;
using Zenject;

public abstract class EntityFactory : IEntityFactory
{
    protected EntityCard _entityCard;

    private IInstantiator _instantiator;

    public EntityFactory()
    {
        LoadResources();
    }

    protected abstract void LoadResources();

    protected void LoadResources(string path)
    {
        _entityCard = Resources.Load<EntityCard>(path);
    }

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public virtual EntityCard Create()
    {
        return _instantiator.InstantiatePrefabForComponent<EntityCard>(_entityCard);
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

public class EntityCommanderFactory : EntityFactory
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

public class RockFactory : EntityFactory
{
    private const string PATH = "Rock";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}

public class ChestFactory : EntityFactory
{
    private const string PATH = "Chest";

    protected override void LoadResources()
    {
        LoadResources(PATH);
    }
}