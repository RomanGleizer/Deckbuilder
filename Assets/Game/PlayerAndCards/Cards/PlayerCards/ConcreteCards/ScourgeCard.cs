using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Game.Table.Scripts.Entities;
using UnityEngine;
using Zenject;

namespace Game.PlayerAndCards.Cards.PlayerCards.ConcreteCards
{
    public class ScourgeCard : PlayerCard
    {
        private CommandFactory _commandFactory;
        private CommandInvoker _commandInvoker; 
        
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        [Inject]
        private void Construct(CommandFactory commandFactory, CommandInvoker commandInvoker)
        {
            _commandFactory = commandFactory;
            _commandInvoker = commandInvoker;
        }

        public override void Use()
        {
            if (!CanSpendEnergy(CardData.EnergyCost)) return;

            var validCells = GetValidCells();
            if (validCells.Length == 0) return;

            var targetCell = validCells[0];
            
            var mover = targetCell.GetObjectOnCell<IMoverToCell>();
            if (mover == null) return;

            var targetRow = targetCell.RowId;
            var firstColumnCell = Field.GetCellAt(targetRow, 0);
            
            if (firstColumnCell != null)
            { 
                var scourgeCommand = _commandFactory.CreateScourgeCommand(targetCell, firstColumnCell, mover);
                _commandInvoker.SetCommandAndExecute(scourgeCommand, _cancellationTokenSource.Token);
                
                SpendEnergy(CardData.EnergyCost);
            }
            HandManager.DeleteCardFromHand(this);
        }
        
        protected override Cell[] GetValidCells()
        {
            return CurrentCell != null && (CurrentCell.IsHidden
                                           || CurrentCell?.ColumnId == 0 
                                           || CurrentCell?.GetObjectOnCell<IMoverToCell>() == null)
                ? new Cell[] {} 
                : new[] { CurrentCell };
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}