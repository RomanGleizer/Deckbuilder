using System;
using Game.PlayerAndCards.PlayerScripts.Interfaces;
using PlayerAndCards.Interfaces;
using UnityEngine;

namespace Game.PlayerAndCards.PlayerScripts
{
    public class Player : MonoBehaviour, ITakeDamagable
    {
        [SerializeField] private PlayerData _playerData;

        private PlayerData _playerDataInstance;
        
        private void Awake()
        {
            _playerDataInstance = ScriptableObject.Instantiate(_playerData);
        }

        public void TakeDamage(int damage)
        {
            _playerDataInstance.Health -= damage;
        }
    }
}