﻿using System;
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
        [SerializeField] private EnergyIndicator _energyIndicator;
        [SerializeField] private ShieldIndicator _shieldIndicator;

        private PlayerData _playerDataInstance;
        private int _currentEnergy;
        private int _shieldDuration;

        public int CurrentHealth => _playerDataInstance.Health;
        public int CurrentEnergy => _currentEnergy;
        
        private StunBh _stunBh;
        private IHaveShield _haveShieldImplementation;
        
        public bool IsStunned => _stunBh.IsStunned;
        
        private void Awake()
        {
            var manager = PlayerProgressionManager.I;
            if (manager != null && manager.DataInstance != null)
                _playerDataInstance = manager.DataInstance;
            else
                _playerDataInstance = ScriptableObject.Instantiate(_playerData);

            _healthIndicator.UpdateIndicator(_playerDataInstance.Health, _playerDataInstance.Health);
            _energyIndicator.UpdateIndicator(_playerDataInstance.MaxEnergy, _playerDataInstance.MaxEnergy);
            _shieldIndicator.UpdateIndicator(_playerDataInstance.Shield);
            _shieldDuration = _playerDataInstance.Shield;
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
            _energyIndicator.UpdateIndicator(_currentEnergy);
        }

        public void RegenerateEnergy()
        {
            _currentEnergy = _playerDataInstance.MaxEnergy;
            _energyIndicator.UpdateIndicator(_currentEnergy);
        }

        public void TakeDamage(int damage)
        {
            if (_shieldDuration > 0)
            {
                _shieldDuration--;
                _shieldIndicator.UpdateIndicator(_shieldDuration);
                return;
            }

            
            _playerDataInstance.Health -= damage;
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health);

            if (_playerDataInstance.Health <= 0) Death();
        }
        
        public void AddShieldDuration(int duration)
        {
            _shieldDuration += duration;
            _shieldIndicator.UpdateIndicator(_shieldDuration);
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

        public void IncreaseMaxHealth(int amount)
        {
            _playerDataInstance.Health += amount;
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health, _playerDataInstance.Health);
        }

        public void IncreaseMaxShield(int amount)
        {
            _playerDataInstance.SetShield(_playerDataInstance.Shield + amount);
            _shieldDuration = _playerDataInstance.Shield;
            _shieldIndicator.UpdateIndicator(_shieldDuration);
        }

        public void RestoreHealth()
        {
            var hp = _playerDataInstance.Health;
            _healthIndicator.UpdateIndicator(hp, hp);
        }
    }
}