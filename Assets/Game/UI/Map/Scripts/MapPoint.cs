using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class MapPoint : CustomButton
{
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private List<MapPoint> _neighboursPoints;
    
    [SerializeField] private Color _unlockedColor;
    [SerializeField] private Color _lockedColor;
    [SerializeField] private Image _viewImage;
    
    private MapPointView _view;
    
    public Vector3 Position => transform.position;
    public Action<MapPoint> OnChoosePoint;

    private void Start()
    {
        _view = new MapPointView(_viewImage, _unlockedColor, _lockedColor);
        if (_isUnlocked) _view.SetUnlockedView();
        else _view.SetLockedView();
    }
    
    public abstract void ActivatePointEvent();

    public void CompletePointEvent()
    {
        foreach (MapPoint point in _neighboursPoints)
        {
            point.UnlockPoint();
        }
    }
    
    public void UnlockPoint()
    {
        _isUnlocked = true;
        _view.SetUnlockedView();
    }
    
    protected virtual void HandleClick()
    {
        if (_isUnlocked) OnChoosePoint?.Invoke(this);
    }

    protected override void OnClick()
    {
        HandleClick();
    }

    public bool IsPointNeighbour(MapPoint point)
    {
        return _neighboursPoints.Contains(point);
    }
}

public class MapPointView
{
    private Color _unlockedColor;
    private Color _lockedColor;
    private Image _viewImage;

    public MapPointView(Image viewImage, Color unlockedColor, Color lockedColor)
    {
        _viewImage = viewImage;
        _unlockedColor = unlockedColor;
        _lockedColor = lockedColor;
    }

    public void SetLockedView()
    {
        _viewImage.color = _lockedColor;
    }

    public void SetUnlockedView()
    {
        _viewImage.color = _unlockedColor;
    }
}