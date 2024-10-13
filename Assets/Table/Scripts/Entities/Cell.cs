using System;
using Table.Scripts.EntityProperties;
using UnityEngine;

namespace Table.Scripts.Entities
{
    /// <summary>
    /// Represents a cell in the game field.
    /// </summary>
    public class Cell : MonoBehaviour
    {
        private int _rowId;
        private int _columnId;
        private bool _isHidden;

        public int RowId => _rowId;
        public int ColumnId => _columnId;
        public bool IsHidden => _isHidden;
        public bool IsBusy { get; set; }

        private CellView _cellView;

        public CellView CellView => _cellView;

        public event Action<Command> OnCommandSet;

        public void SetCommand(Command command)
        {
            OnCommandSet?.Invoke(command);
        }
    }
}