using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 _startPosition;
    RectTransform rectTransform;
    private Transform _parentBeforeDrag;
    private CanvasGroup _canvasGroup;

    void Start()
    {
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
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPosition;
        transform.SetParent(_parentBeforeDrag);
        _canvasGroup.blocksRaycasts = true;
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
}