﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class LevelPlacementStack
{
    private LevelPlacement _levelPlacementInstance;

#if UNITY_EDITOR
    private QueuesEditorVisual _queuesEditorVisual;
#endif

    private Stack<EntityType>[] _mainStack;
    public bool IsEmpty => _mainStack.All(stack => stack.Count == 0);

#if UNITY_EDITOR
    public LevelPlacementStack(LevelPlacement levelPlacement, QueuesEditorVisual queuesEditorVisual)
    {
        _queuesEditorVisual = queuesEditorVisual;
        Init(levelPlacement);
    }
#endif

    public LevelPlacementStack(LevelPlacement levelPlacement)
    {
        Init(levelPlacement);
    }

    private void Init(LevelPlacement levelPlacement)
    {
        _levelPlacementInstance = ScriptableObject.Instantiate(levelPlacement);

        _mainStack = new Stack<EntityType>[_levelPlacementInstance.RowPlacements.Length];
        
        for (int i = 0; i < _levelPlacementInstance.RowPlacements.Length; ++i)
        {
            _mainStack[i] = new Stack<EntityType>();

            var rowMarkers = _levelPlacementInstance.RowPlacements[i].RowMarkers.Reverse();
            foreach (var rowMarker in rowMarkers)
            {
                _mainStack[i].Push(rowMarker);
            }
        }

        UpdateEditorVisual();
    }

    public void Push(int rowIndex, EntityType entityType)
    {
        _mainStack[rowIndex].Push(entityType);

        UpdateEditorVisual();
    }

    public EntityType? Pop(int rowIndex)
    {
        if (_mainStack[rowIndex].Count > 0)
        {
            var value = _mainStack[rowIndex].Pop();
            UpdateEditorVisual();

            return value;
        }
        return null;
    }

    private void UpdateEditorVisual()
    {
#if UNITY_EDITOR
        _queuesEditorVisual.UpdateVisual(_mainStack);
#endif
    }
}
