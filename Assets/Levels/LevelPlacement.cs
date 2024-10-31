using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPlacement", menuName = "New Data/New LevelPlacement")]
public class LevelPlacement : ScriptableObject
{
    [SerializeField] private RowPlacement[] _rowPlacements;

    public RowPlacement[] RowPlacements => _rowPlacements;
}

[Serializable]
public class RowPlacement
{
    [SerializeField] private EntityType[] _rowMarkers;
    public EntityType[] RowMarkers => _rowMarkers;

#if UNITY_EDITOR
    public RowPlacement(Stack<EntityType> entityTypes)
    {
        _rowMarkers = new EntityType[entityTypes.Count];
        entityTypes.CopyTo(_rowMarkers, 0);
    }
#endif
}

public enum EntityType
{
    Armsman,
    Cavalryman,
    Commander,
    Drummer,
    GarbageMan,
    Pioneer,
    Shooter,
    Snare,
    Sniper,
    Spy,
    Swordsman
}
