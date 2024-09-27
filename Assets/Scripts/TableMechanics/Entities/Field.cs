using System;
using UnityEngine;

namespace TableMechanics.Entities
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
            Debug.Log("Table has been created !");
            _cells = new Cell[,]
            {

            };
        }

        /// <summary>
        /// Finds a target cell on the game board based on the current cell
        /// </summary>
        /// <param name="currentCell">The current cell from which to find the target cell.</param>
        /// <param name="direction">The direction to move.</param>
        /// <param name="length">The number of cells to move.</param>
        /// <returns>The target cell if found; otherwise, null.</returns>
        public Cell FindCell(Cell currentCell, Vector2 direction, int length)
        {
            if (direction.x != 0)
            {
                var targetRow = currentCell.RowId + (int)direction.x * length;
                if (targetRow >= 0 && targetRow < _cells.GetLength(0)) 
                    return _cells[targetRow, currentCell.ColumnId];
            }
            else
            {
                var targetColumn = currentCell.ColumnId + (int)direction.y * length;
                if (targetColumn >= 0 && targetColumn < _cells.GetLength(1))
                    return _cells[currentCell.RowId, targetColumn];    
            }

            Debug.LogError("Target cell is out of bounds.");
            return null;
        }
    }
}
