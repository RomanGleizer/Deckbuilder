using System;
using Game.Table.Scripts.EntityProperties;
using UnityEngine;

namespace Game.Table.Scripts.Entities
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
        private EntityCard _entityCard;

        public event Action<Cell> OnObjDestroyed;

        public void Initialize(int rowId, int columnId, bool isHidden)
        {
            _rowId = rowId;
            _columnId = columnId;
            _isHidden = isHidden;
            _isHighlighted = false;
        }

        public void HighlightCell(bool highlight)
        {
            if (_cellView == null)
            {
                InitializeCellView();
            }

            if (_cellView == null)
            {
                Debug.LogError($"Failed to initialize CellView for cell [{_rowId}, {_columnId}]");
                return;
            }

            if (highlight)
                _cellView.ActivateHighlighting();
            else
                _cellView.DeactivateHighlighting();
        }

        private void InitializeCellView()
        {
            var component = GetComponent<Renderer>();
            if (component == null)
            {
                Debug.LogError($"Renderer not found on Cell [{_rowId}, {_columnId}]");
                return;
            }

            _cellView = new CellView(transform, Color.yellow);
        }
        
        public void SetCardOnCell(EntityCard entityCard)
        {
            IsBusy = true;
            _entityCard = entityCard;
        }

        public void ReleaseCellFrom(EntityCard entityCard)
        {
            if (_entityCard != entityCard) return;
            if (!_entityCard.gameObject.activeInHierarchy) OnObjDestroyed?.Invoke(this);

            IsBusy = false;
            _entityCard = null;
        }

        public T GetObjectOnCell<T>()
        {
            var obj = TypeChanger.ChangeObjectTypeWithNull<EntityCard, T>(_entityCard);
            if (obj == null)
            {
                Debug.Log($"Cell [{RowId},{ColumnId}] doesn't have object with type  {typeof(T)}! Return null!");
            }
            return obj;
        }
        
    }
}