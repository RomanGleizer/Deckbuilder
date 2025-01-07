using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards
{
    [CreateAssetMenu(fileName = "PlayerCard", menuName = "PlayerCard")]
    public class PlayerCardData : ScriptableObject
    {
        [SerializeField] private PlayerCardNames _name;
        [SerializeField] private int _damage ;
        [SerializeField] private int _energyCost;
        [SerializeField] private string _description;

        public PlayerCardNames Name => _name;

        public int Damage => _damage;

        public int EnergyCost => _energyCost;
    }
}
