using DG.Tweening;
using UnityEngine;

namespace Game.Table.Scripts.EntityProperties
{
    public class CellView
    {
        private readonly Color _highlightColor;
        private readonly float _highlightDuration;
        private Renderer _renderer;

        public CellView(Transform cellTransform, Color highlightColor, float highlightDuration = 0.5f)
        {
            _highlightColor = highlightColor;
            _highlightDuration = highlightDuration;
            _renderer = cellTransform.GetComponent<Renderer>();
        }
        
        public void ActivateHighlighting()
        {
            if (_renderer == null)
            {
                Debug.LogError("Renderer is not initialized in CellView.");
                return;
            }

            _renderer.material.DOColor(_highlightColor, _highlightDuration);
        }

        public void DeactivateHighlighting()
        {
            if (_renderer == null)
            {
                Debug.LogError("Renderer is not initialized in CellView.");
                return;
            }

            _renderer.material.DOColor(Color.white, _highlightDuration);
        }
    }
}