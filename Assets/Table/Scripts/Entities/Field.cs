using System;
using System.Collections.Generic;
using Table.Scripts.Generation;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Table.Scripts.Entities
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private GridGenerator gridGenerator;
        private Cell[,] _cells;
        
        public int CollumnsCount { get; private set; }
        public int RowsCount { get; private set; }

        public void Initialize()
        {
            //if (gridGenerator == null)
            //{
            //    Debug.LogError("GridGenerator reference is missing!");
            //    return;
            //}

            //_cells = gridGenerator.GenerateGrid();

            CollumnsCount = 5;
            RowsCount = 3;

            var cells = GetComponentsInChildren<Cell>(); // ��� �����, �� ����� ���-�� ������� ���������

            _cells = new Cell[RowsCount, CollumnsCount];

            int j = -1;
            for (int i = 0; i < cells.Length; ++i)
            {
                if (i % CollumnsCount == 0) ++j;

                _cells[j, i % CollumnsCount] = cells[i];
            }
        }
        
        public void HighlightActiveCells(bool highlight)
        {
            TraverseCells(cell =>
            {
                cell.HighlightCell(highlight);
            });
        }

        public void SetCommand(Command command)
        {
            TraverseCells(cell =>
            {
                cell.SetCommand(command);
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
            var newRow = currentCell.RowId + (int)direction.y * length;
            var newColumn = currentCell.ColumnId + (int)direction.x * length;

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

        public Cell FindFirstFreeCellFromRow(int rowIndex)
        {
            var row = GetRowByIndex(rowIndex, true);

            foreach (var cell in row)
            {
                if (!cell.IsBusy) return cell;
            }

            Debug.Log("All cells is busy.");
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
    }
}
