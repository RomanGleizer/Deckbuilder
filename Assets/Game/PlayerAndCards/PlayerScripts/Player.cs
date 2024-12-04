using System;
using Game.PlayerAndCards.PlayerScripts.Interfaces;
using UnityEngine;

namespace Game.PlayerAndCards.PlayerScripts
{
    public class Player : MonoBehaviour, ITakerDamage
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private HpIndicator _healthIndicator;

        private PlayerData _playerDataInstance;
        
        private void Awake()
        {
            _playerDataInstance = ScriptableObject.Instantiate(_playerData);
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health);
        }

        public void TakeDamage(int damage)
        {
            _playerDataInstance.Health -= damage;
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health);

            if (_playerDataInstance.Health <= 0) Death(); 
        }

        public void Death()
        {
            //TODO: Запускает логику поражения и перезагрузки уровня
        }
    }
}