using System;
using System.Collections.Generic;
using Game.Table.Scripts.Generation;
using UnityEngine;

namespace Game.Table.Scripts.Entities
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private GridGenerator gridGenerator;
        private Cell[,] _cells;
        private readonly List<Cell> _traversedCells = new();
        
        public int RowsCount => _cells.GetLength(0);
        public int ColumnsCount => _cells.GetLength(1);

        public void Initialize()
        {
            if (gridGenerator == null)
            {
                Debug.LogError("GridGenerator reference is missing!");
                return;
            }

            InitializeCellsFromScene();
        }

        public void HighlightActiveCells(bool highlight)
        {
            TraverseCells(cell =>
            {
                cell.HighlightCell(highlight);
            });
        }
        
        public void TraverseCells(Action<Cell> action, bool includeHidden = false)
        {
            _traversedCells.Clear();
    
            for (var column = 0; column < _cells.GetLength(1); column++)
            for (var row = 0; row < _cells.GetLength(0); row++)
            {
                var cell = _cells[row, column];
                if (!includeHidden && cell.IsHidden) continue;
                action?.Invoke(cell);
                _traversedCells.Add(cell);
            }
        }
        
        public Cell GetCellAt(int row, int column)
        {
            return IsWithinBounds(row, column) ? _cells[row, column] : null;
        }
        
        public Cell[] GetTraversedCells()
        {
            TraverseCells(null);
            return _traversedCells.ToArray();
        }

        public Cell[] GetRowByCell(Cell cell, bool includeHidden)
        {
            return GetCellsInLine(cell.RowId, isRow: true, includeHidden);
        }

        public Cell[] GetRowByIndex(int index, bool includeHidden)
        {
            return GetCellsInLine(index, isRow: true, includeHidden);
        }

        public Cell[] GetColumnByCell(Cell cell, bool includeHidden)
        {
            return GetCellsInLine(cell.ColumnId, isRow: false, includeHidden);
        }
        
        public Cell[] GetColumnById(int columnId, bool includeHidden)
        {
            if (columnId >= 0 && columnId < ColumnsCount) 
                return GetCellsInLine(columnId, isRow: false, includeHidden);
            
            Debug.LogError($"Column ID {columnId} is out of bounds.");
            return Array.Empty<Cell>();
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
                Debug.Log("No active cells available.");
                return null;
            }

            var randomIndex = UnityEngine.Random.Range(0, activeCells.Count);
            return activeCells[randomIndex];
        }

        public Cell FindCell(Cell currentCell, Vector2 direction, int length)
        {
            var newRow = currentCell.RowId + (int)direction.y * length;
            var newColumn = currentCell.ColumnId + (int)direction.x * length;

            if (IsWithinBounds(newRow, newColumn))
                return _cells[newRow, newColumn];

            Debug.LogWarning("Target cell is out of bounds.");
            return null;
        }

        private bool IsWithinBounds(int row, int column)
        {
            return row >= 0 && row < _cells.GetLength(0) &&
                   column >= 0 && column < _cells.GetLength(1);
        }

        public Cell FindFirstFreeCellFromRow(int rowIndex)
        {
            var row = GetRowByIndex(rowIndex, true);

            foreach (var cell in row)
            {
                if (!cell.IsBusy) return cell;
            }

            Debug.Log("All cells are busy.");
            return null;
        }

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

        private void InitializeCellsFromScene()
        {
            var cellsList = new List<Cell>(GetComponentsInChildren<Cell>());
            if (cellsList.Count == 0)
            {
                Debug.LogError("No cells found on the scene.");
                return;
            }

            var rows = cellsList[0].RowId + 1;
            var columns = cellsList[0].ColumnId + 1;

            foreach (var cell in cellsList)
            {
                rows = Math.Max(rows, cell.RowId + 1);
                columns = Math.Max(columns, cell.ColumnId + 1);
            }

            _cells = new Cell[rows, columns];
            foreach (var cell in cellsList)
                _cells[cell.RowId, cell.ColumnId] = cell;
        }

        public bool MoveEnemyToColumn(EnemyCard enemy, int targetColumn)
        {
            return true;
        }
    }
}
