using UnityEngine;

namespace PlayerAndCards.Player
{
    [CreateAssetMenu(fileName = "Player", menuName = "Player")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _shield;
        [SerializeField] private int _maxEnergy;
    }
}