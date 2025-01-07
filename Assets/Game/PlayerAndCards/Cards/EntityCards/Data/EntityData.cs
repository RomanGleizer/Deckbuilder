using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    [SerializeField] private EntityType _type;
    [SerializeField] private int _speed = 10;
    [SerializeField] private string _typeOnRussian;
    [SerializeField] private string _description;

    public EntityType Type => _type;
    public int Speed => _speed;

    public string TypeOnRussian => _typeOnRussian;

    public string Description => _description;
}
