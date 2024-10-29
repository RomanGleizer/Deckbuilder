using System;
using UnityEngine;
using Table.Scripts.EntityProperties;

namespace Table.Scripts.Entities
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private int _rowId;
        [SerializeField] private int _columnId;
        [SerializeField] private bool _isHidden;


        public int RowId => _rowId;
        public int ColumnId => _columnId;
        public bool IsHidden => _isHidden;
        public bool IsBusy { get; set; }

        private CellView _cellView;
        public CellView CellView => _cellView;

        public event Action<Command> OnCommandSet;

        public void Initialize(int rowId, int columnId, bool isHidden)
        {
            _rowId = rowId;
            _columnId = columnId;
            _isHidden = isHidden;
            _cellView = new CellView();
        }
        
        public void HighlightCell(bool highlight)
        {
            if (highlight)
                _cellView.ActivateHighlighting();
            else
                _cellView.DeactivateHighlighting();
        }


        public void SetCommand(Command command)
        {
            OnCommandSet?.Invoke(command);
        }
    }
}