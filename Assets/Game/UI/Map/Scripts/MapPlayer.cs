using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    public bool IsMoving { get; private set; }
    
    private Vector3 _targetPoint;

    public event Action OnRiched;
    
    private void Update()
    {
        if (IsMoving)
        {
            MoveToPoint(_targetPoint);
            if (transform.position == _targetPoint) StopMoving();
        }
    }

    public void StartMoving(Vector3 point)
    {
        _targetPoint = point;
        IsMoving = true;
    }

    public void StopMoving()
    {
        _targetPoint = Vector3.zero;
        IsMoving = false;
        OnRiched?.Invoke();
    }

    public void MoveToPoint(Vector3 point)
    {
        transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);
    }
}
