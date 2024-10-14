using System;
using System.Collections.Generic;
using Table.Scripts.Generation;
using UnityEngine;

namespace Table.Scripts.Entities
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private GridGenerator gridGenerator;
        private Cell[,] _cells;
        
        public void Initialize()
        {
            if (gridGenerator == null)
            {
                Debug.LogError("GridGenerator reference is missing!");
                return;
            }

            _cells = gridGenerator.GenerateGrid();
        }
        
        public void HighlightActiveCells(bool highlight)
        {
            TraverseCells(cell =>
            {
                cell.HighlightCell(highlight);
            });
        }
        
        private void TraverseCells(Action<Cell> action, bool includeHidden = false)
        {
            for (var row = 0; row < _cells.GetLength(0); row++)
                for (var column = 0; column < _cells.GetLength(1); column++)
                {
                    var cell = _cells[row, column];
                    if (includeHidden || !cell.IsHidden)
                        action(cell);
                }
        }
        
        public Cell GetRandomActiveCell(Cell currentCell)
        {
            var activeCells = new List<Cell>();
            TraverseCells(cell =>
            {
                if (cell != currentCell && !cell.IsHidden)
                    activeCells.Add(cell);
            });

            if (activeCells.Count == 0)
            {
                Debug.LogError("No active cells available.");
                return null;
            }

            var randomIndex = UnityEngine.Random.Range(0, activeCells.Count);
            return activeCells[randomIndex];
        }
        
        public Cell FindCell(Cell currentCell, Vector2 direction, int length)
        {
            var newRow = currentCell.RowId + (int)direction.x * length;
            var newColumn = currentCell.ColumnId + (int)direction.y * length;

            if (IsWithinBounds(newRow, newColumn))
                return _cells[newRow, newColumn];

            Debug.LogError("Target cell is out of bounds.");
            return null;
        }
        
        private bool IsWithinBounds(int row, int column)
        {
            return row >= 0 && row < _cells.GetLength(0) &&
                   column >= 0 && column < _cells.GetLength(1);
        }
    }
}
