using UnityEngine;

[CreateAssetMenu(fileName = "New RockData", menuName = "New EntityData/New RockData")]
public class RockData : EntityData
{
    [SerializeField] private int _hp;

    public int Hp => _hp;
}
