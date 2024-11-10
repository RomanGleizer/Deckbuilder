using Game.PlayerAndCards.PlayerScripts.Interfaces;
using PlayerAndCards.Interfaces;
using UnityEngine;

namespace Game.PlayerAndCards.PlayerScripts
{
    public class Player : MonoBehaviour, ITakeDamagable
    {
        [SerializeField] private PlayerData _playerData;

        private float _stunEndTime;

        public bool IsStunned { get; private set; }

        public void TakeDamage(int damage)
        {
            _playerData.Health -= damage;
        }

        public void Stun(float duration)
        {
            IsStunned = true;
            _stunEndTime = Time.time + duration;
        }

        private void Update()
        {
            if (!IsStunned || !(Time.time >= _stunEndTime)) return;
            IsStunned = false;
        }
    }
}