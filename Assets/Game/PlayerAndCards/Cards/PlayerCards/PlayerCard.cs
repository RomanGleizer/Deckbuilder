using System.Collections.Generic;
using Game.PlayerAndCards.PlayerScripts;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards
{
    public abstract class PlayerCard : MonoBehaviour
    {
        [SerializeField] private string _cardName;
        [SerializeField] private int _energyCost;
        [SerializeField] private string _description;
        [SerializeField] private Player _player;
        [SerializeField] private Field _field;
        
        protected Player Player => _player;
        
        protected Field Field => _field;

        public bool IsCanUse { get; set; }

        public abstract void Use();

        public void HighlightValidCells()
        {
            var validCells = GetValidCells();
            foreach (var cell in validCells)
            {
                cell.HighlightCell(true);
            }
        }

        protected abstract List<Cell> GetValidCells();
        
        protected void SpendEnergy()
        {
            _player.SpendEnergy(_energyCost);
        }
    }
}