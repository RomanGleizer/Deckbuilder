using System;
using Game.General.GeneralBehaviour;
using Game.PlayerAndCards.PlayerScripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.PlayerAndCards.PlayerScripts
{
    public class Player : MonoBehaviour, ITakerDamage, IStunnable
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private HpIndicator _healthIndicator;

        private PlayerData _playerDataInstance;

        private StunBh _stunBh;

        public bool IsStunned => _stunBh.IsStunned;

        private void Awake()
        {
            _playerDataInstance = ScriptableObject.Instantiate(_playerData);
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health);
        }

        [Inject]
        private void Construct(IInstantiator instantiator, TurnManager turnManager)
        {
            _stunBh = new StunBh(turnManager, instantiator.Instantiate<SubscribeHandler>(), true);
        }

        public void TakeDamage(int damage)
        {
            _playerDataInstance.Health -= damage;
            _healthIndicator.UpdateIndicator(_playerDataInstance.Health);

            if (_playerDataInstance.Health <= 0) Death(); 
        }

        public void Stun(int duration)
        {
            _stunBh.StartStun(duration);
        }

        public void Death()
        {
            //TODO: Запускает логику поражения и перезагрузки уровня
        }
    }
}