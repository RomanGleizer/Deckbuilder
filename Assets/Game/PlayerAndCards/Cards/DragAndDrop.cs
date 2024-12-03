using Game.PlayerAndCards.PlayerScripts;
using Game.Table.Scripts.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.PlayerAndCards.Cards
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Player _player;
        private Camera mainCamera;
    
        private Vector2 _startPosition;
        private RectTransform rectTransform;
        private Transform _parentBeforeDrag;
        private CanvasGroup _canvasGroup;

        
        void Start()
        {
            mainCamera = GameObject.FindObjectOfType<Camera>();
            _startPosition = transform.position;
            rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _parentBeforeDrag = transform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {        
            transform.SetParent(transform.root);
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 screenCoords = mainCamera.WorldToScreenPoint(transform.position);

            Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenCoords.z));

            transform.position = newWorldPosition;

            UpdateCurrentCell();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = _startPosition;
            transform.SetParent(_parentBeforeDrag);
            _canvasGroup.blocksRaycasts = true;
            
            _player.UpdateCurrentCell(null);
        }

        public void OnPointerDown(PointerEventData eventData)
        {      
            _parentBeforeDrag = transform.parent;
            transform.SetParent(transform.root);

            Vector2 startPosition = rectTransform.transform.localPosition;
            Vector2 newPosition = new Vector2(0, startPosition.y + 100);

            rectTransform.transform.localPosition = newPosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.SetParent(_parentBeforeDrag);
        }

        private void UpdateCurrentCell()
        {
            if (Camera.main == null) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            
            var cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
            {
                _player.UpdateCurrentCell(cell);
            }
        }
    }
}
