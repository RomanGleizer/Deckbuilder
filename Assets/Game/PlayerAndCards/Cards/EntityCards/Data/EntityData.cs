using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    [SerializeField] private EntityType _type;
    [SerializeField] private int _speed = 10;

    public EntityType Type => _type;
    public int Speed => _speed;
}
