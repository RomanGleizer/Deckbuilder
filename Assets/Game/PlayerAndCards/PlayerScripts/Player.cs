using Game.Table.Scripts.Entities;
using PlayerAndCards.Interfaces;
using UnityEngine;

namespace Game.PlayerAndCards.PlayerScripts
{
    public class Player : MonoBehaviour, ITakeDamagable
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private HpIndicator _health;

        private PlayerData _playerDataInstance;
        private int _currentEnergy;
        private int _shieldDuration;

        public int CurrentHealth => _playerDataInstance.Health;
        public int CurrentEnergy => _currentEnergy;
        
        public Cell CurrentCell { get; private set; }

        private void Awake()
        {
            _playerDataInstance = ScriptableObject.Instantiate(_playerData);
            _health.UpdateIndicator(_playerDataInstance.Health);
            _currentEnergy = _playerDataInstance.MaxEnergy;
        }

        public void UpdateCurrentCell(Cell cell)
        {
            CurrentCell = cell;
        }

        public void SpendEnergy(int amount)
        {
            if (amount > _currentEnergy)
            {
                Debug.LogError("Not enough energy!");
                return;
            }

            _currentEnergy -= amount;
        }

        public void RegenerateEnergy()
        {
            _currentEnergy = _playerDataInstance.MaxEnergy;
        }

        public void TakeDamage(int damage)
        {
            if (_shieldDuration > 0)
            {
                _shieldDuration--;
                return;
            }

            _playerDataInstance.Health -= damage;
            _health.UpdateIndicator(_playerDataInstance.Health);

            if (_playerDataInstance.Health > 0) return;
            Die();
        }

        public void AddShield(int duration)
        {
            _shieldDuration += duration;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}