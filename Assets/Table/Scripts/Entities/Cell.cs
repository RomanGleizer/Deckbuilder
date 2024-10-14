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
        private CellView _cellView;

        public int RowId => _rowId;
        public int ColumnId => _columnId;
        public bool IsHidden => _isHidden;

        public event Func<int> OnAtack;

        public void Initialize(int rowId, int columnId, bool isHidden, CellView cellView)
        {
            _rowId = rowId;
            _columnId = columnId;
            _isHidden = isHidden;
            _cellView = cellView;
        }
        
        public void HighlightCell(bool highlight)
        {
            if (highlight)
                _cellView.ActivateHighlighting();
            else
                _cellView.DeactivateHighlighting();
        }
    }
}