using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPlacement", menuName = "New Data/New LevelPlacement")]
public class LevelPlacement : ScriptableObject
{
    [SerializeField] private FieldEntityMarkerRow[] _markerTable;

    public FieldEntityMarkerRow[] MarkerTable => _markerTable;
}

[Serializable]
public class FieldEntityMarkerRow
{
    [SerializeField] private EntityType[] _rowMarkers;

    public EntityType[] RowMarkers => _rowMarkers;
}

public enum EntityType
{
    Empty,
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
