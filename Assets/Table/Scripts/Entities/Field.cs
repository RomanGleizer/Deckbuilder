using System;
using System.Collections.Generic;
using UnityEngine;

namespace Table.Scripts.Entities
{
    /// <summary>
    /// Game field consisting of a grid of cells.
    /// </summary>
    public class Field : MonoBehaviour
    {
        private Cell[,] _cells;

        /// <summary>
        /// Initializes the game field
        /// </summary>
        public void Initialize()
        {
            Debug.Log("Table has been created!");
            _cells = new Cell[,]
            {
                // Инициализация клеток
            };
        }

        /// <summary>
        /// Finds a target cell on the game board based on the current cell.
        /// </summary>
        public Cell FindCell(Cell currentCell, Vector2 direction, int length)
        {
            var newRow = currentCell.RowId + (int)direction.x * length;
            var newColumn = currentCell.ColumnId + (int)direction.y * length;

            if (IsWithinBounds(newRow, newColumn))
                return _cells[newRow, newColumn];

            Debug.LogError("Target cell is out of bounds.");
            return null;
        }

        /// <summary>
        /// Traverses all cells in the table and performs an action on each non-hidden cell.
        /// </summary>
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

        /// <summary>
        /// Traverses all cells in the table, calling OnCommandSet on each non-hidden cell.
        /// </summary>
        public void SetCommand(Command command)
        {
            TraverseCells(cell =>
            {
                cell.SetCommand(command);
            });
        }

        /// <summary>
        /// Traverses all cells in the table, calling Highlighting on each cell's view except hidden ones.
        /// </summary>
        public void HighlightActiveSells(bool highlight)
        {
            TraverseCells(cell =>
            {
                var view = cell.CellView;
                if (cell.CellView == null) return;

                if (highlight) view.ActivateHighlighting();
                else view.DeactivateHighlighting();
            });
        }

        /// <summary>
        /// Gets all cells in the row of the specified cell, with the option to include hidden cells.
        /// </summary>
        public Cell[] GetRowByCell(Cell cell, bool includeHidden)
        {
            return GetCellsInLine(cell.RowId, isRow: true, includeHidden);
        }

        /// <summary>
        /// Gets all cells in the column of the specified cell, with the option to include hidden cells.
        /// </summary>
        public Cell[] GetColumnByCell(Cell cell, bool includeHidden)
        {
            return GetCellsInLine(cell.ColumnId, isRow: false, includeHidden);
        }

        /// <summary>
        /// Gets all cells in a row or column depending on the specified flag.
        /// </summary>
        private Cell[] GetCellsInLine(int index, bool isRow, bool includeHidden)
        {
            var cells = new List<Cell>();
            var length = isRow ? _cells.GetLength(1) : _cells.GetLength(0);

            for (var i = 0; i < length; i++)
            {
                var cell = isRow ? _cells[index, i] : _cells[i, index];
                if (includeHidden || !cell.IsHidden)
                    cells.Add(cell);
            }

            return cells.ToArray();
        }

        /// <summary>
        /// Checks if the specified row and column indices are within the bounds of the grid.
        /// </summary>
        private bool IsWithinBounds(int row, int column)
        {
            return row >= 0 && row < _cells.GetLength(0) &&
                   column >= 0 && column < _cells.GetLength(1);
        }
    }
}
