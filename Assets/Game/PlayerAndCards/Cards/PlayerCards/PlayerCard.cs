using System.Collections.Generic;
using Game.PlayerAndCards.PlayerScripts;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards
{
    public abstract class PlayerCard : MonoBehaviour
    {
        [SerializeField] private string _cardName;
        [SerializeField] private string _description;
        [SerializeField] private Player _player;
        [SerializeField] private Field _field;

        private CardTriggerHandler _triggerHandler;

        protected Player Player => _player;
        protected Field Field => _field;
        protected Cell CurrentCell => _triggerHandler?.CurrentCell;

        public bool IsCanUse { get; set; }

        private void Awake()
        {
            _triggerHandler = GetComponent<CardTriggerHandler>();
            if (_triggerHandler == null)
            {
                Debug.LogError($"Card {name} is missing a CardTriggerHandler component!");
            }
        }

        public abstract void Use();

        protected abstract List<Cell> GetValidCells();

        protected void SpendEnergy(int energyCost)
        {
            _player.SpendEnergy(energyCost);
        }
    }
}