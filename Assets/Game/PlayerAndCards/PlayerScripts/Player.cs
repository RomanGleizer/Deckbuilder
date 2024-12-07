using System;
using Game.General.GeneralBehaviour;
using Game.PlayerAndCards.PlayerScripts.Interfaces;
using Game.Table.Scripts.Entities;
using UnityEngine;
using Zenject;

namespace Game.PlayerAndCards.PlayerScripts
{
    public class Player : MonoBehaviour, ITakerDamage, IStunnable
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private HpIndicator _healthIndicator;

        private PlayerData _playerDataInstance;
        private int _currentEnergy;
        private int _shieldDuration;

        public int CurrentHealth => _playerDataInstance.Health;
        public int CurrentEnergy => _currentEnergy;
        
        private StunBh _stunBh;

        public bool IsStunned => _stunBh.IsStunned;

        private void Awake()
        {
            _playerDataInstance = ScriptableObject.Instantiate(_playerData);
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health);
            _currentEnergy = _playerDataInstance.MaxEnergy;
        }

        [Inject]
        private void Construct(IInstantiator instantiator, TurnManager turnManager)
        {
            _stunBh = new StunBh(turnManager, instantiator.Instantiate<SubscribeHandler>(), true);
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
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health);

            if (_playerDataInstance.Health <= 0) Death();
        }

        public void AddShield(int duration)
        {
            _shieldDuration += duration;
        }

        public void Stun(int duration)
        {
            _stunBh.StartStun(duration);
        }

        public void Death()
        {
            gameObject.SetActive(false);
            //TODO: Запускает логику поражения и перезагрузки уровня
        }
    }
}