using UnityEngine;

namespace Game.PlayerAndCards.PlayerScripts
{
    [CreateAssetMenu(fileName = "Player", menuName = "Player")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _shield;
        [SerializeField] private int _maxEnergy;

        public int Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }
}