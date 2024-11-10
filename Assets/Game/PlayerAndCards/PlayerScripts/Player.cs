using Game.PlayerAndCards.PlayerScripts.Interfaces;
using PlayerAndCards.Interfaces;
using UnityEngine;

namespace Game.PlayerAndCards.PlayerScripts
{
    public class Player : MonoBehaviour, ITakeDamagable
    {
        [SerializeField] private PlayerData _playerData;

        public void TakeDamage(int damage)
        {
            _playerData.Health -= damage;
        }
    }
}