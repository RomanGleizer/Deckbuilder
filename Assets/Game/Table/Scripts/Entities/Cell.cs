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
        public bool IsBusy { get; private set; }

        private CellView _cellView;
        private bool _isHighlighted;

        public CellView CellView => _cellView;
        private EnemyCard _enemyCard; // TODO: поменять на карты, когда появятся

        public event Action<Command> OnCommandSet;

        public void Initialize(int rowId, int columnId, bool isHidden, CellView cellView)
        {
            _rowId = rowId;
            _columnId = columnId;
            _isHidden = isHidden;
            _cellView = cellView;
            _isHighlighted = false;
        }

        public void HighlightCell(bool highlight)
        {
            if (highlight)
                _cellView.ActivateHighlighting();
            else
                _cellView.DeactivateHighlighting();
        }

        public void SetCardOnCell(EnemyCard enemyCard)
        {
            IsBusy = true;
            _enemyCard = enemyCard;
        }

        public void ReleaseCell()
        {
            IsBusy = false;
            _enemyCard = null;
        }

        public T GetObjectOnCell<T>()
        {
            var obj = TypeChanger.ChangeObjectTypeWithNull<EnemyCard, T>(_enemyCard);
            if (obj == null)
            {
                Debug.Log("Cell doesn't have object with type " + typeof(T) + "! Return null!");
            }
            return obj;
        }
    }
}