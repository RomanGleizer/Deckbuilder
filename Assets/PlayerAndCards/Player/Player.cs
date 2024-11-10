using PlayerAndCards.Interfaces;
using UnityEngine;

namespace PlayerAndCards.Player
{
    public class Player : MonoBehaviour, ITakeDamagable
    {
        [SerializeField] private PlayerData _playerData;
        
        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}