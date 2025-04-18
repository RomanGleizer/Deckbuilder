using UnityEngine;

namespace Game.PlayerAndCards.PlayerScripts
{
    [CreateAssetMenu(fileName = "Player", menuName = "Player")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _shield;
        [SerializeField] private int _maxEnergy;
        [SerializeField] private int _handSize = 5;
        [SerializeField] private int _redrawCount = 1;

        public int Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, int.MaxValue);
        }

        public int MaxEnergy => _maxEnergy;
        public int Shield => _shield;
        public int HandSize => _handSize;
        public int RedrawCount => _redrawCount;

        public void SetShield(int newShield)
        {
            _shield = newShield;
        }

        public void SetHandSize(int newHandSize)
        {
            _handSize = newHandSize;
        }

        public void SetRedrawCount(int newRedrawCount)
        {
            _redrawCount = newRedrawCount;
        }
    }
}
