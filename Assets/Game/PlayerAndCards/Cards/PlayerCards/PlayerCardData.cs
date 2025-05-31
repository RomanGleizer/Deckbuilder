using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.PlayerAndCards.Cards.PlayerCards
{
    [CreateAssetMenu(fileName = "PlayerCard", menuName = "PlayerCard")]
    public class PlayerCardData : ScriptableObject
    {
        [SerializeField] private PlayerCardNames _name;
        [SerializeField] private int _damage;
        [SerializeField] private int _energyCost;
        [SerializeField] private string _nameInRussian;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;

        [SerializeField] private int _countInChest;

        public PlayerCardNames Name => _name;

        public int Damage => _damage;

        public int EnergyCost => _energyCost;

        public string NameInRussian => _nameInRussian;

        public string Description => _description;

        public Sprite Sprite => _sprite;

        public int CountInChest => _countInChest;

    }
}


