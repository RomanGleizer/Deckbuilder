using System;
using UnityEngine;

namespace TableMechanics.Entities
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private bool _isActive;

        private int _rowId;
        private int _columnId;

        public int RowId => _rowId;
        public int ColumnId => _columnId;

        public event Func<int> onAtack;
    }
}