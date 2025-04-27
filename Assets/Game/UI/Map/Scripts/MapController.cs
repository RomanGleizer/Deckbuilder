using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<MapPoint> _mapPoints = new List<MapPoint>();
    [SerializeField] private MapPlayer _mapPlayer;

    [SerializeField] private MapPoint _currentMapPoint;
    
    private void Start()
    {
        Subscribe();
        InitMapPoints();

        var currentPointIndex = SaveService.SaveData.MapPointIndex;
        _currentMapPoint = _mapPoints[currentPointIndex];

        _mapPlayer.transform.position = _currentMapPoint.Position;
    }

    private void InitMapPoints()
    {
        var counts = 0;
        foreach (var point in _mapPoints)
        {
            point.Init(counts);
            if (SaveService.SaveData.UnlockedPoints.Points.Contains(counts)) point.UnlockPoint();
            
            counts++;
        }
    }
    
    private void MovePlayerToPoint(MapPoint mapPoint)
    {
        if (_mapPlayer.IsMoving) return;
        if (!_currentMapPoint.IsPointNeighbour(mapPoint)) return;
        
        _mapPlayer.StartMoving(mapPoint.Position);
        _currentMapPoint = mapPoint;
        
        SaveService.SaveData.MapPointIndex = _currentMapPoint.Index;
        SaveService.Save();
        
        _mapPlayer.OnRiched += ActivatePointEvent; 
        CompletePointEvent(); // временное решение TODO: заменить при появлении механики прохождения ивента
    }

    private void ActivatePointEvent()
    {
        Debug.Log("Event activated!");
        _currentMapPoint.ActivatePointEvent();
        _mapPlayer.OnRiched -= ActivatePointEvent;
    }

    private void CompletePointEvent()
    {
        _currentMapPoint.CompletePointEvent();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        foreach (var point in _mapPoints)
        {
            point.OnChoosePoint += MovePlayerToPoint;
        }
    }
    
    private void Unsubscribe()
    {
        foreach (var point in _mapPoints)
        {
            point.OnChoosePoint -= MovePlayerToPoint;
            if (_mapPlayer.IsMoving) _mapPlayer.OnRiched -= ActivatePointEvent;
        }
    }
}
