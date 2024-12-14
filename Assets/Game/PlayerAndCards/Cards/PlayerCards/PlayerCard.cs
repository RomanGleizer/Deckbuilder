using System.Collections.Generic;
using Game.PlayerAndCards.PlayerScripts;
using Game.Table.Scripts.Entities;
using UnityEngine;

namespace Game.PlayerAndCards.Cards.PlayerCards
{   
    [RequireComponent(typeof(CardTriggerHandler))]
    public abstract class PlayerCard : MonoBehaviour
    {
        [SerializeField] private string _cardName;
        [SerializeField] private string _description;

        private Player _player;
        private Field _field;
        private CardTriggerHandler _triggerHandler;

        protected Player Player => _player;
        protected Field Field => _field;
        protected Cell CurrentCell => _triggerHandler?.CurrentCell;

        public bool IsCanUse { get; set; }

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _field = FindObjectOfType<Field>();
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

            Use();
        }
    }
}