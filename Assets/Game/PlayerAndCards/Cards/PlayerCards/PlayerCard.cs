using System.Collections.Generic;
using Game.PlayerAndCards.PlayerScripts;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards
{   
    [RequireComponent(typeof(CardTriggerHandler))]
    public abstract class PlayerCard : MonoBehaviour
    {
        [SerializeField] private PlayerCardData _cardData;

        private Player _player;
        private Field _field;
        private CardTriggerHandler _triggerHandler;
        private HandManager _handManager;

        protected Player Player => _player;
        protected Field Field => _field;
        protected Cell CurrentCell => _triggerHandler?.CurrentCell;

        protected PlayerCardData CardData => _cardData;

        protected HandManager HandManager => _handManager;

        public bool IsCanUse { get; set; }

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _field = FindObjectOfType<Field>();
            _handManager = FindObjectOfType<HandManager>();
            _triggerHandler = GetComponent<CardTriggerHandler>();
            if (_triggerHandler == null)
            {
                Debug.LogError($"Card {name} is missing a CardTriggerHandler component!");
            }
        }

        public abstract void Use();

        protected abstract Cell[] GetValidCells();

        protected bool CanSpendEnergy(int energyCost)
        {
            if (Player.CurrentEnergy >= energyCost) 
                return true;
            
            Debug.LogWarning("Not enough energy to use the card!");
            return false;
        }

        protected void SpendEnergy(int energyCost)
        {
            Player.SpendEnergy(energyCost);
        }

        private void OnMouseUp()
        {
            if (CurrentCell == null)
                return;

            if (CurrentCell.IsBusy && !CurrentCell.IsHidden)
            {
                Use();
            }            
        }
    }
}