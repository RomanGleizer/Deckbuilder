using DG.Tweening;
using UnityEngine;

namespace Table.Scripts.EntityProperties
{
    public class CellView
    {
        private readonly Transform _cellTransform;
        private readonly Color _highlightColor;
        private readonly float _highlightDuration;
        private Renderer _renderer;

        public CellView(Transform cellTransform, Color highlightColor, float highlightDuration = 0.5f)
        {
            _cellTransform = cellTransform;
            _highlightColor = highlightColor;
            _highlightDuration = highlightDuration;
            _renderer = _cellTransform.GetComponent<Renderer>();
        }
        
        public void ActivateHighlighting()
        {
            _renderer.material.DOColor(_highlightColor, _highlightDuration);
            Debug.Log("Cell is highlighted");
        }
        
        public void DeactivateHighlighting()
        {
            _renderer.material.DOColor(Color.white, _highlightDuration);
            Debug.Log("Cell highlighting is removed");
        }
    }
}